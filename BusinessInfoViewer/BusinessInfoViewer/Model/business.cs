using System;

namespace BusinessInfoViewer.Model
{
	/// <summary>
	/// business:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class business
	{
		public business()
		{}
		#region Model
		private string _businessid;
		private string _businesstitle;
		private string _releasecom;
		private string _releaselocation;
		private DateTime? _releasetime;
		private string _detileurl;
		private string _source;
		/// <summary>
		/// 
		/// </summary>
		public string BusinessID
		{
			set{ _businessid=value;}
			get{return _businessid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BusinessTitle
		{
			set{ _businesstitle=value;}
			get{return _businesstitle;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseCom
		{
			set{ _releasecom=value;}
			get{return _releasecom;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ReleaseLocation
		{
			set{ _releaselocation=value;}
			get{return _releaselocation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ReleaseTime
		{
			set{ _releasetime=value;}
			get{return _releasetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DetileURL
		{
			set{ _detileurl=value;}
			get{return _detileurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Source
		{
			set{ _source=value;}
			get{return _source;}
		}
		#endregion Model

	}
}

