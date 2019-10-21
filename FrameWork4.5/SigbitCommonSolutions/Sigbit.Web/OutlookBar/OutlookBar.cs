using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;

using System.Data.SqlClient;
using System.Xml;
using Sigbit.Data;
using Sigbit.Common;


namespace Sigbit.Web.OutLookBar
{
    /// <summary>
    /// Outlook Bar 类
    /// </summary>
    public class Outlookbar
    {
        private ArrayList _HeaderCaptions = new ArrayList();
        /// <summary>
        /// 提供分组的标题
        /// </summary>
        public ArrayList HeaderCaptions
        {
            get { return _HeaderCaptions; }
            set { _HeaderCaptions = value; }
        }

        /// <summary>
        /// 生成HTML语句
        /// </summary>
        /// <returns>HTML语句</returns>
        public string DoGenerate()
        {
            int nHeaderMenuId;
            string sHeaderMenuName;
            string sMenuId;
            string sMenuName;
            string sMenuIcon;
            string sLink;
            int nLTPos;
            int nGTPos;
            string sPKName;


            OutlookbarGenerate outlookbarGen = new OutlookbarGenerate();

            //========= 1. 取出分组 ==================
            string sHeaderSQL = "select menu_code, menu_name from sbt_sys_menu where parent_menu_code = 0 order by list_order";
            DataTable qryHeader = DataHelper.Instance.ExecuteDataSet(sHeaderSQL).Tables[0];


            int nHeaderRecordCount = qryHeader.Rows.Count;
            for (int nHeaderNo = 0; nHeaderNo < nHeaderRecordCount; nHeaderNo++)
            {

                nHeaderMenuId = int.Parse(qryHeader.Rows[nHeaderNo]["menu_code"].ToString());
                sHeaderMenuName = qryHeader.Rows[nHeaderNo]["menu_name"].ToString();
                _HeaderCaptions.Add(sHeaderMenuName);

                outlookbarGen.SetHeaderCaption(nHeaderNo, sHeaderMenuName);

                //======== 2. 逐个取出按钮 ===========
                //string sSQL = "select menu_code, menu_name, menu_link from sbt_sys_menu "
                //        + "where parent_menu_code =" + nHeaderMenuId.ToString() + " order by list_order";

                string sSQL = "select menu_code, menu_name, menu_icon, menu_link from sbt_sys_menu "
                        + "where parent_menu_code =" + nHeaderMenuId.ToString() + " order by list_order";

                DataTable qry = DataHelper.Instance.ExecuteDataSet(sSQL).Tables[0];

                int nRecordCount = qry.Rows.Count;
                for (int i = 0; i < nRecordCount; i++)
                {
                    sMenuId = qry.Rows[i]["menu_code"].ToString();
                    sMenuName = qry.Rows[i]["menu_name"].ToString();
                    sMenuIcon = qry.Rows[i]["menu_icon"].ToString();
                    sLink = qry.Rows[i]["menu_link"].ToString();

                    nLTPos = sLink.IndexOf('<'); // strpos(sLink, '<');
                    if (nLTPos > -1)
                    {
                        nGTPos = sLink.IndexOf('>');// strpos(sLink, '>');
                        if (nGTPos > nLTPos)
                        {
                            sPKName = sLink.Substring(nLTPos + 1, nGTPos - nLTPos - 1);
                            sLink = sLink.Substring(0, nLTPos) + "FK=" + sPKName + sLink.Substring(nGTPos + 1);// substr(sLink, nGTPos + 1); !!!
                        }
                    }
                    //                sLink = urlencode(sLink);

                    outlookbarGen.SetButtonCaption(nHeaderNo, i , sMenuName);
                    outlookbarGen.SetButtonImage(nHeaderNo, i, sMenuIcon);
                    outlookbarGen.SetButtonUrl(nHeaderNo, i , sLink);
                }
            }
            /*
                    outlookbarGen = new TCOutlookbarGenerate;
                    outlookbarGen->SetHeaderCaption(0, '公共信息');
                    outlookbarGen->SetButtonCaption(0, 0, '法律法规');
                    outlookbarGen->SetButtonImage(0, 0, 'ico_info_law.gif');
                    outlookbarGen->SetButtonUrl(0, 0, '/regulation/law.php?form_id=93');

                    outlookbarGen->SetButtonCaption(0, 1, '规章制度');
                    outlookbarGen->SetButtonImage(0, 1, 'ico_info_regulation.gif');
                    outlookbarGen->SetButtonUrl(0, 1, '/regulation/regulation.php?form_id=93');

                    outlookbarGen->EchoScript();
                    */
            return outlookbarGen.EchoScript();
        }
    }
}
