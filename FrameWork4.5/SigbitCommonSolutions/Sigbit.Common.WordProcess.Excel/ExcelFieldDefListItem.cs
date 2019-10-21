using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GemBox.Spreadsheet;
//using GemBox.ExcelLite;
namespace Sigbit.Common.WordProcess.Excel
{
    
    /// <summary>
    /// �ֶ�����Ϣ
    /// </summary>
    public class ExcelFieldDefListItem
    {
        #region ˽������

        /// <summary>
        /// �ֶ�����
        /// </summary>
        private string _fieldName = "";

        /// <summary>
        /// �����ֶ�����
        /// </summary>
        private string _fieldChsName = "";
        
        /// <summary>
        /// �ֶ�˳��
        /// </summary>
        private int _order = 0;
        
        /// <summary>
        /// ��ʾ���
        /// </summary>
        private int _width = 0;

        private ExcelDataType _dataType = ExcelDataType.Undefine;
        /// <summary>
        /// ��������
        /// </summary>
        public ExcelDataType DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }


        
        ///// <summary>
        ///// �ַ����
        ///// </summary>
        //private int _MaxTextLength = 0;

        #endregion ˽������

        #region ˽�з���

        #endregion ˽�з���

        #region ��������

        /// <summary>
        /// �ֶ�����
        /// </summary>
        public string FieldName
        {
            get { return _fieldName; }
            set 
            {
                int nWidth = value.Length * ExcelChatWidth.EngWidth;
                _width = nWidth;

                //if (_Width < nWidth)
                //{
                //    _Width = nWidth;
                //}

                _fieldName = value; 
            }
        }

        /// <summary>
        /// �ֶ���������
        /// </summary>
        public string FieldChsName
        {
            get { return _fieldChsName; }
            set 
            {
                int nWidth = value.Length * ExcelChatWidth.ChsWidth;
                _width = nWidth;
                
                //if (_Width < nWidth)
                //{
                //    _Width = nWidth;
                //}
                _fieldChsName = value; 
            }
        }

        /// <summary>
        /// �ֶ�ѭ��
        /// </summary>
        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        /// <summary>
        /// ��ʾ���
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }


        ///// <summary>
        ///// �ַ����Ŀ��
        ///// </summary>
        //public int MaxTextLength
        //{
        //    get { return _MaxTextLength; }
        //    set { _MaxTextLength = value; }
        //}

        #endregion ��������

        #region ��������

        #endregion ��������

    }
}
