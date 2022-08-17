<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EjecutarCapa.aspx.cs" Inherits="Presentacion.trainings.EjecutarCapa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="modal" tabindex="-1" role="dialog" id="modal-confirm">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Finalizar Capacitación</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Esta seguro de finalizar la capacitación, una vez finalizada ya no se podra modificar la asistencia</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="bntFinalizarCapa">Aceptar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col col-6 x_panel">
            <div class="x_title">
                <div class="clearfix">
                    <h6>Toma de asistencia</h6>
                </div>
            </div>
            <div class="x_content">
                <div class="form-control search-p d-flex justify-content-between">
                    <input type="text" placeholder="Buscar..." id="txtSearch" />
                    <i class="fa fa-search"></i>
                </div>

                <table class="tbMenu" id="tbParticipantes">
                    <thead>
                        <tr>
                            <th colspan="3">Participantes
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="col col-6">
            <div class="x_panel">
                <div class="x_content">
                    <h5 class="text-center" id="txtNomTema"></h5>
                    <div class="d-flex justify-content-between">
                        <button type="button" id="tbnEdtarMatricula" class="btn btn-primary">Editar matricula</button>
                        <button type="button" id="btnFinalizar" class="btn btn-success">Finalizar capacitación</button>
                    </div>
                </div>
            </div>
            <div class="x_panel">
                <div class="x_content">
                    <table class="tbMenu" id="tbSubtemas">
                        <thead>
                            <tr>
                                <th>Subtemas</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
            <div class="x_panel">
                <div class="x_content">
                    <table class="tbMenu" id="tbArchivos">
                        <thead>
                            <tr>
                                <th>Archivos Anexos</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script src="js/EjecutarCapaJS.js"></script>
</asp:Content>
