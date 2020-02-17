<%@ WebHandler Language="C#" Class="GetBusinessData" %>

using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Xml;
using BusinessInfoViewer.Common;
using BusinessInfoViewer.Model;
using Jayrock.Json;
using NPOI.Util;

public class GetBusinessData : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
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

        List<BusinessInfoViewer.Model.BusinessInfoEx> list =
            bll.SearchKeyWordEx(wordlist);
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