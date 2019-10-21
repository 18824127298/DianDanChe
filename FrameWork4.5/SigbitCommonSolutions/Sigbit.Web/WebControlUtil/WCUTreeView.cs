using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Sigbit.Data;

namespace Sigbit.Web.WebControlUtil
{
    public class WCUTreeView
    {
        /// <summary>
        /// 通过SQL语句初始化TreeView
        /// </summary>
        /// <param name="tv">TreeView实例</param>
        /// <param name="sSQLStr">SQL 语句</param>
        public static void InitTreeView(TreeView tv, string sSQLStr)
        {

            DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQLStr);
            InitTreeView(tv, ds);
        }
        /// <summary>
        /// 通过DataSet初始化TreeView
        /// </summary>
        /// <param name="tv">TreeView实例</param>
        /// <param name="ds">DataSet实例</param>
        public static void InitTreeView(TreeView tv, DataSet ds)
        {
            string idValue, textValue, pathValue;
            TreeNode tn = new TreeNode();

            foreach (DataRow Row in ds.Tables[0].Rows)
            {
                pathValue = Row[0].ToString();
                idValue = Row[1].ToString();
                textValue = "<a href =\"javascript:OnClickTVTest('" + Row[2].ToString() + "');\" >" + Row[2].ToString() + "</a>";

                //把根节点的路径
                //pathValue = pathValue.Replace(",0,", "");

                if (pathValue == "0")
                {
                    tv.Nodes.Add(new TreeNode(textValue, idValue));
                }
                else
                {
                    //去掉最后的,
                    //pathValue = pathValue.Remove(pathValue.Length - 1);
                    tn = tv.FindNode(pathValue);

                    if (tn != null)
                    {
                        tn.ImageUrl = "../Images/Dept_Close.gif";
                        tn.ChildNodes.Add(new TreeNode(textValue, idValue));
                    }
                }

            }

        }
    }
}
