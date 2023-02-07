<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="FotosInventario.aspx.cs" Inherits="Presentacion.InventarioFarmacia.FotosInventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="modal" tabindex="-1" role="dialog" id="loading-modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-center w-100">Ejecutando captura de datos de las cantidades</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="text-center">
                        <div class="preloader" style="display: inline-block"></div>
                    </div>
                    <p class="text-center">Por favor espere mientras se cargan los datos ...</p>
                </div>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="col col-12">
            <div>
                <h3>Generacion y Consulta de Fotos de Inventario</h3>
            </div>
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Generacion de foto actual de cantidades del sistema</h6>
                    </div>
                </div>
                <div class="x_content">

                        <div class="card text-center">
                          <div class="card-header"></div>
                          <div class="card-body">
                            <h5 class="card-title">TOMAR CAPTURA ACTUAL DE LAS CANTIDADES DE INSUMOS CARGADOS EN SISTEMA DINAMICA GERENCIAL</h5>
                            <button type="button" class="btnTomarCaptura btn btn-primary">Tomar captura</button>
                          </div>
                          <div class="card-footer text-muted"></div>
                        </div>

                </div>
            </div>
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Lista de fotos de cantidades en sistema generadas</h6>
                    </div>
                </div>

                <div class="x_content">

                        <div id="divCardsInventario" class="row row row-cols-1 row-cols-md-4 g-4"></div>

                    </div>

            </div>
        </div>
    </div>

    <script src="JS/FotosInventarioJS.js"></script>

</asp:Content>
