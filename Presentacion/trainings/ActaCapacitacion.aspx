<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActaCapacitacion.aspx.cs" Inherits="Generales_1._0.Home.dashboard.production.screens.trainings.ActaCapacitacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../../../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="../../css/print.css" rel="stylesheet" />
    <link href="../GestionDocumental/css/confAPA.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table id="tablePrincipal">
            <thead>
                <tr>
                    <td id="header" style="text-align: center">
                       <table style="width: 100%" class="table-border">
                            <tbody>
                                <tr>
                                    <td rowspan="4">
                                        <img src="../Images/logocrecer.png" width="150" />
                                    </td>
                                    <td>
                                        <p><strong>CENTRO MÉDICO CRECER</strong></p>
                                    </td>
                                    <td>
                                        <p><strong>Código</strong>:<span id="lbCodigo" runat="server"></span></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p><strong>SISTEMA DE GESTIÓN DOCUMENTAL</strong></p>
                                    </td>
                                    <td>
                                        <p><strong>Versión&nbsp;</strong>:<span id="lbVersion" runat="server"></span>1</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="2">
                                        <p><strong><span id="lbNombre" runat="server"></span></strong></p>
                                    </td>
                                    <td>
                                        <p><strong>Emisión</strong>:<span id="lbFecha" runat="server"></span></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p><strong>Página</strong>:<span id="lbPAginas" runat="server"></span></p>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </td>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <div id="contentCapacitacion" runat="server">

                        </div>

                        <table class="table-border">
                           <thead>
                               <tr>
                                   <th>Lugar</th>
                                   <th>Hora Inicial </th>
                                   <th>Hora Final </th>
                               </tr>
                               <tr>
                                   <th><div id="lbLugar" runat="server"></div></th>
                                   <th><div id="lbHoraInicial" runat="server"></div></th>
                                   <th>
                                       <div id="lbHoraFinal" runat="server"></div>
                                   </th>
                               </tr>
                           </thead>    
                        </table>
                        <div id="contenidoActa" runat="server">

                        </div>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td>

                    </td>
                </tr>
            </tfoot>
        </table>
        <script>
            print();
        </script>
    </form>
</body>
</html>
