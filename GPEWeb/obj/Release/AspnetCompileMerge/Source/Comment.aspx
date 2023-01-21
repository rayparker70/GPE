<%@ Page Title="Comment" Language="C#" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Comment.aspx.cs" Inherits="GPEWeb.Comments" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Site.Master" %> 

<form id="form1" runat="server">  
    <asp:ScriptManager ID="ScriptManager1" runat="server">  
    </asp:ScriptManager>  
    <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" />  
    <!-- ModalPopupExtender -->  
    <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow" CancelControlID="btnClose" BackgroundCssClass="modalBackground">  
    </cc1:ModalPopupExtender>  
    <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style="display:none">  
        This is an ASP.Net AJAX ModalPopupExtender Example<br />  
        <asp:Button ID="btnClose" runat="server" Text="Close" />  
    </asp:Panel>  
    <!-- ModalPopupExtender -->  
</form>   
