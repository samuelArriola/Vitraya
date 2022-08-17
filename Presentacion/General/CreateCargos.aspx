<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CreateCargos.aspx.cs" Inherits="Presentacion.General.CreateCargos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>Gestion de cargos</h3>
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="row" id="guardar" runat="server">
            <div class="col-md-12 col-sm-12 ">
                <div class="x_panel">
                    <div class="x_title">
                        <%--<h2>Form Design <small>different form elements</small></h2>--%>
                        <ul class="nav navbar-right panel_toolbox">
                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                            </li>
                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                            </li>
                        </ul>
                    </div>
                    <div class="x_content">
                        <br />
                        <div class="item form-group">
                            <label class="col-form-label col-md-3 col-sm-3 label-align" for="first-name">
                                nombre del Cargo<span class="required">*</span>
                            </label>
                            <div class="col-md-6 col-sm-6 ">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="item form-group">
                            <label class="col-form-label col-md-3 col-sm-3 label-align" for="first-name">
                                Unidad Funcional a la cual pertenece<span class="required">*</span>
                            </label>
                            <div class="col-md-6 col-sm-6">
                                <asp:UpdatePanel runat="server" ID="unfun">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control">
                                           
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="TextBox1" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="ln_solid"></div>
                        <div class="item form-group">
                            <div class="col-md-6 col-sm-6 offset-md-3">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="Button1" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="Button1_Click" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
                                        <asp:PostBackTrigger ControlID="Button1" />
                                    </Triggers>
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
                        <%--                    <h2>Responsive example<small>Users</small></h2>--%>
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
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <label>
                                                            Search:
                                                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input-sm" OnTextChanged="TextBox2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <asp:Label ID="Label7" runat="server" Text=""></asp:Label>
                                                        </label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="row" style="overflow: scroll; height: 800px" id="DivAreaTrabajo">
                                            <input type="hidden" id="divPosition" name="divPosition" />
                                            <div class="col-sm-12">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:GridView 
                                                            ID="GridCargo" 
                                                            runat="server" 
                                                            AutoGenerateColumns="False" 
                                                            DataKeyNames="IntGnDcCgo" 
                                                            CellPadding="3" 
                                                            ForeColor="Black" 
                                                            GridLines="Vertical" 
                                                            CssClass="table" 
                                                            OnRowCancelingEdit="GridCargo_RowCancelingEdit" 
                                                            OnRowDeleting="GridCargo_RowDeleting" 
                                                            OnRowEditing="GridCargo_RowEditing" 
                                                            OnRowUpdating="GridCargo_RowUpdating" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                                            <RowStyle Height="50px" Width="90px" />
                                                            <AlternatingRowStyle Height="50px" Width="90px" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Codigo">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" Text='<%# Bind("IntGnDcCgo") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("IntGnDcCgo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Nombre">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("StrGnNomCgo") %>' CssClass="form-control"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("StrGnNomCgo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Unidad Funcional">
                                                                    <EditItemTemplate>
                                                                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control">
                                                                        </asp:DropDownList>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("StrNomDep") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:CommandField ButtonType="Image" ShowEditButton="true" EditImageUrl="~/Images/Edit.png" UpdateImageUrl="~/Images/Edit.png" CancelImageUrl="~/Images/cancelar.png">
                                                                    <ControlStyle Width="20px" />
                                                                </asp:CommandField>
                                                                <asp:CommandField ButtonType="Image" ShowDeleteButton="true" DeleteImageUrl="~/Images/Delete.png">
                                                                    <ControlStyle Width="20px" />
                                                                </asp:CommandField>
                                                            </Columns>
                                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                                            <FooterStyle BackColor="#CCCCCC" />
                                                            <HeaderStyle BackColor="#2A3F54" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TextBox2" EventName="TextChanged" />
                                                    </Triggers>
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
