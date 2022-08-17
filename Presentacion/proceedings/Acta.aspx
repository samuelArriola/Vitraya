<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Acta.aspx.cs" Inherits="Generales_1._0.Home.dashboard.production.screens.proceedings.Acta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title></title>
    <link href="~/vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="~/vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/css/print.css" rel="stylesheet" />
    <link rel="stylesheet" href="~/GestionDocumental/css/confAPA.css">
</head>
<body>
    <form id="form1" runat="server">


        <table id="tablePrincipal">
            <thead>
                <tr>
                    <td id="header" class="text-center">
                        <table style="width: 100%" class="table-border">
                            <tr>
                                <td rowspan="4" class="text-center">
                                    <img src="../Images/logocrecer.png" width="150" />
                                </td>
                                <td>
                                    <p><strong>CENTRO M&Eacute;DICO CRECER</strong></p>
                                </td>
                                <td>
                                    <p><strong>CÓDIGO</strong>:<span id="codigoActa" runat="server"></span></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p><strong>SISTEMA DE GESTI&Oacute;N DOCUMENTAL</strong></p>
                                </td>
                                <td>
                                    <p><strong>VERSIÓN</strong>:1.0</p>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="2">
                                    <p><strong>ACTA DE COMITÉ &nbsp;<span id="txtNombre1" runat="server"></span></strong></p>
                                </td>
                                <td>
                                    <p><strong>EMISIÓN</strong>:<span id="lbFecha1" runat="server"></span></p>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <p><strong>PÁGINA</strong>:<span id="lbPAginas1" runat="server"></span></p>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <table class="table-border">
                            <tr>
                                <td>Lugar de reunión:</td>
                                <td>Fecha:</td>
                                <td>Hora de inicio</td>
                                <td>Hora de finalización</td>
                            </tr>
                            <tr>
                                <td><span id="lugar" runat="server"></span></td>
                                <td><span id="fecha" runat="server"></span></td>
                                <td><span id="horInicio" runat="server"></span></td>
                                <td><span id="horaFinal" runat="server"></span></td>
                            </tr>
                        </table>

                        <br />

                        <p>
                            EL comité: <strong><em><span id="txtNombre" runat="server"></span></em></strong> del Centro Médico Crecer, 
                                    Se reúnen el día <span id="txtFecha" runat="server"></span> a las <span id="txtHora" runat="server"></span>
                            en <span id="txtlugar" runat="server"></span>, los miembors e invitados, con el fin de realizar el comité de: 
                                    <strong><em><span id="txtNombre2" runat="server"></span></em></strong> del Centro médico Crecer del mes de <span id="txtMes" runat="server"></span>.
                        </p>

                        <h3 class="text-center">Orden del dia</h3>
                        <ul id="ordenDia" runat="server">
                            <li>Verificación del Quorum</li>
                        </ul>
                        <div class="sec">
                            <h4 class="text-center">Verificación del Quorum</h4>
                            <p>Se realiza la verificación Quorum:</p>
                            <table class="table-border">
                                <thead>
                                    <tr>
                                        <th>Nombre(s) y Apellidos</th>
                                        <th>Cargo</th>
                                        <th>Asistencia</th>
                                    </tr>
                                </thead>
                                <tbody id="tableAsistencia" runat="server">
                                </tbody>
                            </table>
                        </div>
                        <div class="sec">
                            <h3 class="text-center">DESARRROLLO DEL COMITÉ</h3>
                            <div id="desarrollo" runat="server"></div>
                        </div>
                        

                        <div class="sec">
                            <h4 class="text-center">Planes de acción</h4>
                            <div id="planes" runat="server"></div>
                        </div>

                        <br />
                        <div class="sec">
                            <h4 class="text-center">Aprobación del acta.</h4>
                            <div id="participantesFirma" runat="server"></div>
                        </div>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td>
                        <p class="text-center" style="font-family: Aria; font-style: italic; line-height: normal">
                            La última versión de cada documento será la única válida para su utilización y estará disponible en el sistema de información institucional Investigue antes de sacar copias de este documento porque corre el riesgo de tener una versión desactualizada                        </p>
                    </td>
                </tr>
            </tfoot>
        </table>
    </form>
    <script>
        window.print();
    </script>
</body>
</html>

