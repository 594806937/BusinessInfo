using System;

namespace BusinessInfoViewer.Model
{
	/// <summary>
	/// view_querykeyword:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class view_querykeyword
	{
		public view_querykeyword()
		{}
		#region Model
		private string _businessid;
		private string _businesstitle;
		private string _content;
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
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		#endregion Model

	}
}

