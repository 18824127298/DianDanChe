<%@ Page Language="C#" AutoEventWireup="true" CodeFile="borrower_information_list.aspx.cs" Inherits="salesman_borrower_list_borrower_information_list" %>

<%@ Register Src="../../module/GridViewPager.ascx" TagName="GridViewPager" TagPrefix="uc2" %>

<html>
<head id="Head1" runat="server">
    <title>客户的资料</title>
</head>
<script language="javascript" src="../../js/grid_ui_func.js"></script>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="titleTable">
                <table border="0" cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <img src="../../images/menu_icon/pen.gif" />
                            客户的资料</td>
                    </tr>
                </table>
                <hr />
            </div>

            <table cellspacing="0" cellpadding="4" width="100%" border="0" align="center">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="90%" border="0" align="center" style="width: 1217px">
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvList" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False" AllowSorting="True" OnSorting="gvList_Sorting" EnableViewState="False" CellPadding="8">
                            <Columns>
                                <asp:TemplateField HeaderText="图片" SortExpression="FilePath">
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

                <tr>
                    <td>
                        <uc2:GridViewPager ID="gridViewPager" runat="server" />
                    </td>
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
</script>
</html>
