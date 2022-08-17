<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RecordMinutes.aspx.cs" Inherits="Presentacion.proceedings.RecordMinutes" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

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
                    <button type="button" class="btn btn-primary" id="CerrarActa">Guardar cambios</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="page-title">
        <div class="title_left">
            <h6>Actas de reunión</h6>
        </div>
    </div>
    <div class="clearfix"></div>
    <div class="row" id="Agregar" runat="server">
        <div class="col-md-12 col-sm-12 ">
            <div class="x_panel">
                <div class="x_title">
                    <%--<h2>Form Design <small>different form elements</small></h2>--%>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix">
                        <h6>Cabecera del Acta</h6>
                    </div>
                </div>
                <div class="x_content">
                    <asp:UpdatePanel runat="server" ID="upCabeceraActa" UpdateMode="Conditional">
                        <ContentTemplate>
                            <!-- Cabecera del Acta -->
                            <div class="row">
                                <div class="col col-6">
                                    <div class="form-group">
                                        <label>Codigo del Acta:</label>
                                        <asp:TextBox ID="txtCodigoActa" runat="server" Class="form-control" Style="border: 1px solid;" Width="100%" ReadOnly="false"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col col-12"></div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <label>Lugar</label>
                                        <asp:TextBox ID="txtLugarReunion" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Fecha</label>
                                        <asp:TextBox ID="txtFechaReunion" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <label>Hora de inicio</label>
                                        <asp:TextBox ID="txtHorainicio" runat="server" TextMode="Time" Width="100%" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Hora final</label>
                                        <asp:TextBox ID="txtHorafinal" runat="server" TextMode="Time" Width="100%" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group col">
                                    <asp:Button CssClass="btn btn-success" Text="Guardar Cabecera" runat="server" ID="btnGuardarCabecera" OnClick="btnGuardarCabecera_Click" />
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <!-- x_content -->
            </div>
            <!-- x_panel -->
        </div>
    </div>

    <asp:UpdatePanel runat="server" ID="uplistaAsistentes" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row" id="Div3" runat="server">
                <div class="col-md-12 col-sm-12 ">
                    <div class="x_panel">
                        <div class="x_title">
                            <ul class="nav navbar-right panel_toolbox">
                                <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                </li>
                                <li><a class="close-link"><i class="fa fa-close"></i></a>
                                </li>
                            </ul>
                            <div class="clearfix">
                                <h6>Verificación del Quorum</h6>
                            </div>
                        </div>
                        <div class="x_content">
                            <!--  Lista de usuarios -->
                            <asp:GridView 
                                ID="GridMembers" 
                                runat="server" 
                                CssClass="table" 
                                AutoGenerateColumns="False" 
                                Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="Nombre de usuario" DataField="Nombre" />
                                    <asp:BoundField HeaderText="Tipo de usuario" DataField="Tipo" />
                                    <asp:TemplateField HeaderText="Asistencia">
                                        <ItemTemplate>
                                            <asp:CheckBox Text="" runat="server" ID="cbAsistencia" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>



                            <!-- fin de la lista cde usuarios-->
                            <div class="form-group">
                                <asp:Button Text="Guardar asistencia" runat="server" class="btn btn-success" ID="btnGuardarAsistencia" OnClick="btnGuardarAsistencia_Click" />
                            </div>
                        </div>
                        <!-- x_content -->
                    </div>
                    <!-- x_panel -->
                </div>
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>
    <div class="row" id="Div2" runat="server">
        <div class="col-md-12 col-sm-12 ">
            <div class="x_panel">
                <div class="x_title">
                    <%--<h2>Form Design <small>different form elements</small></h2>--%>
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix">
                        <h6>Agregar invitados a la reunión</h6>
                    </div>
                </div>
                <div class="x_content">
                    <!-- quorum -->
                    <asp:UpdatePanel runat="server" ID="upDdlAsistencia" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col col-6">
                                    <div class="form-group">
                                        <label>Usuario</label>
                                        <asp:DropDownList ID="DropMiembros" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnAgregarQuorum" runat="server" Text="Agregar" CssClass="btn btn-success" OnClick="btnAgregarQuorum_Click" />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:TextBox runat="server" ID="TextBox11" Visible="false" />
                    <!-- Fin del quorum -->
                </div>
                <!-- x_content -->
            </div>
            <!-- x_panel -->
        </div>
    </div>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Temas a tratar</h6>
            </div>
        </div>
        <div class="x_content">
            <asp:UpdatePanel runat="server" ID="upnListaTemas" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView 
                        ID="tablaTemas" 
                        runat="server" 
                        AutoGenerateColumns="False" 
                        DataKeyNames="IntOidARActasTemas,IntOidARActasC" 
                        CssClass="table table-sm" 
                        OnRowCommand="tablaTemas_RowCommand"
                        OnRowCancelingEdit="tablaTemas_RowCancelingEdit"
                        OnRowDeleting="tablaTemas_RowDeleting"
                        OnRowUpdating="tablaTemas_RowUpdating"
                        OnRowEditing="tablaTemas_RowEditing">
                        <Columns>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:LinkButton runat="server" CommandName="upTema" ID="upTema"><i class="fa fa-arrow-circle-o-up fa-2x"></i></asp:LinkButton>
                                    <asp:LinkButton runat="server" CommandName="downTema" ID="downTema"><i class="fa fa-arrow-circle-o-down fa-2x"></i></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbPosocion" Text='<%# Bind("IntPosicion") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:TextBox runat="server" ID="txtNombre" Text='<%# Bind("StrNomTema")%>' />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="txtNomTema" Text='<%# Bind("StrNomTema") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Image" ShowEditButton="true" EditImageUrl="~/Images/Edit.png" UpdateImageUrl="~/Images/Edit.png" CancelImageUrl="~/Images/cancelar.png">
                                <ControlStyle Width="20px" />
                            </asp:CommandField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ImageUrl="~/Images/Delete.png" runat="server" CommandName="Delete" Width="20px" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="linkbuttonEditar" Text="Desarrollar" runat="server" CommandName="EditarTema" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <div class="form-group">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNombreTema" />
                    </div>
                    <div class="form-group">
                        <asp:Button Text="Guardar tema" runat="server" ID="btnCrearTema" CssClass="btn btn-success" OnClick="btnCrearTema_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <script src="js/tinymce.min.js"></script>
    <script src="js/RecordMinutesJS.js"></script>
    <script>
        crearEditorTextos();
    </script>
</asp:Content>
