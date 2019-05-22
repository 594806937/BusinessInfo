using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace BusinessInfoViewer.Common
{
    /// <summary>
    /// XML操作类库
    /// </summary>
    public class XMLHelper
    {
        private XmlDocument doc = new XmlDocument();
        /// <summary>
        /// 构造函数，每个文件一个Helper
        /// </summary>
        /// <param name="xmlpath">XML文件绝对路径</param>
        public XMLHelper(string xmlpath)
        {
            if (!string.IsNullOrEmpty(xmlpath))
            {
                if (File.Exists(xmlpath))
                {
                    try
                    {
                        doc.Load(xmlpath);
                    }
                    catch (XmlException exception)
                    {
                        GlobeParam.Log.Info(string.Format("读取配置文件{0}失败，错误信息{1}", xmlpath, exception.Message));
                    }
                }
            }

        }
        /// <summary>
        /// 根据Name返回相应的配置信息
        /// </summary>
        /// <param name="name">配置节点名</param>
        /// <returns>配置节点内的信息</returns>
        public string GetValueByName(string name)
        {
            string result = "";
            if (doc == null)
                return result;
            XmlNode node = doc.SelectSingleNode("root");
            if (node != null)
            {
                XmlNodeList nodeList = node.ChildNodes;
                for (int i = 0; i < nodeList.Count; i++)
                {
                    if (nodeList[i].Name == name)
                    {
                        result = nodeList[i].InnerText;
                    }
                }
            }
            return result;
        }
    }
}
