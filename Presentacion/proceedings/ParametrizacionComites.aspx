<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ParametrizacionComites.aspx.cs" Inherits="Presentacion.proceedings.ParametrizacionComites" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    

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
                        <h6>Creación de comités</h6>
                    </div>
                </div>
                <div class="x_content">

                    <asp:UpdatePanel runat="server" ID="upBtncrear" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col col-12 col-md-6 col-lg-3">
                                    <div class="form-group">
                                        <label for="">Sigla</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtSigla" MaxLength="3" />
                                    </div>
                                </div>
                                <div class="col col-12 col-md-6 col-lg-3">
                                    <div class="form-group">
                                        <label for="">tipo</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTipo">
                                            <asp:ListItem Text="Seleccione" Value="-1" />
                                            <asp:ListItem Text="Asistencial" Value="Comite Asistencial" />
                                            <asp:ListItem Text="Administrativo" Value="Comite Administrativo" />
                                            <asp:ListItem Text="Primario" Value="Comite Primario" />
                                            <asp:ListItem Text="Financiero" Value="Comite Financiero" />
                                            <asp:ListItem Text="Sistema de Getión de la Calidad" Value="Sistema de Getión de la Calidad" />

                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col col-12 col-md-6 col-lg-3">
                                    <div class="form-group">
                                        <label for="">Nombre</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
                                    </div>
                                </div>
                                <div class="col col-12 col-md-6 col-lg-3">
                                    <div class="form-group">
                                        <label for="">Unidad Funcional</label>
                                        <asp:DropDownList runat="server" CssClass="form-control" ID="ddlUnidadFuncional">
                                            <asp:ListItem Text="Seleccione" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:Button Text="Crear Comite" runat="server" CssClass="btn btn-success" ID="btnCrearComite" OnClick="btnCrearComite_Click" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row table-responsive">
                        <div class="col col-12">
                            <asp:UpdatePanel runat="server" ID="upComites" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="tablaComites" 
                                        runat="server" 
                                        DataKeyNames="IntOidAReunionC" 
                                        AutoGenerateColumns="False" 
                                        OnRowCancelingEdit="tablaComites_RowCancelingEdit" 
                                        OnRowDeleting="tablaComites_RowDeleting" 
                                        OnRowEditing="tablaComites_RowEditing" 
                                        OnRowUpdating="tablaComites_RowUpdating" 
                                        CssClass="table">
                                        <Columns>
                                            <asp:TemplateField >
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("IntOidAReunionC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sigla">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSiglaTb" MaxLength="3" runat="server" ReadOnly="true" Text='<%# Bind("StrSigla")  %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("StrSigla") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tipo">
                                                <EditItemTemplate>
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTipoTb">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("StrTipo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtNombreTb" runat="server" Text='<%# Bind("StrNomReunion") %>' CssClass="form-control" ></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("StrNomReunion") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unidad Funcional">
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlUnidadFunionalTb" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="Seleccione.." Value="-1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("StrNomUnidadFuncional") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ButtonType="Image" ShowEditButton="true" EditImageUrl="~/Images/Edit.png" UpdateImageUrl="~/Images/Edit.png" CancelImageUrl="~/Images/cancelar.png">
                                                <ControlStyle Width="20px" />
                                            </asp:CommandField>
                                            <asp:CommandField ButtonType="Image" ShowDeleteButton="true" DeleteImageUrl="~/Images/Delete.png">
                                                <ControlStyle Width="20px" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
