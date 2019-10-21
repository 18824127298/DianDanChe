using System;
using System.Collections.Generic;
using System.Text;

using Sigbit.Common;

using GemBox.Spreadsheet;
//using GemBox.ExcelLite;


namespace Sigbit.Common.WordProcess.Excel
{
    /// <summary>
    /// 模板定义类
    /// </summary>
    public class ExcelExportTemplateSetting
    {

        #region 公共属性

        /// <summary>
        /// 模板头行数
        /// </summary>
        private int _headerLineCount = 0;

        /// <summary>
        /// 模板头行数
        /// </summary>
        public int HeaderLineCount
        {
            get { return _headerLineCount; }
        }

        /// <summary>
        /// 模板数据行数
        /// </summary>
        private int _dataLineCount = 0;

        /// <summary>
        /// 模板数据行数
        /// </summary>
        public int DataLineCount
        {
            get { return _dataLineCount; }
        }

        /// <summary>
        /// 模板尾行数
        /// </summary>
        private int _footerLineCount = 0;

        /// <summary>
        /// 模板尾行数
        /// </summary>
        public int FooterLineCount
        {
            get { return _footerLineCount; }
        }

        /// <summary>
        /// 空白列数据
        /// </summary>
        private int _fixColCount = 0;

        /// <summary>
        /// 空白列数据
        /// </summary>
        public int FixColCount
        {
            get { return _fixColCount; }
        }

        /// <summary>
        /// 模板列数
        /// </summary>
        private int _columnCount = 0;

        /// <summary>
        /// 模板列数
        /// </summary>
        public int ColumnCount
        {
            get { return _columnCount; }
        }


        #endregion 公共属性

        #region 公共方法

        /// <summary>
        /// 载入ExcelWorksheet模板设置
        /// </summary>
        /// <param name="ew"></param>
        public void LoadSetting(ExcelWorksheet ew)
        {
            //1.初始化变量
            string sPrarmName = "";
            string sPrarmValue = "";

            //2.循环获取模板设定
            for (int i = 0; i < ew.Rows.Count; i++)
            {
                //3.获取模板数据
                try
                {
                    sPrarmName = ew.Rows[i].Cells[0].Value.ToString();
                    sPrarmName = sPrarmName.ToLower().Trim();

                    sPrarmValue = ew.Rows[i].Cells[1].Value.ToString();
                    sPrarmValue = sPrarmValue.Trim();

                    //4.应用模板数据
                    switch (sPrarmName)
                    {
                        case "头部数据行数":
                            _headerLineCount = ConvertUtil.ToInt(sPrarmValue);
                            break;

                        case "数据区域行数":
                            _dataLineCount = ConvertUtil.ToInt(sPrarmValue);
                            //!!! datalinecount 必须大于3
                            break;

                        case "尾部数据行数":
                            _footerLineCount = ConvertUtil.ToInt(sPrarmValue);
                            break;

                        case "左边固定列":
                            _fixColCount = ConvertUtil.ToInt(sPrarmValue);
                            break;
                        case "模板列数":
                            _columnCount = ConvertUtil.ToInt(sPrarmValue);
                            break;
                        default:
                            throw new Exception("无效参数:" + ew.Rows[i].Cells[0].Value.ToString());

                        //case "headerlinecount":
                        //    _HeaderLineCount = ConvertUtil.ToInt(sPrarmValue);
                        //    break;

                        //case "datalinecount":
                        //    _DataLineCount = ConvertUtil.ToInt(sPrarmValue);
                        //    //!!! datalinecount 必须大于3
                        //    break;

                        //case "footerlinecount":
                        //    _FooterLineCount = ConvertUtil.ToInt(sPrarmValue);
                        //    break;

                        //case "fixcolcount":
                        //    _FixColCount = ConvertUtil.ToInt(sPrarmValue);
                        //    break;
                        //case "columncount":
                        //    _ColumnCount = ConvertUtil.ToInt(sPrarmValue);
                        //    break;
                        //default:
                        //    throw new Exception("无效参数:" + ew.Rows[i].Cells[0].Value.ToString());
                    }
                }
                catch
                {
                    throw new Exception("模板参数错误，参数行:" + (i + 1).ToString());
                }
            }
        }

        #endregion 公共方法

    }
}
