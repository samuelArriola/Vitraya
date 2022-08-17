<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EncuestaCovid.aspx.cs" Inherits="Presentacion.LinkExternos.EncuestaCovid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <iframe src="http://190.242.128.206:8085/EncuestaCovid?identificacion=<%# HttpContext.Current.Session["Admin"] %>" style="width:100%; height:850px; border:0"></iframe>
</asp:Content>
