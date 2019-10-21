<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dept_framework.aspx.cs" Inherits="framework_organize_department_framework" %>

<html>
<head id="Head1" runat="server">
    <title>部门管理</title>
    <META content="MSHTML 6.00.2900.3199" name=GENERATOR>
</head>
<FRAMESET id=frame1 border=0 frameSpacing=0 rows=40,* frameBorder=NO cols=*>
<FRAME name=dept_title src="dept_title.aspx" frameBorder=NO noResize scrolling=no>
	<FRAMESET 	id=frame2 border=0 frameSpacing=0 rows=* frameBorder=YES cols=200,*>
	<FRAME name=dept_list src="dept_list.aspx" frameBorder=YES noResize>
	<FRAME name=dept_main src="dept_new.aspx?dpt_uid=" frameBorder=YES>
	</FRAMESET>
</FRAMESET>
</html>
