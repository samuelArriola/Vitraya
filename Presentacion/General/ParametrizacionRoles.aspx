<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ParametrizacionRoles.aspx.cs" Inherits="Presentacion.General.ParametrizacionRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/ParametrizacionRolesCSS.css" rel="stylesheet" />
    <div class="row">
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
                        <h6>Crear un nuevo Rol</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col col-4"></div>
                        <div class="col col-4">
                            <div class="form-group">
                                <h6 class="text-center">
                                    <label>Crear rol</label></h6>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtCrearRol" />
                            </div>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <asp:Button Text="Crear" runat="server" CssClass="btn btn-primary form-control" ID="btnCrearRol" OnClick="btnCrearRol_Click" />
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col col-4"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12 col-sm-12 " id="parteAbajo" runat="server">
            <div class="x_panel">
                <div class="x_title">
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                        <li><a class="close-link"><i class="fa fa-close"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix">
                        <h6>Asignar permisos a Rol</h6>
                    </div>
                </div>
                <div class="x_content">
                    <asp:UpdatePanel runat="server" ID="upPermisos" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col col-4">
                                    <div class="border pnel p-2">
                                        <asp:RadioButtonList runat="server" ID="rblRoles" OnSelectedIndexChanged="rblRoles_SelectedIndexChanged">
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col col-4">
                                    <div class="border pnel p-2" id="pnOpciones" runat="server">
                                    </div>
                                </div>
                                <div class="col col-4">
                                    <div class="border pnel p-2 pn4">
                                        <input type="checkbox" name="crear" id="crear" value="crear" />
                                        <label for="crear">
                                            <p>Crear</p>
                                        </label>
                                        <input type="checkbox" name="modificar" id="modificar" value="modificar" />
                                        <label for="modificar">
                                            <p>Modificar</p>
                                        </label>
                                        <input type="checkbox" name="eliminar" id="eliminar" value="eliminar" />
                                        <label for="eliminar">
                                            <p>Eliminar</p>
                                        </label>
                                        <input type="checkbox" name="confirmar" id="confirmar" value="confirmar" />
                                        <label for="confirmar">
                                            <p>Confirmar</p>
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <script src="js/ParametrizacionRolesJS.js"></script>
</asp:Content>
