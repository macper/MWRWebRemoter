<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlTaskFileManage.ascx.cs" Inherits="MWRClientWebInterface.Controls.Tasks.CtlTaskFileManage" %>
<table class="FormTable">
	<tr><td style="width:100px">Typ operacji:</td><td>
	<asp:DropDownList ID="dlOperationType" runat="server" onchange="ShowProperFields()" EnableViewState="false" >
		<asp:ListItem Value="0">Pobierz strukture katalogu</asp:ListItem>
		<asp:ListItem Value="1">Pobierz plik</asp:ListItem>
		<asp:ListItem Value="2">Wyślij plik</asp:ListItem>
		<asp:ListItem Value="3">Kopiuj plik</asp:ListItem>
		<asp:ListItem Value="4">Kopiuj plik na FTP</asp:ListItem>
		<asp:ListItem Value="5">Sciągnij plik z FTP</asp:ListItem>
		<asp:ListItem Value="6">Usuń plik</asp:ListItem>
	</asp:DropDownList></td></tr>
	<tr id="tr_Path"><td>Ścieżka:</td><td><asp:TextBox ID="tbPath" runat="server" EnableViewState="false" /></td></tr>
	<tr id="tr_Dest"><td>Ścieżka docelowa:</td><td><asp:TextBox ID="tbDestination" runat="server" EnableViewState="false" /></td></tr>
	<tr id="tr_Content"><td>Zawartość pliku</td><td><asp:TextBox ID="tbContent" runat="server" Rows="10" Columns="70" TextMode="MultiLine" EnableViewState="false"/></td></tr>
	<td id="tr_Tree">Struktura katalogów</td><td style="text-align:left"><asp:TreeView ID="treeView" runat="server" EnableViewState="false" EnableClientScript="true"/></td></td>
</table>
<script language="javascript">
	function ShowProperFields()
	{
		var dl = document.getElementById('<%=dlOperationType.ClientID%>');
		var doc = document.getElementById('tr_Dest');
		var adv = document.getElementById('tr_Content');
		var tree = document.getElementById('tr_Tree');
		doc.style.display = 'none';
		
		if (dl.selectedIndex == 0)
		{
			tree.style.display = '';
		}
		else
		{
			tree.style.display = 'none';
		}
		
		if (dl.selectedIndex == 1 || dl.selectedIndex == 2)
		{
			adv.style.display = '';
		}
		else
		{
			adv.style.display = 'none';
		}
		
		if (dl.selectedIndex == 5)
		{
			doc.style.display = '';
		}
	}
	ShowProperFields();
</script>