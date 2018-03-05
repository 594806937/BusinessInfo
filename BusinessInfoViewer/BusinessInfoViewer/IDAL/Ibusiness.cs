using System.Data;
using BusinessInfoViewer.Model;

namespace BusinessInfoViewer.IDAL
{
	/// <summary>
	/// 接口层business
	/// </summary>
	public interface IBusiness
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string BusinessID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(business model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(business model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string BusinessID);
		bool DeleteList(string BusinessIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		business GetModel(string BusinessID);
		business DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
        ///// <summary>
        ///// 根据分页获得数据列表
        ///// </summary>
        //DataSet GetListByPage(int PageSize, int PageIndex, string strWhere);
		#endregion  成员方法
		#region  MethodEx

		#endregion  MethodEx
	} 
}
