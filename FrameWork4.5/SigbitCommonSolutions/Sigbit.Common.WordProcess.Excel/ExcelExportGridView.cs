using System;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Sigbit.Common.WordProcess.Excel
{
    /// <summary>
    /// 导出DataSet数据源
    /// </summary>
    public class ExcelExportGridView : ExcelExportCustom
    {

        #region 公共属性

        /// <summary>
        /// 输入DataSet
        /// </summary>
        private GridView _inputGridView = null;

        public GridView InputGridView
        {
            get { return _inputGridView; }
            set { _inputGridView = value; }
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

            for (int i = 0; i < _inputGridView.Rows.Count; i++)
            {
                for (int j = 0; j < _inputGridView.Columns.Count; j++)
                {
                    if (_inputGridView.Rows[1].Cells[j].Controls.Count == 0)
                    {
                        sCellStr = _inputGridView.Rows[i].Cells[j].Text;
                    }
                    else
                    {
                        if ((_inputGridView.Rows[i].Cells[j].Controls[1] !=null) &&(_inputGridView.Rows[i].Cells[j].Controls[1] is Label))
                        {
                            sCellStr = (_inputGridView.Rows[i].Cells[j].Controls[1] as Label).Text;
                        }
                        else
                        {
                            sCellStr = "";
                        }
                    }

                    sCellStr = FormatCell(GetDataFieldName(j), j, sCellStr);
                    sUserDefValue = GetUserDefValue(i, j);
                    if (sUserDefValue != null)
                    {
                        sCellStr = sUserDefValue;
                    }

                    _ef.Worksheets[0].Cells[_exportRowIndex + i, nBeginCol + j].Value = sCellStr;
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
            if (_inputGridView.DataSource.GetType() == typeof(DataSet))
            {
                DataSet ds = (DataSet)_inputGridView.DataSource;
                return ds.Tables[0].Columns[nFieldIndex].ColumnName;
            }
            else if (_inputGridView.DataSource.GetType() == typeof(DataTable))
            {
                DataTable dt = (DataTable)_inputGridView.DataSource;
                return dt.Columns[nFieldIndex].ColumnName;
            }
            else
            {
                throw new Exception("无效的数据源");
            }
            
        }

        override protected string FormatCell(string sFieldName, int nFieldIndex, string sValue)
        {
            return sValue;
        }

        #endregion 重载函数
    }
}
