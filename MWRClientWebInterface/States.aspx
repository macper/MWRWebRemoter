<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="States.aspx.cs" Inherits="MWRClientWebInterface.States" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="box_container">
<div class="windowTitle">Wyszukiwanie stanów</div>
<table class="FormTable">
<tr><td>Stan:</td><td><asp:DropDownList runat="server" ID="ddlStates" 
                EnableViewState="false">
    </asp:DropDownList>
</td>
<td>Data:</td><td>TODO: Data</td><td><asp:Button ID="Button1" runat="server" Text="Pobierz" 
        EnableViewState="false" onclick="Button1_Click" CssClass="button"/></td>
</tr>  
</table> 
</div>
  <div class="box_container" style="overflow:auto;height:500px">
  Wczytano:<asp:Label ID="lbDateLoaded" runat="server" Font-Bold="true" ForeColor="White"/>
		<asp:PlaceHolder ID="stateControlPH" runat="server" />
</div>
</asp:Content>
