<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EjecutarCapaUser.aspx.cs" Inherits="Presentacion.trainings.EjecutarCapaUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="">
        <div class="page-title">
            <div class="title_left">
                <h3>
                    <asp:Label ID="lbleje" runat="server" Text=""></asp:Label></h3>
            </div>
            <div class="title_right">
                <div class="col-md-5 col-sm-5   form-group row pull-right top_search">
                    <div class="input-group">
                        <%--<input type="text" class="form-control" placeholder="Search for...">--%>
                        <asp:TextBox ID="TextIdEmple" runat="server"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:Label ID="lblfecha" runat="server" Text=""></asp:Label>
                            <%--<button class="btn btn-default" type="button">Go!</button>--%>
                        </span>
                    </div>
                </div>
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="row">
            <!-- form input mask -->
            <div class="col-md-6 col-sm-12  ">
                <div class="x_panel">
                    <div class="x_title">
                       
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">

                        <asp:Label ID="TextIdCapa" runat="server" Text="" Visible="false"></asp:Label>
                        <h3>Subtemas</h3>
                        <br />
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table">
                            <Columns>
                                <asp:BoundField HeaderText="Nombre Subtema" DataField="StrSUBTEMA" />
                            </Columns>
                        </asp:GridView>
                        <br />
                            <a target="arch" id="NomDoc" class="btn btn-secondary" style="width: 100%; text-align:center" runat="server" visible="false"></a>
                        <br />
                        <hr />
                        <asp:GridView ID="GridView3" runat="server" CssClass="table" DataKeyNames="Id" AutoGenerateColumns="False" Width="100%" OnRowCommand="GridView3_RowCommand">
                            <Columns>
                                <asp:BoundField HeaderText="Id" DataField="id" />
                                <asp:BoundField HeaderText="Nombre del documento" DataField="nombre" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton Text="Descargar" runat="server" CommandName="descargar" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <br />
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:Button ID="Button1" runat="server" Text="Firmar Capacitación" CssClass="btn btn-primary" OnClick="Button1_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="row">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="row">
                        <asp:Button ID="btncerrar" runat="server" Text=" Capacitaciónn Firmada" CssClass="btn-primary" Height="40px" Font-Bold="true" BackColor="#ff3300" Visible="false" OnClick="btncerrar_Click" />
                    </div>
                    <br />
                </div>
            </div>
            <div class="col-md-6 col-sm-12  ">
                <div class="x_panel">
                    <div class="x_title">
                        <h2>Examen de Capacitación</h2>
                        <div class="clearfix"></div>
                    </div>
                    <div class="x_content">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:Button Text="Realizar Examen de Capacitación" runat="server" ID="btnRealizarExa" OnClick="btnRealizarExa_Click" CssClass="btn btn-success" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:GridView runat="server" AutoGenerateColumns="False" ID="tbExamenesSol" OnRowCommand="tbExamenesSol_RowCommand" Width="100%" CssClass="table">
                                    <Columns>
                                        <asp:BoundField HeaderText="Cod" DataField="IntOidCPEXAMENSOL" />
                                        <asp:BoundField HeaderText="Fecha de realización" DataField="DtmFecha" DataFormatString="{0:d}" />
                                        <asp:BoundField HeaderText="Resultado" DataField="IntResultado" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton Text="Ver" runat="server" CommandName="ver" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                    </div>
                </div>
                <iframe id="frameActa" style="max-width: 0; border: 0" runat="server"></iframe>
            </div>
        </div>
        <!-- /form input mask -->
        <!-- form color picker -->

    </div>

</asp:Content>
