<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MisActas.aspx.cs" Inherits="Presentacion.proceedings.MisActas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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
                        <div class="preloader" style="display:inline-block"></div>
                    </div>
                    <p class="text-center">Por favor espere mientras se cargan los datos</p>
                </div>
               
            </div>
        </div>
    </div>
    <div class="page-title">
        <div class="title_left">
            <h6>Mis Actas</h6>
        </div>
    </div>
    <div class="clearfix"></div>
    <div class="row" id="Agregar" runat="server">
        <div class="col-md-12 col-sm-12 ">
            <div class="x_panel">
                <div class="x_content">
                    <div class="tableContainer">
                        <table class="table" id="tbActas">
                            <thead>
                                <tr>
                                    <th style="width: 15%">Código</th>
                                    <th style="width: 25%">Nombre</th>
                                    <th style="width: 20%">Fecha</th>
                                    <th style="width: 20%">Lugar</th>
                                    <th style="width: 10%">Estado</th>
                                    <th style="width: 10%"></th>
                                </tr>
                                <tr>
                                    <th>
                                        <input type="text" class="form-control" id="txtCodigo" />
                                    </th>
                                    <th>
                                        <input type="text" class="form-control" id="txtNombre" />
                                    </th>
                                    <th>
                                        <input type="date" class="form-control" id="txtFecha" />
                                    </th>
                                    <th>
                                        <input type="text" class="form-control" id="txtLugar" />
                                    </th>
                                    <th>
                                        <select class="form-control" id="slcEstado">
                                            <option value="">Todos</option>
                                            <option value="1">Firmado</option>
                                            <option value="0">No firmado</option>
                                        </select></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="printerDiv" style="display:none"></div>
    <script src="js/MisActasJS.js"></script>
</asp:Content>
