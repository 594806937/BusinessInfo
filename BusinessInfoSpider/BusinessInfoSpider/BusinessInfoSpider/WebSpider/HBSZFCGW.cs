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
        public override void StartAnalyse()
        {
            for (int i = 1; i < 5; i++)
            {
                string url = @"http://www.ccgp-hebei.gov.cn/zfcg/web/getBidingList_" + i + ".html#";
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
                    string code = codeString.Substring(codeString.IndexOf("'") + 1, 6);
                    info.DetileURL = "http://www.ccgp-hebei.gov.cn/zfcg/1/bidingAnncDetail_" + code + ".html";
                    info.Content = GetDetileByURL(info.DetileURL);
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
        /// <param name="url"></param>
        /// <returns></returns>
        protected override string GetDetileByURL(string url)
        {
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(url))
                return "";
            string html = GetHTML(url);
            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.Parse(html);
            List<AngleSharp.Dom.IElement> spanlist =
                document.QuerySelectorAll("span")
                    .Where(span => span.GetAttribute("class") == "txt7")
                    .ToList();
            if (spanlist.Count <= 0)
                return "";
            for (int i = 0; i < spanlist.Count; i++)
            {
                sb.Append(spanlist[i].Text());
            }
            return sb.ToString().Replace(" ", "");
        }
    }
}
