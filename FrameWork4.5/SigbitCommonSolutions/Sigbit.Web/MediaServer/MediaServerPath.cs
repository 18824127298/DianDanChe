using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Sigbit.Web.MediaServer
{
    /// <summary>
    /// MediaServer的路径处理类
    /// </summary>
    /// <remarks>
    /// 此类四个属性中从任何一个属性设置，均可获得另外四个属性的值。采用Set构造器赋值。
    /// </remarks>
    public class MediaServerPath
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public MediaServerPath()
        {
            this.FullPath = MediaServerConfig.Instance.RootPath;
        }


        private string _fullUrl = "";
        /// <summary>
        /// 完整Url路径,示例:http://localhost/MediaServer/Site/ChannelName/****.jpg;
        /// </summary>
        public string FullUrl
        {
            get { return _fullUrl; }
            set
            {
                _fullUrl = value;

                //=====================1.路径校验=====================
                string sRootUrl = MediaServerConfig.Instance.RootUrl.ToLower();

                if (_fullUrl.Substring(0, sRootUrl.Length).ToLower() != sRootUrl)
                {
                    throw new Exception("无效完整Url:" + _fullUrl);
                }

                //=====================2.获取相对Url===================
                _relativeUrl = _fullUrl.Substring(sRootUrl.Length);

                //=====================3.获取相对路径==================
                _relativePath = _relativeUrl.Replace('/', '\\');

                //=====================4.获取完整路径==================
                _fullPath = MediaServerConfig.Instance.RootPath + _relativePath;


                //=====================5.格式化路径====================
                _formatFullUrl = MediaServerUtil.UrlFormat(_fullUrl);


            }
        }


        private string _fullPath = "";
        /// <summary>
        /// 完整物理路径,示例:D:\Sigbit\Project\MediaServer\BaseStation\UploadFile\***.jpg;
        /// </summary>
        public string FullPath
        {
            get
            {
                string sRetPath = TrimPath(_fullPath);
                return sRetPath;
            }
            set
            {
                //=====================1.路径校验=====================
                _fullPath = value.Replace('/', '\\');

                string sRootPath = MediaServerConfig.Instance.RootPath.ToLower();
                if (_fullPath.Substring(0, sRootPath.Length).ToLower() != sRootPath)
                {
                    throw new Exception("无效完整物理路径:" + _fullPath);
                }

                //=====================2.获取相对物理路径==================
                _relativePath = _fullPath.Substring(sRootPath.Length);

                //=====================3.获取相对Url===================
                _relativeUrl = _relativePath.Replace('\\', '/');

                //=====================4.获取完整Url===================
                _fullUrl = MediaServerConfig.Instance.RootUrl + _relativeUrl;

                //=====================5.格式化路径====================
                _formatFullUrl = MediaServerUtil.UrlFormat(_fullUrl);

            }
        }

        private string _relativeUrl = "";
        /// <summary>
        /// 相对Url路径,代表MediaServer下的子路径,示例:/SiteName/ChannelName/****.jpg;
        /// </summary>
        public string RelativeUrl
        {
            get { return _relativeUrl; }
            set
            {
                //=====================1.路径校验=====================
                _relativeUrl = value;
                //if (_relativeUrl.Contains("\\"))
                //{
                //    throw new Exception("无效相对Url路径:" + _relativeUrl + "\r\n正确路径格式:***/***/***");
                //}

                _relativeUrl = _relativeUrl.Replace('\\', '/');

                if (_relativeUrl.Contains(":"))
                {
                    throw new Exception("无效相对Url路径:" + _relativeUrl);
                }

                _relativeUrl = "/" + _relativeUrl.TrimStart('/');

                //=====================2.获取相对路径=================
                _relativePath =  _relativeUrl.Replace('/', '\\');

                //=====================3.获取完整Url==================
                _fullUrl = MediaServerConfig.Instance.RootUrl + _relativeUrl;

                //=====================4.获取完整路径=================
                _fullPath = MediaServerConfig.Instance.RootPath + _relativePath;

                //=====================5.完整格式化路径===============
                _formatFullUrl = MediaServerUtil.UrlFormat(_fullUrl);
            }
        }


        private string _relativePath = "";
        /// <summary>
        /// 相对物理路径,代表MediaServer下的卫路径,示例:\BaseStation\UploadFile\***.jpg;
        /// </summary>
        public string RelativePath
        {
            get
            {
                string sRetPath = TrimPath(_relativePath);
                return sRetPath;
            }
            set
            {
                //=====================1.路径校验=====================
                _relativePath = value.Replace('/', '\\');
                if (_relativePath.Contains("/"))
                {
                    throw new Exception("无效相对物理路径:" + _relativePath + "\r\n正确路径格式:***\\***");
                }

                if (_relativePath.Contains(":"))
                {
                    throw new Exception("无效相对物理路径:" + _relativePath);
                }

                _relativePath = "\\" + _relativePath.TrimStart('\\');

                //=====================2.获取相对Url=================
                _relativeUrl = _relativePath.Replace('\\', '/');

                //=====================3.获取完整Url==================
                _fullUrl = MediaServerConfig.Instance.RootUrl + _relativeUrl;

                //=====================4.获取完整路径=================
                _fullPath = MediaServerConfig.Instance.RootPath + _relativePath;

                //=====================5.完整格式化路径===============
                _formatFullUrl = MediaServerUtil.UrlFormat(_fullUrl);
            }
        }


        private string _formatFullUrl = "";
        /// <summary>
        /// 格式化后的URL路径
        /// </summary>
        public string FormatFullUrl
        {
            get
            {
                return _formatFullUrl;
            }
            set
            {
                try
                {
                    this.FullUrl = MediaServerUtil.UrlUnFormat(value);
                }
                catch (Exception)
                {
                    throw new Exception("无效的完整格式化路径:" + value);
                }
            }
        }


        /// <summary>
        /// 整理物理路径
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns></returns>
        private string TrimPath(string sPath)
        {
            if (sPath.Contains("?"))
            {
                sPath = sPath.Substring(0, sPath.IndexOf("?"));
            }


            return sPath;
        }


        public override string ToString()
        {
            string sRet = "";

            sRet += "完整URL:" + this.FullUrl + "\r\n";
            sRet += "相对URL:" + this.RelativeUrl + "\r\n";
            sRet += "完整路径:" + this.FullPath + "\r\n";
            sRet += "相对路径:" + this.RelativePath + "\r\n";
            sRet += "格式化URL:" + this.FormatFullUrl + "\r\n";

            return sRet;
        }


    }
}
