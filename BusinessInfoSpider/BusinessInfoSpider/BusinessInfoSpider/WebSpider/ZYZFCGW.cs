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

namespace BusinessInfoSpider.WebSpider
{
    public class ZYZFCGW : BaseSpider
    {
        public override string Name
        {
            get { return "中央政府采购网"; }
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
            for (int i = 1; i < 5; i++)
            {
                string url = @"http://www.zycg.gov.cn/article/llist?catalog=StockAffiche&page=" + i;
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
                List<BusinessInfo> businessList = new List<BusinessInfo>();
                for (int i = 0; i < liList.Length; i++)
                {
                    BusinessInfo info = new BusinessInfo();
                    var aList = liList[i].QuerySelectorAll("a");
                    if (aList.Length > 0)
                    {
                        info.Title = aList[0].GetAttribute("Title");
                        info.DetileURL = string.Format("http://www.zycg.gov.cn{0}", aList[0].GetAttribute("href"));
                    } var spanList = liList[i].QuerySelectorAll("span");
                    info.ReleaseTime = DateTime.Parse(StringHandler.ReplaceStringExtend(spanList[0].Text()));
                    BuildDetileinfo(info);
                    info.Source = this.Name;
                    bool insertSQL = InsertInfo(info);
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
        protected override void BuildDetileinfo(BusinessInfo info)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(info.DetileURL))
                return;
            string html = GetHTML(info.DetileURL);
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
            info.Content = sb.ToString().Replace(" ", "");
        }
    }
}
