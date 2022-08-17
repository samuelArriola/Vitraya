<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CargarPlanAccion.aspx.cs" Inherits="Presentacion.Auditorias.CargarPlanAccion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6><span id="txtHallazgo" runat="server"></span></h6>
            </div>
        </div>
        <div class="x_content">
            <div class="row justify-content-center">
                <div class="col col-10">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
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
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
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
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_content">
            <div class="d-flex justify-content-between">
                <asp:Button ID="btnAnt" Text="< Ant" runat="server" CssClass="btn btn-success" OnClick="btnAnt_Click"/>
                <asp:Button ID="btnSig" Text="Sig >" runat="server" CssClass="btn btn-success" OnClick="btnSig_Click"/>
                <asp:Button ID="btnFinalizar" Text="Finalizar" Visible="false" runat="server" CssClass="btn btn-success" OnClick="btnFinalizar_Click"/>
            </div>
        </div>
    </div>
</asp:Content>
