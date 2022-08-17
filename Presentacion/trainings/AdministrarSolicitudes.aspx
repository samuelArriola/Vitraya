<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdministrarSolicitudes.aspx.cs" Inherits="Presentacion.trainings.AdministrarSolicitudes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal fade bd-example-modal-lg" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Solicitud para el tama: <span></span></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">    
                    <div class="form-group">
                        <strong style="font-size:14px">Tema:</strong>
                        <span id="lbTema"></span>
                    </div>  
                    <div class="form-group">
                        <strong style="font-size:14px">Fecha de la Capacitación:</strong>
                    <span id="lbFecha"></span>
                    </div>
                    <div class="form-group">
                        <strong style="font-size:14px">Eje Temático:</strong>
                        <span id="lbEje"></span>
                    </div>
                    <div class="form-group">
                        <strong style="font-size:14px">Archivos:</strong>
                        <span id="listaArch"></span>
                    </div>
                    <div class="form-group">
                        <strong style="font-size:14px">Lugar:</strong>
                        <span id="lbLugar"></span>
                    </div>
                    <div class="form-group">
                        <strong style="font-size:14px">Unidad Funcional:</strong>
                        <span id="lbUnidad"></span>
                    </div>
                    <div class="form-group">
                        <strong style="font-size:14px">Modalidad:</strong>
                        <span id="lbModalidad"></span>
                    </div>
                    <div class="form-group">
                        <strong style="font-size:14px">Responsable:</strong>
                        <span id="lbReponsable"></span>
                    </div>
                    <div class="form-group">
                        <strong style="font-size:14px">Link:</strong>
                        <span id="lbLink"></span>
                    </div>
                    <div class="form-group">
                        <strong style="font-size:14px">Información para la matricula:</strong>
                        <span id="lbInfomación"></span>
                    </div>
                    <div class="form-group">
                        <strong style="font-size:14px">Archivo adjuto para el examen:</strong>
                        <span id="lbExamen"></span>
                    </div>
                    <div class="form-group">
                        <strong style="font-size:14px">Subtemas:</strong>
                        <span id="lbSubtemas"></span> 
                    </div>
                    <div class="form-group">
                        <label>Justificación:</label>
                        <textarea id="txtJustificacion" class="form-control" rows="10"></textarea>
                    </div>
                </div>

                <div class="modal-footer">
                    <button id="btnAceptarSoliciutd" class="btn btn-success">Aceptar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <div class="h6">Administrar Solitudes</div>
            </div>
        </div>
        <div class="x_content">
            <table class="table" id="tableSolicitudes">
                <thead>
                    <tr>
                        <th><input type="text" id="txtTema" class="form-control" /></th>
                        <th>
                            <div class="row">
                                <div class="col col-6">
                                    <input type="date" id="fecha1"  class="form-control"/>    
                                </div>
                                <div class="col col-6">
                                    <input type="date" id="fecha2"  class="form-control"/>    
                                </div>
                            </div>
                        </th>
                        <th><input type="text" id="txtLugar" class="form-control" /></th>
                        <th></th>
                        <th></th>
                    </tr>
                    <tr>
                        <th>Tema</th>
                        <th>Fecha</th>
                        <th>Lugar</th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody></tbody>
            </table>
        </div>
    </div>    
    <script src="js/AdministrarSolicitudesJS.js"></script>
</asp:Content>
