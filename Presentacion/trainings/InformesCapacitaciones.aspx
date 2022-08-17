<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="InformesCapacitaciones.aspx.cs" Inherits="Presentacion.trainings.InformesCapacitaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Informes de Capacitaciones</h6>
            </div>
        </div>
        <div class="x_content">
            <ul class="nav nav-tabs bar_tabs" id="myTab" role="tablist">
                <li class="nav-item">
                    <a class="nav-link active" id="informe-asistencia-tab" data-toggle="tab" href="#informe-asistencia" role="tab" aria-controls="informe-asistencia" aria-selected="true">Informe Asistencia</a>
                </li>
            </ul>
            <div class="tab-content ">
                <div class="tab-pane fade active show" id="informe-asistencia" role="tabpanel" aria-labelledby="informe-asistencia-tab">
                    <button class="btn btn-secondary mb-4" id="btnDescargar" type="button"><i class="fa fa-download"></i> Descargar</button>
                    <div class="tableContainer">
                        <table class="table" id="table-informe-asistencia">
                            <thead>
                                <tr>
                                    <th>Capacitación</th>
                                    <th>Documento</th>
                                    <th>Nombre</th>
                                    <th>Unidad Funcional</th>
                                    <th>Asistencia</th>
                                    <th>Frima</th>
                                </tr>
                                <tr>
                                    <th>
                                        <input type="text" class="form-control" id="txtCapacitacion" />
                                    </th>
                                    <th>
                                        <input type="text" class="form-control" id="txtDocumento" />
                                    </th>
                                    <th>
                                        <input type="text" class="form-control" id="txtNombre" />
                                    </th>
                                    <th>
                                        <input type="text" class="form-control" id="txtUnidad" />
                                    </th>
                                    <th></th>
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
    <script src="js/InformesCapacitacionesJS.js"></script>
</asp:Content>
