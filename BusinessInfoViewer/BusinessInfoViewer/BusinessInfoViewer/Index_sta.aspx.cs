using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using BusinessInfoViewer.Common;
using BusinessInfoViewer.Model;

public partial class Index_sta : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Export(object sender, EventArgs e)
    {
        BusinessInfoViewer.BLL.BusinessInfo bll = new BusinessInfoViewer.BLL.BusinessInfo();
        List<Keyword> wordlist = new List<Keyword>();
        if (HttpRuntime.Cache["Keywordlist"] == null)
        {
            string xmlpath = HttpRuntime.AppDomainAppPath + "/config/Keyword.xml";
            XMLHelper helper = new XMLHelper(xmlpath);
            XmlNodeList nodelist = helper.doc.GetElementsByTagName("keyword");
            for (int i = 0; i < nodelist.Count; i++)
            {
                Keyword word = new Keyword();
                word.Name = nodelist[i].Attributes["name"].Value;
                word.Weight = Convert.ToInt32(nodelist[i].Attributes["weight"].Value);
                wordlist.Add(word);
            }
            Cache cache = System.Web.HttpRuntime.Cache;
            cache.Insert("Keywordlist", wordlist);
        }
        else
        {
            wordlist = (List<Keyword>)HttpRuntime.Cache["Keywordlist"];
        }
        List<BusinessInfoViewer.Model.BusinessInfoEx> list = bll.SearchKeyWordEx(wordlist);
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[]
        {
            new DataColumn("序号"),
            new DataColumn("相关度"),  
            new DataColumn("项目金额"), 
            new DataColumn("项目名称"),
            new DataColumn("项目来源"), 
            new DataColumn("单位名称"),
            new DataColumn("日期"),
            new DataColumn("详细地址")
        });
        for (int i = 0; i < list.Count; i++)
        {
            DataRow row = dt.NewRow();
            row["序号"] = i;
            row["相关度"] = list[i].Degree.ToString("p");
            row["项目金额"] = list[i].Money;
            row["项目名称"] = list[i].Title;
            row["项目来源"] = list[i].Source;
            row["单位名称"] = list[i].ComName;
            row["日期"] = list[i].ReleaseTime;
            row["详细地址"] = list[i].DetileURL;
            dt.Rows.Add(row);
        }
        ExcelHelper.ExportByWeb(dt, "商机抓取表", string.Format("商机导出{0}.xls", DateTime.Now));
    }
}