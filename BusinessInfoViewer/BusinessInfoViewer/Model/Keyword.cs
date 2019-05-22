using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessInfoViewer.Model
{
    /// <summary>
    /// 关键字模型
    /// </summary>
    public class Keyword
    {
        /// <summary>
        /// 关键字名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 关键字权重
        /// </summary>
        public int Weight { get; set; }
    }
}
