<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ToExcel.aspx.cs" Inherits="LazyAtHome.Web.WebManage.ToExcel" %>

<html>
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html";charset="gb2312">
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%=DownLoadExcelData() %>
    </div>
    </form>
</body>
</html>
