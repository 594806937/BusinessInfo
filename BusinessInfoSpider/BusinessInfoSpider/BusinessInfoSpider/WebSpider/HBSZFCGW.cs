using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using BusinessInfoSpider.Model;

namespace BusinessInfoSpider.WebSpider
{
    public class HBSZFCGW : BaseSpider
    {
        public HBSZFCGW()
        {

        }

        public override string Name
        {
            get { return "河北省政府采购网"; }
        }

        public override string WebURL
        {
            get { return "http://www.ccgp-hebei.gov.cn/zfcg/"; }
        }
        /// <summary>
        /// 开始处理
        /// </summary>
        public override void StartAnalyse(int processcount)
        {
            for (int i = 1; i < 5; i++)
            {
                string url = @"http://www.ccgp-hebei.gov.cn/province/cggg/zbgg/index_" + i + ".html";
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
                    document.QuerySelectorAll("table")
                        .Where(table => table.GetAttribute("id") == "moredingannctable")
                        .ToList();
                IElement tableElement = divList[0];
                var trList = tableElement.GetElementsByTagName("tr");
                List<BusinessInfo> businessList = new List<BusinessInfo>();
                for (int i = 0; i < trList.Length; i = i + 2)
                {
                    BusinessInfo info = new BusinessInfo();
                    var aList = trList[i].QuerySelectorAll("a");
                    if (aList.Length > 0)
                    {
                        info.Title = aList[0].Text();
                    }
                    string codeString = trList[i].GetAttribute("onclick");
                    var traList = trList[i].QuerySelectorAll("a");
                    if (traList.Length > 0)
                    {
                        string detileinfo = traList[0].GetAttribute("href").TrimStart('.');
                        info.DetileURL = "http://www.ccgp-hebei.gov.cn/province/cggg/zbgg" + detileinfo;
                        BuildDetileinfo(info);
                    }
                    BuildDetileinfo(info);
                    var spanList = trList[i + 1].QuerySelectorAll("span");
                    if (spanList.Length > 0)
                    {
                        info.ReleaseTime = DateTime.Parse(spanList[0].Text());
                        info.Location = spanList[1].Text();
                        info.ComName = spanList[2].Text();
                    }
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
            List<AngleSharp.Dom.IElement> spanlist =
                document.QuerySelectorAll("span")
                    .Where(span => span.GetAttribute("class") == "txt7")
                    .ToList();
            string amt = document.QuerySelector("#amt").Text();
            if (!string.IsNullOrEmpty(amt))
                info.Money = (double.Parse(amt) / 10000).ToString();
            if (spanlist.Count <= 0)
                return;
            for (int i = 0; i < spanlist.Count; i++)
            {
                sb.Append(spanlist[i].Text());
            }
            info.Content = sb.ToString().Replace(" ", "");
        }
    }
}
