<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AuditoriaExterna.aspx.cs" Inherits="Presentacion.PlanAccion.AuditoriaExterna" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <h6>Auditoria Externa</h6>
            <div class="clearfix">
            </div>
        </div>
        <div class="x_content">
            <div class="row justify-content-center">
                <div class="col col-10">
                    <div class="form-group row">
                        <label class="col-sm-2 col-form-label">Fecha de realización</label>
                        <div class="col-sm-10">
                            <input type="date" id="txtFecha" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 col-form-label">Ente Auditor</label>
                        <div class="col-sm-10">
                            <asp:DropDownList runat="server" ID="ddlEnteAuditor" CssClass="form-control">
                               
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 col-form-label">Auditores</label>
                        <div class="col-sm-10">
                            <div class="d-flex justify-content-between">
                                <input type="text" id="txtAuditores" class="form-control" />
                                <button id="btnAuditor" class="btn btn-primary ml-1"><i class="fa fa-plus-circle"></i></button>
                            </div>
                            <div id="lstAuditores"></div>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 col-form-label">Objetivo</label>
                        <div class="col-sm-10">
                            <textarea id="txtObjetivo" class="form-control"></textarea>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 col-form-label">Alcance</label>
                        <div class="col-sm-10">
                            <textarea id="txtAlcance" class="form-control"></textarea>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 col-form-label">Procesos Auditados</label>
                        <div class="col-sm-10">
                            <asp:DropDownList runat="server" ID="ddlProcesos" CssClass="form-control" />
                            <div id="lstProcesos"></div>
                        </div>
                    </div>
                    <div class="form-group row">
                       <label class="col-sm-2 col-form-label">Hallazgos</label>
                        <div class="col-sm-10">
                            <input type="text" id="txtHallazgos" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 col-form-label">Responsables</label>
                        <div class="col-sm-10">
                            <asp:DropDownList runat="server" ID="ddlResponsable" CssClass="form-control" />
                        </div>
                    </div>

                     <div class="form-group row">
                        <div class="col col-12">
                            <div class="d-flex justify-content-end">
                                <button class="btn btn-primary" id="btnHallazgo">Cargar</button>
                            </div>
                        </div>
                    </div>   

                    <div class="row" >
                        <div class="col col-12 mb-2" style="background: #efefef">
                            <ul id="lsthallazgos">
                            </ul>
                        </div>
                    </div>

                    
                    <div class="row form-group">
                        <div class="col col-12">
                            <div id="pnUpLoadArch" class="text-center">
                                <div id="boxUpLoadArch">
                                    <svg width="9em" height="9em" viewBox="0 0 16 16" class="bi bi-cloud-arrow-up-fill pt-3" fill="currentColor" xmlns="http://www.w3.org/2000/svg" id="imgUpLoad">
                                        <path fill-rule="evenodd" d="M8 2a5.53 5.53 0 0 0-3.594 1.342c-.766.66-1.321 1.52-1.464 2.383C1.266 6.095 0 7.555 0 9.318 0 11.366 1.708 13 3.781 13h8.906C14.502 13 16 11.57 16 9.773c0-1.636-1.242-2.969-2.834-3.194C12.923 3.999 10.69 2 8 2zm2.354 5.146l-2-2a.5.5 0 0 0-.708 0l-2 2a.5.5 0 1 0 .708.708L7.5 6.707V10.5a.5.5 0 0 0 1 0V6.707l1.146 1.147a.5.5 0 0 0 .708-.708z"></path>
                                    </svg>
                                </div>
                                <asp:FileUpload runat="server" ID="fuArchivo" class="fuArchivo" />
                            </div>
                        </div>
                        <div id="lsArchivos"></div>
                    </div>
                    <div class="row">
                        <div class="col col-12">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="d-flex justify-content-end">
                                        <asp:Button ID="btnCancelar" Text="Cancelar" CssClass="btn btn-danger" runat="server" OnClick="btnCancelar_Click" />
                                        <asp:Button ID="btnGuardar" Text="Guardar" CssClass="btn btn-success" runat="server"  Style="margin-left: 5px;" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js/AuditoriaExternaJS.js"></script>
</asp:Content>
