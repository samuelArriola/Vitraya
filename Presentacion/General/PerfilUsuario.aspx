<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PerfilUsuario.aspx.cs"
    Inherits="Presentacion.General.PerfilUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/CreateUserCSS.css" rel="stylesheet" />
    <!-- Modal de para el formulario de actualizar datos de usuario -->
    <div class="modal fade" tabindex="-1" role="dialog" id="mdlActualizarUsuario">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Actulizar usuario</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="d-flex justify-content-center">
                    <div class="w-75 text-center">
                        <div class="imagePerfil d-inline-block mb-5" runat="server" id="imagePerfil2">

                        </div>
                        <div></div>
                        <asp:FileUpload runat="server"  ID="fuFotoPerfil" CssClass="d-none"/> 
                        <a href="#" id="lnkEditarFoto">Editar foto de perfil</a>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:Label Text="Nombre y Apellidos" runat="server" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNombreE" AutoPostBack="false"/>
                    </div>
                    <div class="form-group">
                        <asp:Label Text="Correo electrónico" runat="server" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtEmailE" AutoPostBack="false" TextMode="Email"/>
                    </div>
                    <div class="form-group">
                        <asp:Label Text="Teléfono" runat="server" />
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtTelefonoE" AutoPostBack="false" TextMode="Phone"/>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button Text="Actualizar" ID="btnActualizar" CssClass="btn btn-success"  runat="server" OnClick="btnActualizar_Click"/>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Fin del modal -->

    <div class="row"  runat="server">
        <div class="col-md-12 col-sm-12 ">
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h5>Perfil de usuario</h5>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col col-12 col-md-4 col-lg-2 text-center">
                            <div  id="imagePerfil1" runat="server" class="d-inline-block imagePerfil">
                               
                            </div>
                            <asp:FileUpload runat="server" ID="fuImagePerfil" Style="display: none" />
                        </div>
                        <div class="col col-12 col-md-8 col-lg-10">
                            <div class="row">
                                <div class="col col-12 col-lg-6 col-sm-6">
                                    <div class="form-group">
                                        <asp:Label Text="Documento" runat="server" />
                                        <div runat="server" class="info-control" id="txtDocumento"></div>
                                    </div>
                                </div>
                                <div class="col col-12 col-lg-6 col-sm-6">
                                    <div class="form-group">
                                        <asp:Label Text="Nombres Y Apellidos" runat="server" />
                                        <div runat="server" class="info-control" id="txtNombre" />
                                    </div>
                                </div>

                                <div class="col col-12 col-lg-6 col-sm-6">
                                    <div class="form-group">
                                        <asp:Label Text="Unidad Funcional" runat="server" />
                                        <div runat="server" id="txtUnidadFuncional" class="info-control">
                                        </div>
                                    </div>
                                </div>
                                <div class="col col-12 col-lg-6 col-sm-6">
                                    <div class="form-group">
                                        <asp:Label Text="Cargo" runat="server" />
                                        <div runat="server" id="txtCargo" class="info-control">
                                        </div>
                                    </div>
                                </div>
                                <div class="col col-12 col-lg-6 col-sm-6">
                                    <div class="form-group">
                                        <asp:Label Text="Correo Electrónico " runat="server" />
                                        <div runat="server" class="info-control" type="Email" id="txtEmail"></div>
                                    </div>
                                </div>
                                
                                <div class="col col-12 col-lg-6 col-sm-6">
                                    <div class="form-group">
                                        <asp:Label Text="EPS" runat="server" />
                                        <div runat="server" id="ddlEps" class="info-control">
                                        </div>
                                    </div>
                                </div>
                                <div class="col col-12 col-lg-6 col-sm-6">
                                    <div class="form-group">
                                        <asp:Label Text="Teléfono " runat="server" />
                                        <div runat="server" class="info-control" id="txtTelefono"></div>
                                    </div>
                                </div>
                                <div class="col col-12 col-lg-6 col-sm-6">
                                    <div class="form-group">
                                        <asp:Label Text="Rol de Usuario" runat="server" />
                                        <div runat="server" id="ddlRoles" class="info-control">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col col-12">
                            <div class="form-group text-right">
                                <button class="btn btn-success" type="button" onclick="$('#mdlActualizarUsuario').modal()" id="btnShowModalUpdate">Actualizar usuario</button>
                                <button class="btn btn-primary ml-2" type="button" onclick="$('#mdChangePass').modal()">Cambiar contraseña</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js/PerfilUsuarioJS.js"></script>

</asp:Content>
