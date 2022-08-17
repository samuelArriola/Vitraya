<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="HistoricoCierres.aspx.cs" Inherits="Presentacion.Facturacion.HistoricoCierres" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    
    <div class="modal" tabindex="-1" role="dialog" id="loading-modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-center w-100">Cargando Información</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="text-center">
                        <div class="preloader" style="display:inline-block"></div>
                    </div>
                    <p class="text-center">Por favor espere mientras se cargan los datos del historico</p>
                </div>
            </div>
        </div>
    </div>

   <%-- <div class="modal" tabindex="-1" role="dialog" id="modal2">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Detalles de Paciente</h5>
                </div>
                <div class="modal-body" id="modalBody2">

                    <p id="PNombrePaciente"></p>
                    <p id="PIdentificacionPaciente"></p>

                    <table class="table" style="overflow:auto;" id="tableDetalles">
                        <thead>
                            <tr>
                                
                                <th>Numero de Ingreso</th>
                                <th style="display:none;" >Numero de Identificacion</th>
                                <th>Tipo de Servicio</th>
                                <th>Unidad Funcional</th>
                                <th>Cups</th>
                                <th>Cups Descripcion</th>
                                <th>Cama</th>
                                <th>Opciones</th>

                            </tr>  
                        </thead>
                        <tbody id="tbInfoDetalles">

                        </tbody>   
                    </table>

                    <div id="DivCerrar" ></div>
                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">SALIR</button>
                </div>
            </div>
        </div>
    </div>--%>

     <div class="row">

        <div class="col col-12">
             <div class="page-title">
                <div class="title_left">
                    <h3>Historico de Cierre de Pacientes</h3>
                </div>
            </div>

            <div runat="server" class="x_panel" id="panelAdministrador">

                <div class="x_content">

                    <div class="row">

                        <div class="col col-12">

                            <div class="x_title">
                                <div class="clearfix">
                                    <h6 style = "float: left;" >Filtro de busquedas</h6>
                                </div>
                            </div>
                                 <div>
                                    <div class="row">
                                        <div class="col col-3">
                                            <div class="form-group">
                                                <label>Admision:</label>
                                                <input type="number" class="form-control" id="filtroIngreso"/>
                                            </div>
                                        </div>
                                        <div class="col col-3">
                                            <div class="form-group">
                                                <label>Usuario de Cierre:</label>
                                                <input type="text" class="form-control" id="filtroUsuario"/>
                                            </div>
                                        </div>
                                        <div class="col col-1">
                                            <div class="form-group">
                                                <button style="margin-top: 27px;" type="button" class="btnBuscarH btn btn-primary">BUSCAR</button>
                                            </div>
                                        </div>
                                        <div class="col col-2">
                                            <div class="form-group">
                                                <label>Fecha Inicial de Cierre:</label>
                                                <input type="date" class="form-control" id="filtroFechaC"/>
                                            </div>
                                        </div>
                                        <div class="col col-2">
                                            <div class="form-group">
                                                <label>Fecha Final de Cierre:</label>
                                                <input type="date" class="form-control" id="filtroFechaC2"/>
                                            </div>
                                        </div>
                                        <div class="col col-1">
                                            <div class="form-group">
                                                <button style="margin-top: 27px;" type="button" class="btnBuscarFec btn btn-primary">BUSCAR</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="x_title">
                                <div class="clearfix">
                                </div>
                            </div>

                                <table class="table" style="overflow:auto; width:100%;" id="tableHistoricoCierre">
                                    <thead>
                                        <tr>
                                            <th style="width: 130px; text-align: center;" >Admision</th>
                                            <th style="width: 120px; text-align: center;" >Estado</th>
                                            <th style="width: 200px; text-align: center;" >Usuario de Cierre</th>
                                            <th style="width: 160px; text-align: center;">Fecha de Cierre</th>
                                            <th style="width: 900px;" >Motivo</th>
                                        </tr>  
                                    </thead>
                                    <tbody id="tbHistoricoCierre">

                                    </tbody>   
                                </table>
    
                        </div>

                    </div>
                </div>

             </div>

        </div>

    <script src="JS/HistoricoCierresJS.js"></script>

</asp:Content>
