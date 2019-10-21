using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;
using System.IO;
using System.Web;

namespace Sigbit.Web.MediaServer
{
    public class MediaServerConfig : ConfigBase
    {
        private string _DEFAULT_MEDIA_PATH = "media";

        private static MediaServerConfig _instance = null;

        /// <summary>
        /// 唯一实例
        /// </summary>
        public static MediaServerConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MediaServerConfig();
                }
                return _instance;
            }
        }

        /// <summary>
        /// 构造函数，加载配置文件
        /// </summary>
        public MediaServerConfig()
        {
            string sConfigFileName;
            sConfigFileName = AppPath.AppFullPath("config", "sigbit.web.dll.config");

            LoadFromFile(sConfigFileName);
        }


        private string _rootPath = "";
        /// <summary>
        /// 物理根路径,示例D:\Project\
        /// </summary>
        public string RootPath
        {
            get
            {
                if (_rootPath == "")
                {
                    _rootPath = GetString("mediaServer", "rootPath");
                    if (_rootPath == "")
                    {
                        _rootPath = HttpContext.Current.Request.MapPath("~") + "\\" + _DEFAULT_MEDIA_PATH;
                    }
                    else
                    {
                        _rootPath = _rootPath.TrimEnd('\\');
                    }


                    if (!Directory.Exists(_rootPath))
                    {
                        Directory.CreateDirectory(_rootPath);
                    }
                }
                return _rootPath;
            }
            set
            {
                if (!Path.IsPathRooted(value))
                {
                    throw new Exception("设置的MediaServer根路径:" + value + "无效！");
                }
                _rootPath = value;
                //_rootPath = _rootPath.TrimEnd('\\') + '\\';
                //if (!Directory.Exists(_rootPath))
                //{
                //    Directory.CreateDirectory(_rootPath);
                //}

                _rootPath = _rootPath.TrimEnd('\\');
                Directory.CreateDirectory(_rootPath);

            }
        }

        /// <summary>
        /// MediaServer的Url根路径,示例http://*****/MediaServer/
        /// </summary>
        public string RootUrl
        {
            get
            {
                string sRootUrl = GetString("mediaServer", "rootUrl").ToLower();
                if (sRootUrl == "")
                {
                    string sRootPath = this.RootPath;
                    //sRootUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/";
                    //sRootUrl = sRootUrl + AppPath.GetCurrentVirtualPath() + "/" + _DEFAULT_MEDIA_PATH + "/";
                    sRootUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                    sRootUrl = sRootUrl + AppPath.GetCurrentVirtualPath() + "/" + _DEFAULT_MEDIA_PATH;
                }
                else
                {
                    //sRootUrl = sRootUrl.TrimEnd('/') + '/';
                    sRootUrl = sRootUrl.TrimEnd('/');
                }
                return sRootUrl;
            }
        }

    }
}
