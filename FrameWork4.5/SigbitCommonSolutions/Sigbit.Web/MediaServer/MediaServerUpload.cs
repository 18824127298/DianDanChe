using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Web;
using System.Drawing;

using Sigbit.Common;

namespace Sigbit.Web.MediaServer
{
    /// <summary>
    /// 媒体服务器的上传类
    /// </summary>
    public class MediaServerUpload
    {
        private MediaServerPath _savePath = new MediaServerPath();
        /// <summary>
        /// 保存路径，初始媒体服务器根目录
        /// </summary>
        public MediaServerPath SavePath
        {
            get
            {
                return _savePath;
            }
            set { _savePath = value; }
        }


        private int _fileSizeLimit = 0;
        /// <summary>
        /// 文件大小限制(单位K)
        /// </summary>
        public int FileSizeLimit
        {
            get { return _fileSizeLimit; }
            set { _fileSizeLimit = value; }
        }

        private bool _isImage = true;
        /// <summary>
        /// 是否为图片
        /// </summary>
        public bool IsImage
        {
            get { return _isImage; }
            set { _isImage = value; }
        }


        //private int _genThumbnailLimit = 5 * 1024;
        ///// <summary>
        ///// 生成缩略图的大小限制（单位K）
        ///// </summary>
        ///// <remarks>
        ///// 如果大于该值则生成缩略图
        ///// </remarks>
        //public int GenThumbnailLimit
        //{
        //    get { return _genThumbnailLimit; }
        //    set { _genThumbnailLimit = value; }
        //}


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public FileUploadResult DoUploadFile()
        {
            FileUploadResult result = new FileUploadResult();

            try
            {

                //========= 1.是否有上传文件检查 ============

                if (HttpContext.Current.Request.Files.Count <= 0)
                {
                    result.ResultMessage = "无上传文件，请检查文件再提交上传！";
                    return result;
                }


                HttpPostedFile curUploadFile = HttpContext.Current.Request.Files[0];

                result.OriginalName = curUploadFile.FileName;
                result.FileType = FileUtil.ExtractFileExt(result.OriginalName);
                result.FileSize = curUploadFile.ContentLength;


                //========= 2.文件大小限制检查 ==============
                if (this.FileSizeLimit > 0)
                {
                    int nUploadFileSize = result.FileSize / 1024;

                    if (nUploadFileSize > this.FileSizeLimit)
                    {
                        result.ResultMessage = "上传文件过大，超出上传文件大小限制！";
                        return result;
                    }
                }


                //========== 3.路径计算 ====================
                result.FileName = DateTime.Now.ToString("yyyyMMddHHmmss")
                      + RandUtil.NewString(3, RandStringType.Number) + result.FileType;

                result.ResultPath.RelativePath = this.SavePath.RelativePath + "\\" + result.FileName;

                //========== 4.文件保存 ====================

                Directory.CreateDirectory(_savePath.FullPath);

                curUploadFile.SaveAs(result.ResultPath.FullPath);

                result.IsSucc = true;

                result.ResultMessage = "文件上传成功";

            }
            catch (Exception ex)
            {
                result.IsSucc = false;

                result.ResultMessage = "上传出错,异常信息:" + ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        public FileUploadResult DoUploadImage()
        {
            FileUploadResult result = new FileUploadResult();

            try
            {

                //========= 1.是否有上传文件检查 ============

                if (HttpContext.Current.Request.Files.Count <= 0)
                {
                    result.ResultMessage = "无上传图片，请检查图片再提交上传！";
                    return result;
                }


                HttpPostedFile curUploadFile = HttpContext.Current.Request.Files[0];

                result.OriginalName = curUploadFile.FileName;
                result.FileType = FileUtil.ExtractFileExt(result.OriginalName);
                result.FileSize = curUploadFile.ContentLength;

                //========= 2.文件大小限制检查 ==============
                if (this.FileSizeLimit > 0)
                {

                    int nUploadFileSize = result.FileSize / 1024;

                    if (nUploadFileSize > this.FileSizeLimit)
                    {
                        result.ResultMessage = "上传图片过大，超出上传文件大小限制！";
                        return result;
                    }
                }



                //========== 3.路径计算 ====================
                result.FileName = DateTime.Now.ToString("yyyyMMddHHmmss")
                      + RandUtil.NewString(3, RandStringType.Number) + result.FileType;

                result.ResultPath.RelativePath = this.SavePath.RelativePath + "\\" + result.FileName;

                //========== 4.文件保存 ====================

                Directory.CreateDirectory(_savePath.FullPath);

                curUploadFile.SaveAs(result.ResultPath.FullPath);


                //========= 5.上传图片检查 =================
                if (this.IsImage)
                {

                    try
                    {
                        Image imgCurrent = Bitmap.FromFile(result.ResultPath.FullPath);
                    }
                    catch (OutOfMemoryException oome)
                    {
                        result.IsSucc = false;

                        result.ResultMessage = "无效的上传图片文件(" + oome.Message + ")";

                        return result;
                    }


                    ////========== 6.缩略图的处理　================
                    ////如果文件小于缩略图大小限制，则无需再生成缩略图
                    //if (result.FileSize > this.GenThumbnailLimit)
                    //{
                    //    try
                    //    {
                    //        result.ThumbnailPath.RelativePath = this.SavePath.RelativePath + "\\_" + result.FileName;

                    //        ImageThumbnail thumb = new ImageThumbnail();

                    //        bool bThumbResult = thumb.CutImageByWidth(result.ResultPath.FullPath, result.ThumbnailPath.FullPath, 100);

                    //        result.IsSucc = bThumbResult;
                    //        result.ResultMessage = thumb.ErrMSG;

                    //    }
                    //    catch (Exception thumbEx)
                    //    {
                    //        result.IsSucc = false;
                    //        result.ResultMessage = thumbEx.Message;
                    //    }
                    //}
                    //else
                    //{
                    //    result.ThumbnailPath = result.ResultPath;
                    //}

                }


                result.IsSucc = true;

                result.ResultMessage = "文件上传成功";

            }
            catch (Exception ex)
            {
                result.IsSucc = false;

                result.ResultMessage = "上传出错,异常信息:" + ex.Message;
            }

            return result;
        }


        //private MediaServerPath _resultPath = null;
        ///// <summary>
        ///// 上传的结果路径
        ///// </summary>
        //public MediaServerPath ResultPath
        //{
        //    get
        //    {
        //        return _resultPath;
        //    }
        //}


        //private string _resultMessage = "";
        ///// <summary>
        ///// 上传的结果信息
        ///// </summary>
        //public string ResultMessage
        //{
        //    get { return _resultMessage; }
        //}

    }
}
