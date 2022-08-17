<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="VerAuditorias.aspx.cs" Inherits="Presentacion.Auditorias.VerAuditorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal fade bd-example-modal-lg" id="modal-view-plan" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Información de la auditoria</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table-inf">
                        <tr>
                            <td width="200px">Fecha de la Auditoria</td>
                            <td><p id="txtFecha"></p></td>
                        </tr>
                        <tr>
                            <td>Ente Auditor</td>
                            <td><p id="txtEnteAuditor"></p></td>
                        </tr>
                        <tr>
                            <td>Auditores</td>
                            <td><p id="txtAuditores"></p></td>
                        </tr>
                        <tr>
                            <td>Objetivo</td>
                            <td><p id="txtObjetivo"></p></td>
                        </tr>
                        <tr>
                            <td>Procesos</td>
                            <td><p id="txtProcesos"></p></td>
                        </tr>
                        <tr>
                            <td>Alcance</td>
                            <td><p id="txtAlcance"></p></td>
                        </tr>
                        <tr>
                            <td>Anexo</td>
                            <td><p id="txtAnexo"></p></td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    <button class="btn btn-primary" id="btn-asignar">Asignar plan de acción</button>
                </div>
            </div>
        </div>
    </div>
    <div class="row" id="panel-auditorias">
       
    </div>
    <script src="js/VerAuditoriasJS.js"></script>
</asp:Content>
