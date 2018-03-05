using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using BusinessInfoViewer.IDAL;
using BusinessInfoViewer.DBUtility;

//Please add references
namespace BusinessInfoViewer.MySQLDAL
{
    /// <summary>
    /// 数据访问类:business
    /// </summary>
    public partial class business : IBusiness
    {
        public business()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string BusinessID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from business");
            strSql.Append(" where BusinessID=@BusinessID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@BusinessID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = BusinessID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(BusinessInfoViewer.Model.business model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into business(");
            strSql.Append("BusinessID,BusinessTitle,ReleaseCom,ReleaseLocation,ReleaseTime,DetileURL,Source)");
            strSql.Append(" values (");
            strSql.Append("@BusinessID,@BusinessTitle,@ReleaseCom,@ReleaseLocation,@ReleaseTime,@DetileURL,@Source)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@BusinessID", MySqlDbType.VarChar,50),
					new MySqlParameter("@BusinessTitle", MySqlDbType.VarChar,200),
					new MySqlParameter("@ReleaseCom", MySqlDbType.VarChar,200),
					new MySqlParameter("@ReleaseLocation", MySqlDbType.VarChar,200),
					new MySqlParameter("@ReleaseTime", MySqlDbType.Date),
					new MySqlParameter("@DetileURL", MySqlDbType.VarChar,500),
					new MySqlParameter("@Source", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.BusinessID;
            parameters[1].Value = model.BusinessTitle;
            parameters[2].Value = model.ReleaseCom;
            parameters[3].Value = model.ReleaseLocation;
            parameters[4].Value = model.ReleaseTime;
            parameters[5].Value = model.DetileURL;
            parameters[6].Value = model.Source;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BusinessInfoViewer.Model.business model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update business set ");
            strSql.Append("BusinessTitle=@BusinessTitle,");
            strSql.Append("ReleaseCom=@ReleaseCom,");
            strSql.Append("ReleaseLocation=@ReleaseLocation,");
            strSql.Append("ReleaseTime=@ReleaseTime,");
            strSql.Append("DetileURL=@DetileURL,");
            strSql.Append("Source=@Source");
            strSql.Append(" where BusinessID=@BusinessID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@BusinessTitle", MySqlDbType.VarChar,200),
					new MySqlParameter("@ReleaseCom", MySqlDbType.VarChar,200),
					new MySqlParameter("@ReleaseLocation", MySqlDbType.VarChar,200),
					new MySqlParameter("@ReleaseTime", MySqlDbType.Date),
					new MySqlParameter("@DetileURL", MySqlDbType.VarChar,500),
					new MySqlParameter("@Source", MySqlDbType.VarChar,50),
					new MySqlParameter("@BusinessID", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.BusinessTitle;
            parameters[1].Value = model.ReleaseCom;
            parameters[2].Value = model.ReleaseLocation;
            parameters[3].Value = model.ReleaseTime;
            parameters[4].Value = model.DetileURL;
            parameters[5].Value = model.Source;
            parameters[6].Value = model.BusinessID;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string BusinessID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from business ");
            strSql.Append(" where BusinessID=@BusinessID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@BusinessID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = BusinessID;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string BusinessIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from business ");
            strSql.Append(" where BusinessID in (" + BusinessIDlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BusinessInfoViewer.Model.business GetModel(string BusinessID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BusinessID,BusinessTitle,ReleaseCom,ReleaseLocation,ReleaseTime,DetileURL,Source from business ");
            strSql.Append(" where BusinessID=@BusinessID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@BusinessID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = BusinessID;

            BusinessInfoViewer.Model.business model = new BusinessInfoViewer.Model.business();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BusinessInfoViewer.Model.business DataRowToModel(DataRow row)
        {
            BusinessInfoViewer.Model.business model = new BusinessInfoViewer.Model.business();
            if (row != null)
            {
                if (row["BusinessID"] != null)
                {
                    model.BusinessID = row["BusinessID"].ToString();
                }
                if (row["BusinessTitle"] != null)
                {
                    model.BusinessTitle = row["BusinessTitle"].ToString();
                }
                if (row["ReleaseCom"] != null)
                {
                    model.ReleaseCom = row["ReleaseCom"].ToString();
                }
                if (row["ReleaseLocation"] != null)
                {
                    model.ReleaseLocation = row["ReleaseLocation"].ToString();
                }
                if (row["ReleaseTime"] != null && row["ReleaseTime"].ToString() != "")
                {
                    model.ReleaseTime = DateTime.Parse(row["ReleaseTime"].ToString());
                }
                if (row["DetileURL"] != null)
                {
                    model.DetileURL = row["DetileURL"].ToString();
                }
                if (row["Source"] != null)
                {
                    model.Source = row["Source"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select BusinessID,BusinessTitle,ReleaseCom,ReleaseLocation,ReleaseTime,DetileURL,Source ");
            strSql.Append(" FROM business ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM business ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.BusinessID desc");
            }
            strSql.Append(")AS Row, T.*  from business T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            MySqlParameter[] parameters = {
                    new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@PageSize", MySqlDbType.Int32),
                    new MySqlParameter("@PageIndex", MySqlDbType.Int32),
                    new MySqlParameter("@IsReCount", MySqlDbType.Bit),
                    new MySqlParameter("@OrderType", MySqlDbType.Bit),
                    new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "business";
            parameters[1].Value = "BusinessID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}

