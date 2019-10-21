using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Sigbit.Framework;
using Sigbit.Web.JavaScipt;

public partial class framework_ctrlpanel_change_passwd : SbtPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //============ 1. 得到当前的用户及密码 =============
        TbUser tblUser = new TbUser();
        tblUser.UserUid = CurrentUser.UserUid;
        tblUser.Fetch();

        //========= 2. 得到输入的新旧密码 ===========
        string sInputOldPassword = edtOldPassword.Text.Trim();
        string sInputNewPassword = edtNewPassword.Text.Trim();
        string sInputNewPasswordConfirm = edtNewPasswordOK.Text.Trim();

        //======== 3. 判断输入的原密码是否正确 ============
        if (sInputOldPassword != tblUser.Password)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "您输入的原密码不正确，请重新输入！";
            edtOldPassword.Text = "";
            edtOldPassword.Focus();
            return;
        }

        //=========== 4. 判断输入的两个新密码是否一致 ===========
        if (sInputNewPassword != sInputNewPasswordConfirm)
        {
            lblErrMessage.Visible = true;
            lblErrMessage.Text = "您两次输入的新密码不一致，请重新输入！";
            edtNewPassword.Text = "";
            edtNewPasswordOK.Text = "";
            edtNewPassword.Focus();
            return;
        }

        SbtMessageBoxPage page = new SbtMessageBoxPage(this);
        //switch (PasswordStrength(sInputNewPassword))
        //{
        //    case Strength.Invalid:
        //        page.MessageText = "密码必须由数字符号字母构成";
        //        page.Show(); break;
        //    case Strength.Weak: page.MessageText = "密码必须由数字符号字母构成";
        //        page.Show(); break;
        //    case Strength.Normal: page.MessageText = "密码必须由数字符号字母构成";
        //        page.Show(); break;
        //    case Strength.Strong:
        //============== 5. 进行修改密码数据操作 ==============
        if (tblUser.Password != sInputNewPassword)
        {
            tblUser.Password = sInputNewPassword;
            tblUser.Update();

            lblErrMessage.Visible = false;
        }

        page.ReturnUrl = "~/framework/main/table_index/intel_view.aspx";
        page.MessageText = "密码已成功修改，您的密码已由“"
                + sInputOldPassword + "”成功修改为“" + sInputNewPassword + "”"
                + "。您以后须用新的密码登陆系统，谢谢。";
        page.Show(); 
        //break;
        //}
    }

    private enum Strength
    {
        Invalid = 0, //无效密码
        Weak = 1, //低强度密码
        Normal = 2, //中强度密码
        Strong = 3 //高强度密码
    };
    /// <summary>
    /// 计算密码强度
    /// </summary>
    /// <param name="password">密码字符串</param>
    /// <returns></returns>
    private static Strength PasswordStrength(string password)
    {
        //空字符串强度值为0
        if (password == "") return Strength.Invalid;
        //字符统计
        int iNum = 0, iLtt = 0, iSym = 0;
        foreach (char c in password)
        {
            if (c >= '0' && c <= '9') iNum++;
            else if (c >= 'a' && c <= 'z') iLtt++;
            else if (c >= 'A' && c <= 'Z') iLtt++;
            else iSym++;
        }
        if (iLtt == 0 && iSym == 0) return Strength.Weak; //纯数字密码
        if (iNum == 0 && iLtt == 0) return Strength.Weak; //纯符号密码
        if (iNum == 0 && iSym == 0) return Strength.Weak; //纯字母密码
        if (password.Length <= 6) return Strength.Weak; //长度不大于6的密码
        if (iLtt == 0) return Strength.Normal; //数字和符号构成的密码
        if (iSym == 0) return Strength.Normal; //数字和字母构成的密码
        if (iNum == 0) return Strength.Normal; //字母和符号构成的密码
        return Strength.Strong; //由数字、字母、符号构成的密码
    }
}
