using System.Data;
using BusinessInfoViewer.Model;

namespace BusinessInfoViewer.IDAL
{
	/// <summary>
	/// 接口层detile
	/// </summary>
	public interface Idetile
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string DetileID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(detile model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(detile model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string DetileID);
		bool DeleteList(string DetileIDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		detile GetModel(string DetileID);
		detile DataRowToModel(DataRow row);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
		#endregion  成员方法
		#region  MethodEx

		#endregion  MethodEx
	} 
}
