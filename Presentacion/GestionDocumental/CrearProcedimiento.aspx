<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CrearProcedimiento.aspx.cs" Inherits="Presentacion.GestionDocumental.CrearProcedimiento" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="modal" tabindex="-1" role="dialog" id="event-modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">¿Esta seguro de iniciar el Proceso de Revisión?</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Una vez el indicador sea enviado a revisión no se prodra volver a realizar cambios</p>
                </div>
                <div class="modal-footer">
                    <asp:Button Text="Guardar Cambios" runat="server" ID="btnGuardarInd" CssClass="btn btn-success" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <link href="css/CrearProcedimientoCss.css" rel="stylesheet" />
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Crear Procedimiento: <span id="txtNomProcedimientotitle" runat="server"></span></h6>
            </div>
        </div>
        <div class="x_content">

            <%--Header--%>
            <asp:UpdatePanel runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="row justify-content-center">
                        <div class="col col-10">
                            <div class="form-group">
                                <label>Versión</label>
                                <input type="number" class="form-control" id="txtVersion" />
                            </div>
                        </div>

                        <div class="col col-10">
                            <div class="form-group">
                                <label>Proceso</label>
                                <asp:DropDownList runat="server" ID="ddlProcesos" CssClass="form-control">
                                    <asp:ListItem Text="Seleccione" Value="-1" />
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col col-10">
                            <div class="form-group">
                                <label>Nombre</label>
                                <input type="text" value="" id="txtNombre" class="form-control" />
                            </div>
                        </div>

                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtAlcance">Alcance:</label>
                                <asp:TextBox runat="server" ID="txtAlcance" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtObjetivo">Objetivo:</label>
                                <asp:TextBox runat="server" ID="txtObjetivo" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_ddlResponsable">Responsable:</label>
                               <div class="d-flex justify-content-between">
                                    <asp:DropDownList runat="server" ID="ddlResponsable" CssClass="form-control">
                                        <asp:ListItem Text="Seleccione..." Value="-1" />
                                    </asp:DropDownList>
                                    <input type="text" class="form-control d-none" id="txtResponsable" />
                                    <button class="btn btn-success ml-2" id="btnChangeControlResponsable" type="button"><i class="fa fa-refresh"></i></button>
                                </div>
                            </div>
                            <div id="lstResponsables" runat="server">
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="row justify-content-center">
                        <%--Recursos necesarios--%>
                        <div class="col col-12">
                            <div class="form-group">
                                <h6>Recursos Necesarios</h6>
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_ddlTalHumano">Talento Humano:</label>
                                <div class="d-flex justify-content-between">
                                     <asp:DropDownList runat="server" ID="ddlTalHumano" CssClass="form-control">
                                        <asp:ListItem Text="Seleccione..." Value="-1" />
                                    </asp:DropDownList>
                                    <input type="text" value="" class="form-control d-none" id="txtTalentoHumano"/>
                                    <button type="button" class="ml-1 btn btn-success" id="btn-change-th"><i class="fa fa-repeat"></i></button>
                                </div>
                            </div>
                            <div id="lsTalHumano" runat="server">
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtEquiBiomedicos">Equipos Biomédicos:</label>
                                <asp:TextBox runat="server" ID="txtEquiBiomedicos" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtMEDiMEInsumos">Medicamentos, dispositivos médicos e insumos:</label>
                                <asp:TextBox runat="server" ID="txtMEDiMEInsumos" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtRFinancieros">Recursos Financieros:</label>
                                <asp:TextBox runat="server" ID="txtRFinancieros" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtRInformticos">Recursos Informáticos:</label>
                                <asp:TextBox runat="server" ID="txtRInformticos" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>

                        <%--Entradas--%>
                        <div class="col col-12">
                            <div class="form-group">
                                <h6>Entradas</h6>
                            </div>
                        </div>
                        <div class="col col-5">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtProvedores">Provedores:</label>
                                <asp:TextBox runat="server" ID="txtProvedores" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>
                        <div class="col col-5">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtEntradas">Entradas:</label>
                                <asp:TextBox runat="server" ID="txtEntradas" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>

                        <%--Salidas--%>
                        <div class="col col-12">
                            <div class="form-group">
                                <h6>Salidas</h6>
                            </div>
                        </div>
                        <div class="col col-5">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtClientes">Clientes:</label>
                                <asp:TextBox runat="server" ID="txtClientes" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>
                        <div class="col col-5">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtSalidas">Salidas:</label>
                                <asp:TextBox runat="server" ID="txtSalidas" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>

                        <%--ACtividades--%>
                        <div class="col col-12">
                            <div class="form-group">
                                <h6>Actividades</h6>
                            </div>
                        </div>

                        <%--Espacio de actividades--%>
                        <div class="col col-10">
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="txtEditor" />
                            </div>
                        </div>
                        <%--Espacio actividades--%>

                        <%--Flujograma--%>
                        <div class="col col-12">
                            <div class="form-group">
                                <h6>Flujograma</h6>
                            </div>
                        </div>
                        <div class="col col-10 text-center">
                            <div id="imageFlujo" class="d-inline-block">
                                <input type="hidden" value="" id="txtImagen" />
                                <img src="../Images/diagrama-de-flujo.svg" alt="" style="width: 100%" />
                                <label for="ContentPlaceHolder_fuImageFlujo">&nbsp;</label>
                            </div>
                            <asp:FileUpload runat="server" ID="fuImageFlujo" Style="display: none" accept=".jpg, .png, .jpeg, .JPG, .PNG, .JPEG" />
                        </div>

                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtProEsperado">Producto Esperado:</label>
                                <asp:TextBox runat="server" ID="txtProEsperado" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtEstCalidad">Estandar de Calidad:</label>
                                <asp:TextBox runat="server" ID="txtEstCalidad" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>

                        <%--Indicadores--%>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="txtIndicadores">Indicadores</label>
                                <textarea id="txtIndicadores"></textarea>
                            </div>
                        </div>

                        <%--Espacio de indicadores--%>

                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtNormas">Refenrencias Normativas</label>
                                <textarea id="txtNormas" class="form-control"></textarea>
                            </div>

                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label>Definiciones:</label>
                                <textarea class="form-control" id="txtDescripcion"></textarea>
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtAnexos">Anexos:</label>
                                <asp:TextBox runat="server" ID="txtAnexos" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="">Documentos Relacionados:</label>
                                <input type="text" value="" id="txtDocumentosRelacionados" />   
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="d-flex justify-content-between">
                                <asp:Button  type="button" Text="Guardar" runat="server" ID="btnGuardar" CssClass="btn btn-success" Style="float: right" />
                                <button class="btn btn-primary" id="btnViewProcedimiento" type="button"><i class="fa fa-file-pdf-o"></i></button>
                                <asp:Button type="button" Text="Enviar Revisión" runat="server" ID="btnEnviarRevision" CssClass="btn btn-primary" Style="float: right" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--Cuerpo--%>
        </div>
    </div>
    <script src="../proceedings/js/tinymce.min.js"></script>
    <script src="js/CrearProcedimientoJs.js"></script>
</asp:Content>
