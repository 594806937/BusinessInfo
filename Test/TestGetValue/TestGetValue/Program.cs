using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;
using AngleSharp.Services.Default;
using MySql.Data.MySqlClient;
using TestGetValue.DB;

namespace TestGetValue
{
    class Program
    {
        static void Main(string[] args)
        {
            SearchKeyWord(new List<string>());
            List<BusinessInfo> businessList = new List<BusinessInfo>();

            for (int i = 1; i < 5; i++)
            {
                string url = @"http://www.ccgp-hebei.gov.cn/zfcg/web/getBidingList_" + i + ".html#";
                List<BusinessInfo> midList = GetBusinessInfoList(url);
                businessList.AddRange(midList);
                Console.WriteLine("完成第{0}部分处理", i);
            }
            Console.WriteLine("*****查询完成*****");
            Console.ReadKey();
        }
        /// <summary>
        /// 处理商机List
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static List<BusinessInfo> GetBusinessInfoList(string url)
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
                info.Source = new HBZFCGW().Name;
                bool InsertSQL = InsertInfo(info);
                Console.WriteLine("{0}插入数据库{1}", info.Title, InsertSQL ? "成功" : "失败");
                businessList.Add(info);
            }
            return businessList;
        }

        private static string GetDetileByURL(string url)
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

        private static string GetHTML(string url)
        {
            string result = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream htmlStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(htmlStream, Encoding.UTF8);
            result = reader.ReadToEnd();
            return result;
        }

        private static bool InsertInfo(BusinessInfo info)
        {
            List<CommandInfo> commandList = new List<CommandInfo>();

            CommandInfo insertBusiness = new CommandInfo();
            insertBusiness.CommandText = "Insert Into business(BusinessID,BusinessTitle,ReleaseCom,ReleaseLocation,ReleaseTime,DetileURL,Source) Values (?BusinessID,?BusinessTitle,?ReleaseCom,?ReleaseLocation,?ReleaseTime,?DetileURL,?Source);";
            MySqlParameter[] insertBusinessParameters = new MySqlParameter[]
            {
                new MySqlParameter("BusinessID",MySqlDbType.VarChar,50),
                new MySqlParameter("BusinessTitle",MySqlDbType.VarChar,200),
                new MySqlParameter("ReleaseCom",MySqlDbType.VarChar,200),
                new MySqlParameter("ReleaseLocation",MySqlDbType.VarChar,200),
                new MySqlParameter("ReleaseTime",MySqlDbType.Date),
                new MySqlParameter("DetileURL",MySqlDbType.VarChar,500),
                new MySqlParameter("Source",MySqlDbType.VarChar,50)
            };
            insertBusinessParameters[0].Value = info.GUID;
            insertBusinessParameters[1].Value = info.Title;
            insertBusinessParameters[2].Value = info.ComName;
            insertBusinessParameters[3].Value = info.Location;
            insertBusinessParameters[4].Value = info.ReleaseTime;
            insertBusinessParameters[5].Value = info.DetileURL;
            insertBusinessParameters[6].Value = info.Source;

            insertBusiness.Parameters = insertBusinessParameters;
            commandList.Add(insertBusiness);

            CommandInfo insertDetile = new CommandInfo();
            insertDetile.CommandText = "Insert Into Detile(DetileID,BusinessID,DetileURL,Content) Values(?DetileID,?BusinessID,?DetileURL,?Content);";
            MySqlParameter[] insertDetileParameters =
            {
                new MySqlParameter("DetileID",MySqlDbType.VarChar,50),
                new MySqlParameter("BusinessID",MySqlDbType.VarChar,50),
                new MySqlParameter("DetileURL",MySqlDbType.VarChar,500),
                new MySqlParameter("Content",MySqlDbType.VarChar,5000)
            };
            insertDetileParameters[0].Value = Guid.NewGuid().ToString();
            insertDetileParameters[1].Value = info.GUID;
            insertDetileParameters[2].Value = info.DetileURL;
            insertDetileParameters[3].Value = info.Content;

            insertDetile.Parameters = insertDetileParameters;
            commandList.Add(insertDetile);
            int result = DbHelperMySQL.ExecuteSqlTran(commandList);

            if (result > 0)
                return true;
            return false;
        }

        private static List<BusinessInfo> SearchKeyWord(List<string> keywordList)
        {
            keywordList = new List<string>() { "地理", "信息系统", "GIS", "软件", "地灾", "测绘" };
            StringBuilder sb = new StringBuilder();
            sb.Append("Select * From business where business.BusinessID in(");
            sb.Append("select BusinessID from View_QueryKeyWord where");
            for (int i = 0; i < keywordList.Count; i++)
            {
                sb.Append(" View_QueryKeyWord.BusinessTitle");
                sb.Append(" Like ");
                sb.Append(string.Format("'%{0}%'", keywordList[i]));
                sb.Append(" Or ");
            }

            for (int i = 0; i < keywordList.Count; i++)
            {
                sb.Append(" View_QueryKeyWord.Content");
                sb.Append(" Like ");
                sb.Append(string.Format("'%{0}%'", keywordList[i]));
                if (i != (keywordList.Count - 1))
                    sb.Append(" Or ");
            }
            sb.Append(");");
            string SQL_SearchKeyWord = sb.ToString();
            List<BusinessInfo> result = new List<BusinessInfo>();
            DataTable dt = DbHelperMySQL.GetDataTable(SQL_SearchKeyWord);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BusinessInfo info = new BusinessInfo();
                info.GUID = dt.Rows[i]["BusinessID"].ToString();
                info.Title = dt.Rows[i]["BusinessTitle"].ToString();
                info.ComName = dt.Rows[i]["ReleaseCom"].ToString();
                info.Location = dt.Rows[i]["ReleaseLocation"].ToString();
                info.ReleaseTime = DateTime.Parse(dt.Rows[i]["ReleaseTime"].ToString());
                info.DetileURL = dt.Rows[i]["DetileURL"].ToString();
                info.Source = dt.Rows[i]["Source"].ToString();
                result.Add(info);
            }
            return result;
        }
    }

    /// <summary>
    /// 商业信息类
    /// </summary>
    public class BusinessInfo
    {
        private string guid = Guid.NewGuid().ToString();
        /// <summary>
        /// 默认有GUID值，无需初始化赋值
        /// </summary>
        public string GUID
        {
            get { return this.guid; }
            set { this.guid = value; }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 发布单位
        /// </summary>
        public string ComName { get; set; }
        /// <summary>
        /// 单位地址
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime ReleaseTime { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetileURL { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 是否有数据
        /// </summary>
        public bool KeyFlag { get; set; }

        public string Source { get; set; }
    }
}
