<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EstadisticasEC.aspx.cs" Inherits="Presentacion.EncuestaCovid.EstadisticasEC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <script src="../build/js/FileSaver.min.js"></script>
    <script src="../build/js/xlsx.full.min.js"></script>
    <script src="../build/js/tableexport.min.js"></script>

    <div class="row">

        <div class="col col-12">
             <div class="page-title">
                <div class="title_left">
                    <h3>Estadisticas encuesta diaria Covid</h3>
                </div>
            </div>
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Filtro de busqueda por meses</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col col-5">
                            <div class="form-group">
                                <label>Mes Inicial</label>
                                <input type="date" class="form-control" id="txtFechaMes1" />
                            </div>
                        </div>
                        <div class="col col-5">
                            <div class="form-group">
                                <label>Mes Final</label>
                                <input type="date" class="form-control" id="txtFechaMes2" />
                            </div>
                        </div>
                        <div class="col col-2">
                            <div class="form-group">
                                <button style="margin-top: 27px;" type="button" class="btnBuscarMes btn btn-primary">Buscar</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Filtro de busqueda por dias</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col col-5">
                            <div class="form-group">
                                <label>Dia Inicial</label>
                                <input type="date" class="form-control" id="txtFechaDia1" />
                            </div>
                        </div>
                        <div class="col col-5">
                            <div class="form-group">
                                <label>Dia Final</label>
                                <input type="date" class="form-control" id="txtFechaDia2" />
                            </div>
                        </div>
                        <div class="col col-2">
                            <div class="form-group">
                                <button style="margin-top: 27px;" type="button" class="btnBuscarDia btn btn-primary">Buscar</button>
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
                        <h6 class="mb-3">Grafico barra de porcentajes de diligenciamiento de encuesta por meses</h6>
                        <h6 class="mb-3" id="infoMeses">Fechas</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="card-body" id="contenedorCanvas">
                     <canvas id="myChartMeses" style="margin: 15px 10px 10px 0px; width: 70px; height: 70px;"></canvas>
                    </div>
                </div>
             </div>
        </div>
         <div class="col col-12">
             <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6 class="mb-3">Grafico barra de porcentajes de diligenciamiento de encuesta por dias</h6>
                        <h6 class="mb-3" id="infoDias">Fechas</h6>
                    </div>
                </div>
                <div class="x_content">

                    <div class="card-body" id="contenedorCanvas2">
                     <canvas id="myChartDias" height="100" width="400" style="margin: 15px 10px 10px 0px; width: 70px; height: 70px;"></canvas>
                    </div>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <button style="margin-top: 27px;" id="exportInfoDetalle" type="button" class="exportInfoDetalle btn btn-primary">EXPORTAR INFORMACION DE LA TABLA</button>
                            </div>
                        </div>
                    </div>
                    <table class="table" style="overflow:auto; width:100%;" id="tableInfo">
                        <thead>
                            <tr>
                                <th>ID empleado</th>
                                <th>Nombre empleado</th>
                                <th>Cantidad de diligenciamiento</th>
                            </tr>  
                        </thead>
                        <tbody id="tbInfo">

                        </tbody>   
                    </table>
                </div>
             </div>
        </div>
    </div>
    <script src="JS/EstadisticasECJS.js"></script>

</asp:Content>
