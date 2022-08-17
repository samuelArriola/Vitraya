<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ListaReportes.aspx.cs" Inherits="Presentacion.Power_BI.ListaReportes" %>

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
                        <div class="preloader" style="display: inline-block"></div>
                    </div>
                    <p class="text-center">Por favor espere mientras se cargan los reportes</p>
                </div>
            </div>
        </div>
    </div>

    <div class="row">

   <div class="col col-12">

       <div class="page-title">
                <div class="title_center">
                    <h3>Listado de Reportes Power BI</h3>
                </div>
            </div>

            <div class="x_panel">
                <div class="x_content">
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>TABLA DE DATOS</h6>
                        </div>
                    </div>
                 </div>

                    <div class="row">
                        <div class="col col-5">
                            <div class="form-group">
                                <label>Filtro de busqueda por nombre de reporte:</label>
                                <input type="text" class="form-control" id="filtroNomReporte" />
                            </div>
                        </div>
                        <div class="col col-2">
                            <div class="form-group">
                                <button style="margin-top: 27px;" type="button" class="btnFiltroNombreR btn btn-primary">BUSCAR</button>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col col-12">
                            <table class="table" style="overflow:auto; width:100%;" id="tableReportes">
                                <thead>
                                    <tr>
                                        <th>Codigo</th>
                                        <th>Nombre de reporte</th>
                                        <th>Descripción</th>
                                        <th>Opciones</th>
                                    </tr>  
                                </thead>
                                <tbody id="tbInfoR">

                                </tbody>   
                            </table>
                        </div>
                    </div>
               
                </div>

            </div>
     </div>

    <script src="JS/ListaReportesJS.js"></script>

</asp:Content>
