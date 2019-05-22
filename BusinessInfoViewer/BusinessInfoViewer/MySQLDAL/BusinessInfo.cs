using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using BusinessInfoViewer.Model;
using BusinessInfoViewer.DBUtility;
using System.Text;

namespace BusinessInfoViewer.MySQLDAL
{
    public class BusinessInfo : BusinessInfoViewer.IDAL.IBusinessInfo
    {
        /// <summary>
        /// 根据关键字查询内容并返回BusinessInfo
        /// </summary>
        /// <param name="keywordList"></param>
        /// <returns></returns>
        public List<Model.BusinessInfo> SearchKeyWord(List<string> keywordList)
        {
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
            List<Model.BusinessInfo> result = new List<Model.BusinessInfo>();
            DataTable dt = DbHelperMySQL.GetDataTable(SQL_SearchKeyWord);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Model.BusinessInfo info = new Model.BusinessInfo();
                info.GUID = dt.Rows[i]["BusinessID"].ToString();
                info.Title = dt.Rows[i]["BusinessTitle"].ToString();
                info.ComName = dt.Rows[i]["ReleaseCom"].ToString();
                info.Location = dt.Rows[i]["ReleaseLocation"].ToString();
                info.ReleaseTime = DateTime.Parse(dt.Rows[i]["ReleaseTime"].ToString());
                info.DetileURL = dt.Rows[i]["DetileURL"].ToString();
                info.Source = dt.Rows[i]["Source"].ToString();
                info.Money = dt.Rows[i]["Money"].ToString();
                result.Add(info);
            }
            return result;
        }
        /// <summary>
        /// 根据关键字查询相关度后，返回包含相关度的BusinessInfo
        /// </summary>
        /// <param name="keywordList">关键字List</param>
        /// <returns>包含相关度的BusinessInfo</returns>
        public List<Model.BusinessInfoEx> SearchKeyWordEx(List<string> keywordList)
        {
            List<Model.BusinessInfoEx> result = new List<BusinessInfoEx>();

            StringBuilder sb = new StringBuilder();
            sb.Append("select business.*,(m.degree/");
            sb.Append((keywordList.Count).ToString());
            sb.Append(")");
            sb.Append(" as dependcy from business,(select v.BusinessID,v.BusinessTitle,(");
            for (int i = 0; i < keywordList.Count; i++)
            {
                sb.Append("CASE WHEN LOCATE(");
                sb.Append("'" + keywordList[i] + "'");
                sb.Append(",v.BusinessTitle)>0");
                sb.Append(" then 1 else 0 end");
                sb.Append("+");
                sb.Append("CASE WHEN LOCATE(");
                sb.Append("'" + keywordList[i] + "'");
                sb.Append(",v.Content)>0");
                sb.Append(" then 1 else 0 end");
                if (i != keywordList.Count - 1)
                    sb.Append("+");
            }
            sb.Append(") as degree");
            sb.Append(
                " from view_querykeyword as v) as m where business.BusinessID = m.BusinessID and degree>=2 Order by m.degree desc");
            /*select business.*,(m.degree/2) as dependcy from business,(
              select v.BusinessID,v.BusinessTitle,
              (case WHEN LOCATE('地理信息',v.BusinessTitle)>0 then 1 ELSE 0 end +  
              CASE WHEN LOCATE('软件',v.BusinessTitle)>0 then 1 else 0 END) as degree from view_querykeyword as v) as m 
              where business.BusinessID = m.BusinessID order by dependcy desc*/
            //用于参考
            string SQL_SearchKeyWord = sb.ToString();
            DataTable dt = DbHelperMySQL.GetDataTable(SQL_SearchKeyWord);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToDouble(dt.Rows[i]["dependcy"]) > 0)
                {
                    Model.BusinessInfoEx info = new Model.BusinessInfoEx();
                    info.GUID = dt.Rows[i]["BusinessID"].ToString();
                    info.Title = dt.Rows[i]["BusinessTitle"].ToString();
                    info.ComName = dt.Rows[i]["ReleaseCom"].ToString();
                    info.Location = dt.Rows[i]["ReleaseLocation"].ToString();
                    info.ReleaseTime = DateTime.Parse(dt.Rows[i]["ReleaseTime"].ToString());
                    info.DetileURL = dt.Rows[i]["DetileURL"].ToString();
                    info.Source = dt.Rows[i]["Source"].ToString();
                    info.Money = dt.Rows[i]["Money"].ToString();
                    info.Degree = Convert.ToDouble(dt.Rows[i]["dependcy"]);
                    result.Add(info);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据关键字查询相关度后，返回包含相关度的BusinessInfo
        /// </summary>
        /// <param name="keywordList">关键字List</param>
        /// <returns>包含相关度的BusinessInfo</returns>
        public List<Model.BusinessInfoEx> SearchKeyWordEx(List<Keyword> keywordList)
        {
            List<Model.BusinessInfoEx> result = new List<BusinessInfoEx>();
            StringBuilder sb = new StringBuilder();
            sb.Append("select business.*,(m.degree/");
            sb.Append((keywordList.Sum(Keyword => Keyword.Weight)));
            sb.Append(")");
            sb.Append(" as dependcy from business,(select v.BusinessID,v.BusinessTitle,(");
            for (int i = 0; i < keywordList.Count; i++)
            {
                sb.Append("CASE WHEN LOCATE(");
                sb.Append("'" + keywordList[i].Name + "'");
                sb.Append(",v.BusinessTitle)>0");
                sb.Append(string.Format(" then {0} else 0 end", keywordList[i].Weight));
                sb.Append("+");
                sb.Append("CASE WHEN LOCATE(");
                sb.Append("'" + keywordList[i].Name + "'");
                sb.Append(",v.Content)>0");
                sb.Append(string.Format(" then {0} else 0 end", keywordList[i].Weight));
                if (i != keywordList.Count - 1)
                    sb.Append("+");
            }
            sb.Append(") as degree");
            sb.Append(
                " from view_querykeyword as v) as m where business.BusinessID = m.BusinessID and degree>=2 Order by m.degree desc");
            /*select business.*,(m.degree/2) as dependcy from business,(
              select v.BusinessID,v.BusinessTitle,
              (case WHEN LOCATE('地理信息',v.BusinessTitle)>0 then 1 ELSE 0 end +  
              CASE WHEN LOCATE('软件',v.BusinessTitle)>0 then 1 else 0 END) as degree from view_querykeyword as v) as m 
              where business.BusinessID = m.BusinessID order by dependcy desc*/
            //用于参考
            string SQL_SearchKeyWord = sb.ToString();
            DataTable dt = DbHelperMySQL.GetDataTable(SQL_SearchKeyWord);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToDouble(dt.Rows[i]["dependcy"]) > 0)
                {
                    Model.BusinessInfoEx info = new Model.BusinessInfoEx();
                    info.GUID = dt.Rows[i]["BusinessID"].ToString();
                    info.Title = dt.Rows[i]["BusinessTitle"].ToString();
                    info.ComName = dt.Rows[i]["ReleaseCom"].ToString();
                    info.Location = dt.Rows[i]["ReleaseLocation"].ToString();
                    info.ReleaseTime = DateTime.Parse(dt.Rows[i]["ReleaseTime"].ToString());
                    info.DetileURL = dt.Rows[i]["DetileURL"].ToString();
                    info.Source = dt.Rows[i]["Source"].ToString();
                    info.Money = dt.Rows[i]["Money"].ToString();
                    info.Degree = Convert.ToDouble(dt.Rows[i]["dependcy"]);
                    result.Add(info);
                }
            }
            return result;
        }

        /// <summary>
        /// 根据日期返回带相关度的信息，为指定日期导出做准备
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public List<Model.BusinessInfoEx> SearchByDate(DateTime date)
        {
            List<BusinessInfoEx> result = new List<BusinessInfoEx>();
            return result;
        }
    }
}
