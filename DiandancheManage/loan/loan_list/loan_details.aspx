<%@ Page Language="C#" AutoEventWireup="true" CodeFile="loan_details.aspx.cs" Inherits="loan_loan_list_loan_details" %>

<%@ Register Src="../../module/My97DatePicker.ascx" TagName="My97DatePicker" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>客户基本信息
    </title>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/mytable.gif" />
                            <asp:Label ID="lblTitle" runat="server" Text="客户基本信息"></asp:Label></td>
                    </tr>
                </table>
                <hr />
            </div>
            <div class="contentTable">
                <table width="100%" cellpadding="8" cellspacing="1" align="center">
                    <thead>
                        <tr>
                            <td colspan="3" style="text-align: center;">站点基本信息
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>商户名称：<asp:Label Style="color: red;" ID="BusinessName" runat="server"></asp:Label></td>
                            <td>招聘点名称：<asp:Label ID="RecruitmentName" runat="server" Style="color: red; text-align: right;"></asp:Label></td>
                            <td>入职站点名称：<asp:Label Style="color: red;" ID="EntrySiteName" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>站点地址：<asp:Label ID="SiteAddress" runat="server" Style="color: red;"></asp:Label></td>
                            <td>站长电话：<asp:Label ID="StationMasterPhone" runat="server" Style="color: red;"></asp:Label></td>
                            <td>品牌：<asp:Label ID="Brand" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>型号：<asp:Label ID="CheType" runat="server" Style="color: red;"></asp:Label></td>
                            <td>融资租赁总额：<asp:Label ID="TotalAmountStage" runat="server" Style="color: red;"></asp:Label></td>
                            <td>首付：<asp:Label ID="DownPayments" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>期数：<asp:Label ID="Deadline" runat="server" Style="color: red;"></asp:Label></td>
                            <td>价格：<asp:Label ID="CreditAmount" runat="server" Style="color: red;"></asp:Label></td>
                            <td></td>
                        </tr>
                    </tbody>
                </table>

                <table width="100%" cellpadding="8" cellspacing="1" align="center" style="margin-top: 10px;">
                    <thead>
                        <tr>
                            <td colspan="3" style="text-align: center;">个人资料
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>姓名：<asp:Label ID="Name" runat="server" Style="color: red;"></asp:Label></td>
                            <td>性别：<asp:Label ID="Sex" runat="server" Style="color: red;"></asp:Label></td>
                            <td>身份证号码：<asp:Label ID="IdCardNumber" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>身份证出生：<asp:Label ID="Birth" runat="server" Style="color: red;"></asp:Label></td>
                            <td>民族：<asp:Label ID="Nation" runat="server" Style="color: red;"></asp:Label></td>
                            <td>签发机关：<asp:Label ID="SigningOrganization" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>身份证住址：<asp:Label ID="Address" runat="server" Style="color: red;"></asp:Label></td>
                            <td>签发日期：<asp:Label ID="IssuanceDate" runat="server" Style="color: red;"></asp:Label></td>
                            <td>失效日期：<asp:Label ID="ExpirationDate" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>手机：<asp:Label ID="CreditPhone" runat="server" Style="color: red;"></asp:Label></td>
                            <td>住宅电话：<asp:Label ID="ResidentialPhone" runat="server" Style="color: red;"></asp:Label></td>
                            <td>邮箱：<asp:Label ID="Email" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>教育程度：<asp:Label ID="EducationLevel" runat="server" Style="color: red;"></asp:Label></td>
                            <td>婚姻状况：<asp:Label ID="MaritalStatus" runat="server" Style="color: red;"></asp:Label></td>
                            <td>子女数目：<asp:Label ID="ChildrenNumber" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>来市时间：<asp:Label ID="ComingTime" runat="server" Style="color: red;"></asp:Label></td>
                            <td>居住状况：<asp:Label ID="LivingConditions" runat="server" Style="color: red;"></asp:Label></td>
                            <td>现居住地址：<asp:Label ID="ResidenceAddress" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>卡号：<asp:Label ID="Number" runat="server" Style="color: red;"></asp:Label></td>
                            <td>所属银行名：<asp:Label ID="BankCardName" runat="server" Style="color: red;"></asp:Label></td>
                            <td>卡片的类型：<asp:Label ID="BankCardType" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>

                <table width="100%" cellpadding="8" cellspacing="1" align="center" style="margin-top: 10px;">
                    <thead>
                        <tr>
                            <td colspan="3" style="text-align: center;">单位信息
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>上一份工作单位名称：<asp:Label ID="LastWorkName" runat="server" Style="color: red;"></asp:Label></td>
                            <td>工作内容：<asp:Label ID="JobContent" runat="server" Style="color: red;"></asp:Label></td>
                            <td>月工资：<asp:Label ID="MonthlyWage" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>工作时间：<asp:Label ID="WorkingYear" runat="server" Style="color: red;"></asp:Label></td>
                            <td>总工作年限：<asp:Label ID="WorkingSeniority" runat="server" Style="color: red;"></asp:Label></td>
                            <td></td>
                        </tr>
                    </tbody>
                </table>

                <table width="100%" cellpadding="8" cellspacing="1" align="center" style="margin-top: 10px;">
                    <thead>
                        <tr>
                            <td colspan="3" style="text-align: center;">负债情况
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>支出：<asp:Label ID="Expenditure" runat="server" Style="color: red;"></asp:Label></td>
                            <td>生活费用：<asp:Label ID="LivingCost" runat="server" Style="color: red;"></asp:Label></td>
                            <td>房租：<asp:Label ID="Rent" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>芝麻信用：<asp:Label ID="SesameCredit" runat="server" Style="color: red;"></asp:Label></td>
                            <td>是否花呗逾期：<asp:Label ID="IsFlowersOverdue" runat="server" Style="color: red;"></asp:Label></td>
                            <td>花呗逾期金额：<asp:Label ID="FlowersOverdueAmount" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>是否借呗逾期：<asp:Label ID="IsBorrowOverdue" runat="server" Style="color: red;"></asp:Label></td>
                            <td>借呗逾期金额：<asp:Label ID="BorrowOverdueAmount" runat="server" Style="color: red;"></asp:Label></td>
                            <td>是否在银行贷款：<asp:Label ID="IsBankLoan" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>银行贷款余额：<asp:Label ID="BankLoanBalance" runat="server" Style="color: red;"></asp:Label></td>
                            <td>是否银行逾期：<asp:Label ID="IsBorrowOverdueAmount" runat="server" Style="color: red;"></asp:Label></td>
                            <td>银行逾期金额：<asp:Label ID="BankOverdueAmount" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>是否在P2P借款：<asp:Label ID="IsPtpLoan" runat="server" Style="color: red;"></asp:Label></td>
                            <td>P2P贷款余额：<asp:Label ID="PtpLoanBalance" runat="server" Style="color: red;"></asp:Label></td>
                            <td>是否P2P逾期：<asp:Label ID="IsPtpOverdueAmount" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>P2P逾期金额：<asp:Label ID="PtpOverdueAmount" runat="server" Style="color: red;"></asp:Label></td>
                            <td>贷款未通过说明：<asp:Label ID="FailurePassReason" runat="server" Style="color: red;"></asp:Label></td>
                            <td>是否办理银行信用卡：<asp:Label ID="IsCreditcardLoan" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>信用卡已用金额：<asp:Label ID="CreditcardLoanBalance" runat="server" Style="color: red;"></asp:Label></td>
                            <td>是否信用卡逾期：<asp:Label ID="IsCreditcardOverdueAmount" runat="server" Style="color: red;"></asp:Label></td>
                            <td>信用卡逾期金额：<asp:Label ID="CreditcardOverdueAmount" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>客户分类：<asp:Label ID="CustomerClassification" runat="server" Style="color: red;"></asp:Label></td>
                            <td>其他贷款说明：<asp:Label ID="lblOtherLoan" runat="server" Style="color: red;"></asp:Label></td>
                            <td>备注：<asp:Label ID="lblRemark" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>

                <%--   <table width="100%" cellpadding="8" cellspacing="1" align="center" style="margin-top: 10px;">
                    <thead>
                        <tr>
                            <td colspan="3" style="text-align: center;">联系人信息
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>配偶：<asp:Label ID="Spouse" runat="server" Style="color: red;"></asp:Label></td>
                            <td>配偶是否知晓分期：<asp:Label ID="IsSpouseKnowStages" runat="server" Style="color: red;"></asp:Label></td>
                            <td>联系电话：<asp:Label ID="ContactPhone" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>联系地址：<asp:Label ID="ContactAddress" runat="server" Style="color: red;"></asp:Label></td>
                            <td>父母：<asp:Label ID="Parents" runat="server" Style="color: red;"></asp:Label></td>
                            <td>父母是否知晓分期：<asp:Label ID="IsParentsKnowStages" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>父母联系电话：<asp:Label ID="ParentsContactPhone" runat="server" Style="color: red;"></asp:Label></td>
                            <td>兄弟姐妹：<asp:Label ID="Brothers" runat="server" Style="color: red;"></asp:Label></td>
                            <td>兄弟姐妹是否知晓分期：<asp:Label ID="IsBrothersKnowStages" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>兄弟姐妹联系电话：<asp:Label ID="BrothersContactPhone" runat="server" Style="color: red;"></asp:Label></td>
                            <td>朋友：<asp:Label ID="Friend" runat="server" Style="color: red;"></asp:Label></td>
                            <td>朋友是否知晓分期：<asp:Label ID="IsFriendKnowStages" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>朋友联系电话：<asp:Label ID="FriendContactPhone" runat="server" Style="color: red;"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>--%>

                <table width="100%" cellpadding="8" cellspacing="1" align="center" style="margin-top: 10px;">
                    <thead>
                        <tr>
                            <td colspan="3" style="text-align: center;">联系人信息
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:GridView ID="ContactsList" runat="server" Width="100%" AutoGenerateColumns="False" EnableViewState="False" CellPadding="8">
                                    <Columns>
                                        <asp:BoundField DataField="Contact" HeaderText="关系" SortExpression="Contact">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Phone" HeaderText="手机号" SortExpression="Phone">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="是否知晓分期" SortExpression="IsKnowStages">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%# VIVIsKnowStages(Eval("IsKnowStages")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="修改时间" SortExpression="UpdateTime">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%# VIVHTime(Eval("UpdateTime")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Visible="False" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <table width="100%" cellpadding="8" cellspacing="1" align="center" style="margin-top: 10px;">
                    <thead>
                        <tr>
                            <td colspan="3" style="text-align: center;">客户照片信息
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>用户的证件信息：</td>
                            <td colspan="3">身份证：<img src="" style='width: 350px; height: 350px; margin-top: 5px; margin-bottom: 5px;' id="shenfenzhengzhengmian" runat="server" />
                                <input name="button_test" type="button" value="旋转该图片" onclick="fn_xuanzhuansfz('shenfenzhengzhengmian')" />
                                <img src="" style='width: 350px; height: 350px; margin-top: 5px; margin-bottom: 5px;' id="shenfenzhengfanmian" runat="server" /><br />

                                银行卡：
                                <img src="" style='width: 300px; height: 250px; margin-top: 5px; margin-bottom: 5px;' id="yinhangka" runat="server" />
                            </td>

                        </tr>
                        <tr>
                            <td>用户的人身照：</td>
                            <td colspan="3">人身照：
                                <img src="" style='width: 300px; height: 250px; margin-top: 2px; margin-bottom: 5px;' id="renshenzhao" runat="server" />
                                &nbsp;&nbsp;人脸对比分数：<asp:Label ID="Score" runat="server" Width="15%"></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td>客户补充资料：</td>
                            <td colspan="3">
                                <asp:GridView ID="gvList" runat="server" Width="100%" AutoGenerateColumns="False" EnableViewState="False" CellPadding="8">
                                    <Columns>
                                        <asp:TemplateField HeaderText="图片">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <%# VIVFilePath(Eval("FilePath"),Eval("WeChatPath"),Eval("CreateTime")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings Visible="False" />
                                </asp:GridView>
                            </td>
                        </tr>


                        <tr id="Gains" runat="server">
                            <td>用户借款相关：</td>
                            <td colspan="3">融资租赁费：
                                <asp:TextBox ID="edtMonthlyPayment" runat="server"></asp:TextBox>元
                                 参考的融资租赁费：<asp:Label ID="lblMonthlyPayment" runat="server"></asp:Label>元
                    &nbsp;&nbsp;多少个月内提还收取两百加一个月手续费：
                                <asp:TextBox ID="edtWithinMonth" runat="server" Text="4" Width="15%"></asp:TextBox>个月
                                利率：<asp:Label ID="lblRentRate" runat="server"></asp:Label>%
                            </td>
                        </tr>

                        <tr id="Tr1" runat="server">
                            <td>网络关联风险：</td>
                            <td colspan="3">智查分：
                                <asp:TextBox ID="edtZhiCha" runat="server" onblur="fn_Result()"></asp:TextBox>
                                &nbsp;&nbsp;贷前分：
                                <asp:TextBox ID="edtDaiQian" runat="server" onblur="fn_Result()"></asp:TextBox>
                                结论：<asp:Label ID="lblTongDunWang" runat="server"></asp:Label>
                                <asp:TextBox ID="edtTongDunWang" CssClass="cssText1" Style="display: none;" runat="server" Text=""></asp:TextBox>
                            </td>
                        </tr>


                        <tr id="TrBoHui" runat="server">
                            <td>审核备注：</td>
                            <td colspan="3">
                                <asp:DropDownList ID="ddlBoHuiRemark" onchange="fn_AuditorRemark(this)" runat="server">
                                    <asp:ListItem Text="请选择" Value="请选择"></asp:ListItem>
                                    <asp:ListItem Text="客户{0}的亲属{1}手机号有误【车1号】" Value="客户{0}的亲属{1}手机号有误【车1号】"></asp:ListItem>
                                    <asp:ListItem Text="客户{0}首付需要给{1}元【车1号】" Value="客户{0}首付需要给{1}元【车1号】"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="edtAuditorRemark" runat="server" Width="100%" SkinID="MultiLine" TextMode="MultiLine" Rows="3" />
                                <asp:Label ID="lblAuditorRemark" runat="server" Width="80%" Visible="false"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td>附加：</td>
                            <td colspan="3">备注：
                                 <asp:TextBox ID="edtFengKongRemark" runat="server" Width="100%" SkinID="MultiLine" TextMode="MultiLine" Rows="5" />
                                电核：
                                 <asp:TextBox ID="edtElectricCore" runat="server" Width="100%" SkinID="MultiLine" TextMode="MultiLine" Rows="5" />
                                用户回访记录：
                                 <asp:TextBox ID="edtVisitRecord" runat="server" Width="100%" SkinID="MultiLine" TextMode="MultiLine" Rows="5" />
                                是否为骑手： 
                                <asp:DropDownList ID="ddlIsRider" runat="server">
                                    <Items>
                                        <asp:ListItem Value='NotSure'>不清楚</asp:ListItem>
                                        <asp:ListItem Value='True'>是</asp:ListItem>
                                        <asp:ListItem Value='False'>否</asp:ListItem>
                                    </Items>
                                </asp:DropDownList>
                                是否有接听： 
                                <asp:DropDownList ID="ddlIsAnswer" runat="server">
                                    <Items>
                                        <asp:ListItem Value='NotSure'>不清楚</asp:ListItem>
                                        <asp:ListItem Value='True'>是</asp:ListItem>
                                        <asp:ListItem Value='False'>否</asp:ListItem>
                                    </Items>
                                </asp:DropDownList>
                                是否有异常： 
                                <asp:DropDownList ID="ddlIsAbnormal" runat="server">
                                    <Items>
                                        <asp:ListItem Value='NotSure'>不清楚</asp:ListItem>
                                        <asp:ListItem Value='True'>是</asp:ListItem>
                                        <asp:ListItem Value='False'>否</asp:ListItem>
                                    </Items>
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <table width="100%" cellpadding="8" cellspacing="1" align="center" id="btn" runat="server">
                    <tfoot>
                        <tr align="center">
                            <td colspan="5">&nbsp;&nbsp;<asp:Button ID="btnOK" runat="server" OnClick="btnOK_Click" OnClientClick="return confirm('确认审核通过吗?')" Text="审核通过" />
                                &nbsp;&nbsp;<asp:Button ID="btnUnAudit" runat="server" Text=" 审核不通过 " OnClientClick="return confirm('您确认审核不通过吗?')" OnClick="btnUnAudit_Click" />
                                &nbsp;&nbsp;<asp:Button ID="btnLiXiRen" runat="server" Text=" 审核驳回 " OnClientClick="return confirm('您确认驳回吗?')" OnClick="btnLiXiRen_Click" />
                                &nbsp;&nbsp;<asp:Button ID="btnMentionBack" runat="server" Text=" 允许用户提还 " OnClientClick="return confirm('您确认允许客户提还吗?')" OnClick="btnMentionBack_Click" Visible="false" />
                                &nbsp;&nbsp;<asp:Button ID="btnBuChongZiLiaoTiJiao" runat="server" Text=" 补充资料提交 " OnClientClick="return confirm('您确定补充资料提交吗?')" OnClick="btnBuChongZiLiaoTiJiao_Click" />
                                &nbsp;&nbsp;<asp:Button ID="btnxiazai" runat="server" Text=" 一键下载图片 " OnClientClick="return confirm('您确定下载图片吗?')" OnClick="btnxiazai_Click" />
                                &nbsp;&nbsp;<asp:Button ID="btnChedan" runat="server" Text=" 撤单 " OnClientClick="return confirm('您确定撤单吗?')" OnClick="btnChedan_Click" />
                                &nbsp;&nbsp;<asp:Button ID="btnYanZheng" runat="server" Text=" 公安验证图片 " OnClientClick="return confirm('您确定使用公安系统验证吗?')" OnClick="btnYanZheng_Click" />
                            </td>

                        </tr>
                    </tfoot>
                </table>
                <table width="100%" align="center" id="tbART" runat="server">
                    <tr>
                        <td>&nbsp;<asp:Label ID="lblART" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
                    </tr>
                </table>
            </div>
            <br />
            <table width="450" align="center">
                <tr>
                    <td>&nbsp;<asp:Label ID="lblErrMessage" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label></td>
                </tr>
            </table>
        </div>
    </form>
</body>
<script src="../../js/jquery-1.8.2.min.js"></script>
<script src="../../js/jQueryRotate.js"></script>
<script src="../../js/jquery.cookie.js"></script>
<script>
    var isopen = false;
    var newImg;
    var w = 300; //将图片宽度+200 
    var h = 300; // 将图片高度 +200 
    $(document).ready(function () {
        $("img").bind("click", function () {
            newImg = this;
            if (!isopen) {
                isopen = true;
                $(this).width($(this).width() + w);
                $(this).height($(this).height() + h);
                moveImg(10, 10);
            }
            else {
                isopen = false;
                $(this).width($(this).width() - w);
                $(this).height($(this).height() - h);
                moveImg(-10, -10);
            }

        });
    });
    //移位 
    i = 0;
    function moveImg(left, top) {
        var offset = $(newImg).offset();
        $(newImg).offset({ top: offset.top + top, left: offset.left + left });
        if (i == 10) {
            i = 0;
            return;
        }
        setTimeout("moveImg(" + left + "," + top + ")", 10);
        i++;
    }


    function fn_xuanzhuan(imgid) {
        var value = 0;
        if ($.cookie("im" + imgid) == null) {
            $.cookie("im" + imgid, 0);
        }
        $.cookie("im" + imgid, Number($.cookie("im" + imgid)) + Number(90));
        value = $.cookie("im" + imgid);
        $("#img" + imgid + "").rotate({ animateTo: Number(value) });
    }

    var sfzval = 0;
    function fn_xuanzhuansfz(id) {
        sfzval += 90;
        $("#" + id + "").rotate({ animateTo: sfzval });
    }

    function fn_Result() {
        var edtZhiCha = $("#edtZhiCha").val();
        var edtDaiQian = $("#edtDaiQian").val();
        if (edtZhiCha != "" && edtDaiQian != "") {
            var sResult = ""; i
            if (edtZhiCha > 70 || edtDaiQian > 60) {
                sResult = "可考虑否决";
            }
            else if (edtZhiCha <= 70 && 51 <= edtZhiCha && edtDaiQian <= 60 && 31 <= edtDaiQian) {
                sResult = "首付15%，电核近亲2个，需电核申请人，租后管理提供通讯录（较完整，亲戚数量超过4个）";
            }
            else if (edtZhiCha <= 70 && edtDaiQian <= 30 && 50 < edtZhiCha) {
                sResult = "首付0-10%，电核近亲2个，电核申请人无强制要求，电核联系人快递/美团，租后管理提供通讯录";
            }
            else if (edtZhiCha <= 50 && edtDaiQian <= 60 && 31 <= edtDaiQian) {
                sResult = "首付0-10%，电核近亲2个，电核申请人无强制要求，电核联系人快递/美团，租后管理提供通讯录";
            }
            else if (edtZhiCha <= 50 && edtDaiQian <= 30) {
                sResult = "首付无规定，电核近亲1-2个，电核申请人无强制要求，电核联系人快递，租后管理无强制要求";
            }
            $("#lblTongDunWang").text(sResult);
            $("#edtTongDunWang").val(sResult);
        }
    }

    function fn_AuditorRemark(curObj) {
        var selectValue = curObj.options[curObj.selectedIndex].value;
        $("#edtAuditorRemark").val(selectValue);
    }
</script>
</html>
