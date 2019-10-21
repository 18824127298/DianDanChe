using System;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Sigbit.Common.WordProcess.Excel
{
    /// <summary>
    /// 导出DataSet数据源
    /// </summary>
    public class ExcelExportDataSet : ExcelExportCustom
    {

        #region 公共属性

        /// <summary>
        /// 输入DataSet
        /// </summary>
        private DataSet _inputDataSet = null;
        
        /// <summary>
        /// 输入DataSet
        /// </summary>
        public DataSet InputDataSet
        {
            get { return _inputDataSet; }
            set 
            { 
                _inputDataSet = value;
                _dataColCount = _inputDataSet.Tables[0].Columns.Count;
                _dataRowCount = _inputDataSet.Tables[0].Rows.Count;
            }
        }


        #endregion 公共属性

        #region 重载函数

        /// <summary>
        /// 导出部分字段的Excel文件
        /// </summary>
        override protected void ExportExcelData()
        {
            int nBeginCol = 0;

            //1.判断起始行,列的位置
            if (_template == null)
            {
                //nBeginRow = nExportRowIndex;
                nBeginCol = 0;
                //2.输出标题
                ExportFieldTitle();
            }
            else
            {
                _exportRowIndex = _template.Setting.HeaderLineCount;
                nBeginCol = _template.Setting.FixColCount;
            }


            string sCellStr = "";

            //3.输出数据
            string sUserDefValue;

            for (int i = 0; i < _dataRowCount; i++)
            {
                for (int j = 0; j < _fieldDefList.Count; j++)
                {
                    //sCellStr = InputDataSet.Tables[0].Rows[i][j].ToString();

                    sCellStr = InputDataSet.Tables[0].Rows[i][_fieldDefList[j].FieldName].ToString();

                    sCellStr = FormatCell(_fieldDefList[j].FieldName, j, sCellStr);

                    sUserDefValue = GetUserDefValue(i, j);
                    if (sUserDefValue != null)
                    {
                        sCellStr = sUserDefValue;
                    }

                    //if ((arrCellValue != null) && (arrCellValue[i,j] != null))
                    //{
                    //    sCellStr = arrCellValue[i, j];
                    //}

                    Object objValue = sCellStr;
                    try
                    {
                        switch (_fieldDefList[j].DataType)
                        {
                            case ExcelDataType.Int:
                                objValue = Convert.ToInt32(objValue);
                                break;
                            case ExcelDataType.Float :
                                objValue = Convert.ToDouble(objValue);
                                break;
                            case ExcelDataType.DateTime:
                                sCellStr = DateTimeUtil.Now;
                                objValue = DateTime.Parse(sCellStr);
                                break;
                        }
                    }
                    catch
                    {

                    }

                    _ef.Worksheets[0].Cells[_exportRowIndex + i, nBeginCol + j].Value = objValue;
                    if (_template == null)
                    {
                        _ef.Worksheets[0].Cells[_exportRowIndex + i, nBeginCol + j].Style.WrapText = true;
                    }


                    //4.找到每列最长的字符串
                    if (_template == null)
                    {
                        int sCellStrWidth = CalcTextWidth(sCellStr);
                        if (_fieldDefList[j].Width < sCellStrWidth)
                        {
                            _fieldDefList[j].Width = sCellStrWidth;
                        }
                    }
                }
            }

            //4.设置列宽度
            if (_template == null)
            {
                for (int j = 0; j < _fieldDefList.Count; j++)
                {
                    _ef.Worksheets[0].Columns[j].Width = _fieldDefList[j].Width;
                }
            }
        }

        /// <summary>
        /// 获取数据的字段值
        /// </summary>
        /// <param name="nFieldIndex">字段序号</param>
        /// <returns>返回字段的名称</returns>
        override protected string GetDataFieldName(int nFieldIndex)
        {
            return _inputDataSet.Tables[0].Columns[nFieldIndex].ColumnName;
        }

        override protected string FormatCell(string sFieldName, int nFieldIndex, string sValue)
        {
            return sValue;
        }

        #endregion 重载函数
    }
}
