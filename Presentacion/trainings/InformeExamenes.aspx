<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InformeExamenes.aspx.cs" Inherits="Presentacion.trainings.InformeExamenes" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.5.3/dist/css/bootstrap.min.css" integrity="sha384-TX8t27EcRE3e/ihU7zmQxVncDAy5uIKz4rEkgIXeMed4M0jlfIDPvg6uqKI2xXr2" crossorigin="anonymous">
    <link href="../../../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="table table-borderless">
            <thead>
                <tr>
                    <th>
                        <div class="text-center">
                            <img src="../Images/logocrecer.png" />
                        </div>
                        <br />
                        <div class="text-center" style="border: solid 1px #121212; border-radius: 3px; padding: 2px; color: #121212; font-weight: 500; width: 100%">
                            <h4>Resultados para el Examen <span id="nomExa" runat="server"></span></h4>
                        </div>
                        <br />
                        <br />
                        <div id="encabezado" runat="server"></div>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <div id="resultados">
                        </div>
                        <br />
                        <br />
                        <div id="firma" runat="server"></div>
                        ____________________________________________________
                        <div id="datosUsuario" runat="server"></div>
                    </td>
                </tr>
            </tbody>
            <asp:PlaceHolder runat="server">
                <%: Scripts.Render("~/bundles/Webformsjs") %>
            </asp:PlaceHolder>
            <script src="js/InformeExamenesJs.js"></script>
            <script>
                cargarExamneSol();
            </script>
        </table>
    </form>
</body>
</html>
