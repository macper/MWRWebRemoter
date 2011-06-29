<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dictionary.aspx.cs" Inherits="MWRClientWebInterface.Dictionary" Title="Słowniki" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box_container">
        <p>Dictionary: <asp:DropDownList ID="ddlDictionary" runat="server" 
                onselectedindexchanged="ddlDictionary_SelectedIndexChanged" AutoPostBack="true"/></p>
        <table class="FormTable">
            <tr><td>Typ:</td><td><asp:Label ID="lbType" runat="server" /></td></tr>
            <tr><td>GUID</td><td><asp:Label ID="lbGuid" runat="server" /></td></tr>
            <tr><td>Nazwa:</td><td><asp:Label ID="lbName" runat="server" /></td></tr>
            <tr><td>Stan:</td><td><asp:DropDownList ID="ddlState" runat="server">
                <asp:ListItem Text="Aktywne" Value="1" runat="server"/>
                <asp:ListItem Text="Nieaktywne" Value="0" runat="server"/>
                </asp:DropDownList>
            </td></tr>
            <tr><td>Config:</td><td><asp:TextBox ID="tbConfig" TextMode="MultiLine" Rows="5" 
                        runat="server" Width="470px" /></td></tr>
        </table>
        <p><asp:Button runat="server" ID="btSubmit" Text="Zapisz" 
                onclick="btSubmit_Click" /></p>
</div>
</asp:Content>
