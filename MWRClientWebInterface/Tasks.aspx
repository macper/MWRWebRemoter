<%@ Page Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Tasks.aspx.cs" Inherits="MWRClientWebInterface.Tasks" Title="Untitled Page" %>
<%@ Register Src="~/Controls/Tasks/CommonTaskDetails.ascx" TagPrefix="CC" TagName="CtlCommonDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<div class="box_container">
<div class="windowTitle">Wyszukiwanie zadań</div>
<table class="FormTable">
<tr><td>Zadanie:<asp:DropDownList runat="server" ID="ddlTaskSelected" EnableViewState="false" /></td>
<td>Status:<asp:DropDownList runat="server" ID="ddlTaskStates" EnableViewState="true">
    <asp:ListItem Text="Wszystkie" Value="0" />
    <asp:ListItem Text="1 - Zarejestrowane" Value="1" />
    <asp:ListItem Text="2 - Pobrane" Value="2" />
    <asp:ListItem Text="3 - Wykonane" Value="3" />
    <asp:ListItem Text="4 - Sprawdzone" Value="4" />
    <asp:ListItem Text="5 - Błędne" Value="5" />
</asp:DropDownList></td>
<td>Data od:<asp:TextBox runat="server" ID="tbDateFrom" /><br />
Data do:<asp:TextBox runat="server" ID="tbDateTo"  /></td>
<td colspan="2"><asp:Button ID="btSearch" runat="server" Text="Szukaj" 
        onclick="btSearch_Click"/>
<asp:Button ID="btCreate" runat="server" Text="Nowe" onclick="btCreate_Click" />
</td></tr>
</table>
</div>
	<div class="box_container" style="height:300px">
<asp:DataGrid ID="dlSearchResults" runat="server" Width="483px"
        EditItemStyle-BackColor="#CC66FF" SelectedItemStyle-BackColor="#1C5E55" 
        CellPadding="4" ForeColor="#333333" GridLines="None" 
			AllowCustomPaging="True" AllowPaging="True" 
			onpageindexchanged="dlSearchResults_PageIndexChanged" >
        <FooterStyle BackColor="#000066" Font-Bold="True" ForeColor="White" 
					Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
					Font-Underline="False" />
<EditItemStyle BackColor="#7C6F57"></EditItemStyle>

<SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedItemStyle>
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingItemStyle BackColor="White" />
        <ItemStyle BackColor="#E3EAEB" />
        <Columns>
            <asp:ButtonColumn ButtonType="PushButton" CommandName="Select" Text="Select" >
            </asp:ButtonColumn>
        </Columns>
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
    </asp:DataGrid>
    </div>
<div class="box_container" style="height:300px">
	<CC:CtlCommonDetails runat="server" ID="ctlCommonDetails" />
		<div class = "FormTable" style="width:744px">
    <asp:PlaceHolder ID="taskDetailsPH" runat="server" EnableViewState="true"/>
    <asp:PlaceHolder ID="saveTaskPH" runat="server" Visible="false">
    <p>Data zamierzonego wykonania:
    <asp:TextBox ID="tbDateToExecute" runat="server" />&nbsp;
    <asp:Button ID="btSave" runat="server" Text="Zapisz" onclick="btSave_Click"/>
    </p>
    </asp:PlaceHolder>
    </div>
    </div>
</asp:Content>
