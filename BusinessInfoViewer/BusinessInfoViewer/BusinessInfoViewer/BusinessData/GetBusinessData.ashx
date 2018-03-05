<%@ WebHandler Language="C#" Class="GetBusinessData" %>

using System.Collections.Generic;
using System.Web;
using Jayrock.Json;

public class GetBusinessData : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        BusinessInfoViewer.BLL.BusinessInfo bll = new BusinessInfoViewer.BLL.BusinessInfo();
        List<BusinessInfoViewer.Model.BusinessInfoEx> list =
            bll.SearchKeyWordEx(new List<string>() { "地理", "信息系统", "GIS", "软件", "地灾", "测绘", "国土", "信息化" });
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
            writer.WriteMember("date");
            writer.WriteString(list[i].ReleaseTime.ToShortDateString());
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