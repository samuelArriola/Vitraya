<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="VerRegistro.aspx.cs" Inherits="Presentacion.Vacunacion.VerRegistro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal" tabindex="-1" role="dialog" id="modal1">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Ver Registro</h5>
                </div>
                <div class="modal-body" id="modalBody1">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                    <button type="button" class="btn btn-danger" id="btnEliminar">Eliminar</button>
                    <button class="btn btn-success" id="bntEditar" type="button">Editar registro</button>
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
    <div class="modal" tabindex="-1" role="dialog" id="ConfirmDeleteModal">
        <div class="modal-dialog modal-sm" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Eliminar registro</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>¿Está seguro de eliminar el Registro?</p>
                    <div class="form-group">
                        <label>Motivo de eliminación</label>
                        <textarea class="form-control" id="txtMotivo"></textarea>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" id="btnDeleteModal">Eliminar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Ver registros de vacunación</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="form group justify-content-md-between d-flex mb-4">
                <button type="button" class="btn btn-success" id="btnDescargar"><i class="fa fa-download mr-2">Descargar</i></button>
                <select id="slcCantidad" class="form-control" style="width: 200px">
                    <option value="top 1000">Primeros 1000</option>
                    <option value="">Todos</option>
                </select>
            </div>
            <table id="tbRegistrosVacunacion" class="table">
                <thead>
                    <tr>
                        <th style="width: 20%">Fecha de vacunación</th>
                        <th>Documento</th>
                        <th>Nombre y apllidos</th>
                        <th>Biológico</th>
                        <th>Etapa</th>
                        <th>Lugar de Vacunación</th>
                        <th></th>
                    </tr>
                    <tr>
                        <th class="justify-content-between d-flex">
                            <input type="date" class="form-control" id="dtFechaVacunacion1" />
                            <input type="date" class="form-control ml-3" id="dtFechaVacunacion2" />
                        </th>
                        <th>
                            <input type="text" class="form-control" id="txtDocumento" /></th>
                        <th>
                            <input type="text" class="form-control" id="txtNombre" /></th>
                        <th>
                            <select class="form-control" id="slcBiologico">
                                <option value="">Todos</option>
                                <option>AstraZeneca</option>
                                <option>Pfizer</option>
                                <option>Sinovac</option>
                                <option>Janssen</option>
                                <option>Moderna</option>
                            </select>
                        </th>
                        <th>
                            <select class="form-control" id="slcEtapa">
                                <option value="">Todas</option>
                                <option>ETAPA1</option>
                                <option>ETAPA2</option>
                                <option>ETAPA3</option>
                                <option>ETAPA4</option>
                                <option>ETAPA5</option>
                            </select>
                        </th>
                        <th>
                            <select class="form-control" id="slcLugar">
                                <option value="">Todos</option>
                                <option>Cuidado seguro en casa</option>
                                <option>Clínica Crecer</option>
                            </select>
                        </th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>
        </div>
    </div>
    <script src="js/VerRegistro.js"></script>
</asp:Content>
