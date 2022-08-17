<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CrearListaChequeo.aspx.cs" Inherits="Presentacion.ListasDeChequeo.CrearListaChequeo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script src="../build/js/scripts.js"></script>
    <link href="css/CrearListaChequeoCSS.css" rel="stylesheet" />
    <div class="row">
        <div class="col col-2">
            <table class="tbMenu">
                <thead>
                    <tr>
                        <th>
                            Controles
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <div id="ctrLabel" class="ctr" draggable="true"><i></i>Label</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="ctrTextArea" draggable="true"><i></i>TextArea</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="ctrTextBox" draggable="true"><i></i>TextBox</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div><i></i>Select</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="ctrRadio" draggable="true"><i></i>RadioButton</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div><i></i>Check</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div><i></i>Title</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="ctrCelda" draggable="true"><i></i>Celda</div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="col col-10">
            <div class="canvas"  style="background: #fff; width: 100%; min-height: 700px; border: solid 1px #ccc; padding: 10px">

            </div>
        </div>
    </div>
    <script src="js/CrearListaChequeoJS.js"></script>
</asp:Content>
