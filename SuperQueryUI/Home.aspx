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
                        <td><asp:CheckBox ID="checkbox_aol" runat="server" Checked="true"/> AOL </td>
                        <td> <asp:CheckBox ID="checkbox_ask" runat="server" Checked="true"/> Ask </td>                   
                </tr>
            </table>
        </div>
        <br />
        <div style="text-align:center">
            <input id="search" placeholder="search" onkeypress="search_methode" autocomplete="off" runat="server" /> <br /><br />
            <asp:Button ID="btn_search" runat="server" Text="Search" OnClick="btn_search_Click" />
        </div>

        <div id="results_div" style="text-align:left">
            <div id="res1" runat="server"> 
                Title: <asp:Label ID="title1" runat="server" Text=""></asp:Label><br />
                Link: <asp:Label ID="link1" runat="server" Text=""></asp:Label><br />
                Description: <asp:Label ID="description1" runat="server" Text=""></asp:Label>
            </div>
        </div>

        <br />

        
        <asp:TextBox ID="TextBox1" runat="server" Height="295px" TextMode="MultiLine" Width="776px"></asp:TextBox>     
    </form>
</body>
</html>