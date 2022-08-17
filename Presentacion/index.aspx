<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Presentacion.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="x_title">
        <div class="clearfix">
            <h5>Panel de Notificaciones</h5>
        </div>
    </div>
    <div class="row">
        <div class="col col-lg-4 col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Planes de acción: <span id="totalPlanes"></span></h6>
                    </div>
                </div>
                <div class="x_content">
                    <canvas id="myChartPlanes" height="100" width="400" style="margin: 15px 10px 10px 0px; width: 70px; height: 70px;"></canvas>
                </div>
                <div class="text-center">
                    <a href="../PlanAccion/MisPlanes.aspx" class="btn btn-primary">Ver planes</a>
                </div>
            </div>
        </div>
        <div class="col col-lg-4 col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Capacitaciones <span id="totalCapacitaciones"></span></h6>
                    </div>
                </div>
                <div class="x_content">
                    <canvas id="myChartCapacitaciones" height="100" width="400" style="margin: 15px 10px 10px 0px; width: 70px; height: 70px;"></canvas>
                    <div class="text-center">
                        <a href="../trainings/Userprincipal.aspx" class="btn btn-primary">Ver capacitaciones</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="col col-lg-4 col-md-12">
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Actas de Reunión: <span id="totalActas"></span></h6>
                    </div>
                </div>
                <div class="x_content">
                    <canvas id="myChartActas" height="100" width="400" style="margin: 15px 10px 10px 0px; width: 70px; height: 70px;"></canvas>
                </div>
                <div class="text-center">
                    <a href="../proceedings/MisActas.aspx" class="btn btn-primary">Ver actas</a>
                </div>
            </div>
        </div>
    </div>
    <script src="Scripts/Index.js"></script>
</asp:Content>
