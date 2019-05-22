<%@ WebHandler Language="C#" Class="GetBusinessData" %>

using System.Collections.Generic;
using System.Web;
using BusinessInfoViewer.Common;
using Jayrock.Json;

public class GetBusinessData : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        BusinessInfoViewer.BLL.BusinessInfo bll = new BusinessInfoViewer.BLL.BusinessInfo();
        string[] defaultkey = ConfigHelper.GetConfigString("KeyWords") == null ? null : ConfigHelper.GetConfigString("KeyWords").Split(';');
        List<string> keywordList = new List<string>();
        if (defaultkey == null)
            defaultkey = new[] { "地理", "信息系统", "GIS", "软件", "国土调查", "测绘", "信息化" };
        for (int i = 0; i < defaultkey.Length; i++)
        {
            keywordList.Add(defaultkey[i]);
        }
        List<BusinessInfoViewer.Model.BusinessInfoEx> list =
            bll.SearchKeyWordEx(keywordList);
        JsonTextWriter writer = new JsonTextWriter();
        writer.WriteStartObject();
        writer.WriteMember("data");
        writer.WriteStartArray();
        for (int i = 0; i < list.Count; i++)
        {
            writer.WriteStartObject();
            writer.WriteMember("title");
            writer.WriteString(list[i].Title);
            writer.WriteMember("degree");
            writer.WriteString((list[i].Degree.ToString("p")));
            writer.WriteMember("com");
            writer.WriteString(list[i].ComName);
            writer.WriteMember("location");
            writer.WriteString(list[i].Location);
            writer.WriteMember("date");
            writer.WriteString(list[i].ReleaseTime.ToShortDateString());
            writer.WriteMember("money");
            writer.WriteString(list[i].Money);
            writer.WriteMember("source");
            writer.WriteString(list[i].Source);
            writer.WriteMember("url");
            writer.WriteString(list[i].DetileURL);
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
        writer.WriteEndObject();
        string msg = writer.ToString();
        context.Response.Write(msg);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}