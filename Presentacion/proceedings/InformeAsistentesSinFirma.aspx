<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="InformeAsistentesSinFirma.aspx.cs" Inherits="Presentacion.proceedings.InformeAsistentesSinFirma" %>

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
                <h6>Listado de usuarios pendientes por firmas</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="row">
                <div class="col col-12">
                    <div class="form-group">
                        <button class="btn btn-success" id="btnExportar">
                            Esportar a excel
                        </button>
                    </div>
                    <div class="tableContainer">
                        <table class="table" id="tbAsistentesSinFirma">
                            <thead>
                                <tr>
                                    <th>Documento</th>
                                    <th>Nombre</th>
                                    <th>Sigla</th>
                                    <th>Nombre del acta</th>
                                </tr>
                                <tr>
                                    <th>
                                        <input type="text" class="form-control" id="txtDocumento" />
                                    </th>
                                    <th>
                                        <input type="text" class="form-control" id="txtNombreUsuario" />
                                    </th>
                                    <th>
                                        <input type="text" class="form-control" id="txtSigla" />
                                    </th>
                                    <th>
                                        <input type="text" class="form-control" id="txtNombreActa" />
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="tbNoFirmados">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js/InformeAsistentesSinFirmaJS.js"></script>
</asp:Content>
