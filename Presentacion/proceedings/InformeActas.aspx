<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="InformeActas.aspx.cs" Inherits="Presentacion.proceedings.InformeActas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal fade bd-example-modal-sm" id="modal-view-no-signing" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Listado de asistente sin firma</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div id="modal-content">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" tabindex="-1" role="dialog" id="loading-modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-center w-100">Cargando Datos</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="text-center">
                        <div class="preloader" style="display: inline-block"></div>
                    </div>
                    <p class="text-center">Por favor espere mientras se cargan los datos</p>
                </div>

            </div>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Informe de Actas de Reunión</h6>
            </div>
        </div>
        <div class="x_content">
            <button class="btn btn-success" id="btnExpotExcel">Exportar</button>
            <div class="tableContainer">
                <table class="table mt-3" id="tablaActas">
                    <thead>
                        <tr>
                            <th>
                                <input class="form-control" type="text" id="txtCodigo" /></th>
                            <th>
                                <input class="form-control" type="text" id="txtNombre" /></th>
                            <th>
                                <div class="row">
                                    <div class="col col-6">
                                        <input class="form-control" type="date" id="fecha1" /></div>
                                    <div class="col col-6">
                                        <input class="form-control" type="date" id="fecha2" /></div>
                                </div>
                            </th>
                            <th>
                                <input class="form-control" type="text" id="txtCoordinador" /></th>
                            <th>
                                <select class="form-control" id="txtEstado">
                                    <option value="">Todas</option>
                                    <option value="2">Cerrada</option>
                                    <option value="1">En Proceso</option>
                                    <option value="3">Firmada</option>
                                </select>
                            </th>
                            <th></th>
                            <th></th>
                        </tr>
                        <tr>
                            <th>Código</th>
                            <th>Nombre</th>
                            <th>Fecha</th>
                            <th>Coordinador</th>
                            <th>Estado</th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <iframe style="display: none" name="frmActa"></iframe>
    <script src="js/InformeActasJS.js"></script>
</asp:Content>
