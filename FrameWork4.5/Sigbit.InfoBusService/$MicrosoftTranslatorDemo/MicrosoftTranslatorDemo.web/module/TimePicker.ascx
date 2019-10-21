<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TimePicker.ascx.cs" Inherits="module_DateTime" %>
<asp:Literal ID="ltJS" runat="server" />
<input name="time" type="text" onclick="_SetTime(this)"/>