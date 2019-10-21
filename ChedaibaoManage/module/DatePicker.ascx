<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatePicker.ascx.cs" Inherits="module_Calendar" %>
<asp:TextBox ID="edtDate" runat="server"></asp:TextBox>
&nbsp; <a href="#" onclick='javascript:selectDate("<%= _control_real_name %>");return false;'>
<img runat="server" title="从日历选择日期" src="images/datepicker/calendar.gif" border="0" id="imgSelect"></a>
