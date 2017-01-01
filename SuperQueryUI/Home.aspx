<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Home.aspx.cs" Inherits="SuperQueryUI.Home" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Super Query</title>
    <link href="css/myStyle.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    
</head>
<body>
    <form id="form1" runat="server" method="post">
        <asp:ScriptManager ID="_SM" runat="server"></asp:ScriptManager>
    <div style="text-align:center">   
        <img class="logo1" src="images/logo.png">
    </div>

        <div>
            <table class="table_box">
                <tr>

                    <td>
                        <asp:CheckBox ID="checkbox_google" runat="server" Checked="true" />
                        Google </td>
                    <td>
                        <asp:CheckBox ID="checkbox_yandex" runat="server" Checked="true" />
                        Yandex </td>
                    <td>
                        <asp:CheckBox ID="checkbox_bing" runat="server" Checked="true" />
                        Bing </td>

                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="checkbox_gigablast" runat="server" Checked="true" />
                        GigaBlast </td>
                    <td>
                        <asp:CheckBox ID="checkbox_HotBot" runat="server" Checked="true" />
                        HotBot </td>
                    <td>
                        <asp:CheckBox ID="checkbox_rambler" runat="server" Checked="true" />
                        Rambler </td>
                </tr>
            </table>
        </div>
        <br />

        <div style="text-align: center">
            <input id="search" placeholder="search" onkeypress="search_methode" autocomplete="off" value="jerusalem" runat="server" />
            <br />
            <br />
            <asp:Button ID="btn_search" runat="server" Text="Search" OnClientClick="x()" OnClick="btn_search_Click" AutoPostback="False" />
        </div>

        <%--<div id="loaderDiv" style="text-align: center" runat="server" visible="false">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/loader2.gif" Height="150" Width="150" />
        </div>--%>

        <div id="spinner" style="display:none">
            <img id="image-spinner" src="images/ajax-loader2.gif" alt="loading" height="20" width="20" />
        </div>
        <script>
            function x(){
                $('#spinner').show();
            }
        </script>


    

        <br />
        <div id="resDiv" runat="server"></div>
        <br />
        <div id="pagingDiv" runat="server" style="text-align:center">
            <asp:Button ID="page1Button" runat="server" Text="1" Visible="False" OnClick="changePage"/>
            <asp:Button ID="page2Button" runat="server" Text="2" Visible="False" OnClick="changePage"/>
            <asp:Button ID="page3Button" runat="server" Text="3" Visible="False" OnClick="changePage"/>
            <asp:Button ID="page4Button" runat="server" Text="4" Visible="False" OnClick="changePage"/>
            <asp:Button ID="page5Button" runat="server" Text="5" Visible="False" OnClick="changePage"/>
            <asp:Button ID="page6Button" runat="server" Text="6" Visible="False" OnClick="changePage"/>
            <asp:Button ID="page7Button" runat="server" Text="7" Visible="False" OnClick="changePage"/>
            <asp:Button ID="page8Button" runat="server" Text="8" Visible="False" OnClick="changePage"/>
            <asp:Button ID="page9Button" runat="server" Text="9" Visible="False" OnClick="changePage"/>
            <asp:Button ID="page10Button" runat="server" Text="10" Visible="False" OnClick="changePage"/>
        </div>    
    </form>
</body>
</html>