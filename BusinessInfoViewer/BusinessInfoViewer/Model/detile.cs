using System;

namespace BusinessInfoViewer.Model
{
	/// <summary>
	/// detile:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class detile
	{
		public detile()
		{}
		#region Model
		private string _detileid;
		private string _businessid;
		private string _detileurl;
		private string _content;
		/// <summary>
		/// 
		/// </summary>
		public string DetileID
		{
			set{ _detileid=value;}
			get{return _detileid;}
		}
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
		public string DetileURL
		{
			set{ _detileurl=value;}
			get{return _detileurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		#endregion Model

	}
}

