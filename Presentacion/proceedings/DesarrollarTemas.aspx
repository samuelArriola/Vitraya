<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DesarrollarTemas.aspx.cs" Inherits="Presentacion.proceedings.DesarrollarTemas" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="ccs/DesarrollarTemasCss.css" rel="stylesheet" />

    <div class="modal" tabindex="-1" role="dialog" id="event-modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">¿Esta seguro de Finalizar el Acta?</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Si continúa con el proceso no prodrá volver a hacer cambios</p>
                </div>
                <div class="modal-footer">
                    <asp:Button Text="Guardar Cambios" runat="server" ID="btnGuardarAct" CssClass="btn btn-success" OnClick="btnGuardarAct_Click"/>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <div id="menu_left">

        <div id="circle_left" style="right: initial"><i class="fa fa-arrow-left"></i></div>
        <div id="content_menu_left" style="transform: translate(0, -50%)" runat="server"></div>
    </div>

    <asp:Label Text="" runat="server" ID="idListArc" Visible="true" />

    <%--Sección del desarrollo del tama--%>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Desarrollo del Tema: <span id="nomTema" runat="server"></span></h6>
            </div>
        </div>
        <div class="x_content">
            <asp:UpdatePanel runat="server" ID="upDesarrollo" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row">
                        <div class="col col-12">
                            <asp:TextBox runat="server" ID="txtEditor" TextMode="MultiLine" CssClass="d-none"></asp:TextBox>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="form-group mt-3">
                <button class="btn btn-success" id="btnGuardarAvance">Guardar avance</button>
            </div>
        </div>
    </div>
    <%--Fin de la sección del desarrollo del tema--%>


    <%--Seccion de la subida de los archivos--%>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Adjuntar archivos al tema</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="row">
                <div class="col col-12">
                    <div id="pnUpLoadArch" class="text-center">
                        <div id="boxUpLoadArch">
                            <svg width="9em" height="9em" viewBox="0 0 16 16" class="bi bi-cloud-arrow-up-fill pt-3" fill="currentColor" xmlns="http://www.w3.org/2000/svg" id="imgUpLoad">
                                <path fill-rule="evenodd" d="M8 2a5.53 5.53 0 0 0-3.594 1.342c-.766.66-1.321 1.52-1.464 2.383C1.266 6.095 0 7.555 0 9.318 0 11.366 1.708 13 3.781 13h8.906C14.502 13 16 11.57 16 9.773c0-1.636-1.242-2.969-2.834-3.194C12.923 3.999 10.69 2 8 2zm2.354 5.146l-2-2a.5.5 0 0 0-.708 0l-2 2a.5.5 0 1 0 .708.708L7.5 6.707V10.5a.5.5 0 0 0 1 0V6.707l1.146 1.147a.5.5 0 0 0 .708-.708z"></path>
                            </svg>
                        </div>
                        <asp:FileUpload runat="server" ID="fuArchivo" class="fuArchivo" onchange="this.form.submit();" />
                    </div>
                    <asp:UpdatePanel runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:GridView ID="tableArch" runat="server" CssClass="table mt-3" DataKeyNames="IntOidGNArchivo" AutoGenerateColumns="False" Width="100%" OnRowCommand="tableArch_RowCommand">
                                <Columns>
                                    <asp:BoundField HeaderText="Id" DataField="IntOidGNArchivo" />
                                    <asp:BoundField HeaderText="Nombre" DataField="StrNombre" />
                                    <asp:BoundField HeaderText="Tipo" DataField="StrExt" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton Text="Eliminar" runat="server" CommandName="eliminar" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <%-- fin de la seccion de la subida de los archivos--%>


    <%--Seccion de los compromisos--%>
    <div class="x_panel">
        <div class="x_title mt-4 col-12 col">
            <div class="clearfix">
                <h6>Compromisos</h6>
            </div>
        </div>
        <div class="x_content">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upCompromisos">
                <ContentTemplate>
                    <div class="form-group row">
                            <label class="col-sm-2 col-form-label">Acción de Mejora (¿Qué?)</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="taActividad" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label class="col-sm-2 col-form-label">Como se Realiza<span> (¿Cómo?)</span></label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="taComo" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                            <label class="col-sm-2 col-form-label">Motivo<span> (¿Por Qué?)</span></label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="taMotivo" runat="server" TextMode="MultiLine" CssClass="form-control" />
                        </div>
                    </div>
                     <div class="form-group row">
                            <label class="col-sm-2 col-form-label">Responsable de la actividad <span> (¿Quién?)</span></label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlResponsableActividad" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlResponsableActividad_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Seleccione Resposable" Value="-1" />
                            </asp:DropDownList>
                        </div>
                    </div>
                     <div class="form-group row">
                            <label class="col-sm-2 col-form-label">Fecha limite <span>(¿Cuándo?)</span></label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtFechaLimiteCompromiso" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                            <label class="col-sm-2 col-form-label">Lugar <span>(¿Dónde?)</span></label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtLugar" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group row">
                            <label class="col-sm-2 col-form-label">¿Cuánto Costará?</span></label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="taCosto" runat="server" TextMode="MultiLine" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">

                            <label class="col-sm-2 col-form-label">Soporte de la Acción de mejora <span>(¿Cómo se Soporta?)</span></label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="taSoporteActividad" runat="server" TextMode="MultiLine" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="form-group row">
                            <label class="col-sm-2 col-form-label">Responsable del seguimiento (¿Quién Realiza Seguimiento?)</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlReposnsableSeguimiento" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlReposnsableSeguimiento_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Text="Seleccione responsable de seguimiento" Value="-1" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                            <label class="col-sm-2 col-form-label">Proceso que se desea mejorar (¿Qué Preceso?)</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddlProceso" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Seleccione Proceso" Value="-1" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-10">
                            <asp:Button Text="Guardar Compromiso" runat="server" CssClass="btn btn-success" ID="btnGuardarCompromiso" OnClick="btnGuardarCompromiso_Click" />
                        </div>
                    </div>
                    <div class="col col-12">
                        <asp:GridView runat="server" ID="tbCompromisos" AutoGenerateColumns="False" CssClass="table table-sm" OnRowCommand="tbCompromisos_RowCommand" DataKeyNames="IntOidPAPlanAccion">
                            <Columns>
                                <asp:BoundField DataField="IntOidPAPlanAccion" />
                                <asp:BoundField HeaderText="Acción de Mejora" DataField="StrActividad" />
                                <asp:BoundField HeaderText="Como se Realiza" DataField="StrComo" />
                                <asp:BoundField HeaderText="Responsable de la actividad" DataField="StrNombreUsuarioResponsable" />
                                <asp:BoundField HeaderText="Fecha limite" DataField="DtmFecFinalActa" DataFormatString="{0:d}" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CommandName="eliminar"><i class="fa fa-trash"></i></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton Text="Editar" runat="server" CommandName="editar" ></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <%--Fin d la sección de los compromisos--%>

    <div class="x_panel">
        <div class="x_content">

            <div class="d-flex justify-content-between">
                <div>
                    <asp:LinkButton ID="btnPrevius" OnClick="btnPrevius_Click" runat="server" CssClass="btn btn-success rounded">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-arrow-circle-left" style="font-size:18px"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:LinkButton>
                    <asp:Button Text="Regresar" runat="server" CssClass="btn btn-success" ID="btnRegresar" Visible="false" OnClick="btnRegresar_Click" />
                </div>
                    <asp:LinkButton ID="btnInicio" OnClick="btnInicio_Click" runat="server" CssClass="btn btn-primary rounded">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-bars" style="font-size:18px"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:LinkButton>
                    <button class="btn btn-primary" id="btnPreView" type="button">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-file"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</button>
                <div>
                    <asp:LinkButton ID="btnNext" OnClick="btnNext_Click" runat="server" CssClass="btn btn-success rounded">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i class="fa fa-arrow-circle-right"  style="font-size:18px"></i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:LinkButton>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Button Text="Guardar y cerrar Acta" runat="server" CssClass="btn btn-success" ID="btnGuardarActa" Visible="false"  />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <iframe style="display:none" name="acta" id="pnActa"></iframe>
    <script src="../Scripts/jquery-3.5.1.min.js"></script>
    <script src="js/tinymce.min.js"></script>
    <script src="js/DesarrollarTemasJS.js"></script>
</asp:Content>
