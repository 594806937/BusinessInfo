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
        public XmlDocument doc = new XmlDocument();
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

        /// <summary>
        /// 根据节点名称和属性名称，获取属性值
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <param name="attr">属性名称</param>
        /// <returns></returns>
        public string GetAttrValue(string name, string attr)
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
                        if (nodeList[i].Attributes["attr"] != null)
                            result = nodeList[i].Attributes["attr"].Value;
                    }
                }
            }

            return result;
        }
    }
}
