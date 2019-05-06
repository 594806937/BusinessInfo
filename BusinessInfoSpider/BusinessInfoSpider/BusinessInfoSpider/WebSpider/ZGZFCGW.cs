using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private int total = 100;
        private int process = 4;//线程数
        private int currentStep;//当前处理到第多少个
        private int successcount;//成功结果
        private int failcount;//失败结果
        /// <summary>
        /// 开始处理
        /// </summary>
        public override void StartAnalyse(int processcount)
        {
            process = processcount;
            string dateNow = DateTime.Now.ToString("yyyy:MM:dd");
            string dateBefore7Days = DateTime.Now.AddDays(-7).ToString("yyyy:MM:dd");
            //获取有多少个信息
            string firsturl = string.Format(@"http://search.ccgp.gov.cn/bxsearch?searchtype=1&page_index=1&bidSort=0&buyerName=&projectId=&pinMu=0&bidType=1&dbselect=bidx&kw=&start_time={0}&end_time={1}&timeType=2&displayZone=&zoneId=&pppStatus=0&agentName=", dateNow, dateBefore7Days);
            string firstpagehtml = GetHTML(firsturl);
            HtmlParser parser = new HtmlParser();
            IHtmlDocument document = parser.Parse(firstpagehtml);
            List<IElement> pagerList =
                document.QuerySelectorAll("p")
                    .Where(table => table.GetAttribute("class") == "pager")
                    .ToList();
            if (pagerList.Count > 0)
            {
                string pagertxt = pagerList[0].Html();
                if (pagertxt != string.Empty)
                {
                    string[] pagerarr = pagertxt.Split(':');
                    if (pagerarr.Length > 0)
                    {
                        total = Convert.ToInt32(pagerarr[1].Split(',')[0]);
                    }
                }
            }
            AppLog.Info(string.Format("{0}:检测到{1}个待抓取页面，处理线程数:{2}", Name, total, processcount));
            for (int m = 0; m < processcount; m++)
            {
                Thread tr = new Thread(ProcessByThread);
                tr.Start(m);
                AppLog.Info(string.Format("{0}:已启动第{1}个线程开始处理", Name, m));
                Thread.Sleep(10000);
            }
        }

        private void ProcessByThread(object index)
        {
            int newindex = (int)index;
            string dateNow = DateTime.Now.ToString("yyyy:MM:dd");
            string dateBefore7Days = DateTime.Now.AddDays(-7).ToString("yyyy:MM:dd");
            for (int i = newindex * (total / process) + 1; i <= (newindex + 1) * (total / process); i++)
            {
                string url = string.Format(@"http://search.ccgp.gov.cn/bxsearch?searchtype=1&page_index={0}&bidSort=0&buyerName=&projectId=&pinMu=0&bidType=1&dbselect=bidx&kw=&start_time={1}&end_time={2}&timeType=6&displayZone=&zoneId=&pppStatus=0&agentName=", i, dateNow, dateBefore7Days);
                HandleBusinessInfo(url);
            }
        }

        /// <summary>
        /// 处理商机List
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private void HandleBusinessInfo(object url)
        {
            bool result = true;
            string url_string = (string)url;
            try
            {
                string html = GetHTML(url_string);
                HtmlParser parser = new HtmlParser();
                IHtmlDocument document = parser.Parse(html);
                List<IElement> divList =
                    document.QuerySelectorAll("ul")
                        .Where(table => table.GetAttribute("class") == "vT-srch-result-list-bid")
                        .ToList();
                if (divList.Count == 0)
                {
                    throw new Exception(string.Format("处理失败,未获取到Div信息{0}", Name));
                }
                IElement tableElement = divList[0];
                var liList = tableElement.GetElementsByTagName("li");
                List<BusinessInfo> businessList = new List<BusinessInfo>();
                for (int i = 0; i < liList.Length; i++)
                {
                    BusinessInfo info = new BusinessInfo();
                    var aList = liList[i].QuerySelectorAll("a");
                    if (aList.Length > 0)
                    {
                        info.Title = aList[0].Text().Replace('\n', ' ').Trim(' ');
                        info.DetileURL = aList[0].GetAttribute("href");
                    }
                    var spanList = liList[i].QuerySelectorAll("span");
                    string time = spanList[0].Text().Split('|')[0];
                    string midComName = spanList[0].Text().Split('|')[1].Replace('\n', ' ').Trim(' ');
                    info.ComName = midComName.Substring(midComName.IndexOf("：") + 1, midComName.Length - midComName.IndexOf("：") - 1);
                    info.ReleaseTime = DateTime.Parse(time);
                    BuildDetileinfo(info);
                    info.Source = Name;

                    bool insertSQL = InsertInfo(info);
                }
                successcount++;
            }
            catch (Exception ex)
            {
                result = false;
                failcount++;
                AppLog.Info(string.Format("处理失败{0}:{1}", url, ex.Message));
            }
            finally
            {
                currentStep++;
                AppLog.Info(string.Format("{0}/{1}已处理完成，成功:{2}，失败:{3}", currentStep, total, successcount, failcount));
            }
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

            List<IElement> tablelist =
                document.QuerySelectorAll("tr").ToList();
            if (tablelist.Count <= 0)
                return;
            for (int i = 0; i < tablelist.Count; i++)
            {
                IHtmlDocument trDocument = parser.Parse(tablelist[i].InnerHtml);
                string moneycontent = trDocument.ChildNodes[0].TextContent;
                if (moneycontent.Contains("金额"))
                {
                    if (moneycontent.Contains("万"))
                    {
                        string midresult = moneycontent.Substring(moneycontent.IndexOf('￥') + 1,
    moneycontent.Length - moneycontent.IndexOf('￥') - 1);
                        string finalresult = midresult.Substring(0, midresult.IndexOf("万"));
                        info.Money = finalresult;
                        break;
                    }
                    else if (moneycontent.Contains("详见"))
                    {
                        info.Money =
                            moneycontent.Substring(moneycontent.IndexOf("额") + 1,
                                moneycontent.Length - moneycontent.IndexOf("额") - 1);
                    }
                    else
                    {
                        info.Money = "未获取到金额数据，请自行查看";
                    }

                }
            }
            List<IElement> list =
                document.QuerySelectorAll("div")
                    .Where(div => div.GetAttribute("class") == "vF_detail_content").ToList();
            if (list.Count <= 0)
                return;
            IHtmlDocument midDocument = parser.Parse(list[0].InnerHtml);
            List<IElement> pList = midDocument.QuerySelectorAll("p").ToList();
            for (int i = 0; i < pList.Count; i++)
            {
                sb.Append(pList[i].Text());
            }
            info.Content = sb.ToString().Replace(" ", "");
        }
    }
}
