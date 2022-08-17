<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Normagramas.aspx.cs" Inherits="Presentacion.Procesos.Normagramas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Parametrización</h6>
            </div>
        </div>
        <div class="x_content">
            <asp:UpdatePanel runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>Tipo de Documento</label>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTipo">
                                    <asp:ListItem Text="Selccione" Value="-1" Enabled="false" />
                                    <asp:ListItem Text="Constitución" Value="Constitución" />
                                    <asp:ListItem Text="Decreto" Value="Decreto" />
                                    <asp:ListItem Text="Ley" Value="Ley" />
                                    <asp:ListItem Text="Resolución" Value="Resolución" />
                                    <asp:ListItem Text="Circular" Value="Circular" />
                                    <asp:ListItem Text="Sentencia" Value="Sentencia" />
                                    <asp:ListItem Text="Ordenanza" Value="Ordenanza" />
                                    <asp:ListItem Text="Acto administrativo" Value="Acto administrativo" />
                                    <asp:ListItem Text="Acto legislativo" Value="Acto legislativo" />
                                    <asp:ListItem Text="Acuerdo" Value="Acuerdo" />
                                    <asp:ListItem Text="Reglamento" Value="Reglamento" />
                                    <asp:ListItem Text="Código" Value="Código" />
                                    <asp:ListItem Text="Directiva Presidencial" Value="Directiva Presidencial" />
                                    <asp:ListItem Text="Documento técnico" Value="Documento técnico" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>Número de la Norma</label>
                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNumNorma" />
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>Fecha de Emisión</label>
                                <asp:TextBox runat="server" TextMode="Date" CssClass="form-control" ID="txtFechEmision" />
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>Estado</label>
                                <asp:DropDownList runat="server" CssClass="form-control" ID="ddlEstado">
                                    <asp:ListItem Text="Selccione" Value="-1" Enabled="false" />
                                    <asp:ListItem Text="Vigente" Value="Vigente" />
                                    <asp:ListItem Text="Derogada" Value="Derogada" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col col-12">
                            <div class="form-group">
                                <label>Emitido por</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" ID="txtEmision" />
                            </div>
                        </div>
                        <div class="col col-12">
                            <div class="form-group">
                                <label>Descripcion</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" ID="txtDescripcion" />

                            </div>
                        </div>
                        <div class="col col-12">
                            <div class="form-group">
                                <label>DIRECCIÓN ELECTRÓNICA - URL EN EL RID</label>
                                <asp:TextBox runat="server" CssClass="form-control" TextMode="MultiLine" ID="txtUrl" />
                            </div>
                        </div>
                    </div>
                    <div class="col col-12">
                            <div class="form-group">
                                <asp:Button Text="Crear" runat="server" CssClass="btn btn-success" ID="btnCrear"  OnClick="btnCrear_Click"/>
                            </div>
                        </div>
                    </div>
                    <div class="col col-12">
                        <div class="title">
                            <div class="clearfix">
                                <h6>Normagrama</h6>
                            </div>
                        </div>
                    </div>
                    <div class="col col-12">
                        <asp:GridView runat="server" CssClass="table" AutoGenerateColumns="False" ID="tbNormagrama">
                            <Columns>
                                <asp:BoundField HeaderText="Tipo de Documento" DataField="StrTipo"/>
                                <asp:BoundField HeaderText="Número de Norma" DataField="IntNumNorma"/>
                                <asp:BoundField HeaderText="Fecha de Emisión" DataField="DtmFecEmision" DataFormatString="{0:d}"/>
                                <asp:BoundField HeaderText="Emitido por" DataField="StrEmision"/>
                                <asp:BoundField HeaderText="Descripción" DataField="StrDescripcion"/>
                                <asp:BoundField HeaderText="Estado" DataField="StrEstado"/>
                                <asp:BoundField HeaderText="Dirección Electrónica" DataField="StrUrl"/>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
