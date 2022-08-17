<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Estadisticas.aspx.cs" Inherits="Presentacion.PlanAccion.Estadisticas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/EstadisticasCSS.css" rel="stylesheet" />
    <div class="row">
        <div class="col col-12">
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Filtros</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col col-lg-3 col-md-6 col-12">
                            <div class="form-group">
                                <label>Usuario</label>
                                <asp:DropDownList runat="server" ID="ddlUsuarios" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col col-lg-3 col-md-6 col-12">
                            <div class="form-group">
                                <label>Procesos</label>
                                <asp:DropDownList runat="server" ID="ddlProcesos" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col col-lg-3 col-md-6 col-12">
                            <div class="form-group">
                                <label>Fecha Inicial</label>
                                <input type="date" class="form-control" id="txtFecha1" />
                            </div>
                        </div>
                        <div class="col col-lg-3 col-md-6 col-12">
                            <div class="form-group">
                                <label>Fecha Final</label>
                                <input type="date" class="form-control" id="txtFecha2" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col col-12">
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6 class="mb-3">Grafico dona de porcentajes</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="card-body" id="contenedorCanvas">
                        <canvas id="myChart" height="100" width="400" style="margin: 15px 10px 10px 0px; width: 70px; height: 70px;"></canvas>
                    </div>
                </div>
            </div>
        </div>
        <div class="col col-12">
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Estadisticas</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="tableContainer">
                        <table class="table-inf" style="width: 100%" id="tableEstadisticas">
                            <thead>
                                <tr>
                                    <th>Proceso</th>
                                    <th>Asignado</th>
                                    <th>En proceso</th>
                                    <th>En Evalución</th>
                                    <th>Terminado</th>
                                </tr>
                            </thead>
                            <tbody id="tbEstadistica">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="js/EstadisticasJS.js"></script>
</asp:Content>
