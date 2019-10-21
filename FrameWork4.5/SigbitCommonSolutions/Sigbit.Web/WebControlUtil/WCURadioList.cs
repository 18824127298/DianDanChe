using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

using Sigbit.Common;
using Sigbit.Data;

namespace Sigbit.Web.WebControlUtil
{
    /// <summary>
    /// RadioList�Ĳ���������
    /// </summary>
    public class WCURadioList
    {
        /// <summary>
        /// ����CodeTable��ʼ��RadioList
        /// </summary>
        /// <param name="combo">RadioList</param>
        /// <param name="codeTable">CodeTable</param>
        public static void InitRadioList(RadioButtonList radioList, CodeTableBase codeTable)
        {
            radioList.Items.Clear();

            for (int i = 0; i < codeTable.Count; i++)
            {
                string sCode = codeTable.GetCode(i);
                string sDes = codeTable.GetDes(i);
                radioList.Items.Add(new ListItem(sDes, sCode));
            }

            if (codeTable.DefaultCode != "")
                radioList.SelectedValue = codeTable.DefaultCode;
        }
    }
}
//======================= ע��ByOldix��2007.5.4����������ʽʵ�� ====
///// <summary>
///// ͨ��SQL����ʼ��Combbox�ؼ�
///// </summary>
///// <param name="Combbox">Combboxʵ��</param>
///// <param name="sSQLStr">SQL���</param>
//public static void InitCombBox(DropDownList Combbox, string sSQLStr)
//{
//    DataSet ds = DataHelper.Instance.ExecuteDataSet(sSQLStr);
//    InitCombBox(Combbox, ds);
//}

///// <summary>
///// ͨ��DataSet��ʼ��Combbox
///// </summary>
///// <param name="Combbox">Combboxʵ��</param>
///// <param name="ds">DataSetʵ��</param>
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
//    List.Add("F", "Ů");
//    List.Add("M", "��");
//    InitCombBox(Combbox, List);
//}

//public static void InitCombBoxYesNo(DropDownList Combbox)
//{
//    Dictionary<string, string> List = new Dictionary<string, string>();
//    List.Add("Y", "��");
//    List.Add("N", "��");
//    InitCombBox(Combbox, List);
//}