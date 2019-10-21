using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Sigbit.Data;

using Sigbit.Common;

namespace Sigbit.Web.WebControlUtil
{
    /// <summary>
    /// ComboBox的操作函数集
    /// </summary>
    public class WCUComboBox
    {
        /// <summary>
        /// 根据CodeTable初始化ComboBox
        /// </summary>
        /// <param name="combo">ComboBox</param>
        /// <param name="codeTable">CodeTable</param>
        public static void InitComboBox(DropDownList combo, CodeTableBase codeTable)
        {
            combo.Items.Clear();

            for (int i = 0; i < codeTable.Count; i++)
            {
                string sCode = codeTable.GetCode(i);
                string sDes = codeTable.GetDes(i);
                combo.Items.Add(new ListItem(sDes, sCode));
            }

            if (codeTable.DefaultCode != "")
                combo.SelectedValue = codeTable.DefaultCode;
        }
    }
}
//======================= 注释ByOldix，2007.5.4，用其它方式实现 ====
///// <summary>
///// 通过SQL语句初始化Combbox控件
///// </summary>
///// <param name="Combbox">Combbox实例</param>
///// <param name="sSQLStr">SQL语句</param>
//public static void InitCombBox(DropDownList Combbox, string sSQLStr)
//{
//    DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQLStr);
//    InitCombBox(Combbox, ds);
//}

///// <summary>
///// 通过DataSet初始化Combbox
///// </summary>
///// <param name="Combbox">Combbox实例</param>
///// <param name="ds">DataSet实例</param>
//public static void InitCombBox(DropDownList Combbox, DataSet ds)
//{
//    if (ds != null)
//    {
//        Combbox.Items.Clear();
//        foreach (DataRow Row in ds.Tables[0].Rows)
//        {
//            ListItem Item = new ListItem();
//            Item.Text = Row[0].ToString();
//            Item.Value = Row[1].ToString();
//            Combbox.Items.Add(Item);
//        }
//    }
//}


//public static void InitCombBox(DropDownList Combbox, Dictionary<string, string> List)
//{
//    Combbox.Items.Clear();
//    foreach (KeyValuePair<string, string> OptionItem in List)
//    {
//        ListItem Item = new ListItem();
//        Item.Text = OptionItem.Value.ToString();
//        Item.Value = OptionItem.Key.ToString();
//        Combbox.Items.Add(Item);
//    }
//}

//public static void InitCombBoxGender(DropDownList Combbox)
//{
//    Dictionary<string, string> List = new Dictionary<string, string>();
//    List.Add("F", "女");
//    List.Add("M", "男");
//    InitCombBox(Combbox, List);
//}

//public static void InitCombBoxYesNo(DropDownList Combbox)
//{
//    Dictionary<string, string> List = new Dictionary<string, string>();
//    List.Add("Y", "是");
//    List.Add("N", "否");
//    InitCombBox(Combbox, List);
//}
