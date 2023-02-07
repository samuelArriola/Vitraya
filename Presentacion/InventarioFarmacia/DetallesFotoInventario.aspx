<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DetallesFotoInventario.aspx.cs" Inherits="Presentacion.InventarioFarmacia.DetallesFotoInventario" %>
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
                    <p class="text-center">Por favor espere mientras se cargan los datos de la foto de inventario</p>
                </div>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="col col-12">
             <div class="page-title">
                <div class="title_left">
                    <h3>Detalles de Registros de Foto Inventario</h3>
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
                                        <div class="col col-4">
                                            <div class="form-group">
                                                <label>Nombre o codigo:</label>
                                                <input type="text" class="form-control" id="filtroCodNom"/>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="x_title">
                                <div class="clearfix">
                                </div>
                            </div>

                                <table class="table" style="overflow:auto; width:100%;" id="tableDetallesFoto">
                                    <thead>
                                        <tr>
                                            <th>Cod almacen</th>
                                            <th>Nom almacen</th>
                                            <th>Cod producto</th>
                                            <th>Nom producto</th>
                                            <th>Est producto</th>
                                            <th>Cod lote</th>
                                            <th>Fec vencimiento</th>
                                            <th>Cant sistema</th>

                                        </tr>  
                                    </thead>
                                    <tbody id="tbDetallesF">

                                    </tbody>   
                                </table>
    
                        </div>

                    </div>
                </div>

             </div>

        </div>

    <script src="JS/DetallesFotoInventarioJS.js"></script>

</asp:Content>
