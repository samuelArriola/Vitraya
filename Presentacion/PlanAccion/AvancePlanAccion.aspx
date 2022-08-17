<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AvancePlanAccion.aspx.cs" Inherits="Presentacion.PlanAccion.AvancePlanAccion" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="x_panel">
        <div class="x_title">
            <h6>Información del Plan de Acción</h6>
            <div class="clearfix"></div>
        </div>
        <div class="x_content">
            <table class="table-inf">
                <tr>
                    <td>Oportunidad de Mejora</td>
                    <td>
                        <p id="txtOperMej"></p>
                    </td>
                </tr>
                <tr>
                    <td>No conformidad</td>
                    <td>
                        <p id="txtNoConf"></p>
                    </td>
                </tr>
                <tr>
                    <td>Acción de mejora</td>
                    <td>
                        <p id="txtActividad"></p>
                    </td>
                </tr>
                <tr>
                    <td>¿Cómo?</td>
                    <td>
                        <p id="txtComo"></p>
                    </td>
                </tr>
                <tr>
                    <td>¿Por Qué?</td>
                    <td>
                        <p id="txtPorQue"></p>
                    </td>
                </tr>
                <tr>
                    <td>¿Cuándo?</td>
                    <td>
                        <p id="txtCuando"></p>
                    </td>
                </tr>
                <tr>
                    <td>¿Dónde?</td>
                    <td>
                        <p id="txtDonde"></p>
                    </td>
                </tr>
                <tr>
                    <td>¿Cuanto Costará?</td>
                    <td>
                        <p id="txtCuanto"></p>
                    </td>
                </tr>
                <tr>
                    <td>¿Cómo se Soporta?</td>
                    <td>
                        <p id="txtSoporte"></p>
                    </td>
                </tr>
                <tr>
                    <td>¿Quién realiza seguimiento?</td>
                    <td>
                        <p id="txtQuienSeguimiento"></p>
                    </td>
                </tr>
                <tr>
                    <td>Proceso</td>
                    <td>
                        <p id="txtProceso"></p>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div class="x_panel">
        <div class="x_title">
            <h6>Listado de Avences Previos</h6>
            <div class="clearfix"></div>
        </div>
        <div class="x_content">
            <ul class="list-unstyled timeline" id="lsAvances">
                
            </ul>
        </div>
    </div>

    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Avance Plan de Acción</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="row">
                <div class="form-group">
                    <asp:TextBox runat="server" CssClass="form-control" ID="txtTitulo" />
                </div>
                <asp:TextBox runat="server" ID="textEditor" />
            </div>
        </div>
        <div class="x_title mt-5">
            <div class="clearfix">
                <h6>Soporte del plan de mejora</h6>
            </div>
        </div>
        <div class="pnUpLoadArch text-center">
            <div id="boxUpLoadArch">
                <svg width="9em" height="9em" viewBox="0 0 16 16" class="bi bi-cloud-arrow-up-fill pt-3" fill="currentColor" xmlns="http://www.w3.org/2000/svg" id="imgUpLoad">
                    <path fill-rule="evenodd" d="M8 2a5.53 5.53 0 0 0-3.594 1.342c-.766.66-1.321 1.52-1.464 2.383C1.266 6.095 0 7.555 0 9.318 0 11.366 1.708 13 3.781 13h8.906C14.502 13 16 11.57 16 9.773c0-1.636-1.242-2.969-2.834-3.194C12.923 3.999 10.69 2 8 2zm2.354 5.146l-2-2a.5.5 0 0 0-.708 0l-2 2a.5.5 0 1 0 .708.708L7.5 6.707V10.5a.5.5 0 0 0 1 0V6.707l1.146 1.147a.5.5 0 0 0 .708-.708z"></path>
                </svg>
            </div>
            <asp:FileUpload runat="server" ID="fuArchivo" class="fuArchivo" onchange="this.form.submit();" />
        </div>
        <asp:UpdatePanel runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:GridView ID="tableArch" runat="server" CssClass="table mt-3" DataKeyNames="IntOidGNArchivo" AutoGenerateColumns="False" Width="100%" OnRowCommand="tableArch_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Id" DataField="IntOidGNArchivo" />
                        <asp:BoundField HeaderText="Nombre" DataField="StrNombre" />
                        <asp:BoundField HeaderText="Tipo" DataField="StrExt" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton Text="Eliminar" runat="server" CommandName="eliminar" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="form-group mt-5">
            <asp:Button CssClass="btn btn-success" Text="Guardar Avance" runat="server" ID="btnGuardarAvance" OnClick="btnGuardarAvance_Click" />
        </div>
    </div>
    <script src="../proceedings/js/tinymce.min.js"></script>
    <script src="js/AvancePlanAccionJS.js"></script>
</asp:Content>
