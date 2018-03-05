using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessInfoViewer.Model
{
    /// <summary>
    /// 商业信息类
    /// </summary>
    public class BusinessInfo
    {
        private string guid = Guid.NewGuid().ToString();
        /// <summary>
        /// 默认有GUID值，无需初始化赋值
        /// </summary>
        public string GUID
        {
            get { return this.guid; }
            set { this.guid = value; }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 发布单位
        /// </summary>
        public string ComName { get; set; }
        /// <summary>
        /// 单位地址
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime ReleaseTime { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetileURL { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 来源网站
        /// </summary>
        public string Source { get; set; }
    }
    /// <summary>
    /// 拓展了匹配度的BusinessInfo，用于查询结果返回
    /// </summary>
    public class BusinessInfoEx : BusinessInfo
    {
        /// <summary>
        /// 匹配率
        /// </summary>
        public double Degree { get; set; }
    }
}
