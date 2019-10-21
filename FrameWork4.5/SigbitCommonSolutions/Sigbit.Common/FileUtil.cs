using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Collections;

namespace Sigbit.Common
{
    /// <summary>
    /// 封装文件相关的函数调用
    /// </summary>
    public class FileUtil
    {
        #region 文件读写
        /// <summary>
        /// 从文件读取字符串
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <returns>从文件中读取的字符串</returns>
        public static string ReadStringFromFile(string sFileName)
        {
            byte[] bsFileContent = ReadBytesFromFile(sFileName);
            string sRet = Encoding.Default.GetString(bsFileContent);

            return sRet;
        }

        /// <summary>
        /// 从文件读取字符数组
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <returns>从文件中读取的字符数组</returns>
        public static byte[] ReadBytesFromFile(string sFileName)
        {
            FileStream fs = File.OpenRead(sFileName);
            int nFileSize = (int)fs.Length;
            byte[] bsFileContent = new byte[nFileSize];
            fs.Read(bsFileContent, 0, nFileSize);
            fs.Close();

            return bsFileContent;
        }

        /// <summary>
        /// 从文件中读取字符串数组
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <returns>从文件中读取的字符串数组</returns>
        /// <remarks>本函数会跳过空行</remarks>
        public static string[] ReadStringListFromFile(string sFileName)
        {
            return ReadStringListFromFile(sFileName, false);
        }

        /// <summary>
        /// 从文件中读取字符串数组
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <param name="bIncludeEmptyLine">是否包括空行</param>
        /// <returns>从文件中读取的字符串数组</returns>
        public static string[] ReadStringListFromFile(string sFileName, bool bIncludeEmptyLine)
        {
            //============ 1. 打开文件 ==============
            StreamReader reader;
            reader = new StreamReader(
                         (System.IO.Stream)File.OpenRead(sFileName),
                         System.Text.Encoding.Default);

            //========== 2. 读取文件 =============
            ArrayList arrReadContent = new ArrayList();
            while (!reader.EndOfStream)
            {
                string sLine = reader.ReadLine();

                if (sLine == "")
                {
                    if (bIncludeEmptyLine)
                        arrReadContent.Add(sLine);
                }
                else
                    arrReadContent.Add(sLine);
            }
            reader.Close();

            //=========== 3. 准备返回值 ============
            string[] arrRet = new string[arrReadContent.Count];

            for (int i = 0; i < arrReadContent.Count; i++)
            {
                string sLine = (string)arrReadContent[i];
                arrRet[i] = sLine;
            }

            return arrRet;
        }


        /// <summary>
        /// 写入字符串到文件中。创建一个新的文件，并写入字符串
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <param name="sString">待写入的字符串</param>
        public static void WriteStringToFile(string sFileName, string sString)
        {
            byte[] bsFileContent = Encoding.Default.GetBytes(sString);
            WriteBytesToFile(sFileName, bsFileContent);
        }

        /// <summary>
        /// 写入字节数组到文件中。创建一个新的文件，并写入字节数组。
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <param name="bsContent">待写入的字节数组</param>
        public static void WriteBytesToFile(string sFileName, byte[] bsContent)
        {
            FileStream fs = File.Create(sFileName);
            fs.Write(bsContent, 0, bsContent.Length);
            fs.Close();
        }

        /// <summary>
        /// 写入字符串列表到文件中。创建一个新的文件，并写入字符串列表。
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <param name="slContent">待写入的字符串列表</param>
        public static void WriteStringListToFile(string sFileName, string[] slContent)
        {
            StreamWriter writer;
            writer = new StreamWriter(
                         (System.IO.Stream)File.Create(sFileName),
                         System.Text.Encoding.Default);

            for (int i = 0; i < slContent.Length; i++)
            {
                string sLine = slContent[i];
                writer.WriteLine(sLine);
            }

            writer.Close();
        }

        /// <summary>
        /// 写入字符串列表到文件中。创建一个新的文件，并写入字符串列表。
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <param name="slContent">待写入的字符串列表</param>
        public static void WriteStringListToFile(string sFileName, ArrayList slContent)
        {
            StreamWriter writer;
            writer = new StreamWriter(
                         (System.IO.Stream)File.Create(sFileName),
                         System.Text.Encoding.Default);

            for (int i = 0; i < slContent.Count; i++)
            {
                string sLine = (string)slContent[i];
                writer.WriteLine(sLine);
            }

            writer.Close();
        }
        #endregion 文件读写

        #region 扩展名
        /// <summary>
        /// 改变文件的扩展名
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <param name="sExtension">扩展名</param>
        /// <returns>改变过扩展名的文件名</returns>
        public static string ChangeFileExt(string sFileName, string sExtension)
        {
            char[] arrDelimiter = new char[] { '.', '\\', ':', '/' };
            int nPos = sFileName.LastIndexOfAny(arrDelimiter);
            if (nPos == -1 || sFileName[nPos] != '.')
                return sFileName + sExtension;
            else
                return sFileName.Substring(0, nPos) + sExtension;
        }

        /// <summary>
        /// 截掉一个文件的扩展名，返回文件的其余部分
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <returns>截掉扩展名的文件名</returns>
        /// <remarks> 如果文件名带路径，则带路径一起返回</remarks>
        public static string CutFileExt(string sFileName)
        {
            char[] arrDelimiter = new char[] { '.', '\\', ':', '/' };
            int nPos = sFileName.LastIndexOfAny(arrDelimiter);
            if (nPos == -1 || sFileName[nPos] != '.')
                return sFileName;
            else
                return sFileName.Substring(0, nPos);
        }

        /// <summary>
        /// 得到文件的扩展名，如无扩展名返回""，扩展名形式为".xxx"
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <returns>扩展名</returns>
        public static string ExtractFileExt(string sFileName)
        {
            char[] arrDelimiter = new char[] { '.', '\\', ':', '/' };
            int nPos = sFileName.LastIndexOfAny(arrDelimiter);
            if (nPos == -1 || sFileName[nPos] != '.')
                return "";
            else
                return sFileName.Substring(nPos);
        }
        #endregion 扩展名

        #region 文件及路径名称
        /// <summary>
        /// 是否绝对路径
        /// </summary>
        /// <param name="sPathName">路径名</param>
        /// <returns>是否绝对路径</returns>
        public static bool IsAbsolutePath(string sPathName)
        {
            if (sPathName.IndexOf(":") >= 0)
                return true;

            if (sPathName[0] == '\\' || sPathName[0] == '/')
                return true;

            return false;
        }

        /// <summary>
        /// 去掉字符串尾部的目录分隔符
        /// </summary>
        /// <param name="sSrc">字符串</param>
        /// <returns>去除分隔符以后的字符串</returns>
        public static string ExcludeTrailingSlash(string sSrc)
        {
            string sRet = sSrc;

            if (IsPathDelimiter(sRet, sRet.Length - 1))
                sRet = sRet.Substring(0, sRet.Length - 1);

            return sRet;
        }

        /// <summary>
        /// 一个字符串中的指定位置字符是否是目录分隔符
        /// </summary>
        /// <param name="sString">字符串</param>
        /// <param name="nIndex">指定位置</param>
        /// <returns>是否目录分隔符</returns>
        public static bool IsPathDelimiter(string sString, int nIndex)
        {
            return (nIndex >= 0) && (nIndex < sString.Length)
                    && (sString[nIndex] == '\\' || sString[nIndex] == '/');
        }

        /// <summary>
        /// 得到一个字符串中最后一个分隔符，分隔符定义在指定字符串中
        /// </summary>
        /// <param name="sDelimiters">分隔符串</param>
        /// <param name="sString">待查找的字符串</param>
        /// <returns>找到的分隔符位置，如果没有找到则返回0</returns>
        public static int LastDelimiter(string sDelimiters, string sString)
        {
            char[] arrDelimiters = new char[sDelimiters.Length];
            for (int i = 0; i < sDelimiters.Length; i++)
            {
                char ch = sDelimiters[i];
                arrDelimiters[i] = ch;
            }

            return sString.LastIndexOfAny(arrDelimiters);
        }

        /// <summary>
        /// 得到一个文件名的目录部分
        /// </summary>
        /// <param name="sFileName">文件名</param>
        /// <returns>目录字符串</returns>
        public static string ExtractFilePath(string sFileName)
        {
            int nIndex = LastDelimiter("\\:/", sFileName);

            return sFileName.Substring(0, nIndex + 1);
        }

        /// <summary>
        /// 得到路径文件名的文件部分
        /// </summary>
        /// <param name="sPathName">路径文件名</param>
        /// <returns>文件部分</returns>
        public static string ExtractFileName(string sPathName)
        {
            int nIndex = LastDelimiter("\\:/", sPathName);

            return sPathName.Substring(nIndex + 1);
        }
        #endregion 文件及路径名称

        #region 目录清理
        /// <summary>
        /// 删除指定目录指定时间前的所有文件
        /// </summary>
        /// <param name="sDirectoryName"></param>
        /// <param name="dtBeforeTime"></param>
        public static int RemoveFilesBeforeTime(string sDirectoryName, DateTime dtBeforeTime)
        {

            int nDeleCount = 0;

            if (!Directory.Exists(sDirectoryName))
                return nDeleCount;

            
            //============ 1. 列举目录中的所有文件 ===============
            string[] arrFiles = Directory.GetFiles(sDirectoryName);

            //========== 2. 处理每一个文件 ===========
            for (int i = 0; i < arrFiles.Length; i++)
            {
                string sFileName = arrFiles[i];
                DateTime dtLastWriteTime = File.GetLastWriteTime(sFileName);
                if (dtLastWriteTime < dtBeforeTime)
                {
                    File.Delete(sFileName);
                    nDeleCount++;
                }
            }

            return nDeleCount;
        }
        #endregion 目录清理
    }
}
