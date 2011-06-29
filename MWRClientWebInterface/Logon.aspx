<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="MWRClientWebInterface.Logon" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box_container">
        <asp:Login ID="LoginForm" runat="server" DisplayRememberMe="False" FailureTextStyle-ForeColor="Black" 
            FailureText="Nieprawidłowa nazwa użytkownika lub hasło." 
            LoginButtonText="Loguj" onauthenticate="LoginForm_Authenticate" 
            PasswordLabelText="Hasło:" TitleText="Logowanie" 
            UserNameLabelText="Nazwa użytkownika" Width="280px" CssClass="login">
        </asp:Login>
    </div>
</asp:Content>
