using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Collections;

namespace Sigbit.Common
{
    /// <summary>
    /// ��װ�ļ���صĺ�������
    /// </summary>
    public class FileUtil
    {
        #region �ļ���д
        /// <summary>
        /// ���ļ���ȡ�ַ���
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <returns>���ļ��ж�ȡ���ַ���</returns>
        public static string ReadStringFromFile(string sFileName)
        {
            byte[] bsFileContent = ReadBytesFromFile(sFileName);
            string sRet = Encoding.Default.GetString(bsFileContent);

            return sRet;
        }

        /// <summary>
        /// ���ļ���ȡ�ַ�����
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <returns>���ļ��ж�ȡ���ַ�����</returns>
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
        /// ���ļ��ж�ȡ�ַ�������
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <returns>���ļ��ж�ȡ���ַ�������</returns>
        /// <remarks>����������������</remarks>
        public static string[] ReadStringListFromFile(string sFileName)
        {
            return ReadStringListFromFile(sFileName, false);
        }

        /// <summary>
        /// ���ļ��ж�ȡ�ַ�������
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <param name="bIncludeEmptyLine">�Ƿ��������</param>
        /// <returns>���ļ��ж�ȡ���ַ�������</returns>
        public static string[] ReadStringListFromFile(string sFileName, bool bIncludeEmptyLine)
        {
            //============ 1. ���ļ� ==============
            StreamReader reader;
            reader = new StreamReader(
                         (System.IO.Stream)File.OpenRead(sFileName),
                         System.Text.Encoding.Default);

            //========== 2. ��ȡ�ļ� =============
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

            //=========== 3. ׼������ֵ ============
            string[] arrRet = new string[arrReadContent.Count];

            for (int i = 0; i < arrReadContent.Count; i++)
            {
                string sLine = (string)arrReadContent[i];
                arrRet[i] = sLine;
            }

            return arrRet;
        }


        /// <summary>
        /// д���ַ������ļ��С�����һ���µ��ļ�����д���ַ���
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <param name="sString">��д����ַ���</param>
        public static void WriteStringToFile(string sFileName, string sString)
        {
            byte[] bsFileContent = Encoding.Default.GetBytes(sString);
            WriteBytesToFile(sFileName, bsFileContent);
        }

        /// <summary>
        /// д���ֽ����鵽�ļ��С�����һ���µ��ļ�����д���ֽ����顣
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <param name="bsContent">��д����ֽ�����</param>
        public static void WriteBytesToFile(string sFileName, byte[] bsContent)
        {
            FileStream fs = File.Create(sFileName);
            fs.Write(bsContent, 0, bsContent.Length);
            fs.Close();
        }

        /// <summary>
        /// д���ַ����б��ļ��С�����һ���µ��ļ�����д���ַ����б�
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <param name="slContent">��д����ַ����б�</param>
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
        /// д���ַ����б��ļ��С�����һ���µ��ļ�����д���ַ����б�
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <param name="slContent">��д����ַ����б�</param>
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
        #endregion �ļ���д

        #region ��չ��
        /// <summary>
        /// �ı��ļ�����չ��
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <param name="sExtension">��չ��</param>
        /// <returns>�ı����չ�����ļ���</returns>
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
        /// �ص�һ���ļ�����չ���������ļ������ಿ��
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <returns>�ص���չ�����ļ���</returns>
        /// <remarks> ����ļ�����·�������·��һ�𷵻�</remarks>
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
        /// �õ��ļ�����չ����������չ������""����չ����ʽΪ".xxx"
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <returns>��չ��</returns>
        public static string ExtractFileExt(string sFileName)
        {
            char[] arrDelimiter = new char[] { '.', '\\', ':', '/' };
            int nPos = sFileName.LastIndexOfAny(arrDelimiter);
            if (nPos == -1 || sFileName[nPos] != '.')
                return "";
            else
                return sFileName.Substring(nPos);
        }
        #endregion ��չ��

        #region �ļ���·������
        /// <summary>
        /// �Ƿ����·��
        /// </summary>
        /// <param name="sPathName">·����</param>
        /// <returns>�Ƿ����·��</returns>
        public static bool IsAbsolutePath(string sPathName)
        {
            if (sPathName.IndexOf(":") >= 0)
                return true;

            if (sPathName[0] == '\\' || sPathName[0] == '/')
                return true;

            return false;
        }

        /// <summary>
        /// ȥ���ַ���β����Ŀ¼�ָ���
        /// </summary>
        /// <param name="sSrc">�ַ���</param>
        /// <returns>ȥ���ָ����Ժ���ַ���</returns>
        public static string ExcludeTrailingSlash(string sSrc)
        {
            string sRet = sSrc;

            if (IsPathDelimiter(sRet, sRet.Length - 1))
                sRet = sRet.Substring(0, sRet.Length - 1);

            return sRet;
        }

        /// <summary>
        /// һ���ַ����е�ָ��λ���ַ��Ƿ���Ŀ¼�ָ���
        /// </summary>
        /// <param name="sString">�ַ���</param>
        /// <param name="nIndex">ָ��λ��</param>
        /// <returns>�Ƿ�Ŀ¼�ָ���</returns>
        public static bool IsPathDelimiter(string sString, int nIndex)
        {
            return (nIndex >= 0) && (nIndex < sString.Length)
                    && (sString[nIndex] == '\\' || sString[nIndex] == '/');
        }

        /// <summary>
        /// �õ�һ���ַ��������һ���ָ������ָ���������ָ���ַ�����
        /// </summary>
        /// <param name="sDelimiters">�ָ�����</param>
        /// <param name="sString">�����ҵ��ַ���</param>
        /// <returns>�ҵ��ķָ���λ�ã����û���ҵ��򷵻�0</returns>
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
        /// �õ�һ���ļ�����Ŀ¼����
        /// </summary>
        /// <param name="sFileName">�ļ���</param>
        /// <returns>Ŀ¼�ַ���</returns>
        public static string ExtractFilePath(string sFileName)
        {
            int nIndex = LastDelimiter("\\:/", sFileName);

            return sFileName.Substring(0, nIndex + 1);
        }

        /// <summary>
        /// �õ�·���ļ������ļ�����
        /// </summary>
        /// <param name="sPathName">·���ļ���</param>
        /// <returns>�ļ�����</returns>
        public static string ExtractFileName(string sPathName)
        {
            int nIndex = LastDelimiter("\\:/", sPathName);

            return sPathName.Substring(nIndex + 1);
        }
        #endregion �ļ���·������

        #region Ŀ¼����
        /// <summary>
        /// ɾ��ָ��Ŀ¼ָ��ʱ��ǰ�������ļ�
        /// </summary>
        /// <param name="sDirectoryName"></param>
        /// <param name="dtBeforeTime"></param>
        public static int RemoveFilesBeforeTime(string sDirectoryName, DateTime dtBeforeTime)
        {

            int nDeleCount = 0;

            if (!Directory.Exists(sDirectoryName))
                return nDeleCount;

            
            //============ 1. �о�Ŀ¼�е������ļ� ===============
            string[] arrFiles = Directory.GetFiles(sDirectoryName);

            //========== 2. ����ÿһ���ļ� ===========
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
        #endregion Ŀ¼����
    }
}
