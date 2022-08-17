<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ReponderExamenCapacitacion.aspx.cs" Inherits="Presentacion.trainings.ReponderExamenCapacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/CreacionExamenCapacitacionCSS.css" rel="stylesheet" />
    <div class="modal fade bd-example-modal-sm" tabindex="-1" role="dialog" id="mdlResultado">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div id="notificacion">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="btnReiniciar" onclick="location.reload()">Reiniciar Examen</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal" id="btnCerrar">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div id="DesarrolloExamen">
        <div id="Lienzo" class="jctx-host jctx-id-lienzo">
            <textarea id="NomExa" class="text-center" disabled="disabled"></textarea>
        </div>
        <div class="text-right mt-2">
            <button id="BtnReponderExa" class="btn btn-success mt-2">Enviar Resultados</button>
        </div>
    </div>
    <%--<script src="js/CreacionExamenCapacitacionJS.js"></script>--%>
    <script src="js/ReponderExamenCapacitacion.js"></script>
</asp:Content>
