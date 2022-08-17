<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EncuestaCovid.aspx.cs" Inherits="Presentacion.LinkExternos.EncuestaCovid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <style>
        .table td{
            border-right:0px;
        }
    </style>
    <div class="row">
        <div class="col col-12">
            <div class="page-title">
                <div class="title_left">
                    <h3>Encuesta Diaria Covid</h3>
                </div>
            </div>
            <div class="x_content">
                <div class="x_panel">

                    <div class="row justify-content-center">
                        <div class="col col-lg-8 col-12">
                            <div>
                                <table class="table table-sm" style="overflow: auto; width: 100%; " id="tableDocs">

                                    <tbody>

                                        <tr class="">
                                            <td>Adinamia/Fatiga?</td>
                                            <td>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="adinamia" value="SI">
                                                    <label class="form-check-label">SI</label>
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="adinamia" value="NO" checked>
                                                    <label class="form-check-label">NO</label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Temperatura mayor a 37,5 °C?</td>
                                            <td>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="temperatura" value="SI">
                                                    <label class="form-check-label">SI</label>
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="temperatura" value="NO" checked>
                                                    <label class="form-check-label">NO</label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="">
                                            <td>Ingrese su temperatura</td>
                                            <td>
                                                <input type="text" class="form-control" name="valorTemperatura" >
                                            </td>
                                        </tr>
                                        <tr class="">
                                            <td>Presenta tos?</td>
                                            <td>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="tos" value="SI">
                                                    <label class="form-check-label">SI</label>
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="tos" value="NO" checked>
                                                    <label class="form-check-label">NO</label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="">
                                            <td>Dificultad respiratoria?</td>
                                            <td>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="dificultadRespiratoria" value="SI">
                                                    <label class="form-check-label">SI</label>
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="dificultadRespiratoria" value="NO" checked>
                                                    <label class="form-check-label">NO</label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="">
                                            <td>Odinofagia?</td>
                                            <td>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="odinofagia" value="SI">
                                                    <label class="form-check-label">SI</label>
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="odinofagia" value="NO" checked>
                                                    <label class="form-check-label">NO</label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="">
                                            <td>Dolor lumbar?</td>
                                            <td>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="dolorLumbar" value="SI">
                                                    <label class="form-check-label">SI</label>
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="dolorLumbar" value="NO" checked>
                                                    <label class="form-check-label">NO</label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="">
                                            <td>Dolor torácico?</td>
                                            <td>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="dolorToracico" value="SI">
                                                    <label class="form-check-label">SI</label>
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="dolorToracico" value="NO" checked>
                                                    <label class="form-check-label">NO</label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="">
                                            <td>Malestar general?</td>
                                            <td>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="malestarGeneral" value="SI">
                                                    <label class="form-check-label">SI</label>
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="malestarGeneral" value="NO" checked>
                                                    <label class="form-check-label">NO</label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="">
                                            <td>Pérdida del olfato?</td>
                                            <td>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="perdidaOlfato" value="SI">
                                                    <label class="form-check-label">SI</label>
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="perdidaOlfato" value="NO" checked>
                                                    <label class="form-check-label">NO</label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="">
                                            <td>Pérdida del gusto?</td>
                                            <td>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="perdidaGusto" value="SI">
                                                    <label class="form-check-label">SI</label>
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="perdidaGusto" value="NO" checked>
                                                    <label class="form-check-label">NO</label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="">
                                            <td>Ha recibido todos los elementos de bioseguridad?</td>
                                            <td>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="elemtnosBioseguridad" value="SI" checked>
                                                    <label class="form-check-label">SI</label>
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="elemtnosBioseguridad" value="NO">
                                                    <label class="form-check-label">NO</label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="">
                                            <td>Ha tenido contacto estrecho(A menos de 2 metros de distancia y por minimo 15 minutos) sin protección?</td>
                                            <td>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="contactoEstrecho" value="SI">
                                                    <label class="form-check-label">SI</label>
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="contactoEstrecho" value="NO" checked>
                                                    <label class="form-check-label">NO</label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="SesionContacto">
                                            <td>Nombre de la persona con contacto estrecho</td>
                                            <td>
                                                <input type="text" class="form-control" name="nombreContactoEstrecho">
                                            </td>
                                        </tr>
                                        <tr class="SesionContacto">
                                            <td>Documento de identidad de la persona</td>
                                            <td>
                                                <input type="number" class="form-control" name="idContactoEstrecho">
                                            </td>
                                        </tr>
                                        <tr class="SesionContacto">
                                            <td>Tipo de caso?</td>
                                            <td>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="tipoCaso" value="POSITIVO">
                                                    <label class="form-check-label">POSITIVO</label>
                                                </div>
                                                <div class="form-check form-check-inline">
                                                    <input class="form-check-input checkEncuesta" type="radio" name="tipoCaso" value="SOSPECHOSO">
                                                    <label class="form-check-label">SOSPECHOSO</label>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <button type="button" class="btnGuardar btn btn-primary btn-lg">ENVIAR</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="JS/EncuestaCovidJS.js"></script>

</asp:Content>
