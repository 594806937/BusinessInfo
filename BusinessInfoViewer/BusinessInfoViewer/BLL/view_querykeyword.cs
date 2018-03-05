using System;
using System.Collections.Generic;
using System.Data;
using BusinessInfoViewer.Common;
using BusinessInfoViewer.DALFactory;
using BusinessInfoViewer.IDAL;
using DataCache = BusinessInfoViewer.Common.DataCache;

namespace BusinessInfoViewer.BLL
{
	/// <summary>
	/// view_querykeyword
	/// </summary>
	public partial class view_querykeyword
	{
		private readonly Iview_querykeyword dal=DataAccess.Createview_querykeyword();
		public view_querykeyword()
		{}
		#region  BasicMethod
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.view_querykeyword GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			return dal.GetModel();
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Model.view_querykeyword GetModelByCache()
		{
			//该表无主键信息，请自定义主键/条件字段
			string CacheKey = "view_querykeywordModel-" ;
			object objModel = DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel();
					if (objModel != null)
					{
						int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
						DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.view_querykeyword)objModel;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.view_querykeyword> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.view_querykeyword> DataTableToList(DataTable dt)
		{
			List<Model.view_querykeyword> modelList = new List<Model.view_querykeyword>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.view_querykeyword model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
				}
			}
			return modelList;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

