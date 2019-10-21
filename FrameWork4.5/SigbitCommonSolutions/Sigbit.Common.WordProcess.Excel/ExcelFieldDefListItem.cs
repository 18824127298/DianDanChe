using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GemBox.Spreadsheet;
//using GemBox.ExcelLite;
namespace Sigbit.Common.WordProcess.Excel
{
    
    /// <summary>
    /// 字段详信息
    /// </summary>
    public class ExcelFieldDefListItem
    {
        #region 私有属性

        /// <summary>
        /// 字段名称
        /// </summary>
        private string _fieldName = "";

        /// <summary>
        /// 中文字段名称
        /// </summary>
        private string _fieldChsName = "";
        
        /// <summary>
        /// 字段顺序
        /// </summary>
        private int _order = 0;
        
        /// <summary>
        /// 显示宽度
        /// </summary>
        private int _width = 0;

        private ExcelDataType _dataType = ExcelDataType.Undefine;
        /// <summary>
        /// 数据类型
        /// </summary>
        public ExcelDataType DataType
        {
            get { return _dataType; }
            set { _dataType = value; }
        }


        
        ///// <summary>
        ///// 字符宽度
        ///// </summary>
        //private int _MaxTextLength = 0;

        #endregion 私有属性

        #region 私有方法

        #endregion 私有方法

        #region 公共属性

        /// <summary>
        /// 字段名称
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
        /// 字段中文名称
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
        /// 字段循序
        /// </summary>
        public int Order
        {
            get { return _order; }
            set { _order = value; }
        }

        /// <summary>
        /// 显示宽度
        /// </summary>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }


        ///// <summary>
        ///// 字符串的宽度
        ///// </summary>
        //public int MaxTextLength
        //{
        //    get { return _MaxTextLength; }
        //    set { _MaxTextLength = value; }
        //}

        #endregion 公共属性

        #region 公共方法

        #endregion 公共方法

    }
}
