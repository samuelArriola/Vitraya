<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RevisarPlanAccion.aspx.cs" Inherits="Presentacion.PlanAccion.RevisarPlanAccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal" tabindex="-1" role="dialog" id="modalRechazo">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Rechazo de la acción de merojar</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <h5 class="text-center">Motivo del rechazo</h5>
                    <div class="form-group">
                        <textarea class="form-control" id="taMotivo" ></textarea>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="btnEnviarRechazo">Enviar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="row" id="panel">
        <div class="col col-12">
            <div class="x_panel">
                <div class="x_title">
                    <h6>Avances al Plan de Acción</h6>
                    <div class="clearfix"></div>
                </div>
                <div class="x_content">
                    <div class="dashboard-widget-content">
                        <ul class="list-unstyled timeline widget" id="listaAvances">
                        </ul>
                    </div>
                </div>
                <div class="col col-12 d-flex justify-content-between">
                    <div class="form-group">
                        <button class="btn btn-danger" id="btnRechazar">Rechazar</button>
                    </div>
                    <div class="form-group">
                        <button class="btn btn-success" id="btnAceptar">Aceptar</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js/RevisarPlanAccionJS.js"></script>
</asp:Content>
