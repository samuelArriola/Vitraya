<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CrearPlan.aspx.cs" Inherits="Presentacion.PlanAccion.CrearPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Asignar Plan de Acción</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="row justify-content-center">
                <div class="col col-10">
                    <asp:UpdatePanel runat="server" UpdateMode="Always">
                        <ContentTemplate>
                             <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Titulo</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtTitulo" runat="server"  CssClass="form-control"></asp:TextBox>
                                </div>
                            </div> <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Fuente</label>
                                <div class="col-sm-10">
                                    <asp:DropDownList runat="server" ID="ddlFuente" CssClass="form-control">
                                        <asp:ListItem Text="Seleccione" Value="-1" />
                                        <asp:ListItem Text="Oportunidad de Mejora" />
                                        <asp:ListItem Text="No Conformidad"/>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Descripción</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtDescriptcion" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Acción de Mejora (¿Qué?)</label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="taActividad" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Como se Realiza<span> (¿Cómo?)</span></label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="taComo" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Motivo<span> (¿Por Qué?)</span></label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="taMotivo" runat="server" TextMode="MultiLine" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Responsable de la actividad <span>(¿Quién?)</span></label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddlResponsableActividad" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Seleccione Resposable" Value="-1" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Fecha limite <span>(¿Cuándo?)</span></label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtFechaLimiteCompromiso" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Lugar <span>(¿Dónde?)</span></label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txtLugar" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Cuanto Costara</span></label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="taCosto" runat="server" TextMode="MultiLine" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Soporte de la Acción de mejora <span>(¿Cómo se Soporta?)</span></label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="taSoporteActividad" runat="server" TextMode="MultiLine" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Responsable del seguimiento (¿Quién Realiza Seguimiento?)</label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddlReposnsableSeguimiento" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Seleccione responsable de seguimiento" Value="-1" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <label class="col-sm-2 col-form-label">Proceso que se desea mejorar (¿Qué Preceso?)</label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="ddlProceso" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="Seleccione Proceso" Value="-1" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group row">
                                <div class="col-sm-10">
                                    <asp:Button Text="Crear" runat="server" CssClass="btn btn-success" ID="btnGuardarCompromiso" OnClick="btnGuardarCompromiso_Click" />
                                </div>
                            </div>
                            <div class="col col-12">
                                <div class="tableContainer">
                                    <asp:GridView runat="server" ID="tbCompromisos" AutoGenerateColumns="False" CssClass="table table-sm" OnRowCommand="tbCompromisos_RowCommand" DataKeyNames="IntOidPAPlanAccion">
                                        <Columns>
                                            <asp:BoundField DataField="IntOidPAPlanAccion" />
                                            <asp:BoundField HeaderText="Acción de Mejora" DataField="StrActividad" />
                                            <asp:BoundField HeaderText="Como se Realiza" DataField="StrComo" />
                                            <asp:BoundField HeaderText="Responsable de la actividad" DataField="StrNombreUsuarioResponsable" />
                                            <asp:BoundField HeaderText="Fecha limite" DataField="DtmFecFinalActa" DataFormatString="{0:d}" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" CommandName="eliminar"><i class="fa fa-trash"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                             <div class="form-group row">
                                <div class="col-sm-10">
                                    <asp:Button Text="Guardar Compromisos" runat="server" CssClass="btn btn-success" ID="btnGuardar" OnClick="btnGuardar_Click" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
