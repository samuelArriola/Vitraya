<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AddEjeTematico.aspx.cs" Inherits="Presentacion.trainings.AddEjeTematico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>ADMINISTRAR EJES TEMATICOS </h3>
            </div>

            <div class="title_right">
                <div class="col-md-5 col-sm-5   form-group pull-right top_search">
                    <div class="input-group">

                        <span class="input-group-btn">
                            <%--<asp:Button ID="Button1" runat="server" Text="Nueva Capacitacion" BackColor="#6666FF" Font-Bold="True" ForeColor="White" />--%>
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <br />
            <br />
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style6">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Width="500px" placeholder="Ingresar eje tematico"></asp:TextBox>
                    </td>
                    <td class="auto-style6">
                        <asp:Button ID="Button1" runat="server" CssClass="btn-primary" Font-Bold="True" Font-Size="Medium" ForeColor="White" OnClick="Button1_Click1" Text="Agregar" Height="40px" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style6">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel runat="server" ID="upTablaEjes" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView ID="tbEjes" runat="server" CssClass="table"
                                    DataKeyNames="IntOidCPEJETEMATICO"
                                    AutoGenerateColumns="False"
                                    OnRowCancelingEdit="tbEjes_RowCancelingEdit"
                                  
                                    OnRowEditing="tbEjes_RowEditing"
                                    OnRowUpdating="tbEjes_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Codigo">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true" Text='<%# Bind("IntOidCPEJETEMATICO") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("IntOidCPEJETEMATICO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("StrEJETEMATICO") %>' CssClass="form-control"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("StrEJETEMATICO") %>'></asp:Label>
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

                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">

                        <br />
                        <asp:Label ID="Label1" runat="server" Font-Size="Medium"></asp:Label>
                    </td>
                    <td class="auto-style6">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style6">&nbsp;&nbsp;&nbsp; </td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style6">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style6">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2">&nbsp;</td>
                    <td class="auto-style6">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                </tr>
            </table>
            <br />
            <asp:Button ID="Button2" runat="server" CssClass="btn-primary" Font-Bold="True" Font-Size="Medium" ForeColor="White" Height="40px" OnClick="Button2_Click" Text="Terminar" Width="200px" />
            <br />
        </div>
    </div>
</asp:Content>
