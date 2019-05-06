using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessInfoViewer.Common;

public partial class Index_sta : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Export(object sender, EventArgs e)
    {
        BusinessInfoViewer.BLL.BusinessInfo bll = new BusinessInfoViewer.BLL.BusinessInfo();
        string[] defaultkey = ConfigHelper.GetConfigString("KeyWords") == null ? null : ConfigHelper.GetConfigString("KeyWords").Split(';');
        List<string> keywordList = new List<string>();
        if (defaultkey == null)
            defaultkey = new[] { "地理", "信息系统", "GIS", "软件", "国土调查", "测绘", "信息化" };
        for (int i = 0; i < defaultkey.Length; i++)
        {
            keywordList.Add(defaultkey[i]);
        }
        List<BusinessInfoViewer.Model.BusinessInfoEx> list = bll.SearchKeyWordEx(keywordList);
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
            row["相关度"] = list[i].Degree;
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