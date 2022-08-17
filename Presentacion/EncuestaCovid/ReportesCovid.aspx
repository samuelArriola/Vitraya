<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ReportesCovid.aspx.cs" Inherits="Presentacion.EncuestaCovid.ReportesCovid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <script src="../build/js/FileSaver.min.js"></script>
    <script src="../build/js/xlsx.full.min.js"></script>
    <script src="../build/js/tableexport.min.js"></script>

    <style>

       #divTabla {
            overflow-x:scroll;
       }

    </style>

    <div class="row">

        <div class="col col-12">
             <div class="page-title">
                <div class="title_left">
                    <h3>Reportes Encuesta Covid Diaria</h3>
                </div>
            </div>

            <div runat="server" class="x_panel" id="panelAdministrador2">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>TABLA DE REPORTES</h6>
                    </div>
                </div>

                <div class="x_content">

                    <div class="row">

                        <div class="col col-12">

                            <div class="x_title">
                                <div class="clearfix">
                                    <h6>Filtro de busquedas</h6>
                                </div>
                                 <div>
                                    <div class="row">
                                        <div class="col col-3">
                                            <div class="form-group">
                                                <label>Fecha Inicial</label>
                                                <input type="date" class="form-control" id="txtFecha1"/>
                                            </div>
                                        </div>
                                        <div class="col col-3">
                                            <div class="form-group">
                                                <label>Fecha Final</label>
                                                <input type="date" class="form-control" id="txtFecha2"/>
                                            </div>
                                        </div>
                                        <div class="col col-3">
                                            <div class="form-group">
                                                <button style="margin-top: 27px;" type="button" class="BuscarReporte btn btn-primary">BUSCAR</button>
                                                <button style="margin-top: 27px;" type="button" class="ExportarReporte btn btn-secondary">EXPORTAR</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divTabla">
                                <table class="table" style="overflow:auto; width:100%;" id="tableDocs">
                                    <thead>
                                        <tr>
                                            <th>Identificacion</th>
                                            <th>Empleado</th>
                                            <th>Telefono</th>
                                            <th>EPS</th>
                                            <th>Cargo</th>
                                            <th>Unidad</th>
                                            <th>Adinamia</th>
                                            <th>Temperatura sobre 37,5 °C</th>
                                            <th>Temperatura</th>
                                            <th>Tos</th>
                                            <th>Dificultad respiratoria</th>
                                            <th>Odinofagia</th>
                                            <th>Dolor lumbar</th>
                                            <th>Dolor toracico</th>
                                            <th>Malestar general</th>
                                            <th>Perdida olfato</th>
                                            <th>Perdida gusto</th>
                                            <th>Contacto estrecho</th>
                                            <th>Nombre persona CE</th>
                                            <th>Identificacion persona CE</th>
                                            <th>Tipo de caso CE</th>
                                            <th>Elementos de bioseguridad</th>
                                            <th>Fecha</th>
                                        </tr>  
                                    </thead>
                                    <tbody id="tbReporte">

                                    </tbody>   
                                </table>
                            </div>
                        </div>

                    </div>
                </div>

             </div>

        </div>

    </div>

    <script src="JS/ReporteCovidJS.js"></script>

</asp:Content>
