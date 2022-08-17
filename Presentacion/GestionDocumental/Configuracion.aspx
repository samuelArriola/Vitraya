<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Configuracion.aspx.cs" Inherits="Presentacion.GestionDocumental.Configuracion" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="../build/css/tabsCss.css" rel="stylesheet" />
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Configuración de Gestión Documental</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="tab">
                <button class="tablinks" onclick="openDoc(event, 'Procedimientos')">Procedimientos</button>
                <button class="tablinks" onclick="openDoc(event, 'Indicadores')">Indicadores</button>
            </div>

            <!-- Tab content -->
            <div id="Procedimientos" class="tabcontent">
            </div>
            <div id="Indicadores" class="tabcontent">
                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upDominios">
                    <ContentTemplate>
                        <div class="row justify-content-center">
                            <div class="col col-12">
                                <div class="x_title">
                                    <div class="clearfix">
                                        <h6>Configuración del Dominio</h6>
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <label>Nombre del Dominio</label>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNomDominio" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Button Text="Crear" runat="server" CssClass="btn btn-success" ID="btnCrearDominio" OnClick="btnCrearDominio_Click" />
                                    </div>
                                </div>
                                <div class="col col-12">
                                    <asp:GridView ID="tbDominio" runat="server" CssClass="table table-sm"
                                        DataKeyNames="IntOidGDDominio"
                                        AutoGenerateColumns="False"
                                        OnRowCancelingEdit="tbDominio_RowCancelingEdit"
                                        OnRowDeleting="tbDominio_RowDeleting"
                                        OnRowEditing="tbDominio_RowEditing"
                                        OnRowUpdating="tbDominio_RowUpdating">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Codigo">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" Text='<%# Bind("IntOidGDDominio") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("IntOidGDDominio") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nombre">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("StrNombre") %>' CssClass="form-control"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("StrNombre") %>'></asp:Label>
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
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script src="../build/js/tabsJs.js"></script>
</asp:Content>
