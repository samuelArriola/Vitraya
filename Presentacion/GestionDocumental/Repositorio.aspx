<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Repositorio.aspx.cs" Inherits="Presentacion.GestionDocumental.Repositorio" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <meta http-equiv="Cache-Control" content ="no-cache" />
        <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Información del Plan de Acción</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Área o Proceso</label>
                        <div class="col-sm-10">
                            <p id="txtProceso"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Código</label>
                        <div class="col-sm-10">
                            <p id="txtCodigo"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Estandar de la Norma</label>
                        <div class="col-sm-10">
                            <p id="txtEstandar"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Nombre del Documento</label>
                        <div class="col-sm-10">
                            <p id="txtNombre"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Tipo de Documento</label>
                        <div class="col-sm-10">
                            <p id="txtTipo"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Dirección o Gerencia</label>
                        <div class="col-sm-10">
                            <p id="txtDireccion"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Versión</label>
                        <div class="col-sm-10">
                            <p id="txtVersion"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Clasificación</label>
                        <div class="col-sm-10">
                            <p id="txtClasificacion"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Estado</label>
                        <div class="col-sm-10">
                            <p id="txtEstado"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Cambio</label>
                        <div class="col-sm-10">
                            <p id="txtCambio"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Fecha de Revidsión</label>
                        <div class="col-sm-10">
                            <p id="txtElaborador"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Revisado por</label>
                        <div class="col-sm-10">
                            <p id="txtRevisor"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Fecha de Aprobación</label>
                        <div class="col-sm-10">
                            <p id="txtfechaAprobacion"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Aprobado por</label>
                        <div class="col-sm-10">
                            <p id="txtAprobador"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Fecha de Emisión</label>
                        <div class="col-sm-10">
                            <p id="txtfechaEmision"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Enlace Editable</label>
                        <div class="col-sm-10">
                            <p id="txtEnlace"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Fecha de Actualización</label>
                        <div class="col-sm-10">
                            <p id="txtfechaActualizacion"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Responsable de Actualización</label>
                        <div class="col-sm-10">
                            <p id="txtfechaResponsableActualizacion"></p>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="inputEmail3" class="col-sm-2 col-form-label">Observaciones</label>
                        <div class="col-sm-10">
                            <p id="txtObservaciones"></p>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary">Save changes</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <table class="table" id="tableL">
        <thead>
            <tr>
                <th style="width:20%"><input type="text" class="form-control" id="textProceso"/></th>
                <th style="width:20%"><input type="text" class="form-control" id="textCodigo" /></th>
                <th style="width:30%"><input type="text" class="form-control" id="textNombre"/></th>
                <th style="width:20%"><input type="text" class="form-control" id="textFecha"/></th>
                <th style="width:5%"></th>
                <th style="width:5%"></th>
            </tr>
            <tr>
                <th>Area o Processo</th>
                <th>Código</th>
                <th>Nombre</th>
                <th>Fecha de emisión</th>
                <th></th>
                <th></th>
            </tr>
        </thead> 
        <tbody id="table"></tbody>
    </table>
    <script src="js/RepositorioJS.js"></script>
</asp:Content>
