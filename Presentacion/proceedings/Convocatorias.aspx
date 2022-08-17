<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Convocatorias.aspx.cs" Inherits="Presentacion.proceedings.Convocatorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">


    <div class="modal" tabindex="-1" role="dialog" id="event-modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-center w-100">Enviando Correos Electrónicos</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="text-center">
                        <div class="preloader" style="display:inline-block"></div>
                    </div>
                    <p>Por favor espere mientras se envían las Notificaciones por correo Electrónico</p>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" tabindex="-1" role="dialog" id="event-modal2">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title text-center w-100">Cargando proceso</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="text-center">
                        <div class="preloader" style="display:inline-block"></div>
                    </div>
                    <p>Por favor espere mientras se carga la acción.</p>
                </div>
            </div>
        </div>
    </div>

    <div class="page-title">
        <div class="title_left">
            <h6>Convocatorias</h6>
        </div>
    </div>
    <div class="clearfix"></div>
    <div class="row" id="Agregar" runat="server">
        <div class="col-md-12 col-sm-12 ">
            <div class="x_panel">
                <div class="x_content">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upComites">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col col-12 col-lg-6">
                                    <div class="x_title">
                                        <div class="clearfix">
                                            <h6>Realizar una convocatoria</h6>
                                        </div>
                                    </div>
                                    <asp:GridView runat="server" 
                                        ID="tbComite" 
                                        CssClass="table table-sm" 
                                        AutoGenerateColumns="False" 
                                        OnRowCommand="tbComite_RowCommand"
                                        DataKeyNames ="IntOidAReunionC,IntOidARActas">
                                        <Columns>
                                            <asp:BoundField HeaderText="Comité" DataField="StrNombre" />
                                            <asp:BoundField HeaderText="Fecha Proxima" DataField="DtmFecInicio" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="Convocar" runat="server" CommandName="convocar" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:LinkButton Text="Convocar Reuniones para Gestión General" runat="server" ID="btnConvocar" OnClick="btnConvocar_Click" CssClass="font-weight" />
                                </div>
                                <div class="col col-12 col-lg-6">
                                    <div class="x_title">
                                        <div class="clearfix">
                                            <h6>Mis Convocatorias</h6>
                                        </div>
                                    </div>
                                    <asp:GridView runat="server" ID="tbMisConvocatorias" AutoGenerateColumns="False" CssClass="table table-sm table-striped" OnRowCommand="tbMisConvocatorias_RowCommand">
                                        <Columns>
                                            <asp:BoundField HeaderText="Nombre del comité" DataField="nombre" />
                                            <asp:BoundField HeaderText="Lugar de runuión" DataField="lugar" />
                                            <asp:BoundField HeaderText="Fecha de y hora reunión" DataField="fecha" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="Empezar" runat="server" CommandName="empezar" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="upContenidoComite" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="x_title col col-12 mt-5">
                                    <div class="clearfix">
                                        <h6>Parametrizar convocatoria</h6>
                                    </div>
                                </div>
                                <div class="col col-12 col-lg-4">
                                    <div class="form-group">
                                        <asp:Label Text="Nombre de la Reunión" Visible="false" runat="server" ID="lbNombre" />
                                        <asp:TextBox runat="server" ID="txtNombre" Visible="false" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col col-12 col-lg-4">
                                    <div class="form-group">
                                        <asp:Label Text="Unidad Funcional" runat="server" ID="lbUnidadFuncional" />
                                        <asp:DropDownList runat="server" ID="ddlUnidaFuncional" Visible="false" CssClass="form-control">
                                            <asp:ListItem Text="Seleccione Unidad Funcional" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col col-12 col-lg-4">
                                    <div class="form-group">
                                        <asp:Label Text="Coordinador" runat="server" ID="lbCoordinador" />
                                        <asp:DropDownList runat="server" ID="ddlCoordinador" Visible="false" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col col-12 col-lg-4">
                                    <div class="form-group">
                                        <label>Fecha y hora:</label>
                                        <asp:TextBox runat="server" ID="txtFechaReunion" TextMode="DateTimeLocal" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col col-12 col-lg-4">
                                    <div class="form-group">
                                        <label>Lugar:</label>
                                        <asp:TextBox runat="server" ID="txtLugarReunion" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col col-12 col-lg-4">
                                    <div class="form-group">
                                        <label>Link de la reunión</label>
                                        <asp:TextBox runat="server" ID="txtLink" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col col-12 col-lg-6">
                                    <h6 class="mb-4">Lista de Miembros</h6>
                                    <div class="form-group">
                                        <asp:DropDownList runat="server" CssClass="form-control" AutoPostBack="true" ID="ddlUsuarios" OnSelectedIndexChanged="ddlUsuarios_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                    <asp:GridView runat="server" 
                                        CssClass="table table-sm" 
                                        AutoGenerateColumns="False" 
                                        ID="tbMiembros" 
                                        OnRowDeleting="tbMiembros_RowDeleting"
                                        DataKeyNames="GNCodUsu1">

                                        <Columns>
                                            <asp:BoundField DataField="GNCodUsu1" />
                                            <asp:BoundField DataField="NombreUsuario" HeaderText="Nombre" />
                                            <asp:BoundField DataField="TpNomUsu1" HeaderText="Tipo" />
                                            <asp:TemplateField HeaderText="Estado">
                                                <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlEstadoMiembro">
                                                        <asp:ListItem Text="Activo" Value="1" />
                                                        <asp:ListItem Text="Inactivo" Value="0" />
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ButtonType="Image" ShowDeleteButton="true" DeleteImageUrl="~/Images/Delete.png">
                                                <ControlStyle Width="20px" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                    
                                </div>
                                <div class="col col-12 col-lg-6">
                                    <h6 class="mb-4">Agenda</h6>
                                    <div class="d-flex justify-content-around mb-2">
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtAgregarTema"/>
                                        <asp:Button Text="Agregar" ID="btnAgregarTema" runat="server" CssClass="btn btn-success ml-2" OnClick="btnAgregarTema_Click" />
                                    </div>
                                    <asp:GridView runat="server" CssClass="table table-sm text-center" ID="tbAgenda" AutoGenerateColumns="False"
                                        OnRowCancelingEdit="tbAgenda_RowCancelingEdit"
                                        OnRowDeleting="tbAgenda_RowDeleting"
                                        OnRowEditing="tbAgenda_RowEditing"
                                        OnRowUpdating="tbAgenda_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Tema">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtTema" CssClass="form-control" Text='<%# Bind("Nombre") %>' runat="server" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbTema" Text='<%# Bind("Nombre") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ControlStyle-Width="40" ButtonType="Image" ShowEditButton="true" EditImageUrl="~/Images/Edit.png" UpdateImageUrl="~/Images/Edit.png" CancelImageUrl="~/Images/cancelar.png">
                                                <ControlStyle Width="20px" />
                                            </asp:CommandField>
                                            <asp:CommandField ControlStyle-Width="40" ButtonType="Image" ShowDeleteButton="true" DeleteImageUrl="~/Images/Delete.png">
                                                <ControlStyle Width="20px" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="form-group">
                                    <asp:Button Text="Realizar convocatoria" runat="server" CssClass="btn btn-success" ID="bntConvocar" OnClick="bntConvocar_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <script src="js/ConvocatoriasJS.js"></script>
</asp:Content>
