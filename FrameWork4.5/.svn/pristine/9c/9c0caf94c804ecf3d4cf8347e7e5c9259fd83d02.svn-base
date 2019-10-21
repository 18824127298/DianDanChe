using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GemBox.Spreadsheet;
//using GemBox.ExcelLite;

namespace Sigbit.Common.WordProcess.Excel
{
    /// <summary>
    /// Excel导入类
    /// </summary>
    public class ExcelImport
    {
        #region 私有属性
        /// <summary>
        /// 行数
        /// </summary>
        private int _rowCount = 0;

        /// <summary>
        /// 列数
        /// </summary>
        private int _colCount = 0;

        /// <summary>
        /// 获取行文本
        /// </summary>
        ExcelImportData _rows = new ExcelImportData();

        /// <summary>
        /// 数据行
        /// </summary>
        public ExcelImportData Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        private int _beginLine = 0;

        /// <summary>
        /// 起始行
        /// </summary>
        public int BeginLine
        {
            get { return _beginLine; }
            set 
            {
                if (value <= 0)
                {
                    throw new Exception("起始行从1开始，必须大于0！");
                }
                
                _beginLine = value;
                _beginLine--;
            }
        }
        private int _endLine = 0;

        /// <summary>
        /// 截止行
        /// </summary>
        public int EndLine
        {
            get { return _endLine; }
            set 
            {
                if (value <= 0)
                {
                    throw new Exception("终止必须大于0！");
                }

                _endLine = value; 
            }
        }

        #endregion 私有属性

        #region 私有函数
        /// <summary>
        /// 从文件导入
        /// </summary>
        /// <param name="sFilename">文件名</param>
        /// <param name="sSheetName">工作表名</param>
        /// <param name="nSheetIndex">工作表序号，从1开始</param>
        private void ImportExcellFile(string sFilename, string sSheetName, int nSheetIndex, int nBeginLine, int nEndLine)
        {
            ExcelFile ef = new ExcelFile();
            ExcelWorksheet ew = null;
            if ((_endLine != 0) && (_beginLine != 0) && (_endLine < _beginLine))
            {
                throw new Exception("终止行必须小于起始行！");
            }

            //========= 1.判断文件是否存在 ===============
            if (File.Exists(sFilename))
            {
                //========= 2.打开Excel文件 ============
                string sNewFileName = "_" + Path.GetFileName(sFilename);
                sNewFileName = Path.GetDirectoryName(sFilename) + "\\" + sNewFileName;
                //Mango 在NTFS模式下Excel控件会打不开一些文件,因此需要复制一次,
                //好像就可以解决这个问题
                File.Copy(sFilename, sNewFileName, true);
                sFilename = sNewFileName;


                ef.LoadXls(sFilename);

                if (sSheetName != "")
                {
                    //======== 3.检查Sheet名称是否存在 ==========
                    foreach (ExcelWorksheet sheet in ef.Worksheets)
                    {
                        if (sheet.Name == sSheetName)
                        {
                            ew = sheet;
                            break;
                        }
                    }

                    if (ew == null)
                    {
                        throw new Exception("指定的SheetName不存在:" + sSheetName);
                    }
                }
                else
                {
                    //======== 4.检查Sheet下标是否有效 ============
                    if ((nSheetIndex <= ef.Worksheets.Count) && (nSheetIndex > 0))
                    {
                        ew = ef.Worksheets[(int) (nSheetIndex - 1)];
                    }
                    else
                    {
                        throw new Exception("指定的下标SheetIndex超界:" + nSheetIndex.ToString());
                    }
                }

                if (ew != null)
                {
                    FetchWorkSheet(ew, _beginLine, _endLine);
                }
            }
            else
            {
                throw new Exception("文件不存在:" + sFilename);

            }
        }




        /// <summary>
        /// 得到一个工作表的列数
        /// </summary>
        /// <param name="ew">工作表</param>
        /// <returns>列数</returns>
        private int GetColumsCount(ExcelWorksheet ew)
        {
            int nCount = 0;
            foreach (ExcelRow row in ew.Rows)
            {
                if (nCount < row.AllocatedCells.Count)
                {
                    nCount = row.AllocatedCells.Count;
                }
            }
            return nCount;        
        }

        /// <summary>
        /// 读取文件的一个表单
        /// </summary>
        /// <param name="ew">工作表</param>
        private void FetchWorkSheet(ExcelWorksheet ew)
        {
            FetchWorkSheet(ew, 0, 0);
        }

        private void FetchWorkSheet(ExcelWorksheet ew,int nBeginLine,int nEndLine)
        {
            if (ew == null)
            {
                return;
            }

            //====== 1.初始化数据 ===========
            _rowCount = ew.Rows.Count;
            _colCount = GetColumsCount(ew);

            if (nBeginLine > _rowCount)
            {
                nBeginLine = _rowCount;
            }

            if (nEndLine == 0)
            {
                nEndLine = _rowCount;
            }
            else
            {
                if (nEndLine > _rowCount)
                {
                    nEndLine = _rowCount;
                }
            }

            //======== 2.循环读取表单里面的数据 =============
            for (int i = nBeginLine; i < nEndLine; i++)
            {
                string[] arrLines = new string[_colCount];
                for (int j = 0; j < _colCount; j++)
                {
                    if ((ew.Cells[i, j] != null) && (ew.Cells[i, j].Value != null))
                    {
                        arrLines[j] = ew.Cells[i, j].Value.ToString();
                    }
                    else
                    {
                        arrLines[j] = "";
                    }
                }
                _rows.AddLine(arrLines);
            }
            _rowCount = _rows.Count;
        }


        #endregion 私有函数

        #region 公共属性
        /// <summary>
        /// 行数
        /// </summary>
        public int RowCount
        {
            get { return _rowCount; }
        }

        /// <summary>
        /// 列数
        /// </summary>
        public int ColCount
        {
            get { return _colCount; }
        }
        #endregion 公共属性

        #region 公共函数

        /// <summary>
        /// 导入一个Excel文件
        /// </summary>
        /// <param name="sFilename">导入的文件名</param>
        public void ImportFile(string sFilename)
        {
            ImportExcellFile(sFilename, "", 1,_beginLine,_endLine);
        }

        /// <summary>
        /// 导入一个Excel文件
        /// </summary>
        /// <param name="sFilename">导入的文件名</param>
        /// <param name="sSheetName">导入的Sheet的名称</param>
        public void ImportFile(string sFilename, string sSheetName)
        {
            ImportExcellFile(sFilename, sSheetName, 0,_beginLine,_endLine);
        }

        /// <summary>
        ///  导入一个Excel文件
        /// </summary>
        /// <param name="sFilename">导入的文件名</param>
        /// <param name="nSheetIndex">导入的Sheet索引</param>
        public void ImportFile(string sFilename, int nSheetIndex)
        {
            ImportExcellFile(sFilename, "", nSheetIndex,_beginLine,_endLine);
        }

        public static string[] GetSheetNames(string sFilename)
        {
            ExcelFile ef = new ExcelFile();
            ArrayList sheetName = new ArrayList();

            //========= 1.判断文件是否存在 ===============
            if (File.Exists(sFilename))
            {
                //========= 2.打开Excel文件 ============
                ef.LoadXls(sFilename);

                //======== 3.检查Sheet名称是否存在 ==========
                foreach (ExcelWorksheet sheet in ef.Worksheets)
                {
                    sheetName.Add(sheet.Name);
                }
            }
            else
            {
                throw new Exception("文件不存在:" + sFilename);
            }
            return (string [])sheetName.ToArray(typeof(string));
        }
        #endregion 公共函数

    }
}
