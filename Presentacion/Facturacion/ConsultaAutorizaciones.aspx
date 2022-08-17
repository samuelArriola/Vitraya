<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ConsultaAutorizaciones.aspx.cs" Inherits="Presentacion.Facturacion.ConsultaAutorizaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <script src="../build/js/FileSaver.min.js"></script>
    <script src="../build/js/xlsx.full.min.js"></script>
    <script src="../build/js/tableexport.min.js"></script>

    <div class="modal" tabindex="-1" role="dialog" id="loading-modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-center w-100">Consultando Información</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="text-center">
                        <div class="preloader" style="display:inline-block"></div>
                    </div>
                    <p class="text-center">Por favor espere mientras se consulta la autorización.</p>
                </div>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="col col-12">
             <div class="page-title">
                <div class="title_left">
                    <h3>Validación de Autorizaciones</h3>
                </div>
            </div>

            <div runat="server" class="x_panel" id="panelConsulta">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>CONSULTA Y VALIDACIÓN DE AUTORIZACIONES</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col col-6">

                            <div class="form-group">
                                <label for="id">Numero de autorización</label>
                                <input type="text" class="form-control" id="inputAutorizacion" placeholder="Ingrese numero completo de autorización" />

                             </div>
                            
                        </div>
                        <div class="col col-3">

                           <button style="margin-top: 27px;" type="button" class="btnConsultarAut btn btn-primary btn-lg">CONSULTAR</button>
                            
                        </div>
                    </div>

                </div>
             </div>

            <div runat="server" class="x_panel" id="panelAdministrador">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>OPCIONES USUARIO ADMINISTRADOR.</h6>
                    </div>
                </div>

                <div class="x_content">

                    <div class="row">

                        <div class="col col-12">

                            <div class="x_title">
                                <div class="clearfix">
                                    <h6>HISTORICO DE VALIDACIONES</h6>
                                </div>
                                 <div>
                                    <div class="row">
                                        <div class="col col-3">
                                            <div class="form-group">
                                                <label>Fecha Inicial</label>
                                                <input type="month" class="form-control" id="txtFecha1"/>
                                            </div>
                                        </div>
                                        <div class="col col-3">
                                            <div class="form-group">
                                                <label>Fecha Final</label>
                                                <input type="month" class="form-control" id="txtFecha2"/>
                                            </div>
                                        </div>
                                        <div class="col col-3">
                                            <div class="form-group">
                                                <label>Tipo</label>
                                                <select id="TipoI" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example">
                                                  <option value="0" selected>Repetidos y No repetidos</option>
                                                  <option value="1">Repetidos</option>
                                                  <option value="2">No repetidos</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col col-3">
                                            <div class="form-group">
                                                <button style="margin-top: 27px;" type="button" class="BuscarHistorico btn btn-primary">BUSCAR</button>
                                                <button style="margin-top: 27px;" type="button" class="ExportarHistorico btn btn-secondary">EXPORTAR</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <table class="table" style="overflow:auto; width:100%;" id="tableDocs">
                                    <thead>
                                        <tr>
                                            <th>ID empleado</th>
                                            <th>Nombre empleado</th>
                                            <th>Numero autorización</th>
                                            <th>Estado autorización</th>
                                            <th>Fecha</th>
                                        </tr>  
                                    </thead>
                                    <tbody id="tbHistorico">

                                    </tbody>   
                                </table>
                            </div>
                        </div>

                    </div>
                </div>

             </div>

        </div>

    </div>

    <script src="JS/ConsultaAutorizacionesJS.js"></script>

</asp:Content>
