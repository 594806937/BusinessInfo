using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessInfoSpider.Model
{
    /// <summary>
    /// 招标信息
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
        /// 项目金额
        /// </summary>
        public string Money { get; set; }

        /// <summary>
        /// 是否有数据
        /// </summary>
        public bool KeyFlag { get; set; }

        public string Source { get; set; }
    }
}
