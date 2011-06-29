<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CtlTaskSpecific.ascx.cs" Inherits="MWRClientWebInterface.Controls.CtlTaskSpecific" %>
<%@ Register Src="~/Controls/CommonTaskDetails.ascx" TagName="CommonTaskDetails" TagPrefix="CC" %>
<%@ Register Src="~/Controls/CommonDetailsToAdd.ascx" TagName="CommonDetailsToAdd" TagPrefix="CC" %>
<%@ Register Src="~/Controls/CtlTaskKillProcess.ascx" TagName="TskKillProcess" TagPrefix="Tsk" %>
<%@ Register Src="~/Controls/CtlTaskRunProcess.ascx" TagName="TskRunProcess" TagPrefix="Tsk" %>
<%@ Register Src="~/Controls/CtlTaskMakeScreenshoot.ascx" TagName="TskMakeScreenShoot" TagPrefix="Tsk" %>


<cc:CommonTaskDetails runat="server" ID="ctlCommonTaskDetails" />
<cc:CommonDetailsToAdd runat="server" ID="ctlCommonDetailsToAdd" />
<Tsk:TskKillProcess runat="server" ID="ctlTskKillProcess" />
<Tsk:TskRunProcess runat="server" ID="ctlTskRunProcess" />
<Tsk:TskMakeScreenShoot runat="server" ID="ctlTskMakeScreenShoot" />
<asp:Button ID="btSave" runat="server" Text="Zapisz" onclick="btSave_Click" />
<asp:Button ID="btRefresh" runat="server" Text="Odswież" 
    onclick="btRefresh_Click" />
<asp:HiddenField ID="hdCreateMode" Value="0" runat="server"/>
<asp:HiddenField ID="hdTaskGuid" runat="server" />

