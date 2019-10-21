using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using GemBox.Spreadsheet;
//using GemBox.ExcelLite;
using Sigbit.Common;

namespace Sigbit.Common.WordProcess.Excel
{
    /// <summary>
    /// 导出时字符串的显示宽度
    /// </summary>
    public class ExcelChatWidth
    {   
        //一个中文610
        //1000个点=3.14
        //一个中文等于一个大写的"W" = 1.86
        //一个"l"= 0.58因此,英文需要平均一下. 
        public const int ChsWidth = 540;
        public const int EngWidth = 200;
    }
    
    /// <summary>
    /// Excell 导出类
    /// </summary>
    public abstract class ExcelExportCustom
    {
        //#region 委托声明
        ///// <summary>
        ///// 用户自定义格式化单元格
        ///// </summary>
        ///// <param name="sFieldName">字段名称</param>
        ///// <param name="vFieldValue">字段数据</param>
        //public delegate void EventFormatCell(string sFieldName, ref string vFieldValue);
        //public event EventFormatCell OnFormatCell = null;
        //private void 
        //#endregion 


        #region 私有属性
        /// <summary>
        /// 导出Excel文件名
        /// </summary>
        private string _exportFileName = "";

        /// <summary>
        /// 导出的Sheet名
        /// </summary>
        private string _sheetName = "";

        /// <summary>
        /// 导出的标题
        /// </summary>
        private string _titleText = "";

        /// <summary>
        /// 模板文件
        /// </summary>
        private string _templateFile = "";

        private Hashtable htUserDefValue = new Hashtable();


        #endregion 私有属性

        #region 保护级成员变量
        /// <summary>
        /// Excel对象
        /// </summary>
        protected ExcelFile _ef = new ExcelFile();

        /// <summary>
        /// 字段定义
        /// </summary>
        protected ExcelFieldDefList _fieldDefList = null;

        /// <summary>
        /// 导出模板
        /// </summary>
        protected ExcelExportTemplate _template = null;

        /// <summary>
        /// 数据行数
        /// </summary>
        protected int _dataRowCount = 0;

        /// <summary>
        /// 数据列数
        /// </summary>
        protected int _dataColCount = 0;

        /// <summary>
        /// 是否覆盖已有文件
        /// </summary>
        protected bool _isOverWriteFile = true;

        /// <summary>
        /// 是否需要动态调整列数
        /// </summary>
        //protected bool bIsDynamicCol = false;

        /// <summary>
        /// 
        /// </summary>
        protected int _exportRowIndex = 0;

        /// <summary>
        /// 当前操作sheet
        /// </summary>

        #endregion 保护级成员变量

        #region 私有函数


        /// <summary>
        /// 输出标题栏
        /// </summary>
        /// <remarks>没有模板的情况下</remarks>
        private void ExportTitle()
        {
            if (_titleText != "")
            {
                //CellRange mergedRange = ef.Worksheets[0].Cells.GetSubrangeAbsolute(0, 0, 0, nDataColCount - 1);
                CellRange mergedRange = _ef.Worksheets[0].Cells.GetSubrangeAbsolute(0, 0, 0, _fieldDefList.Count - 1);
                mergedRange.Merged = true;
                mergedRange.Value = _titleText;
                mergedRange.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                _exportRowIndex++;
            }
        }

        /// <summary>
        /// 初始化WorkSheet
        /// </summary>
        /// <param name="ew"></param>
        private void InitWorksheet(ExcelWorksheet ew)
        {
            int nWorksheetRowCount = _dataRowCount;
            int nWorksheetColCount = _dataColCount;


            if (_template == null)
            {
                if (_titleText != "")
                {
                    nWorksheetRowCount++;
                }
            }
            else
            {
                nWorksheetRowCount += _template.Setting.HeaderLineCount + _template.Setting.FooterLineCount;
                nWorksheetColCount += _template.Setting.FixColCount;

            }

            //增加标题行
            nWorksheetRowCount++;
            for (int i = 0; i < nWorksheetRowCount; i++)
            {
                for (int j = 0; j < nWorksheetColCount; j++)
                {
                    ew.Cells[i, j].Value = "";
                }
            }

            
        }

        //protected void CopyTemplate()
        //{
        //    //复制Header
        //    int nEndColumnIndex = nDataColCount - 1;
        //    CellRange range = template.Worksheet.Cells.GetSubrangeAbsolute(0, 0, template.Setting.HeaderLineCount - 1, nEndColumnIndex);
        //    range.CopyTo(ef.Worksheets[0], 0, 0);

        //    //复制数据区
        //    //这里要多加一个标题行
        //    range = template.Worksheet.Cells.GetSubrangeAbsolute(template.Setting.HeaderLineCount + 1, 0, template.Setting.HeaderLineCount + 2, nEndColumnIndex);
        //    for (int i = 0; i <=nDataRowCount; i++)
        //    {
        //        range.CopyTo(ef.Worksheets[0], template.Setting.HeaderLineCount + i, 0);
        //    }

        //    //复制Footer
        //    range = template.Worksheet.Cells.GetSubrangeAbsolute(template.Setting.HeaderLineCount + template.Setting.DataLineCount, 0,
        //        template.Setting.HeaderLineCount + template.Setting.DataLineCount + template.Setting.FooterLineCount - 1, nEndColumnIndex);
        //    range.CopyTo(ef.Worksheets[0], template.Setting.HeaderLineCount + nDataRowCount + 1, 0);
        //}

        /// <summary>
        /// 初始化字段数据
        /// </summary>
        protected void InitFieldList()
        {
            _fieldDefList = new ExcelFieldDefList();
            for (int i = 0; i < _dataColCount; i++)
            {
                ExcelFieldDefListItem item = new ExcelFieldDefListItem();
                item.FieldName = GetDataFieldName(i);
                _fieldDefList.Add(item);
            }
        }

        protected string GetUserDefValue(int nRow, int nCol)
        {
            Hashtable ht = (Hashtable)htUserDefValue[nRow];
            string sResult = null;
            if (ht != null)
            {
                if (ht[nCol] != null)
                {
                    sResult = ht[nCol].ToString();
                }
            }
            return sResult;        

        }

        #endregion 私有函数

        #region 保护函数
        
        /// <summary>
        /// 输出标题行
        /// </summary>
        /// 
        protected void ExportFieldTitle()
        {
            string sFieldTitle = "";
            //string sFieldChsTitle = "";

            //.选择数据
            if (_fieldDefList == null)
            {
                throw new Exception("没有指定输出字段");
            }

            for (int i = 0; i < _fieldDefList.Count; i++)
            {
                if (_fieldDefList[i].FieldChsName != "")
                {
                    sFieldTitle = _fieldDefList[i].FieldChsName;
                }
                else
                {
                    sFieldTitle = _fieldDefList[i].FieldName;
                }

                
                _ef.Worksheets[0].Cells[_exportRowIndex, i].Value = sFieldTitle;
            }
            _exportRowIndex++;
        }

        protected void __ExportFieldTitle()
        {
            //string sFieldTitle = "";
            //string sFieldChsTitle = "";

            ////判断是否为全部数据
            //if (!IsDynamicCol)
            //{
            //    //1.全部数据
            //    for (int i = 0; i < nDataColCount; i++)
            //    {
            //        sFieldTitle = GetDataFieldName(i);

            //        if (fieldDefList != null)
            //        {
            //            sFieldChsTitle = fieldDefList[i].FieldChsName;
            //            if (sFieldChsTitle != "")
            //            {
            //                sFieldTitle = sFieldChsTitle;
            //            }
            //        }
            //        ef.Worksheets[0].Cells[nExportRowIndex, i].Value = sFieldTitle;
            //    }
            //}
            //else
            //{
            //    //2.选择数据
            //    if (fieldDefList == null)
            //    {
            //        throw new Exception("没有指定输出字段");
            //    }

            //    for (int i = 0; i < fieldDefList.Count; i++)
            //    {
            //        if (fieldDefList[i].FieldChsName != "")
            //        {
            //            sFieldTitle = fieldDefList[i].FieldChsName;
            //        }
            //        else
            //        {
            //            sFieldTitle = fieldDefList[i].FieldName;
            //        }

            //        ef.Worksheets[0].Cells[nExportRowIndex, i].Value = sFieldTitle;
            //    }
            //}
            //nExportRowIndex++;
        }

        /// <summary>
        /// 计算宽度
        /// </summary>
        /// <param name="sStr">字符串</param>
        /// <returns>字符串的宽度</returns>
        /// <remarks>
        /// 这个计算方法很不准确,只能大概的计算一下,最长的宽度是30个字符
        /// </remarks>
        protected int CalcTextWidth(string sStr)
        { 
            int nChsCharCount = 0;
            int nEngCharCount = 0;
            int nMaxWidth = ExcelChatWidth.ChsWidth * 30;
            for(int i = 0;i<sStr.Length;i++)
            {
                if (StringUtil.IsEnglishLetter(sStr[i]))
                {
                    nEngCharCount++;
                }
                else
                {
                    nChsCharCount++;
                }
            }

            int nWidth = nEngCharCount * ExcelChatWidth.EngWidth + nChsCharCount * ExcelChatWidth.ChsWidth;
            if (nWidth > nMaxWidth)
            {
                nWidth = nMaxWidth;
            }
            return nWidth;

            //Graphics g =new Graphics();
            //Font font = new Font(cell.Style.Font.Name, cell.Style.Font.Weight);

            //font.FontFamily = cell.Style.Font.Name;
            //font.Italic = cell.Style.Font.Italic;
            //font.Bold = cell.Style.Font.GetType;
            //font.Italic = cell.Style.Font.Italic;
            //font.Italic = cell.Style.Font.Italic;
            //font.Italic = cell.Style.Font.Italic;
            //font.Italic = cell.Style.Font.Italic;


            //return  (cell.Style.Font.Size / 15) * sStr.Length;
            //int nWidth = ExcelFont.NormalWeight * sStr.Length;
            //if (nWidth > 30 * ExcelFont.NormalWeight)
            //{
            //    nWidth = 30 * ExcelFont.NormalWeight;
            //}
            //return nWidth;
            

            //SizeF size = new SizeF();
            //return ConvertUtil.ToInt(size.Width);
        }
        #endregion 保护函数

        #region 可重载函数

        ///// <summary>
        ///// 导出全部DataSet
        ///// </summary>
        //abstract protected void ExportFullExcel();

        ///// <summary>
        ///// 导出部分字段的DataSet
        ///// </summary>
        //abstract protected void ExportDefineExcel();

        abstract protected void ExportExcelData();

       
        /// <summary>
        /// 获取字段的名称
        /// </summary>
        /// <param name="nIndex"></param>
        abstract protected string GetDataFieldName(int nIndex);

        abstract protected string FormatCell(string sFieldName, int nFieldIndex, string sValue);


        /// <summary>
        /// 导出DataSet数据源的数据
        /// </summary>
        virtual protected void ExportExcel()
        {
            InitWorksheet(_ef.Worksheets[0]);

            if (_template == null)
            {
                ExportTitle();
            }
            else
            {
                _template.CopyTo(_ef.Worksheets[0], _dataRowCount);
            }

            ExportExcelData();

            //if (bIsDynamicCol)
            //{
            //    ExportDefineExcel();
            //}
            //else
            //{
            //    ExportFullExcel();
            //}
        }

        #endregion

        //protected int 0 = 0;

        //public int CurrentSheet
        //{
        //    get { return 0; }
        //    set { 0 = value; }
        //}


        #region 公共属性
        /// <summary>
        /// 导出Excel文件名
        /// </summary>
        public string ExportFileName
        {
            get { return _exportFileName; }
            set { _exportFileName = value; }
        }


        /// <summary>
        /// 导出的Sheet名
        /// </summary>
        public string SheetName
        {
            get { return _sheetName; }
            set { _sheetName = value; }
        }

        /// <summary>
        /// 导出的标题
        /// </summary>
        public string TitleText
        {
            get { return _titleText; }
            set { _titleText = value; }
        }

        /// <summary>
        /// 是否需要动态调整列数
        /// </summary>
        //public bool IsDynamicCol
        //{
        //    get { return bIsDynamicCol; }
        //    set { bIsDynamicCol = value; }
        //}

        ///// <summary>
        ///// 是否覆盖已有文件
        ///// </summary>
        //public bool IsOverWriteFile
        //{
        //    get { return _IsOverWriteFile; }
        //    set { _IsOverWriteFile = value; }
        //}

        /// <summary>
        /// 导出模板文件
        /// </summary>
        public string TemplateFile
        {
            get { return _templateFile; }
            set 
            {                              
                //加载模板
                _templateFile = value;
                _template = new ExcelExportTemplate();
                _template.LoadTemplate(_templateFile);
            }
        }
        #endregion 公共属性

        #region 公共函数
        
        /// <summary>
        /// 导出文件
        /// </summary>
        public void DoExport()
        {
            _exportRowIndex = 0;
            //if ((fieldDefList == null) && (!IsDynamicCol))
            //{
            //    //在全输出的模式下生成字段属性
            //    InitFieldList();
            //}

            if (_fieldDefList == null)
            {
                InitFieldList();
            }

            //1.检查文件名和路径
            if (File.Exists(_exportFileName) && (!_isOverWriteFile))
            {
                throw new Exception("文件已经存在:" + _exportFileName);
            }

            string sFilePath = Path.GetDirectoryName(_exportFileName);
            if (!Directory.Exists(sFilePath))
            {
                throw new Exception("目录不存在:" + sFilePath);
            }

            //2.清空全部的Sheet
            while (_ef.Worksheets.Count > 0)
            {
                _ef.Worksheets[0].Delete();
            }
            

            //3.增加一个新的Sheet
            if (_sheetName == "")
            {
                _ef.Worksheets.Add("导出文件");
            }
            else
            {
                _ef.Worksheets.Add(_sheetName);
            }

            //4.导出数据
            ExportExcel();

            //5.保存文件
            _ef.SaveXls(_exportFileName);
        }
    
        /// <summary>
        /// 增加导出字段名
        /// </summary>
        /// <param name="sFieldName">字段名称</param>
        /// <remarks>!!!本函数不做字段是否在数据集的效验</remarks>
        public void AddFieldName(string sFieldName)
        {
            if (_fieldDefList == null)
            {
                _fieldDefList = new ExcelFieldDefList();
            }
            ExcelFieldDefListItem item = new ExcelFieldDefListItem();
            if (_fieldDefList.GetItemByFieldName(sFieldName) == null)
            {
                item.FieldName = sFieldName;
                _fieldDefList.Add(item);
            }
            else
            {
                throw new Exception("字段名称已经添加:" + sFieldName);
            }
        }

        /// <summary>
        /// 增加导出字段索引
        /// </summary>
        /// <param name="nFieldIndex">字段索引</param>
        public void AddFieldIndex(int nFieldIndex)
        {
            if (nFieldIndex < _dataColCount)
            {
                string sFieldName = GetDataFieldName(nFieldIndex);
                AddFieldName(sFieldName);
            }
            else
            {
                throw new Exception("字段下标超界:" + nFieldIndex.ToString());
            }
        }

        /// <summary>
        /// 设置中文字段名称
        /// </summary>
        /// <param name="sFiledName">字段名称</param>
        /// <param name="sChineseName">中文名称</param>
        public void SetFieldChsName(string sFiledName, string sChineseName)
        {
            if (_fieldDefList == null)
            {
                InitFieldList();
            }
                
            ExcelFieldDefListItem item = _fieldDefList.GetItemByFieldName(sFiledName);
            if (item != null)
            {
                item.FieldChsName = sChineseName;
            }
            else
            {
                throw new Exception("字段名不存在:" + sFiledName);
            }
        }

        public void SetFiledDataType(int nFieldIndex, ExcelDataType dataType)
        {
            if (_fieldDefList == null)
            {
                InitFieldList();
            }

            if (nFieldIndex >= _fieldDefList.Count)
            {
                throw new Exception("字段下标超级:" + nFieldIndex);
            }

            ExcelFieldDefListItem item = _fieldDefList[nFieldIndex];
            item.DataType = dataType;
        }

        public void SetFiledDataType(string sFiledName, ExcelDataType dataType)
        {
            if (_fieldDefList == null)
            {
                InitFieldList();
            }

            ExcelFieldDefListItem item = _fieldDefList.GetItemByFieldName(sFiledName);
            if (item != null)
            {
                item.DataType = dataType;
            }
            else
            {
                throw new Exception("字段名不存在:" + sFiledName);
            }


        }

        /// <summary>
        /// 设置中文字段名称
        /// </summary>
        /// <param name="nFieldIndex">字段名称</param>
        /// <param name="sChineseName">中文名称</param>
        public void SetFieldChsName(int nFieldIndex,string sChineseName)
        {
            if (nFieldIndex < _dataColCount)
            {
                string sFieldName = GetDataFieldName(nFieldIndex);
                SetFieldChsName(sFieldName, sChineseName);
            }
            else
            {
                throw new Exception("字段下标超界:" + nFieldIndex.ToString());
            }
        }

        /// <summary>
        /// 设定一个行列的值
        /// </summary>
        /// <param name="nRow">行-从1开始</param>
        /// <param name="nCol">列-从1开始</param>
        /// <param name="sValue">数据</param>
        public void SetCellValue(int nRow, int nCol, string sValue)
        {
            //if ((nRow > nDataRowCount + template.Setting.HeaderLineCount + template.Setting.FooterLineCount) || (nRow < 1))
            if ((nRow > _dataRowCount) || (nRow < 1))
            {
                throw new Exception("设置单元格数据行超界:" + nRow.ToString());
            }

            if ((nCol > _dataColCount) || (nCol < 1))
            {
                throw new Exception("设置单元格数据列超界:" + nCol.ToString());
            }

            Hashtable ht = (Hashtable)htUserDefValue[nRow - 1];
            if (ht == null)
            { 
                ht = new Hashtable();
                htUserDefValue.Add(nRow - 1, ht);
            }

            ht.Add(nCol - 1, sValue);


            //if (arrCellValue == null)
            //{
            //    //初始化自定义数据
            //    arrCellValue = new string[nDataRowCount, nDataColCount];
            //}

            //if ((nRow > nDataRowCount) || (nRow < 1))
            //{
            //    throw new Exception("设置单元格数据行超界:" + nRow.ToString());
            //}

            //if ((nCol > nDataColCount) || (nCol < 1))
            //{
            //    throw new Exception("设置单元格数据列超界:" + nCol.ToString());
            //}

            //arrCellValue[nRow - 1, nCol - 1] = sValue;
        
        }

        /// <summary>
        /// 设置替换数据
        /// </summary>
        /// <param name="sIndicator">标识</param>
        /// <param name="sText">替换文本</param>
        public void SetSingleText(string sIndicator, string sText)
        {
            _template.IndicatorList.Add(sIndicator, sText);
        }

        #endregion 公共函数
    }

}
