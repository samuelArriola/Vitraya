<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CreationOfCommittees.aspx.cs" Inherits="Presentacion.proceedings.CreationOfCommittees" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <link href="ccs/CreationOfCommitteesCSS.css" rel="stylesheet" />
    <div class="modal" tabindex="-1" role="dialog" id="event-modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Agregar fecha al cronograma</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <input type="hidden" name="event-index" value="" />


                    <div class="form-group ">
                        <label for="min-date" class="col-sm-4 control-label">Lugar de reunión</label>
                        <div class="col-sm-7">
                            <input name="event-location" type="text" class="form-control mb-2" id="lugar" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="min-date" class="col-sm-4 control-label">Hora</label>
                        <div class="col-sm-7">
                            <asp:TextBox runat="server" TextMode="Time" CssClass="form-control" ID="txtFecha" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="save-event">Guardar cambios</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="page-title">
        <h6>Parametrización de comités</h6>
    </div>
    <div class="row" id="Agregar" runat="server">
        <div class="col-md-12 col-sm-12 ">
            <div class="x_panel">
                <div class="x_title">
                    <ul class="nav navbar-right panel_toolbox">
                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                        </li>
                    </ul>
                    <div class="clearfix">
                    </div>
                </div>
                <div class="x_content">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upComite">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <label>Nombre del comité</label>
                                        <asp:DropDownList runat="server" ID="ddlNombreC" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlNombreC_SelectedIndexChanged">
                                            <asp:ListItem Text="Seleccione" Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <label>Sigla del comité</label>
                                        <asp:TextBox runat="server" ID="txtSiglaComite" CssClass="form-control" Enabled="false" />
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <label>Tipo de comité</label>
                                        <asp:TextBox runat="server" ID="txtTipoComite" CssClass="form-control" Enabled="false" />
                                    </div>
                                </div>

                                <div class="col col-6">
                                    <div class="form-group">
                                        <label>Coordinador</label>
                                        <asp:DropDownList runat="server" class="form-control" ID="ddlCoordinador" OnSelectedIndexChanged="ddlCoordinador_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="-1">Seleccione</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <label>Secretario</label>
                                        <asp:DropDownList runat="server" class="form-control" ID="ddlSecretario" OnSelectedIndexChanged="ddlSecretario_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="-1">Seleccione</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col col-12 col-md-12 col-lg-6">
                            <div class="x_title">
                                <div class="clearfix">
                                    <h6 class="dtr-title">Agregar miembros</h6>
                                </div>
                            </div>
                            <div class="form-group">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList runat="server" class="form-control" ID="ddlParticipantes" OnSelectedIndexChanged="ddlParticipante_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Seleccione</asp:ListItem>
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                            <asp:UpdatePanel runat="server" ID="upMiembros" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView runat="server" ID="tablaMiembros" AutoGenerateColumns="False" CssClass="table table-sm" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="tablaMiembros_RowCommand">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="codigo" />
                                            <asp:BoundField HeaderText="Nombre" DataField="nombre" />
                                            <asp:BoundField HeaderText="Tipo de usuario" DataField="tipo" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton Text="Eliminar" CommandName="eliminar" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />  
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upTemas" class="col col-12 col-md-12 col-lg-6">
                            <ContentTemplate>
                                <div class="x_title">
                                    <div class="clearfix">
                                        <h6>Agenda Por defeto</h6>
                                    </div>
                                </div>
                                <div class="form-group d-flex justify-content-around">
                                    <asp:TextBox runat="server" CssClass="form-control" ID="txtTAgregarTema" placeholder="Nombre del tema a agregar"></asp:TextBox>
                                    <asp:Button Text="Agregar" runat="server" CssClass="btn btn-success ml-2" ID="btnAgregarTema" OnClick="btnAgregarTema_Click" />
                                </div>
                                <asp:GridView
                                    runat="server"
                                    ID="tbTemas"
                                    CssClass="table"
                                    AutoGenerateColumns="False"
                                    OnRowCommand="tbTemas_RowCommand"
                                    OnRowEditing="tbTemas_RowEditing"
                                    OnRowCancelingEdit="tbTemas_RowCancelingEdit"
                                    OnRowUpdating="tbTemas_RowUpdating"
                                    OnRowDeleting="tbTemas_RowDeleting"
                                    DataKeyNames="OidARAgenda">
                                    <Columns>
                                        <asp:TemplateField ControlStyle-Width="20px" ControlStyle-CssClass="text-center" HeaderText="No.">
                                            <EditItemTemplate>
                                                <asp:LinkButton runat="server" CommandName="upTema" ID="upTema"><i class="fa fa-arrow-circle-o-up fa-2x"></i></asp:LinkButton>
                                                <asp:LinkButton runat="server" CommandName="downTema" ID="downTema"><i class="fa fa-arrow-circle-o-down fa-2x"></i></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbPosicion" Text='<%# Bind("Posicion") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Nombre">
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="txtNombre" Text='<%# Bind("Nombre") %>' />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbNombre" Text='<%# Bind("Nombre") %>' runat="server" />
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
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Cronograma de reuniones</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div id="calendar"></div>
                </div>
            </div>
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Cronograma  Programado</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="tableContainer">
                        <table class="table" id="tableComite">
                            <thead>
                                <tr>
                                    <th>Comité</th>
                                    <th>Coordinador</th>
                                    <th>Fecha</th>
                                    <th>Lugar</th>
                                </tr>
                                <tr>
                                    <th>
                                        <asp:DropDownList runat="server" ID="ddlComites" CssClass="form-control">
                                        </asp:DropDownList>
                                    </th>
                                    <th>
                                        <input type="text" class="form-control" id="txtNombre" /></th>
                                    <th>
                                        <input type="date" class="form-control" id="txtFecha2" /></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody id="tbCronograma">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js/CreationOfCommitteesJS.js"></script>
    <script src="js/calendario.js"></script>
</asp:Content>
