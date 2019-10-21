<%@ WebHandler Language="C#" Class="NewAjaxuploadHandler" %>

using System;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;

using Sigbit.Common;
using Sigbit.Web.MediaServer;

//using Aliyun.OSS;
//using Aliyun.OSS.Util;
public class NewAjaxuploadHandler : IHttpHandler
{
    private string _filedir = "";    //文件目录  
    /// <summary>  
    /// 处理上传文件
    /// </summary>  
    /// <param name="context"></param>  
    public void ProcessRequest(HttpContext context)
    {
        //HttpPostedFile curUploadFile = HttpContext.Current.Request.Files[0];
        //string sSavePath = ConvertUtil.ToString(context.Request["save_path"], "");
        //Stream fileStream = curUploadFile.InputStream;

        //Random r = new Random();
        //string uploadFileName = curUploadFile.FileName;
        //string md5 = OssUtils.ComputeContentMd5(fileStream, curUploadFile.ContentLength);
        //string today = DateTime.Now.ToString("yyyyMMdd");
        //string FileName = today + r.Next(1, 9999) + Path.GetExtension(uploadFileName);
        //string FilePath = sSavePath + "/" + today + "/" + FileName;
        //string sResponse = "";
        //try
        //{
        //    //初始化阿里云配置--外网Endpoint、访问ID、访问password
        //    OssClient aliyun = new OssClient("https://oss-cn-shenzhen.aliyuncs.com", "fEzp5OS35HQKhkca", "f24UJOS0xrVbmUpcpFeXo8Fy53mhOg");

        //    //将文件md5值赋值给meat头信息，服务器验证文件MD5
        //    var objectMeta = new ObjectMetadata
        //    {
        //        ContentMd5 = md5,
        //    };
        //    //文件上传--空间名、文件保存路径、文件流、meta头信息(文件md5) //返回meta头信息(文件md5)
        //    PutObjectResult result = aliyun.PutObject("phonelee", FilePath, fileStream, objectMeta);

        //    //返回给UEditor的插入编辑器的图片的src
        //    sResponse = "succ|" + "http://phonelee.oss-cn-shenzhen.aliyuncs.com/" + FilePath;
        //}
        //catch (Exception e)
        //{
        //    context.Response.Write(e.Message);
        //}
        //finally
        //{
        //    context.Response.Write(sResponse);
        //}




        int nFileSizeLimit = ConvertUtil.ToInt(context.Request["file_size_limit"], 0);

        string sSavePath = ConvertUtil.ToString(context.Request["save_path"], "");

        bool bIsImage = ConvertUtil.ToBool(context.Request["IsImage"]);
        string sResponse = "";

        try
        {

            MediaServerUpload msUpload = new MediaServerUpload();
            msUpload.SavePath.RelativePath = sSavePath;
            msUpload.FileSizeLimit = nFileSizeLimit;
            msUpload.IsImage = bIsImage;
            FileUploadResult result = msUpload.DoUploadImage();

            if (result.IsSucc)
            {
                sResponse = "succ|" + result.ResultPath.FullUrl + "|" + result.ThumbnailPath.FullUrl + "|" + result.FileName;
            }
            else
            {
                sResponse = "fail|" + result.ResultMessage;
            }
        }
        catch (Exception ex)
        {
            DebugLogger.LogDebugMessage(ex.Message + ex.StackTrace);
            sResponse = "fail|" + ex.Message;
        }

        context.Response.Write(sResponse);

    }

    public bool IsReusable
    {
        get { throw new NotImplementedException(); }
    }


}