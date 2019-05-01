using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using AngleSharp.Dom;
using BusinessInfoSpider.DB;
using BusinessInfoSpider.Model;
using MySql.Data.MySqlClient;

namespace BusinessInfoSpider.WebSpider
{
    /// <summary>
    /// 招标信息爬虫基类
    /// </summary>
    public abstract class BaseSpider
    {
        /// <summary>
        /// 向数据库插入招标信息
        /// </summary>
        /// <param name="info">招标信息</param>
        /// <returns>是否插入成功</returns>
        protected bool InsertInfo(BusinessInfo info)
        {
            if (JudegeInfoExist(info))
                return true;

            List<CommandInfo> commandList = new List<CommandInfo>();
            CommandInfo insertBusiness = new CommandInfo();
            insertBusiness.CommandText = "Insert Into business(BusinessID,BusinessTitle,ReleaseCom,Money,ReleaseLocation,ReleaseTime,DetileURL,Source) Values (?BusinessID,?BusinessTitle,?ReleaseCom,?Money,?ReleaseLocation,?ReleaseTime,?DetileURL,?Source);";
            MySqlParameter[] insertBusinessParameters = new MySqlParameter[]
            {
                new MySqlParameter("BusinessID",MySqlDbType.VarChar,50),
                new MySqlParameter("BusinessTitle",MySqlDbType.VarChar,200),
                new MySqlParameter("ReleaseCom",MySqlDbType.VarChar,200),
                new MySqlParameter("Money",MySqlDbType.VarChar,50),
                new MySqlParameter("ReleaseLocation",MySqlDbType.VarChar,200),
                new MySqlParameter("ReleaseTime",MySqlDbType.Date),
                new MySqlParameter("DetileURL",MySqlDbType.VarChar,500),
                new MySqlParameter("Source",MySqlDbType.VarChar,50)
            };
            insertBusinessParameters[0].Value = info.GUID;
            insertBusinessParameters[1].Value = info.Title ?? "";
            insertBusinessParameters[2].Value = info.ComName ?? "";
            insertBusinessParameters[3].Value = info.Money ?? "";
            insertBusinessParameters[4].Value = info.Location ?? "";
            insertBusinessParameters[5].Value = info.ReleaseTime;
            insertBusinessParameters[6].Value = info.DetileURL ?? "";
            insertBusinessParameters[7].Value = info.Source ?? "";

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
            insertDetileParameters[2].Value = info.DetileURL ?? "";
            info.Content = info.Content.Length > 20000 ? info.Content.Substring(0, 20000) : info.Content;
            insertDetileParameters[3].Value = info.Content ?? "";
            insertDetile.Parameters = insertDetileParameters;
            commandList.Add(insertDetile);
            int result = DbHelperMySQL.ExecuteSqlTran(commandList);

            if (result > 0)
                return true;
            return false;
        }
        /// <summary>
        /// 判断改数据是否存在，存在则不再保存
        /// </summary>
        /// <param name="info">相关信息</param>
        /// <returns>是否存在</returns>
        protected bool JudegeInfoExist(BusinessInfo info)
        {
            string SQL_GetInfo =
                "Select * From business where business.Source=?Source and business.BusinessTitle=?BusinessTitle";
            MySqlParameter[] getinfoParameters = new MySqlParameter[]
            {
                new MySqlParameter("Source", MySqlDbType.VarChar, 50),
                new MySqlParameter("BusinessTitle", MySqlDbType.VarChar, 200)
            };
            getinfoParameters[0].Value = info.Source;
            getinfoParameters[1].Value = info.Title;
            object infoobj = DbHelperMySQL.GetSingle(SQL_GetInfo, getinfoParameters);
            if (infoobj != null) //如果已经有了该数据
                return true;
            return false;
        }

        /// <summary>
        /// 根据网络地址获取网页信息
        /// </summary>
        /// <param name="url">网页地址</param>
        /// <returns>全部HTML信息</returns>
        protected string GetHTML(string url)
        {
            string result = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Timeout = 180000;//10秒钟获取
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream htmlStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(htmlStream, Encoding.UTF8);
            result = reader.ReadToEnd();
            return result;
        }
        /// <summary>
        /// 网站名称
        /// </summary>
        public virtual string Name
        {
            get { return ""; }
        }
        /// <summary>
        /// 网站主页地址
        /// </summary>
        public virtual string WebURL
        {
            get { return ""; }
        }
        /// <summary>
        /// 总解析数量
        /// </summary>
        public virtual int Total { get; set; }
        /// <summary>
        /// 已处理数量
        /// </summary>
        public virtual int Current { get; set; }

        /// <summary>
        /// 解析信息
        /// <param name="processcount">启动抓取线程数</param>
        /// </summary>
        public virtual void StartAnalyse(int processcount)
        {
        }
        /// <summary>
        /// 根据招标详细信息地址获取招标详细信息
        /// </summary>
        /// <param name="info">招标详细信息地址</param>
        /// <returns>相应内容</returns>
        protected virtual void BuildDetileinfo(BusinessInfo info)
        {
        }
    }
}
