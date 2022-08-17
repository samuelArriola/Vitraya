<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Userprincipal.aspx.cs" Inherits="Presentacion.trainings.Userprincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/UserprincipalCSS.css" rel="stylesheet" />
    <link href="css/InformesCcss.css" rel="stylesheet" />
    <div class="row">
        <div class="col col-4 x_panel">
            <div class="x_title">
                <h6>Mis Capacitaciones</h6>
            </div>
            <div class="x_content">
                <div class="estados-caps text-center">
                    <input type="radio" name="estado" id="rdM" value="matriculado" checked="checked" />
                    <label for="rdM">Matrculado</label>
                    <input type="radio" name="estado" id="rdA" value="asistido" />
                    <label for="rdA">Asistido</label>
                    <input type="radio" name="estado" id="rdF" value="firmado" />
                    <label for="rdF">Firmado</label>
                </div>
                <div class="form-control search-p d-flex justify-content-between">
                    <input type="text" placeholder="Buscar..." id="txtSearch" />
                    <i class="fa fa-search"></i>
                </div>
                <table class="tbMenu" id="tbAgenda">
                    <thead>
                        <tr>
                            <th colspan="2">Agendas
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="col col-8" id="info-agenda">
            <div class="x_panel">
                <div class="x_content" id="nom-capacitacion">
                </div>
            </div>
            <div class ="x_panel d-none">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Firmar Asistencia</h6>
                    </div>
                </div>
                <div class="x_content" id="panel-firma">

                </div>
            </div>
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Listdo de Subemas</h6>
                    </div>
                </div>
                <div class="x_content" id="content-sutemas">
                    <table class="tbMenu">
                        <thead>
                            <tr>
                                <th>Subtemas</th>
                            </tr>
                        </thead>
                        <tbody>

                        </tbody>
                    </table>
                </div>
            </div>
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Listado de Archivos</h6>
                    </div>
                </div>
                <div class="x_content" id="content-archivos">
                    <table class="tbMenu">
                        <thead>
                            <tr>
                                <th>Archivos anexos</th>
                            </tr>
                        </thead>
                        <tbody>

                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <script src="js/UserprincipalJS.js"></script>
</asp:Content>
