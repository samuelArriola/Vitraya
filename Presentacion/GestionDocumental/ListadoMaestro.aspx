<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ListadoMaestro.aspx.cs" Inherits="Presentacion.GestionDocumental.ListadoMaestro1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal fade bd-example-modal-lg" id="modal-view-documento" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Información del Plan de Acción</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table class="table-inf">
                        <tr>
                            <td>Procesos</td>
                            <td><p id="lbProcesos"></p></td>
                        </tr>
                        <tr>
                            <td>Nombre</td>
                            <td><p id="lbNombre"></p></td>
                        </tr>
                        <tr>
                            <td>Estado</td>
                            <td><p id="lbEstado"></p></td>
                        </tr>
                        <tr>
                            <td>Cambio</td>
                            <td><p id="lbCambio"></p></td>
                        </tr>
                        <tr>
                            <td>Fecha de elaboración</td>
                            <td><p id="lbFecElab"></p></td>
                        </tr>
                        <tr>
                            <td>Elaborador</td>
                            <td><p id="lbElaborador"></p></td>
                        </tr>
                        <tr>
                            <td>Fecha de revisión</td>
                            <td><p id="lbFecRev"></p></td>
                        </tr>
                        <tr>
                            <td>Revisor</td>
                            <td><p id="lbRevisor"></p></td>
                        </tr>
                        <tr>
                            <td>Fecha de aprobación</td>
                            <td><p id="lbFecApro"></p></td>
                        </tr>
                        <tr>
                            <td>Aprobador</td>
                            <td><p id="lbAprobador"></p></td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>




    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Flujo de gestión documental</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="row">
                <div class="col col-12">
                    <a href="#" id="btnExpotExcel">Exportar a Excel</a>
                    <div class="tableContainer">
                        <table class="table" id="tableDocs">
                            <thead>
                                <tr>
                                    <th>Proceso</th>
                                    <th style="width:150px;">Código</th>
                                    <th>Nombre</th>
                                    <th>Tipo de Documento</th>
                                    <th>Versión</th>
                                    <th>Estado</th>
                                    <th style="width: 100px" class="static-cell"></th>
                                </tr>  
                                <tr>
                                    <th><input type="text" class="form-control" id="txtProceso" /></th>
                                    <th><input type="text" class="form-control" id="txtCodigo" /></th>
                                    <th><input type="text" class="form-control" id="txtNombre" /></th>
                                    <th>
                                        <select class="form-control" id="slcTipo">
                                            <option>Indicador</option>
                                            <option>Procedimiento</option>
                                            <option>Protocolo</option>
                                            <option>Manual</option>
                                        </select>
                                    </th>
                                    <th><input type="text" class="form-control" id="txtVersion" /></th>
                                    <th></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody id="tbDocsRev">

                            </tbody>   
                        </table>
                    </div>
                </div>
            </div>
        </div> 
    </div>
    <script src="js/ListadoMaestroJS.js"></script>
</asp:Content>
