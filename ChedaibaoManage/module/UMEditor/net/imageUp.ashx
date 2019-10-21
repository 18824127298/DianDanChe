<%@ WebHandler Language="C#" Class="imageUp" %>
<%@ Assembly Src="Uploader.cs" %>

using System;
using System.Web;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using Sigbit.Common;
using Sigbit.Web.MediaServer;

public class imageUp : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {

        //DebugLogger.LogDebugMessage("AAAAA");

        context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //上传配置
        string pathbase = "upload/";                                                          //保存路径

        int size = 10;                     //文件大小限制,单位mb                            //文件大小限制，单位KB
        string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };                    //文件允许格式

        string callback = context.Request["callback"];
        string editorId = context.Request["editorid"];

        //上传图片
        Hashtable infoList = new Hashtable();

        //Uploader up = new Uploader();
        //info = up.upFile(context, pathbase, filetype, size); //获取上传状态


        MediaServerUpload msUpload = new MediaServerUpload();
        msUpload.SavePath.RelativePath = "site/umeditor/" + DateTime.Now.ToString("yyyyMMdd");
        msUpload.FileSizeLimit = 2048;



        FileUploadResult result = msUpload.DoUploadImage();


        if (result.IsSucc)
        {
            infoList.Add("state", "SUCCESS");
            infoList.Add("url", result.ResultPath.FullUrl);
            infoList.Add("originalName", result.OriginalName);
            infoList.Add("name", result.FileName);
            infoList.Add("size", result.FileSize);
            infoList.Add("type", result.FileType);
        }
        else
        {
            infoList.Add("state", result.ResultMessage);
            infoList.Add("url", "");
            infoList.Add("originalName", result.OriginalName);
            infoList.Add("name", result.FileName);
            infoList.Add("size", result.FileSize);
            infoList.Add("type", result.FileType);
        }

        string json = BuildJson(infoList);

        context.Response.ContentType = "text/html";
        if (callback != null)
        {
            context.Response.Write(String.Format("<script>{0}(JSON.parse(\"{1}\"));</script>", callback, json));
        }
        else
        {
            context.Response.Write(json);
        }

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private string BuildJson(Hashtable info)
    {
        List<string> fields = new List<string>();
        string[] keys = new string[] { "originalName", "name", "url", "size", "state", "type" };
        for (int i = 0; i < keys.Length; i++)
        {
            fields.Add(String.Format("\"{0}\": \"{1}\"", keys[i], info[keys[i]]));
        }
        //return "{" + String.Join(",", fields) + "}";

        string sRet = "{";

        for (int i = 0; i < fields.Count; i++)
        {
            sRet += fields[i] + ",";
        }

        sRet = sRet.TrimEnd(',');

        sRet += "}";

        return sRet;

    }

}