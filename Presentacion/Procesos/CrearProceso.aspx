<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CrearProceso.aspx.cs" Inherits="Presentacion.Procesos.CrearProceso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="CSS/CrearProcesosCSS.css" rel="stylesheet" />
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
                    <p>Una vez el Proceso sea enviado a revisión no se prodra volver a realizar cambios</p>
                </div>
                <div class="modal-footer">
                    <button runat="server" id="btnGuardarPro" class="btn btn-success">Guardar Cambios</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Crear Proceso</h6>
            </div>
        </div>
        <div class="x_content">

            <div class="row justify-content-center">

                <div class="col col-10">
                    <div class="form-group">
                        <label>Proceso Padre</label>
                        <asp:DropDownList runat="server" ID="ddlProcesoPadre" CssClass="form-control">
                            <asp:ListItem Text="Seleccione" Value="-1" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col col-10">
                    <div class="form-group">
                        <label>Unidad Funcional</label>
                        <asp:DropDownList runat="server" ID="ddlUnidades" CssClass="form-control">
                            <asp:ListItem Text="Seleccione" Value="-1" />
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtNombre">Nombre:</label>
                        <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                    </div>
                </div>

                <%--Tipo de proceso--%>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_ddlTipPro">Tipo De Proceso</label>
                        <asp:DropDownList runat="server" ID="ddlTipPro" CssClass="form-control">
                            <asp:ListItem Text="Seleccione..." Value="-1" />
                            <asp:ListItem Text="Estratégicos" Value="Estratégicos" />
                            <asp:ListItem Text="Misionales" Value="Misionales" />
                            <asp:ListItem Text="Apoyo " Value="Apoyo " />
                            <asp:ListItem Text="Evaluación" Value="Evaluación" />
                        </asp:DropDownList>
                    </div>
                </div>


                <%--Prefijo--%>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtPrefijo">Prefijo:</label>
                        <asp:TextBox runat="server" ID="txtPrefijo" CssClass="form-control" MaxLength="3" style="text-transform:uppercase" />
                    </div>
                </div>

                <%--objetivo--%>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtObjetivo">Objetivo:</label>
                        <asp:TextBox runat="server" ID="txtObjetivo" CssClass="form-control" Style="height: 80px" TextMode="MultiLine" />
                    </div>
                </div>

                <%--Alcance--%>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtAlcance">Alcance  :</label>
                        <asp:TextBox runat="server" ID="txtAlcance" CssClass="form-control" Style="height: 80px" TextMode="MultiLine" />
                    </div>
                </div>



                <%--Responsables--%>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_ddlLideres">Lider del preceso</label>
                        <asp:DropDownList runat="server" ID="ddlLideres" CssClass="form-control">
                            <asp:ListItem Text="Seleccione..." Value="-1" />
                        </asp:DropDownList>
                    </div>
                    <div id="lstddlLideres" runat="server">
                    </div>
                </div>

                <div class="col col-12 mt-3 mb-4">
                    <div class="x_title">
                        <div class="clearfix">
                        </div>
                    </div>
                </div>

                <%--SIPOC--%>
                <div class="col col-12">
                    <div class="form-group">
                        <h6>Diagrama SIPOC </h6>
                    </div>
                </div>

                <%--Entradas--%>
                <div class="col col-11">
                    <div class="form-group">
                        <h6>Entradas</h6>
                    </div>
                </div>
                <div class="col col-5">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtProvedores">Provedores:</label>
                        <asp:TextBox runat="server" ID="txtProvedores" CssClass="form-control" Style="height: 80px" TextMode="MultiLine" />
                    </div>
                </div>
                <div class="col col-5">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtEntradas">Entradas:</label>
                        <asp:TextBox runat="server" ID="txtEntradas" CssClass="form-control" Style="height: 80px" TextMode="MultiLine" />
                    </div>
                </div>

                <%--Salidas--%>
                <div class="col col-11">
                    <div class="form-group">
                        <h6>Gestión PHVA</h6>
                    </div>
                </div>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_ddlTipoActividad">Tipo Actividad:</label>
                        <asp:DropDownList runat="server" ID="ddlTipoActividad" CssClass="form-control">
                            <asp:ListItem Text="Seleccione..." Value="-1" />
                            <asp:ListItem Text="Planear" Value="Planear" />
                            <asp:ListItem Text="Hacer" Value="Hacer" />
                            <asp:ListItem Text="Verificar " Value="Verificar" />
                            <asp:ListItem Text="Actuar" Value="Actuar" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtActividad">Descripcion Actividad:</label>
                        <asp:TextBox runat="server" ID="txtActividad" CssClass="form-control" Style="height: 80px" TextMode="MultiLine" />
                    </div>
                </div>
                <div class="col col-11">
                    <div class="form-group">
                        <h6>Salidas</h6>
                    </div>
                </div>
                <div class="col col-5">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtClientes">Clientes:</label>
                        <asp:TextBox runat="server" ID="txtClientes" CssClass="form-control" Style="height: 80px" TextMode="MultiLine" />
                    </div>
                </div>
                <div class="col col-5">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtClientes">salidas:</label>
                        <asp:TextBox runat="server" ID="txtSalidas" CssClass="form-control" Style="height: 80px" TextMode="MultiLine" />
                    </div>
                </div>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_ddlResponsables">Responsables:</label>
                        <asp:DropDownList runat="server" ID="ddlResponsables" CssClass="form-control">
                            <asp:ListItem Text="Seleccione..." Value="-1" />
                        </asp:DropDownList>
                    </div>
                    <div id="lstddlResponsables" runat="server">
                    </div>
                </div>
                <div class="col col-10">
                    <div class="form-group">
                        <asp:Button Text="Agregar" runat="server" ID="btnCrearActividad" CssClass="btn btn-success" Style="float: right" />
                    </div>
                </div>

                <div class="col col-10">
                    <div class="" id="">
                        <table class="table table-responsive" id="tbActividades">
                            <thead>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="col col-12 mb-4">
                    <div class="x_title">
                        <div class="clearfix">
                        </div>
                    </div>
                </div>

                <%--Recursos finacieros--%>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtRecFinacieros">Recursos Financieros:</label>
                        <asp:TextBox runat="server" ID="txtRecFinacieros" CssClass="form-control" Style="height: 80px" TextMode="MultiLine" />
                    </div>
                </div>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtRecTeg">Recursos Tecnológicos:</label>
                        <asp:TextBox runat="server" ID="txtRecTeg" CssClass="form-control" Style="height: 80px" TextMode="MultiLine" />
                    </div>
                </div>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtRecInf">Recursos Informáticos:</label>
                        <asp:TextBox runat="server" ID="txtRecInf" CssClass="form-control" Style="height: 80px" TextMode="MultiLine" />
                    </div>
                </div>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtRecFis">Recursos Fisicos:</label>
                        <asp:TextBox runat="server" ID="txtRecFis" CssClass="form-control" Style="height: 80px" TextMode="MultiLine" />
                    </div>
                </div>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtRecMed">Recursos de insumos, medicamentos y dispositivos médicos</label>
                        <asp:TextBox runat="server" ID="txtRecMed" CssClass="form-control" Style="height: 80px" TextMode="MultiLine" />
                    </div>
                </div>

                <%--Recursos humanos: --%>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_ddlRecHumanos">Recursos humanos: </label>
                        <asp:DropDownList runat="server" ID="ddlRecHumanos" CssClass="form-control">
                            <asp:ListItem Text="Seleccione..." Value="-1" />
                        </asp:DropDownList>
                    </div>
                    <div id="lstddlRecHumanos" runat="server">
                    </div>
                </div>

                <%--Normas--%>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtNormas">Normas:</label>
                        <asp:DropDownList runat="server" ID="txtNormas" CssClass="form-control">
                            <asp:ListItem Text="Seleccione" Value="-1" />
                        </asp:DropDownList>
                    </div>
                    <div id="lstNormas">

                    </div>
                </div>

                <%--Riesgos--%>
                <div class="col col-10">
                    <div class="form-group">
                        <label for="ContentPlaceHolder_txtRiesgos">Riesgos:</label>
                        <asp:TextBox runat="server" ID="txtRiesgos" CssClass="form-control" Style="height: 80px" TextMode="MultiLine" />
                    </div>
                </div>

                <%--Documentos relacionados--%>


                <div class="col col-12 mt-3">
                    <div class="x_title">
                        <div class="clearfix">
                        </div>
                    </div>
                </div>

                <%--Flujograma--%>
                <div class="col col-12">
                    <div class="form-group">
                        <h6>Flujograma</h6>
                    </div>
                </div>
                <div class="col col-10 text-center">
                    <div id="imageFlujo" class="d-inline-block">
                        <img src="../Images/diagrama-de-flujo.svg" alt="" />
                        <label for="ContentPlaceHolder_fuImageFlujo">&nbsp;</label>
                    </div>
                    <input type="hidden" value="" id="txtImg" />
                    <asp:FileUpload runat="server" ID="fuImageFlujo" Style="display: none" accept=".jpg, .png, .jpeg, .JPG, .PNG, .JPEG" />
                </div>

                <%--boton Crear Proceso--%>
                <div class="col col-12">
                    <div class="form-group">
                        <asp:Button Text="Crear" runat="server" ID="btnCrearPro" CssClass="btn btn-success" />
                        <button class="btn btn-success" style="float: right" id="btnEnviarSol">Enviar a Revisión</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <iframe name="archivo" id="archivo" style="display:none"></iframe>
    <script src="js/CrearProcesoJS.js"></script>
</asp:Content>
