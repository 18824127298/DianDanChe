using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;

namespace Sigbit.Common
{
    /// <summary>
    /// 代码表中的一项，记录代码和描述的关系
    /// </summary>
    class CodeTableItem
    {
        string _code = "";
        /// <summary>
        /// 代码
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }

        string _des = "";
        /// <summary>
        /// 描述
        /// </summary>
        public string Des
        {
            get { return _des; }
            set { _des = value; }
        }
    }

    /// <summary>
    /// 代码表
    /// </summary>
    public class CodeTableBase
    {
        /// <summary>
        /// 数据的存储位置
        /// </summary>
        private ArrayList _list = new ArrayList();

        /// <summary>
        /// 加入一项
        /// </summary>
        /// <param name="sCode">代码</param>
        /// <param name="sDes">描述</param>
        public void AddItem(string sCode, string sDes)
        {
            CodeTableItem item = new CodeTableItem();
            item.Code = sCode;
            item.Des = sDes;

            _list.Add(item);
        }

        /// <summary>
        /// 根据描述得到相应的代码
        /// </summary>
        /// <param name="sDes">描述</param>
        /// <returns>代码</returns>
        public string GetCodeByDes(string sDes)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                CodeTableItem item = (CodeTableItem)_list[i];
                if (item.Des == sDes)
                    return item.Code;
            }

            return "--undefined--";
        }

        /// <summary>
        /// 得到代码的相关描述
        /// </summary>
        /// <param name="sCode">代码</param>
        /// <returns>描述</returns>
        public string GetDesByCode(string sCode)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                CodeTableItem item = (CodeTableItem)_list[i];
                if (item.Code == sCode)
                    return item.Des;
            }

            return sCode;
        }

        /// <summary>
        /// 由索引得到代码
        /// </summary>
        /// <param name="nIndex">索引号</param>
        /// <returns>代码</returns>
        public string GetCode(int nIndex)
        {
            CodeTableItem item = (CodeTableItem)_list[nIndex];
            return item.Code;
        }

        /// <summary>
        /// 由索引得到描述
        /// </summary>
        /// <param name="nIndex">索引号</param>
        /// <returns>描述</returns>
        public string GetDes(int nIndex)
        {
            CodeTableItem item = (CodeTableItem)_list[nIndex];
            return item.Des;
        }

        #region default
        /// <summary>
        /// 缺省的下标
        /// </summary>
        private int _nDefaultIndex = 0;

        /// <summary>
        /// 缺省的下标
        /// </summary>
        public int DefaultIndex
        {
            get
            {
                return _nDefaultIndex;
            }
        }

        /// <summary>
        /// 缺省的代码
        /// </summary>
        public string DefaultCode
        {
            get
            {
                if (_list.Count == 0)
                    return "";
                CodeTableItem item = (CodeTableItem)_list[_nDefaultIndex];
                return item.Code;
            }
            set
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    CodeTableItem item = (CodeTableItem)_list[i];
                    if (item.Code == value)
                    {
                        _nDefaultIndex = i;
                        return;
                    }
                }

                throw new Exception("CodeTable.SetDefaultByCode Error: 未找到Code - " + value);
            }
        }

        /// <summary>
        /// 缺省的描述
        /// </summary>
        public string DefaultDes
        {
            get
            {
                CodeTableItem item = (CodeTableItem)_list[_nDefaultIndex];
                return item.Des;
            }
            set
            {
                for (int i = 0; i < _list.Count; i++)
                {
                    CodeTableItem item = (CodeTableItem)_list[i];
                    if (item.Des == value)
                    {
                        _nDefaultIndex = i;
                        return;
                    }
                }

                throw new Exception("CodeTable.SetDefaultByDes Error: 未找到Des - " + value);
            }
        }
        #endregion default

        /// <summary>
        /// 得到代码项的数量
        /// </summary>
        /// <returns>代码项的数量</returns>
        public int Count
        {
            get
            {
                return _list.Count;
            }
        }

        /// <summary>
        /// 清零
        /// </summary>
        public void Clear()
        {
            _list.Clear();
            _nDefaultIndex = 0;
        }
    }
}
