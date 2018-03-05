using System;
using System.Data;
using System.Text;
using BusinessInfoViewer.DBUtility;
using BusinessInfoViewer.IDAL;
using MySql.Data.MySqlClient;

namespace BusinessInfoViewer.MySQLDAL
{
    /// <summary>
    /// 数据访问类:detile
    /// </summary>
    public partial class Detile : Idetile
    {
        public Detile()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string DetileID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from detile");
            strSql.Append(" where DetileID=@DetileID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@DetileID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = DetileID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(BusinessInfoViewer.Model.detile model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into detile(");
            strSql.Append("DetileID,BusinessID,DetileURL,Content)");
            strSql.Append(" values (");
            strSql.Append("@DetileID,@BusinessID,@DetileURL,@Content)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@DetileID", MySqlDbType.VarChar,50),
					new MySqlParameter("@BusinessID", MySqlDbType.VarChar,50),
					new MySqlParameter("@DetileURL", MySqlDbType.VarChar,500),
					new MySqlParameter("@Content", MySqlDbType.VarChar,5000)};
            parameters[0].Value = model.DetileID;
            parameters[1].Value = model.BusinessID;
            parameters[2].Value = model.DetileURL;
            parameters[3].Value = model.Content;

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
        public bool Update(BusinessInfoViewer.Model.detile model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update detile set ");
            strSql.Append("BusinessID=@BusinessID,");
            strSql.Append("DetileURL=@DetileURL,");
            strSql.Append("Content=@Content");
            strSql.Append(" where DetileID=@DetileID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@BusinessID", MySqlDbType.VarChar,50),
					new MySqlParameter("@DetileURL", MySqlDbType.VarChar,500),
					new MySqlParameter("@Content", MySqlDbType.VarChar,5000),
					new MySqlParameter("@DetileID", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.BusinessID;
            parameters[1].Value = model.DetileURL;
            parameters[2].Value = model.Content;
            parameters[3].Value = model.DetileID;

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
        public bool Delete(string DetileID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from detile ");
            strSql.Append(" where DetileID=@DetileID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@DetileID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = DetileID;

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
        public bool DeleteList(string DetileIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from detile ");
            strSql.Append(" where DetileID in (" + DetileIDlist + ")  ");
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
        public BusinessInfoViewer.Model.detile GetModel(string DetileID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DetileID,BusinessID,DetileURL,Content from detile ");
            strSql.Append(" where DetileID=@DetileID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@DetileID", MySqlDbType.VarChar,50)			};
            parameters[0].Value = DetileID;

            BusinessInfoViewer.Model.detile model = new BusinessInfoViewer.Model.detile();
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
        public BusinessInfoViewer.Model.detile DataRowToModel(DataRow row)
        {
            BusinessInfoViewer.Model.detile model = new BusinessInfoViewer.Model.detile();
            if (row != null)
            {
                if (row["DetileID"] != null)
                {
                    model.DetileID = row["DetileID"].ToString();
                }
                if (row["BusinessID"] != null)
                {
                    model.BusinessID = row["BusinessID"].ToString();
                }
                if (row["DetileURL"] != null)
                {
                    model.DetileURL = row["DetileURL"].ToString();
                }
                if (row["Content"] != null)
                {
                    model.Content = row["Content"].ToString();
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
            strSql.Append("select DetileID,BusinessID,DetileURL,Content ");
            strSql.Append(" FROM detile ");
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
            strSql.Append("select count(1) FROM detile ");
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
                strSql.Append("order by T.DetileID desc");
            }
            strSql.Append(")AS Row, T.*  from detile T ");
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
            parameters[0].Value = "detile";
            parameters[1].Value = "DetileID";
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

