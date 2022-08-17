<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ActualizarUsuario.aspx.cs" Inherits="Presentacion.General.ActualizarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/CreateUserCSS.css" rel="stylesheet" />
    <div class="page-title">
        <div class="title_left">
            <h6>Comités</h6>
        </div>
    </div>
    <div class="clearfix"></div>
    <div class="row" id="Agregar" runat="server">
        <div class="col-md-12 col-sm-12 ">
            <div class="x_panel">
                <div class="x_title">

                    <div class="clearfix">
                        <div class="row d-flex justify-content-between">
                            <div class="col col-2">
                                <h6>Nuevo Usuario</h6>
                            </div>
                            <div class="col col-sm-3 col-md-3 col-lg-2 d-flex justify-content-between">
                                <label class="p-2 mr-4">Estado</label>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList AutoPostBack="true" CssClass="form-control" ID="ddlEstadoUsuario" runat="server" OnSelectedIndexChanged="ddlEstadoUsuario_SelectedIndexChanged">
                                            <asp:ListItem Text="Activo" />
                                            <asp:ListItem Text="Inactivo" />
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col col-3 text-center">
                            <div id="imagePerfil" class="d-inline-block imagePerfil">
                                <label for="ContentPlaceHolder_fuImagePerfil">&nbsp;</label>
                            </div>
                            <asp:FileUpload runat="server" ID="fuImagePerfil" Style="display: none" />
                        </div>
                        <div class="col col-9">
                            <div class="row">
                                <div class="col col-6">
                                    <div class="form-group">
                                        <asp:Label Text="Documento *" runat="server" />
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDocumento" />
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <asp:Label Text="Nombres Y Apellidos *" runat="server" />
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <asp:Label Text="Unidad Funcional *" runat="server" />
                                        <asp:DropDownList runat="server" ID="txtUnidadFuncional" CssClass="form-control">
                                            <asp:ListItem Text="Seleccione" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <asp:Label Text="Cargo *" runat="server" />
                                        <asp:DropDownList runat="server" ID="txtCargo" CssClass="form-control">
                                            <asp:ListItem Text="Seleccione" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <asp:Label Text="Correo Electrónico *" runat="server" />
                                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Email" ID="txtEmail" />
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <asp:Label Text="Contraseña" runat="server" />
                                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="txtPasssword" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col col-3">
                            <div class="form-group">
                                <asp:Label Text="EPS *" runat="server" />
                                <asp:DropDownList runat="server" ID="ddlEps" CssClass="form-control">
                                    <asp:ListItem Text="Seleccione" Value="-1" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col col-3">
                            <div class="form-group">
                                <asp:Label Text="Teléfono *" runat="server" />
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTelefono" />
                            </div>
                        </div>
                        <div class="col col-3">
                            <div class="form-group">
                                <asp:Label Text="Firma *" runat="server" />
                                <label class="form-control" for="ContentPlaceHolder_txtFoto" id="lbFoto"></label>
                                <asp:FileUpload runat="server" ID="txtFoto" CssClass="d-none" />
                            </div>
                        </div>
                        <div class="col col-3">
                            <div class="form-group">
                                <asp:Label Text="Rol de Usuario *" runat="server" />
                                <asp:DropDownList runat="server" ID="ddlRoles" CssClass="form-control">
                                    <asp:ListItem Text="Seleccione" Value="-1" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col col-12">
                            <div class="form-group text-right">
                                <asp:Button Text="Actualizar Usuario" runat="server" CssClass="btn btn-success" ID="btnActualizarUsuario" OnClick="btnActualizarUsuario_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js/ActulizarUsuarioJs.js"></script>
</asp:Content>
