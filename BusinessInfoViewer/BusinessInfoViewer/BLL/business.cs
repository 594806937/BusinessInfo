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
	/// business
	/// </summary>
	public partial class business
	{
		private readonly IBusiness dal=DataAccess.Createbusiness();
		public business()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string BusinessID)
		{
			return dal.Exists(BusinessID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Model.business model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.business model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string BusinessID)
		{
			
			return dal.Delete(BusinessID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string BusinessIDlist )
		{
			return dal.DeleteList(PageValidate.SafeLongFilter(BusinessIDlist,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.business GetModel(string BusinessID)
		{
			
			return dal.GetModel(BusinessID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Model.business GetModelByCache(string BusinessID)
		{
			
			string CacheKey = "businessModel-" + BusinessID;
			object objModel = DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(BusinessID);
					if (objModel != null)
					{
						int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
						DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.business)objModel;
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
		public List<Model.business> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.business> DataTableToList(DataTable dt)
		{
			List<Model.business> modelList = new List<Model.business>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.business model;
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

