<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RevisarDocumento.aspx.cs" Inherits="Presentacion.GestionDocumental.RevisarDocumento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/CrearProcedimientoCss.css" rel="stylesheet" />
    <div class="modal" tabindex="-1" role="dialog" id="event-modal">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Aprobar o Rechazar documento</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">


                    <label><strong>Nombre:</strong></label>
                    <span id="lbNomDoc"></span><br />

                     <label><strong>Elaborado por:</strong></label>
                    <span id="lbElaborador"></span><br />

                     <label><strong>Tipo de Documento:</strong></label>
                    <span id="lbTipo"></span><br />

                    <div class="form-group">
                        <label>Destalles</label>
                        <textarea class="form-control" id="txtDetalles"></textarea>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnAprobar" class="btn btn-success">Aprobar</button>
                    <button type="button" id="btnRechazar" class="btn btn-danger">Rechazar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Aprobar o Rechazar documento</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="row">
                <div class="col col-12 tableContainer">
                    <table class="table" id="tableDocs">
                        <thead>
                            <tr>
                                <th>
                                    <input type="text" id="txtNomDoc" class="form-control" />

                                </th>
                                <th>
                                    <input type="text" id="txtTipDoc" class="form-control" />
                                </th>
                                <th>
                                    <select id="txtEstado" class="form-control">
                                        <option value="">Todos</option>
                                        <option value="0">Preliminar</option>
                                        <option value="1">Construcción</option>
                                        <option value="2">Revisión</option>
                                        <option value="3">Aprobación</option>
                                        <option value="4">Publicado</option>
                                    </select>
                                </th>
                                <th></th>
                                <th></th>
                            </tr>
                            <tr>
                                <th>Nombre del Documento</th>
                                <th>Tipo de Documento</th>
                                <th>Estado</th>
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
    <iframe style="display: none" name="frmIndicador"></iframe>
    <script src="js/RevisarDocumentoJS.js"></script>
</asp:Content>
