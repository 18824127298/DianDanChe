using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

using Sigbit.Common;

namespace Sigbit.Data.DBStruct
{
    class TCXFieldComment
    {
        private string _fieldName = "";
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        private string _fieldChs = "";
        /// <summary>
        /// 字段的中文解释
        /// </summary>
        public string FieldChs
        {
            get { return _fieldChs; }
            set { _fieldChs = value; }
        }
    }

    class TCXTableComment
    {
        private string _tableName = "";
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }

        private string _tableChs = "";
        /// <summary>
        /// 表的中文解释
        /// </summary>
        public string TableChs
        {
            get { return _tableChs; }
            set { _tableChs = value; }
        }

        /// <summary>
        /// 字段库
        /// </summary>
        private Hashtable _htFields = new Hashtable();

        /// <summary>
        /// 加入字段注释
        /// </summary>
        /// <param name="fieldComment">字段注释</param>
        public void AddFieldComment(TCXFieldComment fieldComment)
        {
            _htFields.Add(fieldComment.FieldName, fieldComment);
        }

        /// <summary>
        /// 得到字段注释
        /// </summary>
        /// <param name="sFieldName">字段名</param>
        /// <returns>字段注释</returns>
        public TCXFieldComment GetFieldComment(string sFieldName)
        {
            return (TCXFieldComment)_htFields[sFieldName];
        }
    }

    /// <summary>
    /// 表及字段的注释库
    /// </summary>
    public class TCXTableCommentPool : Hashtable
    {
        private static TCXTableCommentPool _thisInstance = null;
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static TCXTableCommentPool Instance
        {
            get
            {
                if (_thisInstance == null)
                    _thisInstance = new TCXTableCommentPool();
                return _thisInstance;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TCXTableCommentPool()
        {
            //============ 1. 读取定义的表注释文本文件名 =========
            string sDefineFileName = AppPath.AppFullPath("config", "data_copy\\table_define.txt");
            string[] arrDefineLines = FileUtil.ReadStringListFromFile(sDefineFileName);

            int nBeginSearchIndex = 0;

            while (true)
            {
                //======== 2. 找到表格的标题行和表格的结束行 ===========
                int nTableBeginLineIndex = TableBeginLineIndex(arrDefineLines, nBeginSearchIndex);
                if (nTableBeginLineIndex == -1)
                    break;

                int nTableEndLineIndex = TableEndLineIndex(arrDefineLines, nTableBeginLineIndex);
                nBeginSearchIndex = nTableEndLineIndex + 1;

                //========== 3. 表的第一行的前一行为表定义行，解析该行 =============
                string sTableDefineLine = arrDefineLines[nTableBeginLineIndex - 1];
                string sTableName = "";
                string sChsTableName = "";
                ParseTableNameFromFirstLine(sTableDefineLine, ref sTableName, ref sChsTableName);
                this.AddTableComment(sTableName, sChsTableName);

                //========== 4. 解析字段定义的部分 ===========
                for (int i = nTableBeginLineIndex + 1; i <= nTableEndLineIndex; i++)
                {
                    string sLine = arrDefineLines[i];
                    string[] arrItems = sLine.Split('\t');
                    string sFieldName = arrItems[2];
                    string sFieldChs = arrItems[6];
                    this.AddFieldComment(sTableName, sFieldName, sFieldChs);
                }
            }
        }

        private int TableBeginLineIndex(string[] arrDefineLines, int nBeginIndex)
        {
            for (int i = nBeginIndex; i < arrDefineLines.Length; i++)
            {
                string sLine = arrDefineLines[i];
                if (StringUtil.Occurs("\t", sLine) < 6)
                    continue;
                return i;
            }

            return -1;
        }

        private int TableEndLineIndex(string[] arrDefineLines, int nBeginIndex)
        {
            for (int i = nBeginIndex; i < arrDefineLines.Length; i++)
            {
                string sLine = arrDefineLines[i];
                if (StringUtil.Occurs("\t", sLine) >= 6)
                    continue;
                return i - 1;
            }

            return arrDefineLines.Length - 1;
        }

        public void AddTableComment(string sTableName, string sTableChs)
        {
            TCXTableComment tableComment = new TCXTableComment();
            tableComment.TableName = sTableName;
            tableComment.TableChs = sTableChs;
            this.Add(sTableName, tableComment);
        }

        public string GetTableComment(string sTableName)
        {
            TCXTableComment tableComment = (TCXTableComment)this[sTableName];
            if (tableComment == null)
                return "";
            else
                return tableComment.TableChs;
        }

        public void AddFieldComment(string sTableName, string sFieldName, string sFieldChs)
        {
            //=========== 1. 定位到表 =============
            TCXTableComment tableComment = (TCXTableComment)this[sTableName];
            if (tableComment == null)
                throw new Exception("定位不到表 - " + sTableName);

            //=========== 2. 得到字段定义 ===========
            TCXFieldComment fieldComment = new TCXFieldComment();
            fieldComment.FieldName = sFieldName;
            fieldComment.FieldChs = sFieldChs;

            //========== 3. 在表中增在字段定义 ============
            tableComment.AddFieldComment(fieldComment);
        }

        public string GetFieldComment(string sTableName, string sFieldName)
        {
            //=========== 1. 定位到表 =============
            TCXTableComment tableComment = (TCXTableComment)this[sTableName];
            if (tableComment == null)
                return "";

            TCXFieldComment fieldComment = tableComment.GetFieldComment(sFieldName);
            if (fieldComment == null)
                return "";

            return fieldComment.FieldChs;
        }

        private bool ParseTableNameFromFirstLine(string sFirstLine, ref string PsTableName, ref string PsChsTableName)
        {
            string sTableName = "", sChsTableName = "";
            int i, nPos;
            char ch;

            //==== 1. 变为小写 ==========
            sFirstLine = sFirstLine.ToLower();
            sFirstLine = sFirstLine.Replace("（", "(");
            sFirstLine = sFirstLine.Replace("）", ")");

            //====== 2. 找到表名的第一个字母的位置 ==========
            nPos = -1;
            for (i = 0; i < sFirstLine.Length; i++)
            {
                ch = sFirstLine[i];
                if (ch >= 'a' && ch <= 'z')
                {
                    nPos = i;
                    break;
                }
            }
            if (nPos == -1)
                return false;

            //=========== 3. 得到表名 ==========
            for (i = nPos; i < sFirstLine.Length; i++)
            {
                ch = sFirstLine[i];
                if ((ch >= 'a' && ch <= 'z') || ch == '_')
                    sTableName += ch;
                else
                    break;
            }

            //========== 4. 得到反括号的位置 ==========
            int nChsNamePos, nChsNameEndPos, nChsNameLen;
            nChsNamePos = sFirstLine.IndexOf(")");
            nChsNamePos++;

            //========== 5. 得到之后第一个正括号的位置 =======
            nChsNameEndPos = sFirstLine.IndexOf("(", nChsNamePos);
            nChsNameLen = nChsNameEndPos - nChsNamePos;
            if (nChsNamePos < 0 || nChsNameLen < 0)
                sChsTableName = "";
            else
                sChsTableName = sFirstLine.Substring(nChsNamePos, nChsNameLen);

            //========== 6. 赋值两个表名 ==========
            PsTableName = sTableName;
            PsChsTableName = sChsTableName;

            return true;
        }

    }
}
