<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CreacionExamenCapacitacion.aspx.cs" Inherits="Presentacion.trainings.CreacionExamenCapacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/CreacionExamenCapacitacionCSS.css" rel="stylesheet" />

    <div class="modal fade bd-example-modal-sm" tabindex="-1" role="dialog" id="pn-num-apr">
            <div class="modal-dialog modal-sm" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Porcentaje de cumplimiento</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="form-group">
                            <label for="">Ingrese el Porcentaje de Aprobación 60 a 100</label>
                            <input type="number" class="form-control" id="txtPorcentajeAprobacion" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" id="btnGuradarExamen">Guardar Examen</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>

    <div id="DesarrolloExamen">
        <div id="Lienzo" class="jctx-host jctx-id-lienzo">
            <div id="TolBox">
                <div id="ControlPreguntaOM">Pregunta de Opción multiple</div>
                <div id="ControlPreguntaVF">Pregunta de Verdadero o Falso</div>
            </div>
            <textarea id="NomExa" placeholder="Nombre Del Examen"></textarea>
        </div>
        <div class="text-right mt-2">
            <button id="BtnCrearExaCap" class="btn btn-success mt-2">Crear Examen de Capaciatación</button>
        </div>
    </div>
    <script src="js/CreacionExamenCapacitacionJS.js"></script>
</asp:Content>
