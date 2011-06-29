<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonTaskDetails.ascx.cs" Inherits="MWRClientWebInterface.Controls.Tasks.CommonTaskDetails" %>
<table class="FormTable" style="text-align:left">
<tr><td>Nazwa:</td><td><asp:Label ID="lbTaskName" runat="server" /></td></tr>
<tr><td>GUID:</td><td><asp:Label ID="lbTaskGuid" runat="server" /></td></tr>
<tr><td>ID:</td><td><asp:Label ID="lbTaskID" runat="server" /></td></tr>
<tr><td>Status:</td><td><asp:Label ID="lbTaskState" runat="server" /></td></tr>
<tr><td>Data rejestracji:</td><td><asp:Label ID="lbTaskDateRegistered" runat="server" /></td></tr>
<tr><td>Data pobrania:</td><td><asp:Label ID="lbTaskDateSended" runat="server" /></td></tr>
<tr><td>Data wykonania:</td><td><asp:Label ID="lbTaskDateCompleted" runat="server" /></td></tr>
<tr><td>Data zamierzonego wykonania:</td><td><asp:Label ID="lbTaskDateExecute" runat="server" /></td></tr>
<asp:PlaceHolder ID="phErrorDetails" runat="server" Visible="false">
<tr><td>Szczegóły błędu:</td><td><asp:Label ID="lbErrorDetails" runat="server" /></td></tr>
</asp:PlaceHolder>
</table>