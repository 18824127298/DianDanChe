<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageControl.ascx.cs" Inherits="module_WebPageControl" %>
<table cellSpacing="0" width="100%" border="0" class="controlPanel">
	<tr>
		<td align="left" noWrap>
			[<asp:linkbutton id="btnFirstPage" runat="server" CommandArgument="FirstPage" CausesValidation="False" OnClick="btnFirstPage_Click">首页</asp:linkbutton>]
			[<asp:linkbutton id="btnPrevPage" runat="server" CommandArgument="PrevPage" CausesValidation="False" OnClick="btnPrevPage_Click">上页</asp:linkbutton>]
			[<asp:linkbutton id="btnNextPage" runat="server" CommandArgument="NextPage" CausesValidation="False" OnClick="btnNextPage_Click">下页</asp:linkbutton>]
			[<asp:linkbutton id="btnLastPage" runat="server" CommandArgument="LastPage" CausesValidation="False" OnClick="btnLastPage_Click">尾页</asp:linkbutton>]
		</td>
		<td align="center" width="80%"></td>
		<td align="center" noWrap id="tdGoPage" runat="server">
			每页
			<asp:TextBox id="edtPageSize" runat="server" Width="30px">10</asp:TextBox>行 
			&nbsp;转到<asp:TextBox id="edtGoPage" runat="server" Width="30px"></asp:TextBox>页
			<asp:Button id="btnGoPage" runat="server" CssClass="normalButton" Text="GO" OnClick="btnGoPage_Click"></asp:Button>
		</td>
		<td align="center" noWrap><asp:Button id="btnShowAll" runat="server" CssClass="normalButton" Text="全部" ToolTip="不分页显示，将符合条件的记录全部列出" OnClick="btnShowAll_Click"></asp:Button></td>
		<TD noWrap align="right" width="20%"></TD>
		<td align="right" noWrap id="tdPageCount" runat="server">
			第<asp:label id="lblCurrentIndex" runat="server">0</asp:label>页/共<asp:label id="lblPageCount" runat="server">0</asp:label>页&nbsp;
		</td>
		<td align="right" noWrap>
			共
			<asp:label id="lblRecordCount" runat="server">0</asp:label>
			条记录&nbsp;
		</td>
	</tr>
</table>
