<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MatricularUnidad.aspx.cs" Inherits="Presentacion.trainings.MatricularUnidad" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>MATRICULAR USUARIOS POR UNIDAD FUNCIONAL</h3>
            </div>
            <div class="title_right">
                <div class="col-md-5 col-sm-5   form-group pull-right top_search">
                    <div class="input-group">
                        <span class="input-group-btn">
                            <%--<asp:Button ID="Button1" runat="server" Text="Nueva Capacitacion" BackColor="#6666FF" Font-Bold="True" ForeColor="White" />--%>
                        </span>
                        <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-round btn-success" Text="Volver" OnClick="btnVolver_Click" />
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div>
            <asp:Label ID="lblfecha" runat="server" Text="Label" Visible="false"></asp:Label>
        </div>
        <br />
        <br />
        <br />
        <asp:TextBox ID="txtidcapa" runat="server" Visible="False"></asp:TextBox>
        <div>
            <asp:DropDownList ID="DropUnidad" CssClass="form-control" runat="server" Width="400px">
            </asp:DropDownList>
            <br />
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:Button ID="Button4" runat="server" CssClass="btn-primary" Text="Matricular por unidad" Font-Bold="True" ForeColor="White" Height="40px" Font-Size="Medium" Width="400px" OnClick="Button4_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <asp:UpdatePanel runat="server" ID="upUsuariosMatricuculados" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="tbUsuariosMatriculados" runat="server" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CssClass="table">
                        <Columns>
                            <asp:BoundField DataField="IntGNCodUsu" HeaderText="Documento" SortExpression="IDEMPLEADO" />
                            <asp:BoundField DataField="StrNOMUSUARIO" HeaderText="Nombre de Usuario" SortExpression="NOMUSUARIO" />
                            <asp:BoundField DataField="StrUNIDAD" HeaderText="Unidad Funcional" SortExpression="UNIDAD" />
                            <asp:BoundField DataField="StrCARGO" HeaderText="Cargo" SortExpression="CARGO" />
                            <asp:CommandField SelectText="Eliminar" ShowSelectButton="True">
                                <ItemStyle ForeColor="Red" />
                            </asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:Label ID="Label2" runat="server"></asp:Label>
        </div>
    </div>
</asp:Content>
