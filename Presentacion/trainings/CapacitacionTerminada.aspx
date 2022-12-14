<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CapacitacionTerminada.aspx.cs" Inherits="Presentacion.trainings.CapacitacionTerminada" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>CAPACITACIONES REALIZADAS</h3>
            </div>
            <div class="title_right">
                <div class="col-md-5 col-sm-5   form-group pull-right top_search">
                    <div class="input-group">
                        <span class="input-group-btn">
                            <%--<asp:Button ID="Button1" runat="server" Text="AGREGAR CAPACITACION" BackColor="#6666FF" Width="250px" Font-Bold="True" ForeColor="White" OnClick="Button1_Click" CssClass="btn-primary" Height="40px" />--%>
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <div class="clearfix" style="height: 1010px">
            <%--  <asp:ScriptManager ID="ScriptManager1" runat="server">
                  </asp:ScriptManager>--%>
            <asp:TextBox ID="TextBox1" runat="server" Height="20px" Style="display: none" ReadOnly="True" Width="80px"></asp:TextBox>
            <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                      <ContentTemplate>--%>
            <br />
            <table style="width: 100%;">
                <tr>
                    <td class="auto-style11">FILTRAR POR TEMAS</td>
                    <td class="auto-style12"></td>
                    <td class="auto-style13">FILTRAR POR RANGO DE FECHAS</td>
                    <td class="auto-style14"></td>
                    <td class="auto-style15">&nbsp;</td>
                    <td>FILTRAR POR UNIDAD FUNCIONAL</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" Width="300px"></asp:TextBox>
                    </td>
                    <td class="auto-style7">
                        <asp:ImageButton ID="ImageButton3" runat="server" BorderColor="#0066FF" ImageUrl="~/production/images/lupa.png" OnClick="ImageButton3_Click" />
                    </td>
                    <td class="auto-style10">
                        <asp:TextBox ID="TextFecha1" runat="server" CssClass="form-control" TextMode="Date" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextFecha2" runat="server" CssClass="form-control" TextMode="Date" Width="200px"></asp:TextBox>
                    </td>
                    <td class="auto-style16">
                        <asp:ImageButton ID="ImageButton2" runat="server" BorderColor="#0066FF" ImageUrl="~/production/images/lupa.png" OnClick="ImageButton2_Click" />
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList3" CssClass="form-control" runat="server" AutoPostBack="True" Width="250px" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged" DataSourceID="SqlCargarArea" DataTextField="GnNomAra" DataValueField="GnNomAra">
                            <asp:ListItem>Seleccionar</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlCargarArea" runat="server" ConnectionString="<%$ ConnectionStrings:VitrayaConnectionString %>" SelectCommand="SELECT [GnNomAra] FROM [Area]"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
            <br />
            <asp:TextBox ID="txtemple" runat="server" Visible="False"></asp:TextBox>
            <br />
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource2" ForeColor="#333333" GridLines="None" Width="100%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
                    <asp:BoundField DataField="IDCAPACITACION" HeaderText="ID" SortExpression="IDCAPACITACION" />
                    <asp:BoundField DataField="EJETEMATICO" HeaderText="EJETEMATICO" SortExpression="EJETEMATICO" />
                    <asp:BoundField DataField="TEMA" HeaderText="TEMA" SortExpression="TEMA" />
                    <asp:BoundField DataField="FECHA" HeaderText="FECHA" SortExpression="FECHA" />
                    <asp:BoundField DataField="HORAINICIAL" HeaderText="HORAINICIAL" SortExpression="HORAINICIAL" />
                    <asp:BoundField DataField="HORAFINAL" HeaderText="HORAFINAL" SortExpression="HORAFINAL" />
                    <asp:BoundField DataField="LUGAR" HeaderText="LUGAR" SortExpression="LUGAR" />
                    <asp:BoundField DataField="UNIDADFUNCIONAL" HeaderText="UNIDAD FUNCIONAL" SortExpression="UNIDADFUNCIONAL" />
                    <asp:BoundField DataField="MODALIDAD" HeaderText="MODALIDAD" SortExpression="MODALIDAD" />
                    <asp:BoundField DataField="RESPONSABLE" HeaderText="RESPONSABLE" SortExpression="RESPONSABLE" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <br />
            <table style="width: 100%;">
                <tr>
                    <td class="auto-style6">EJE TEMATICO</td>
                    <td class="auto-style5">TEMA</td>
                    <td class="auto-style9">SUBTEMAS</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style6">
                        <asp:TextBox ID="TextEjeTematico" CssClass="form-control" runat="server" Width="300px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox ID="TextTema" CssClass="form-control" runat="server" Width="300px" ReadOnly="True"></asp:TextBox>
                    </td>
                    <td class="auto-style9">
                        <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" DataSourceID="SqlDataSource1" DataTextField="SUBTEMA" DataValueField="SUBTEMA" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="Button2" runat="server" BackColor="#009933" CssClass="form-control" Font-Bold="True" ForeColor="White" Text="Revisar" Width="200px" OnClick="Button2_Click" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Image/LIMPIAR.png" OnClick="ImageButton4_Click" />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server"></asp:Label>
            <br />
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:VitrayaConnectionString %>" SelectCommand="SELECT [IDCAPACITACION], [EJETEMATICO], [TEMA], [FECHA], [HORAINICIAL], [HORAFINAL], [LUGAR], [UNIDADFUNCIONAL], [MODALIDAD], [RESPONSABLE] FROM [CPCAPACITACION] WHERE (([ESTADO] = @ESTADO) AND ([IDRESPONSABLE] = @IDRESPONSABLE)) ORDER BY [IDCAPACITACION] DESC">
                <SelectParameters>
                    <asp:Parameter DefaultValue="INACTIVA" Name="ESTADO" Type="String" />
                    <asp:ControlParameter ControlID="txtemple" Name="IDRESPONSABLE" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:VitrayaConnectionString %>" SelectCommand="SELECT [SUBTEMA] FROM [CPSUBTEMAS] WHERE ([IDCAPACITACION] = @IDCAPACITACION)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="TextBox1" Name="IDCAPACITACION" PropertyName="Text" Type="String" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </div>
</asp:Content>
