using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

using Sigbit.Common;

namespace Sigbit.Net.CsvPacket.BatchIO
{
    public class CSVImportPathUtil
    {
        private static DateTime _lastRemoveFileTime = DateTime.Now.AddDays(-1);

        public static string TempFilePathNameOfPrefix(string sFileNamePrefix)
        {
            //=========== 1. 文件名 ===========
            string sFileName = sFileNamePrefix + "_" + DateTimeUtil.ToDateTime14Str(DateTimeUtil.Now)
                    + "_" + RandUtil.NewString(4, RandStringType.Lower) + ".csv";

            //============= 1.1 在data\temp\目录下 ============
            string sDirPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\data\\temp";
            Directory.CreateDirectory(sDirPath);

            //=========== 2. 删掉两个小时前的文件 ===========
            //========== 2.1 每15分钟清一次数据 ====================
            if ((DateTime.Now - _lastRemoveFileTime).TotalMinutes > 15)
            {
                _lastRemoveFileTime = DateTime.Now;

                //======== 2.2 每次清两个小时前的数据 ===========
                int nRemoveFileCount = FileUtil.RemoveFilesBeforeTime(sDirPath, DateTime.Now.AddHours(-2));

                if (nRemoveFileCount != 0)
                {
                    DebugLogger.LogDebugMessage("删除文件夹" + sDirPath + "中的" + nRemoveFileCount.ToString() + "个文件",
                            "CSV_IMPORT_PATH_UTIL");
                }
            }

            //=========== 3. 全路径 ===========
            string sRet = sDirPath + "\\" + sFileName;

            return sRet;
        }
    }
}
