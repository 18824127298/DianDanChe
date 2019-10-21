using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Web;
using System.Web.Configuration;

namespace Sigbit.Common
{
    /// <summary>
    /// 应用的路径
    /// </summary>
    public class AppPath
    {
        /// <summary>
        /// 应用的根路径(应mango 2013-12)
        /// </summary>
        public static string AppRootPath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\');
            }
        }

        /// <summary>
        /// 得到应用完整的全路径
        /// </summary>
        /// <param name="sDirName">目录名</param>
        /// <param name="sFileName">文件名</param>
        /// <returns>完整的路径</returns>
        public static string AppFullPath(string sDirName, string sFileName)
        {
            //======== 1. 目录的规整 =============
            string sAbsDirName = sDirName;
            if (!IsAbsolutePath(sAbsDirName))
            {
                sAbsDirName = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')
                        + "\\" + sAbsDirName;
            }

            //======== 2. 无虚拟目录的判断 ==========
            string sRet = sAbsDirName + "\\" + sFileName;
            if (File.Exists(sRet))
                return sRet;
            if (Directory.Exists(sRet))
                return sRet;

            //========== 3. 获得虚拟目录 ============
            string sSeriesName = GetPathSeriesName();
            if (sSeriesName == "")
                sRet = sAbsDirName + "\\" + sFileName;
            else
                sRet = sAbsDirName + "\\" + sSeriesName + "\\" + sFileName;

            return sRet;
        }

        /// <summary>
        /// 是否绝对路径
        /// </summary>
        /// <param name="sPathName">路径名</param>
        /// <returns>是否绝对路径</returns>
        private static bool IsAbsolutePath(string sPathName)
        {
            if (sPathName.IndexOf(":") >= 0)
                return true;

            if (sPathName[0] == '\\' || sPathName[0] == '/')
                return true;

            return false;
        }

        private static string _currentVirtualPath = "PCJNAINQMB";

        /// <summary>
        /// 得到当前的虚拟目录
        /// </summary>
        /// <returns>当前的虚拟目录</returns>
        public static string GetCurrentVirtualPath()
        {
            if (_currentVirtualPath != "PCJNAINQMB")
                return _currentVirtualPath;

            string sRet;
            if (HttpContext.Current == null)
                sRet = "";
            else
            {
                try
                {
                    sRet = HttpContext.Current.Request.ApplicationPath
                            .Replace("/", "").Replace("\\", "");
                }
                catch
                {
                    sRet = "";
                }
            }
            _currentVirtualPath = sRet;
            return sRet;
        }

        private static string _pathSeriesName = "";
        /// <summary>
        /// 设置用于区分配置项的组名
        /// </summary>
        /// <param name="sSeriesName">组名</param>
        public static void SetPathSeriesName(string sSeriesName)
        {
            _pathSeriesName = sSeriesName;
        }

        private static string GetPathSeriesName()
        {
            if (_pathSeriesName != "")
                return _pathSeriesName;

            return GetCurrentVirtualPath();
        }
    }
}
