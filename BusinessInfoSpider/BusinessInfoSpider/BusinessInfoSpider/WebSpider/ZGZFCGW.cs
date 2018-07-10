﻿using System;
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
    public class ZGZFCGW : BaseSpider
    {
        public override string Name
        {
            get { return "中国政府采购网"; }
        }

        public override string WebURL
        {
            get { return "http://www.ccgp.gov.cn/"; }
        }
        /// <summary>
        /// 开始处理
        /// </summary>
        public override void StartAnalyse()
        {
            for (int i = 1; i < 5; i++)
            {
                
                string url = @"http://search.ccgp.gov.cn/bxsearch?searchtype=1&page_index=1&bidSort=0&buyerName=&projectId=&pinMu=0&bidType=1&dbselect=bidx&kw=&start_time=2018%3A02%3A11&end_time=2018%3A03%3A14&timeType=3&displayZone=&zoneId=&pppStatus=0&agentName=" + i;
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
                    info.Content = GetDetileByURL(info.DetileURL);
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
            List<AngleSharp.Dom.IElement> list =
                document.QuerySelectorAll("script")
                    .Where(script => script.GetAttribute("id") == "container")
                    .ToList();
            if (list.Count <= 0)
                return "";
            IHtmlDocument midDocument = parser.Parse(list[0].InnerHtml);
            List<AngleSharp.Dom.IElement> spanList = midDocument.QuerySelectorAll("span").ToList();
            for (int i = 0; i < spanList.Count; i++)
            {
                sb.Append(spanList[i].Text());
            }
            return sb.ToString().Replace(" ", "");
        }
    }
}
