<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ParametrizacionEntesAuditores.aspx.cs" Inherits="Presentacion.Auditorias.EntesAuditores"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Parametrizacion de Entes Auditores</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="row">
                <div class="col col-4">
                    <div class="form-group">
                        <label>Nombre:</label>
                        <asp:TextBox runat="server" class="form-control" ID="txtNombre" />
                    </div>
                </div>
                <div class="col col-4">
                    <div class="form-group">
                        <label>Sigla:</label>
                        <asp:TextBox runat="server" class="form-control" ID="txtSigla" />
                    </div>
                </div>
                <div class="col col-4">
                    <div class="form-group">
                        <label>Código</label>
                        <asp:TextBox runat="server" class="form-control" ID="txtCodigo" />
                    </div>
                </div>
                <div class="form-group col col-12 d-flex justify-content-end">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Button Text="Crear" runat="server" ID="btnCrear" CssClass="bnt btn-success" OnClick="btnCrear_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col col-12">
                    <asp:UpdatePanel ID="upEntesAuditores" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:GridView ID="tbEnteAuditor" runat="server" CssClass="table" Style="width: 100%" AutoGenerateColumns="false"
                                OnRowCancelingEdit="tbEnteAuditor_RowCancelingEdit"
                                OnRowDeleting="tbEnteAuditor_RowDeleting"
                                OnRowEditing="tbEnteAuditor_RowEditing"
                                OnRowUpdating="tbEnteAuditor_RowUpdating"
                                DataKeyNames="IntOidAUEnteAuditor">
                                <Columns>
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtNombre" runat="server"  Text='<%# Bind("StrNombre") %>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbNombre" runat="server" Text='<%# Bind("StrNombre") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtSigla" runat="server"  Text='<%# Bind("StrSigla") %>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbSigla" runat="server" Text='<%# Bind("StrSigla") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtCodigo" runat="server"  Text='<%# Bind("StrCodigo") %>' CssClass="form-control"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LbCodigo" runat="server" Text='<%# Bind("StrCodigo") %>'></asp:Label>
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
</asp:Content>
