<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CreateUser.aspx.cs" Inherits="Presentacion.General.CreateUser" %>

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
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix">
                        <h6>Nuevo Usuario</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col col-3 text-center">
                            <div  id="imagePerfil" class="d-inline-block imagePerfil">
                                <img src="../Images/user.png" />
                                <label for="ContentPlaceHolder_fuImagePerfil">&nbsp;</label>
                            </div>
                            <asp:FileUpload runat="server" ID="fuImagePerfil"  style="display:none"/>
                        </div>
                        <div class="col col-9">
                            <div class="row">
                                <div class="col col-6">
                                    <div class="form-group">
                                        <asp:Label Text="Documento" runat="server" />
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDocumento"/>  
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <asp:Label Text="Nombres Y Apellidos *" runat="server"  />
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre"/>  
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <asp:Label Text="Unidad Funcional *" runat="server"  />
                                        <asp:DropDownList runat="server" ID="txtUnidadFuncional"  CssClass="form-control">
                                            <asp:ListItem Text="Seleccione" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <asp:Label Text="Cargo *" runat="server" />
                                        <asp:DropDownList runat="server" ID="txtCargo"  CssClass="form-control">
                                            <asp:ListItem Text="Seleccione" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <asp:Label Text="Correo Electrónico *" runat="server"  />
                                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Email" ID="txtEmail"/>  
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <asp:Label Text="Contraseña *" runat="server" />
                                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Password"  ID="txtPasssword"/>  
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col col-3">
                            <div class="form-group">
                                <asp:Label Text="EPS *" runat="server" />
                                <asp:DropDownList runat="server" ID="ddlEps"  CssClass="form-control">
                                    <asp:ListItem Text="Seleccione"  Value="-1"/>
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
                                <asp:FileUpload runat="server" ID="txtFoto" CssClass="d-none"/>
                            </div>
                        </div>
                        <div class="col col-3">
                            <div class="form-group">
                                <asp:Label Text="Rol de Usuario *" runat="server" />
                                <asp:DropDownList runat="server" ID="ddlRoles"  CssClass="form-control">
                                    <asp:ListItem Text="Seleccione" Value="-1" />
                                </asp:DropDownList>    
                            </div>
                        </div>
                        <div class="col col-12">
                            <div class="form-group text-right">
                                <asp:Button Text="Crear Usuario" runat="server" CssClass="btn btn-success" ID="btnCrearUsuarios"  OnClick="btnCrearUsuarios_Click"/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12 col-sm-12 ">
            <div class="x_panel">
                <div class="x_title">
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix">
                        <h6>Listado de usuarios</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div style="overflow-x: auto">
                        <table class="table table-hover" id="tbUsuarios">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>
                                        <input type="text" class="form-control form-control-sm" name="documento" id="documento" /></th>
                                    <th>
                                        <input type="text" class="form-control form-control-sm" name="nombre" id="nombre" /></th>
                                    <th>
                                        <input type="text" class="form-control form-control-sm" name="cargo" id="cargo" /></th>
                                    <th>
                                        <input type="text" class="form-control form-control-sm" name="email" id="email" /></th>
                                    <th>
                                        <select class="form-control form-control-sm" name="estado" id="estado">
                                            <option value="Activo" selected="selected">Activo</option>
                                            <option value="Inactivo">Inactivo</option>
                                        </select>
                                    </th>
                                    <th class="static-cell"></th>
                                </tr>
                                <tr>
                                    <th>#</th>
                                    <th>Documento</th>
                                    <th>Nombre</th>
                                    <th>Cargo</th>
                                    <th>Correo Eletrónico</th>
                                    <th>Estado</th>
                                    <th class="static-cell"></th>
                                </tr>
                            </thead>
                            <tbody id="tbdUsuarios">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        
    </div>
    <script src="js/CreateUserJS.js"></script>
</asp:Content>
