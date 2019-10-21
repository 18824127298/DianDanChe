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
    /// ����ʱ�ַ�������ʾ���
    /// </summary>
    public class ExcelChatWidth
    {   
        //һ������610
        //1000����=3.14
        //һ�����ĵ���һ����д��"W" = 1.86
        //һ��"l"= 0.58���,Ӣ����Ҫƽ��һ��. 
        public const int ChsWidth = 540;
        public const int EngWidth = 200;
    }
    
    /// <summary>
    /// Excell ������
    /// </summary>
    public abstract class ExcelExportCustom
    {
        //#region ί������
        ///// <summary>
        ///// �û��Զ����ʽ����Ԫ��
        ///// </summary>
        ///// <param name="sFieldName">�ֶ�����</param>
        ///// <param name="vFieldValue">�ֶ�����</param>
        //public delegate void EventFormatCell(string sFieldName, ref string vFieldValue);
        //public event EventFormatCell OnFormatCell = null;
        //private void 
        //#endregion 


        #region ˽������
        /// <summary>
        /// ����Excel�ļ���
        /// </summary>
        private string _exportFileName = "";

        /// <summary>
        /// ������Sheet��
        /// </summary>
        private string _sheetName = "";

        /// <summary>
        /// �����ı���
        /// </summary>
        private string _titleText = "";

        /// <summary>
        /// ģ���ļ�
        /// </summary>
        private string _templateFile = "";

        private Hashtable htUserDefValue = new Hashtable();


        #endregion ˽������

        #region ��������Ա����
        /// <summary>
        /// Excel����
        /// </summary>
        protected ExcelFile _ef = new ExcelFile();

        /// <summary>
        /// �ֶζ���
        /// </summary>
        protected ExcelFieldDefList _fieldDefList = null;

        /// <summary>
        /// ����ģ��
        /// </summary>
        protected ExcelExportTemplate _template = null;

        /// <summary>
        /// ��������
        /// </summary>
        protected int _dataRowCount = 0;

        /// <summary>
        /// ��������
        /// </summary>
        protected int _dataColCount = 0;

        /// <summary>
        /// �Ƿ񸲸������ļ�
        /// </summary>
        protected bool _isOverWriteFile = true;

        /// <summary>
        /// �Ƿ���Ҫ��̬��������
        /// </summary>
        //protected bool bIsDynamicCol = false;

        /// <summary>
        /// 
        /// </summary>
        protected int _exportRowIndex = 0;

        /// <summary>
        /// ��ǰ����sheet
        /// </summary>

        #endregion ��������Ա����

        #region ˽�к���


        /// <summary>
        /// ���������
        /// </summary>
        /// <remarks>û��ģ��������</remarks>
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
        /// ��ʼ��WorkSheet
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

            //���ӱ�����
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
        //    //����Header
        //    int nEndColumnIndex = nDataColCount - 1;
        //    CellRange range = template.Worksheet.Cells.GetSubrangeAbsolute(0, 0, template.Setting.HeaderLineCount - 1, nEndColumnIndex);
        //    range.CopyTo(ef.Worksheets[0], 0, 0);

        //    //����������
        //    //����Ҫ���һ��������
        //    range = template.Worksheet.Cells.GetSubrangeAbsolute(template.Setting.HeaderLineCount + 1, 0, template.Setting.HeaderLineCount + 2, nEndColumnIndex);
        //    for (int i = 0; i <=nDataRowCount; i++)
        //    {
        //        range.CopyTo(ef.Worksheets[0], template.Setting.HeaderLineCount + i, 0);
        //    }

        //    //����Footer
        //    range = template.Worksheet.Cells.GetSubrangeAbsolute(template.Setting.HeaderLineCount + template.Setting.DataLineCount, 0,
        //        template.Setting.HeaderLineCount + template.Setting.DataLineCount + template.Setting.FooterLineCount - 1, nEndColumnIndex);
        //    range.CopyTo(ef.Worksheets[0], template.Setting.HeaderLineCount + nDataRowCount + 1, 0);
        //}

        /// <summary>
        /// ��ʼ���ֶ�����
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

        #endregion ˽�к���

        #region ��������
        
        /// <summary>
        /// ���������
        /// </summary>
        /// 
        protected void ExportFieldTitle()
        {
            string sFieldTitle = "";
            //string sFieldChsTitle = "";

            //.ѡ������
            if (_fieldDefList == null)
            {
                throw new Exception("û��ָ������ֶ�");
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

            ////�ж��Ƿ�Ϊȫ������
            //if (!IsDynamicCol)
            //{
            //    //1.ȫ������
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
            //    //2.ѡ������
            //    if (fieldDefList == null)
            //    {
            //        throw new Exception("û��ָ������ֶ�");
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
        /// ������
        /// </summary>
        /// <param name="sStr">�ַ���</param>
        /// <returns>�ַ����Ŀ��</returns>
        /// <remarks>
        /// ������㷽���ܲ�׼ȷ,ֻ�ܴ�ŵļ���һ��,��Ŀ����30���ַ�
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
        #endregion ��������

        #region �����غ���

        ///// <summary>
        ///// ����ȫ��DataSet
        ///// </summary>
        //abstract protected void ExportFullExcel();

        ///// <summary>
        ///// ���������ֶε�DataSet
        ///// </summary>
        //abstract protected void ExportDefineExcel();

        abstract protected void ExportExcelData();

       
        /// <summary>
        /// ��ȡ�ֶε�����
        /// </summary>
        /// <param name="nIndex"></param>
        abstract protected string GetDataFieldName(int nIndex);

        abstract protected string FormatCell(string sFieldName, int nFieldIndex, string sValue);


        /// <summary>
        /// ����DataSet����Դ������
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


        #region ��������
        /// <summary>
        /// ����Excel�ļ���
        /// </summary>
        public string ExportFileName
        {
            get { return _exportFileName; }
            set { _exportFileName = value; }
        }


        /// <summary>
        /// ������Sheet��
        /// </summary>
        public string SheetName
        {
            get { return _sheetName; }
            set { _sheetName = value; }
        }

        /// <summary>
        /// �����ı���
        /// </summary>
        public string TitleText
        {
            get { return _titleText; }
            set { _titleText = value; }
        }

        /// <summary>
        /// �Ƿ���Ҫ��̬��������
        /// </summary>
        //public bool IsDynamicCol
        //{
        //    get { return bIsDynamicCol; }
        //    set { bIsDynamicCol = value; }
        //}

        ///// <summary>
        ///// �Ƿ񸲸������ļ�
        ///// </summary>
        //public bool IsOverWriteFile
        //{
        //    get { return _IsOverWriteFile; }
        //    set { _IsOverWriteFile = value; }
        //}

        /// <summary>
        /// ����ģ���ļ�
        /// </summary>
        public string TemplateFile
        {
            get { return _templateFile; }
            set 
            {                              
                //����ģ��
                _templateFile = value;
                _template = new ExcelExportTemplate();
                _template.LoadTemplate(_templateFile);
            }
        }
        #endregion ��������

        #region ��������
        
        /// <summary>
        /// �����ļ�
        /// </summary>
        public void DoExport()
        {
            _exportRowIndex = 0;
            //if ((fieldDefList == null) && (!IsDynamicCol))
            //{
            //    //��ȫ�����ģʽ�������ֶ�����
            //    InitFieldList();
            //}

            if (_fieldDefList == null)
            {
                InitFieldList();
            }

            //1.����ļ�����·��
            if (File.Exists(_exportFileName) && (!_isOverWriteFile))
            {
                throw new Exception("�ļ��Ѿ�����:" + _exportFileName);
            }

            string sFilePath = Path.GetDirectoryName(_exportFileName);
            if (!Directory.Exists(sFilePath))
            {
                throw new Exception("Ŀ¼������:" + sFilePath);
            }

            //2.���ȫ����Sheet
            while (_ef.Worksheets.Count > 0)
            {
                _ef.Worksheets[0].Delete();
            }
            

            //3.����һ���µ�Sheet
            if (_sheetName == "")
            {
                _ef.Worksheets.Add("�����ļ�");
            }
            else
            {
                _ef.Worksheets.Add(_sheetName);
            }

            //4.��������
            ExportExcel();

            //5.�����ļ�
            _ef.SaveXls(_exportFileName);
        }
    
        /// <summary>
        /// ���ӵ����ֶ���
        /// </summary>
        /// <param name="sFieldName">�ֶ�����</param>
        /// <remarks>!!!�����������ֶ��Ƿ������ݼ���Ч��</remarks>
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
                throw new Exception("�ֶ������Ѿ����:" + sFieldName);
            }
        }

        /// <summary>
        /// ���ӵ����ֶ�����
        /// </summary>
        /// <param name="nFieldIndex">�ֶ�����</param>
        public void AddFieldIndex(int nFieldIndex)
        {
            if (nFieldIndex < _dataColCount)
            {
                string sFieldName = GetDataFieldName(nFieldIndex);
                AddFieldName(sFieldName);
            }
            else
            {
                throw new Exception("�ֶ��±곬��:" + nFieldIndex.ToString());
            }
        }

        /// <summary>
        /// ���������ֶ�����
        /// </summary>
        /// <param name="sFiledName">�ֶ�����</param>
        /// <param name="sChineseName">��������</param>
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
                throw new Exception("�ֶ���������:" + sFiledName);
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
                throw new Exception("�ֶ��±곬��:" + nFieldIndex);
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
                throw new Exception("�ֶ���������:" + sFiledName);
            }


        }

        /// <summary>
        /// ���������ֶ�����
        /// </summary>
        /// <param name="nFieldIndex">�ֶ�����</param>
        /// <param name="sChineseName">��������</param>
        public void SetFieldChsName(int nFieldIndex,string sChineseName)
        {
            if (nFieldIndex < _dataColCount)
            {
                string sFieldName = GetDataFieldName(nFieldIndex);
                SetFieldChsName(sFieldName, sChineseName);
            }
            else
            {
                throw new Exception("�ֶ��±곬��:" + nFieldIndex.ToString());
            }
        }

        /// <summary>
        /// �趨һ�����е�ֵ
        /// </summary>
        /// <param name="nRow">��-��1��ʼ</param>
        /// <param name="nCol">��-��1��ʼ</param>
        /// <param name="sValue">����</param>
        public void SetCellValue(int nRow, int nCol, string sValue)
        {
            //if ((nRow > nDataRowCount + template.Setting.HeaderLineCount + template.Setting.FooterLineCount) || (nRow < 1))
            if ((nRow > _dataRowCount) || (nRow < 1))
            {
                throw new Exception("���õ�Ԫ�������г���:" + nRow.ToString());
            }

            if ((nCol > _dataColCount) || (nCol < 1))
            {
                throw new Exception("���õ�Ԫ�������г���:" + nCol.ToString());
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
            //    //��ʼ���Զ�������
            //    arrCellValue = new string[nDataRowCount, nDataColCount];
            //}

            //if ((nRow > nDataRowCount) || (nRow < 1))
            //{
            //    throw new Exception("���õ�Ԫ�������г���:" + nRow.ToString());
            //}

            //if ((nCol > nDataColCount) || (nCol < 1))
            //{
            //    throw new Exception("���õ�Ԫ�������г���:" + nCol.ToString());
            //}

            //arrCellValue[nRow - 1, nCol - 1] = sValue;
        
        }

        /// <summary>
        /// �����滻����
        /// </summary>
        /// <param name="sIndicator">��ʶ</param>
        /// <param name="sText">�滻�ı�</param>
        public void SetSingleText(string sIndicator, string sText)
        {
            _template.IndicatorList.Add(sIndicator, sText);
        }

        #endregion ��������
    }

}
