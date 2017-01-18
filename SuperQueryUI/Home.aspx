<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="Home.aspx.cs" Inherits="SuperQueryUI.Home" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Super Query</title>
    <link href="css/myStyle.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="scripts/jquery-3.1.1.min.js"></script>
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
            <input id="search" placeholder="Search for..." onkeypress="search_methode" autocomplete="off" runat="server"/>
            <br />
            <br />
            <asp:Button ID="btn_search" runat="server" Text="Search" OnClientClick="x()" AutoPostback="False" OnClick="btn_search_Click"/>
        </div>

        <%--<div id="loaderDiv" style="text-align: center" runat="server" visible="false">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/loader2.gif" Height="150" Width="150" />
        </div>--%>
        <%--<br />--%>
        <br />
        <div id="spinner" style="display:none">
            <img id="image-spinner" src="images/loader8.gif" alt="loading" height="30" width="30" />
        </div>
        <br />
        <div id="noResDIv" style="text-align:center" runat="server" visible="False">
            <asp:Label ID="noResLabel" runat="server" Text="No results"></asp:Label>
        </div>
        
        <script>
            var flag = 0;
            $("#search").change(function () {
                flag=1;
            });
            $("#checkbox_google").change(function () {
                flag = 1;
            });
            $("#checkbox_yandex").change(function () {
                flag = 1;
            });
            $("#checkbox_bing").change(function () {
                flag = 1;
            });
            $("#checkbox_gigablast").change(function () {
                flag = 1;
            });
            $("#checkbox_HotBot").change(function () {
                flag = 1;
            });
            $("#checkbox_rambler").change(function () {
                flag = 1;
            });
            function x() {
                
                if (!(($("#checkbox_google").is(":checked")) || ($("#checkbox_yandex").is(":checked")) || ($("#checkbox_bing").is(":checked")) || ($("#checkbox_gigablast").is(":checked")) || ($("#checkbox_HotBot").is(":checked")) || ($("#checkbox_rambler").is(":checked")))) {
                    window.alert("please select at least one searche engine");
                    $('#noResDIv').hide();
                    $('#resDiv').hide();
                    $('#pagingDiv').hide();
                    return;
                }
                if (flag == 0) return;
                if ($('#search').val().length == 0) return;
                $('#noResDIv').hide();
                $('#resDiv').hide();
                $('#pagingDiv').hide();
                $('#spinner').show();
                }

        </script>

        <br />
        
        <br />
        <div id="resDiv" runat="server"></div>
        <br />
        <div id="pagingDiv" runat="server" style="text-align:center">
            <asp:Button ID="page1Button" runat="server" Text="1" Visible="False" OnClick="ChangePage"/>
            <asp:Button ID="page2Button" runat="server" Text="2" Visible="False" OnClick="ChangePage"/>
            <asp:Button ID="page3Button" runat="server" Text="3" Visible="False" OnClick="ChangePage"/>
            <asp:Button ID="page4Button" runat="server" Text="4" Visible="False" OnClick="ChangePage"/>
            <asp:Button ID="page5Button" runat="server" Text="5" Visible="False" OnClick="ChangePage"/>
            <asp:Button ID="page6Button" runat="server" Text="6" Visible="False" OnClick="ChangePage"/>
            <asp:Button ID="page7Button" runat="server" Text="7" Visible="False" OnClick="ChangePage"/>
            <asp:Button ID="page8Button" runat="server" Text="8" Visible="False" OnClick="ChangePage"/>
            <asp:Button ID="page9Button" runat="server" Text="9" Visible="False" OnClick="ChangePage"/>
            <asp:Button ID="page10Button" runat="server" Text="10" Visible="False" OnClick="ChangePage"/>
        </div>    
    </form>
</body>
</html>