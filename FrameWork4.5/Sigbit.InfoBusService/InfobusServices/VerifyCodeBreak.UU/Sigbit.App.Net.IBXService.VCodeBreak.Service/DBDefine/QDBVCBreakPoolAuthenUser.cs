using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Collections;

using Sigbit.Common;
using Sigbit.Data;
using Sigbit.Web;

namespace Sigbit.App.Net.IBXService.VCodeBreak.Service.DBDefine
{
    public class QDBVCBreakPoolAuthenUser
    {
        #region 容器
        private ArrayList _arrAuthenUser = new ArrayList();
        private Hashtable _htAuthenUserUid = new Hashtable();
        private Hashtable _htAuthenUserName = new Hashtable();
        #endregion

        #region 构造函数
        public QDBVCBreakPoolAuthenUser()
        {
            string sSQL = "select * from vcb_authen_user order by authen_user_name";
            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQL);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TbAuthenUser tblAuthenUser = new TbAuthenUser();
                tblAuthenUser.AssignByDataRow(ds, i);

                _arrAuthenUser.Add(tblAuthenUser);
                _htAuthenUserUid[tblAuthenUser.AuthenUserUid] = tblAuthenUser;
                _htAuthenUserName[tblAuthenUser.AuthenUserName] = tblAuthenUser;
            }
        }
        #endregion

        #region 基础操作
        public int AuthenUserCount
        {
            get
            {
                return _arrAuthenUser.Count;
            }
        }

        public TbAuthenUser GetAuthenUserRec(int nIndex)
        {
            return (TbAuthenUser)_arrAuthenUser[nIndex];
        }

        public TbAuthenUser GetAuthenUserRec(string sAuthenUserUid)
        {
            return (TbAuthenUser)_htAuthenUserUid[sAuthenUserUid];
        }

        public TbAuthenUser GetAuthenUserRecByUserName(string sUserName)
        {
            return (TbAuthenUser)_htAuthenUserName[sUserName];
        }
        #endregion

        #region 定位查找

        public bool IsValidUserAndPassword(string sUserName, string sPassword)
        {
            TbAuthenUser tblAutherUser = GetAuthenUserRecByUserName(sUserName);
            if (tblAutherUser == null)
                return false;

            if (tblAutherUser.AuthenPassword != sPassword)
                return false;

            return true;
        }

        //public string GetAuthenUserNameByID(string sAuthenUserUid)
        //{
        //    TbAuthenUser tbl = GetAuthenUserRec(sAuthenUserUid);

        //    if (tbl == null)
        //        return sAuthenUserUid;
        //    else
        //        return tbl.AuthenUserName;
        //}
        #endregion

        #region CodeTable
        //private CodeTable _codeTableOfAll = null;
        //public CodeTable CodeTableOfAll
        //{
        //    get
        //    {
        //        if (_codeTableOfAll == null)
        //        {
        //            _codeTableOfAll = new CodeTable();

        //            for (int i = 0; i < this.AuthenUserCount; i++)
        //            {
        //                TbAuthenUser tblAuthenUser = GetAuthenUserRec(i);
        //                _codeTableOfAll.AddItem(tblAuthenUser.AuthenUserUid, tblAuthenUser.AuthenUserName);
        //            }
        //        }
        //        return _codeTableOfAll;
        //    }
        //}
        #endregion
    }
}
