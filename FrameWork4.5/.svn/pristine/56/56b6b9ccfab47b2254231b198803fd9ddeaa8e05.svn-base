using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Sigbit.Data;

namespace Sigbit.Framework
{
    public class ___SbtDBPool
    {
        private static TbSysParameter _currentSystemMenu = null;
        /// <summary>
        /// 当前系统使用的菜单
        /// </summary>  
        public static TbSysParameter CurrentSystemMenu
        {
            get
            {
                if (_currentSystemMenu == null)
                {
                    string sSQL = "SELECT * FROM sbt_user_parameter ";
                    sSQL += " where application_name = 'CurrentMenu' ";
                    DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);
                    _currentSystemMenu = new TbSysParameter();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        _currentSystemMenu.AssignByDataRow(ds.Tables[0].Rows[0]);
                    }
                    else
                    {
                        _currentSystemMenu.ParameterValue = "main_menu";
                    }
                }
                return _currentSystemMenu;
            }
        }
    }
}
