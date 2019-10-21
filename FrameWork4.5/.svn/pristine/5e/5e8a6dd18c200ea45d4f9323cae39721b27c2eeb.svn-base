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
    /// 导出模板
    /// </summary>
    public class ExcelExportTemplate
    {


        #region 私有属性

        /// <summary>
        /// 模板文件
        /// </summary>
        private ExcelFile _ef = new ExcelFile();

        private const string SwapTemplatePrefix = "~tmp~";

        private string _tempFilename = "";

        #endregion 私有属性

        #region 私有方法


        /// <summary>
        /// 分析模板文件
        /// </summary>
        private void AnalyzeTemplate()
        {
            if (_ef.Worksheets.Count < 2)
            {
                throw new Exception("模板文件工作表必须大于2");
            }
            Setting.LoadSetting(_ef.Worksheets[1]);
        }

        #endregion 私有方法

        #region 公有属性

        //private int 0 = 0;
        ///// <summary>
        ///// 当前工作 Sheet
        ///// </summary>
        //public int CurrentSheet
        //{
        //    get { return 0; }
        //    set { 0 = value; }
        //}

        /// <summary>
        /// 标识列表
        /// </summary>
        private ExcelExportIndicatorList _indicatorList = new ExcelExportIndicatorList();
        /// <summary>
        /// 标识列表
        /// </summary>
        public ExcelExportIndicatorList IndicatorList
        {
            get { return _indicatorList; }
            set { _indicatorList = value; }
        }

        /// <summary>
        /// 模板工作表
        /// </summary>
        public ExcelWorksheet Worksheet
        {
            get
            {
                return _ef.Worksheets[0];
            }
        }

        /// <summary>
        /// 模板配置
        /// </summary>
        ExcelExportTemplateSetting _setting = new ExcelExportTemplateSetting();

        /// <summary>
        /// 模板配置
        /// </summary>
        public ExcelExportTemplateSetting Setting
        {
            get { return _setting; }
        }

        #endregion 公有属性


        #region 公有方法

        public void DeleteSwapTemplateFile()
        {
            string filename = Path.GetFileName(_tempFilename);
            if (filename != "")
            {
                if (filename.IndexOf(SwapTemplatePrefix) == 0)
                {
                    try
                    {
                        File.Delete(_tempFilename);
                    }
                    catch (Exception)
                    {
                    }
                }

            }
        }

        /// <summary>
        /// 加载模板
        /// </summary>
        /// <param name="sFilename"></param>
        public void LoadTemplate(string sFilename)
        {
            if (File.Exists(sFilename))
            {
                try
                {
                    string sNewFileName = SwapTemplatePrefix + "_" + Path.GetFileName(sFilename);
                    sNewFileName = Path.GetDirectoryName(sFilename) + "\\" + sNewFileName;
                    //Mango 在NTFS模式下Excel控件会打不开一些文件,因此需要复制一次,
                    //好像就可以解决这个问题
                    File.Copy(sFilename, sNewFileName, true);
                    sFilename = sNewFileName;
                    _tempFilename = sFilename;
                    _ef.LoadXls(sFilename);
                    AnalyzeTemplate();
                }
                catch (Exception ex)
                {
                    throw new Exception("加载模板文件出错:" + sFilename + ";出错信息:" + ex.Message);
                }
            }
            else
            {
                throw new Exception("模板文件不存在:" + sFilename);
            }
        }

        /// <summary>
        /// 复制模板到Excel
        /// </summary>
        /// <param name="ew"></param>
        /// <param name="nDataRowCount">数据行数</param>
        public void CopyTo(ExcelWorksheet ew, int nDataRowCount)
        {
            ReplaceIndicator();
            //复制Header
            int nEndColumnIndex = _setting.ColumnCount - 1;
            CellRange range = Worksheet.Cells.GetSubrangeAbsolute(0, 0, Setting.HeaderLineCount - 1, nEndColumnIndex);
            range.CopyTo(ew, 0, 0);
            for (int i = 0; i < Setting.HeaderLineCount; i++)
            {
                ew.Rows[i].Height = Worksheet.Rows[i].Height;
            }

            //这里要多加一个标题行

            //复制数据区
            range = Worksheet.Cells.GetSubrangeAbsolute(Setting.HeaderLineCount + 1, 0, Setting.HeaderLineCount + 2, nEndColumnIndex);
            //range = Worksheet.Cells.GetSubrangeAbsolute(Setting.HeaderLineCount - 1, 0, Setting.HeaderLineCount , nEndColumnIndex);
            for (int i = 0; i <= nDataRowCount - 1; i++)
            {
                range.CopyTo(ew, Setting.HeaderLineCount + i, 0);
                ew.Rows[Setting.HeaderLineCount + i].Height = Worksheet.Rows[Setting.HeaderLineCount + 1].Height;
                for (int j = 0; j < nEndColumnIndex; j++)
                {
                    ew.Rows[i].Cells[j].Style.WrapText = Worksheet.Cells[Setting.HeaderLineCount + 1, j].Style.WrapText;
                }
                ew.Rows[i].AutoFit();
            }

            //删除多余的模板示例
            if (nDataRowCount < Setting.DataLineCount)
            {
                for (int i = nDataRowCount; i < Setting.DataLineCount; i++)
                {
                    ew.Rows[Setting.HeaderLineCount + i].Delete();
                }
            }


            //复制Footer
            if (Setting.FooterLineCount > 0)
            {
                range = Worksheet.Cells.GetSubrangeAbsolute(Setting.HeaderLineCount + Setting.DataLineCount, 0,
                    Setting.HeaderLineCount + Setting.DataLineCount + Setting.FooterLineCount - 1, nEndColumnIndex);
                range.CopyTo(ew, Setting.HeaderLineCount + nDataRowCount, 0);
                for (int i = 0; i < Setting.FooterLineCount; i++)
                {
                    ew.Rows[Setting.HeaderLineCount + nDataRowCount + i].Height = Worksheet.Rows[Setting.HeaderLineCount + Setting.DataLineCount + i].Height;
                }
            }

            //复制列宽度
            for (int i = 0; i < _setting.ColumnCount; i++)
            {
                ew.Columns[i].Width = Worksheet.Columns[i].Width;
            }
        }

        /// <summary>
        /// 替换文本内容
        /// </summary>
        public void _ReplaceIndicator()
        {
            string sCellText = "";
            string sIndicator = "";
            ExcelExportIndicatorItem item = null;

            //1.在模板中变量
            foreach (ExcelRow row in Worksheet.Rows)
            {
                foreach (ExcelCell cell in row.AllocatedCells)
                {
                    if (cell.Value != null)
                    {
                        sCellText = cell.Value.ToString();
                        //2.查找标识
                        if ((sCellText.Length > 2) && (sCellText.IndexOf('{') >= 0) && (sCellText.IndexOf("}") >= 0))
                        {
                            sIndicator = sCellText.Substring(1, sCellText.Length - 2);
                            item = _indicatorList.GetIndicatorByName(sIndicator);
                            if (item != null)
                            {
                                cell.Value = item.Text;
                            }
                        }
                    }
                }
            }
        }

        public void ReplaceIndicator()
        {
            string sCellText = "";
            string sIndicator = "";
            ExcelExportIndicatorItem item = null;

            //1.在模板中变量
            foreach (ExcelRow row in Worksheet.Rows)
            {
                foreach (ExcelCell cell in row.AllocatedCells)
                {
                    if (cell.Value != null)
                    {
                        sCellText = cell.Value.ToString();

                        //2.查找全部的
                        string[] arrIndicator = ExcelExportIndicatorList.GetAllIndicatorFromText(sCellText);
                        arrIndicator = ExcelExportIndicatorList.GetAllIndicatorFromText(sCellText);
                        if (arrIndicator != null)
                        {
                            foreach (string sOneIndicator in arrIndicator)
                            {
                                item = _indicatorList.GetIndicatorByName(sOneIndicator);
                                if (item != null)
                                {
                                    sCellText = sCellText.Replace("{" + sOneIndicator + "}", item.Text);
                                }
                            }
                            cell.Value = sCellText;
                        }


                        //2.查找标识
                        if ((sCellText.Length > 2) && (sCellText.IndexOf('{') >= 0) && (sCellText.IndexOf("}") >= 0))
                        {
                            sIndicator = sCellText.Substring(1, sCellText.Length - 2);
                            item = _indicatorList.GetIndicatorByName(sIndicator);
                            if (item != null)
                            {
                                cell.Value = item.Text;
                            }
                        }
                    }
                }
            }
        }

        //if ((sCellText.Length > 2) && (sCellText[0] == '{') && (sCellText[sCellText.Length-1] == '}'))


        #endregion 公有方法
    }
}
