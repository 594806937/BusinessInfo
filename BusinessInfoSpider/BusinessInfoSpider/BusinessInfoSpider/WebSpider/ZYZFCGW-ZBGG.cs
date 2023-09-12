using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using BusinessInfoSpider.Model;
using BusinessInfoSpider.Utility;
using System.Text.RegularExpressions;

namespace BusinessInfoSpider.WebSpider
{
    public class ZYZFCGW_ZBGG : BaseSpider
    {
        public override string Name
        {
            get { return "中央政府采购网-中标公告"; }
        }

        public override string WebURL
        {
            get { return "http://www.zycg.gov.cn/"; }
        }
        /// <summary>
        /// 开始处理
        /// </summary>
        public override void StartAnalyse(int processcount)
        {
            for (int i = 1; i < 65; i++)
            {
                string url = @"http://www.zycg.gov.cn/article/llist?catalog=ZhongBiao&page=" + i;
                bool midResult = HandleBusinessInfo(url);
                //记录日志
            }
        }

        /// <summary>
        /// 处理商机List
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private bool HandleBusinessInfo(string url)
        {
            bool result = true;
            try
            {
                string html = GetHTML(url);
                HtmlParser parser = new HtmlParser();
                IHtmlDocument document = parser.Parse(html);
                List<AngleSharp.Dom.IElement> divList =
                    document.QuerySelectorAll("ul")
                        .Where(table => table.GetAttribute("class") == "lby-list")
                        .ToList();
                IElement tableElement = divList[0];
                var liList = tableElement.GetElementsByTagName("li");
                List<Bidding> biddingList = new List<Bidding>();
                for (int i = 0; i < liList.Length; i++)
                {
                    Bidding bidding = new Bidding();
                    var aList = liList[i].QuerySelectorAll("a");
                    if (aList.Length > 0)
                    {
                        bidding.BusinessTitle = aList[0].GetAttribute("Title");
                        bidding.DetileURL = string.Format("http://www.zycg.gov.cn{0}", aList[0].GetAttribute("href"));
                    } var spanList = liList[i].QuerySelectorAll("span");
                    bidding.ReleaseTime = DateTime.Parse(StringHandler.ReplaceStringExtend(spanList[0].Text()));
                    BuildDetileinfo(bidding);
                    bidding.Source = this.Name;
                    bool insertSQL = InsertInfo(bidding);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 获取详细信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected override void BuildDetileinfo(Bidding bidding)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(bidding.DetileURL))
                return;
            string html = GetHTML(bidding.DetileURL);
            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.Parse(html);
            List<AngleSharp.Dom.IElement> list =
                document.QuerySelectorAll("script")
                    .Where(script => script.GetAttribute("id") == "container")
                    .ToList();
            if (list.Count <= 0)
                return;
            IHtmlDocument midDocument = parser.Parse(list[0].InnerHtml);
            List<AngleSharp.Dom.IElement> spanList = midDocument.QuerySelectorAll("span").ToList();
            for (int i = 0; i < spanList.Count; i++)
            {
                sb.Append(spanList[i].Text());
            }
            bidding.Content = sb.ToString().Replace(" ", "");
            string sbstr = sb.ToString().Replace(" ", "");
            //中标金额
            Match matchmoney = Regex.Match(sbstr, @"(中标金额|成交金额)[：|:]?人民币[0-9.]+.元", RegexOptions.None);
            if (matchmoney.Success)
            {
                bidding.Money = matchmoney.Value.Replace("中标金额：", "").Replace("成交金额：", "");
            }
            //采购人
            Match matchRelease = Regex.Match(sbstr, @"采购人名称[：|:]?[\u4e00-\u9fa5]{0,}[(|（|-|—]?", RegexOptions.None);
            if (matchRelease.Success)
            {
                bidding.ReleaseCom = matchRelease.Value.Replace("采购人名称：", "").Replace("采购人名称:", "").Replace("地址", "");
            }
            //成交供应商名称[：|:]?[\u4e00-\u9fa5]{0,}?[(|（|-|—]?[\u4e00-\u9fa5]{0,}）?[）|)|-|—]?[\u4e00-\u9fa5]{0,}?
            ///中标供应商
            ///(中标供应商名称|成交供应商名称)[：|:]?[\u4e00-\u9fa5]{0,}[(|（|-|—]?[\u4e00-\u9fa5]{0,}）[）|)|-|—]?[\u4e00-\u9fa5]{0,}
            Match matchSupplier = Regex.Match(sbstr, @"(中标供应商名称|成交供应商名称)[：|:]?[\u4e00-\u9fa5]{0,}[(|（|-|—]?[\u4e00-\u9fa5]{0,}[）|)|-|—]?[\u4e00-\u9fa5]{0,}", RegexOptions.None);
            if (matchSupplier.Success)
            {
                bidding.Supplier = matchSupplier.Value.Replace("中标供应商名称：", "").Replace("成交供应商名称：", "");
            }
            //项目编号
            Match matchProjectID = Regex.Match(sbstr, @"项目编号：[\u4E00-\u9FA5A-Za-z0-9]{2,20}[-|—]?[\u4E00-\u9FA5A-Za-z0-9]{2,20}", RegexOptions.None);
            if (matchProjectID.Success)
            {
                bidding.ProjectID = matchProjectID.Value.Replace("项目编号：", "");
            }
        }
    }
}