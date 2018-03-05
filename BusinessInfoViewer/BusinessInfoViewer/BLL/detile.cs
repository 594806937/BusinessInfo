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
	/// detile
	/// </summary>
	public partial class detile
	{
		private readonly Idetile dal=DataAccess.Createdetile();
		public detile()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string DetileID)
		{
			return dal.Exists(DetileID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Model.detile model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.detile model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string DetileID)
		{
			
			return dal.Delete(DetileID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string DetileIDlist )
		{
			return dal.DeleteList(PageValidate.SafeLongFilter(DetileIDlist,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.detile GetModel(string DetileID)
		{
			
			return dal.GetModel(DetileID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Model.detile GetModelByCache(string DetileID)
		{
			
			string CacheKey = "detileModel-" + DetileID;
			object objModel = DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(DetileID);
					if (objModel != null)
					{
						int ModelCache = ConfigHelper.GetConfigInt("ModelCache");
						DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.detile)objModel;
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
		public List<Model.detile> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Model.detile> DataTableToList(DataTable dt)
		{
			List<Model.detile> modelList = new List<Model.detile>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Model.detile model;
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

