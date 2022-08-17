<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintProceso.aspx.cs" Inherits="Presentacion.Procesos.PrintProceso" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/printH.css" rel="stylesheet" />
    <style type="text/css">
        #table td {
            padding: 2px;
            border: 1px solid #000;
        }

        #table > tbody > tr > td{
            height: 100vh;
        }

        table {
            width: 100%;
        }

        .table-b td {
            border: solid 1px #000;
            padding: 4px;
        }

        .text-vertical {
            writing-mode: vertical-lr;
            text-orientation: upright;
        }
    </style>

    <style media="print">
       @media print{
           #modelo{
               page-break-before:always;
           }
       }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table id="table">
            <thead>
                <tr>
                    <td class="text-center">
                        <table style="width:100%" class="table-b">
                            <tbody>
                                <tr>
                                    <td rowspan="4">
                                        <img src="../Images/logocrecer.png" width="150" />
                                    </td>
                                    <td>
                                        <p><strong>CENTRO M&Eacute;DICO CRECER</strong></p>
                                    </td>
                                    <td>
                                        <p><strong>C&oacute;digo</strong>:<span id="codigo" runat="server"></span></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p><strong>SISTEMA DE GESTI&Oacute;N DOCUMENTAL</strong></p>
                                    </td>
                                    <td>
                                        <p><strong>Versi&oacute;n:&nbsp;</strong>:<span id="version" runat="server"></span> </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td rowspan="2">
                                        <p><strong><span id="NomPro1" runat="server"></span></strong></p>
                                    </td>
                                    <td>
                                        <p><strong>Emisi&oacute;n:</strong>:<span id="fecha" runat="server"></span></p>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <p><strong>P&aacute;gina:</strong>:<span id="paginas" runat="server"></span></p>
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

                        <table class="table-b">
                            <tr>
                                <td colspan="2"><strong>Tipo de Proceso</strong></td>
                                <td><span id="tipo" runat="server"></span></td>
                                <td colspan="2"><strong>Proceso Padre</strong></td>
                                <td><span id="proPadre" runat="server"></span></td>
                                <td><strong>Prefijo</strong></td>
                                <td><span id="prefijo" runat="server"></span></td>
                            </tr>
                            <tr>
                                <td colspan="2"><strong>Lider del Proceso:</strong></td>
                                <td colspan="6"><span id="LiderPro" runat="server"></span></td>
                            </tr>
                            <tr>
                                <td colspan="2"><strong>Alcance</strong></td>
                                <td colspan="6"><span id="Alcance" runat="server"></span></td>
                            </tr>
                            <tr>
                                <td colspan="2"><strong>Objetivo:</strong></td>
                                <td colspan="6"><span id="Objetivo" runat="server"></span></td>
                            </tr>
                        </table>
                        <div id="tblSipoc" runat="server"></div>
                        <div id="modelo" style="width: 100% ; border: solid 1px #000; margin-bottom: 2px;">
                            <strong>MODELADO DEL PROCESO</strong><br />
                         
                        </div>
                        
                        <table class="table-b">
                            <tr>
                                <td colspan="3"><strong>MARCO NORMATIVO</strong></td>
                                <td colspan="3"><strong>DOCUMENTOS RELACIONADOS</strong></td>
                                <td colspan="2"><strong>RECURSOS</strong></td>
                            </tr>
                            <tr>
                                <td colspan="3" rowspan="5"><span id="Normativa" runat="server"></span></td>
                                <td colspan="3" rowspan="5"></td>
                                <td><strong>HUMANOS</strong></td>
                                <td><span id="TalHumano" runat="server"></span></td>
                            </tr>
                            <tr>
                                <td><strong>FINANCIEROS</strong></td>
                                <td><span id="financieros" runat="server"></span></td>
                            </tr>
                            <tr>
                                <td><strong>FISICOS</strong></td>
                                <td><span id="Fisicos" runat="server"></span></td>
                            </tr>
                            <tr>
                                <td><strong>INFORM&Aacute;TICOS</strong></td>
                                <td><span id="informaticos" runat="server"></span></td>
                            </tr>
                            <tr>
                                <td><strong>TECNOL&Oacute;GICOS</strong></td>
                                <td><span id="tecnologicos" runat="server"></span></td>
                            </tr>
                            <tr>
                                <td><strong>INSUMOS, MEDICAMENTOS Y DISPOSITIVOS MÉDICOS</strong></td>
                                <td><span id="medicamentos" runat="server"></span></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tbody>
            <tfoot>
                <tr>
                    <td>
                        <table class="table-b">
                            <tr>
                                <td>Eleborado por</td>
                                <td>Revisado por</td>
                                <td>Aprobado por</td>
                            </tr>
                            <tr>
                                <td>Eleborado por</td>
                                <td>Revisado por</td>
                                <td>Aprobado por</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </tfoot>
        </table>
    </form>
</body>
<script>
    print();
</script>
</html>
