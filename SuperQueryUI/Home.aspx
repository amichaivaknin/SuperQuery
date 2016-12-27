<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SuperQueryUI.Home" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Super Query</title>
    <link href="css/myStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">   
        <img class="logo1" src="images/logo.png">
    </div>
    
        <div>
            <table class="table_box">
                <tr>
                     
                        <td> <asp:CheckBox ID="checkbox_google" runat="server" Checked="true" /> Google </td> 
                       <td> <asp:CheckBox ID="checkbox_yandex" runat="server" Checked="true" /> Yandex </td>
                      <td>  <asp:CheckBox ID="checkbox_bing" runat="server" Checked="true"/> Bing </td>
                                      
                </tr>
                <tr>
                        <td> <asp:CheckBox ID="checkbox_gigablast" runat="server" Checked="true"/> GigaBlast </td>
                        <td><asp:CheckBox ID="checkbox_HotBot" runat="server" Checked="true"/> HotBot </td>
                        <td> <asp:CheckBox ID="checkbox_rambler" runat="server" Checked="true"/> Rambler </td>                   
                </tr>
            </table>
        </div>
        <br />
        <div style="text-align:center">
            <input id="search" placeholder="search" onkeypress="search_methode" autocomplete="off" value="jerusalem" runat="server" /> <br /><br />
            <asp:Button ID="btn_search" runat="server" Text="Search" OnClick="btn_search_Click" />
        </div>
        <br />
        <div id="resDiv" runat="server"></div>
        <br />
        <div id="pagingDiv" runat="server" style="text-align:center"></div>
    </form>
</body>
</html>