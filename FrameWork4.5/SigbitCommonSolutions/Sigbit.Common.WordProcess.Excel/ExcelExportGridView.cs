using System;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Sigbit.Common.WordProcess.Excel
{
    /// <summary>
    /// ����DataSet����Դ
    /// </summary>
    public class ExcelExportGridView : ExcelExportCustom
    {

        #region ��������

        /// <summary>
        /// ����DataSet
        /// </summary>
        private GridView _inputGridView = null;

        public GridView InputGridView
        {
            get { return _inputGridView; }
            set { _inputGridView = value; }
        }
        
        #endregion ��������

        #region ���غ���

        /// <summary>
        /// ���������ֶε�Excel�ļ�
        /// </summary>
        override protected void ExportExcelData()
        {
            int nBeginCol = 0;

            //1.�ж���ʼ��,�е�λ��
            if (_template == null)
            {
                //nBeginRow = nExportRowIndex;
                nBeginCol = 0;
                //2.�������
                ExportFieldTitle();
            }
            else
            {
                _exportRowIndex = _template.Setting.HeaderLineCount;
                nBeginCol = _template.Setting.FixColCount;
            }


            string sCellStr = "";

            //3.�������
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

                    //4.�ҵ�ÿ������ַ���
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


            //4.�����п��
            if (_template == null)
            {
                for (int j = 0; j < _fieldDefList.Count; j++)
                {
                    _ef.Worksheets[0].Columns[j].Width = _fieldDefList[j].Width;
                }
            }
        }

        /// <summary>
        /// ��ȡ���ݵ��ֶ�ֵ
        /// </summary>
        /// <param name="nFieldIndex">�ֶ����</param>
        /// <returns>�����ֶε�����</returns>
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
                throw new Exception("��Ч������Դ");
            }
            
        }

        override protected string FormatCell(string sFieldName, int nFieldIndex, string sValue)
        {
            return sValue;
        }

        #endregion ���غ���
    }
}
