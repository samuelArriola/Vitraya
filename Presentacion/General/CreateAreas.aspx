<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CreateAreas.aspx.cs" Inherits="Presentacion.General.CreateAreas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>Gestion de Direcciones</h3>
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
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <br />
                        <div class="item form-group">
                            <label class="col-form-label col-md-3 col-sm-3 label-align" for="first-name">
                                nombre de la Direccion<span class="required">*</span>
                            </label>
                            <div class="col-md-6 col-sm-6 ">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>

                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="item form-group">
                            <label class="col-form-label col-md-3 col-sm-3 label-align" for="first-name">
                                Sigla de la direccion<span class="required">*</span>
                            </label>
                            <div class="col-md-6 col-sm-6 ">
                                <asp:UpdatePanel runat="server" ID="updateSigla">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" MaxLength="3" AutoPostBack="true"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="ln_solid"></div>
                        <div class="item form-group">
                            <div class="col-md-6 col-sm-6 offset-md-3">
                                <asp:UpdatePanel runat="server" ID="updateBoton">
                                    <ContentTemplate>
                                        <asp:Button ID="Button1" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="Button1_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="card-box table-responsive">
                                    <div id="datatable-responsive_wrapper" class="dataTables_wrapper container-fluid dt-bootstrap no-footer">
                                        <div class="title_left">
                                            <div id="datatable_filter" class="dataTables_filter">
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="search">
                                                            <asp:TextBox ID="TextBox2" runat="server" OnTextChanged="TextBox2_TextChanged"></asp:TextBox>
                                                            <button>
                                                                <i class="fa fa-search"></i>
                                                            </button>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row" id="DivAreaTrabajo">
                                            <input type="hidden" id="divPosition" name="divPosition" />
                                            <div class="col-sm-12">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView ID="tbDireciones" runat="server" CssClass="table table-sm"
                                                            DataKeyNames="intOidGNDir"
                                                            AutoGenerateColumns="False"
                                                            OnRowCancelingEdit="GridArea_RowCancelingEdit"
                                                            OnRowDeleting="GridArea_RowDeleting"
                                                            OnRowEditing="GridArea_RowEditing"
                                                            OnRowUpdating="GridArea_RowUpdating">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Codigo">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("IntOidGNDir") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("IntOidGNDir") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nombre">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("StrNomDir") %>' CssClass="form-control"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("StrNomDir") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nombre">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("StrSiglaDir") %>' CssClass="form-control" MaxLength="3"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("StrSiglaDir") %>'></asp:Label>
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
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
