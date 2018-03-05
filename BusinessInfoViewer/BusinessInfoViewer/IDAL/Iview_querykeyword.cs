using System.Data;
using BusinessInfoViewer.Model;

namespace BusinessInfoViewer.IDAL
{
    /// <summary>
    /// 接口层view_querykeyword
    /// </summary>
    public interface Iview_querykeyword
    {
        #region  成员方法
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        view_querykeyword GetModel();
        view_querykeyword DataRowToModel(DataRow row);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        #endregion  成员方法
        #region  MethodEx

        #endregion  MethodEx
    }
}
