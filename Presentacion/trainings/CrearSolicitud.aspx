<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CrearSolicitud.aspx.cs" Inherits="Presentacion.trainings.CrearSolicitud" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-12 col-sm-12">
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Solicitud de capacitación</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="field item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">
                            Fecha de capacitación<span
                                class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:TextBox ID="TextFecha" runat="server" CssClass="form-control " placeholder="Fecha" TextMode="Date" required="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="field item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">
                            Hora Inicial<span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:TextBox ID="TextHoraIni" runat="server" CssClass="form-control " placeholder="Hora inicial" TextMode="Time" required="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="field item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">
                            Hora Final<span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:TextBox ID="TextHoraFinal" runat="server" CssClass="form-control " placeholder="Hora Final" TextMode="Time" required="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="field item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">
                            Modalidad<span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:DropDownList ID="DropModalidad" CssClass="form-control" runat="server" AutoPostBack="false">
                                <asp:ListItem Value="-1">Seleccione</asp:ListItem>
                                <asp:ListItem Value="Presencial">Presencial</asp:ListItem>
                                <asp:ListItem Value="Virtual con facilitador">Virtual con facilitador</asp:ListItem>
                                <asp:ListItem Value="Virtual documental">Virtual documental</asp:ListItem>
                            </asp:DropDownList>
                            
                            
                        </div>
                    </div>
                    <div class="field item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">
                            Lugar <span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:DropDownList ID="DropLugar" CssClass="form-control" runat="server">
                                <asp:ListItem Value="-1">Seleccione</asp:ListItem>
                                <asp:ListItem Value="Crecer administración">Crecer administración</asp:ListItem>
                                <asp:ListItem Value="Crecer consulta externa">Crecer consulta externa</asp:ListItem>
                                <asp:ListItem Value="Crecer asistencial">Crecer asistencial</asp:ListItem>
                                <asp:ListItem>Virtual</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" InitialValue="" runat="server" ControlToValidate="DropLugar" ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                            <div class="invalid-feedback">
                            </div>

                        </div>
                    </div>
                    <div class="field item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">
                            Responsable<span
                                class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">

                            <asp:TextBox ID="TextResponsable" runat="server" CssClass="form-control " placeholder="Responsable" required="true" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtidresponsable" runat="server" Visible="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlUsuarios" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUsuarios_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem>Elegir otro responsable</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="field item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">
                            Unidad<span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:DropDownList ID="DropUnidad" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">Seleccionar</asp:ListItem>
                            </asp:DropDownList>
                            
                        </div>
                    </div>

                    <div class="field item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">
                            Eje Tematico<span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:DropDownList ID="DropEje" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">Seleccionar eje tematico</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="field item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">
                            Tema<span
                                class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:TextBox ID="Texttema" runat="server" CssClass="form-control " placeholder="Tema" required="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Texttema" ErrorMessage="*" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="field item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">
                            Subtemas<span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Ingresar Subtema"></asp:TextBox><th>
                                        <asp:Button ID="Button6" runat="server" CssClass="btn btn-primary mt-3 mb-4" OnClick="Button6_Click" Text="Agregar" /></th>
                                    <asp:Label ID="Label4" runat="server"></asp:Label>
                                    <asp:UpdatePanel runat="server" ID="upSubtemas" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Subtema" DataField="StrSUBTEMA" >
                                                        <ItemStyle BorderStyle="None" Font-Bold="True" Font-Size="medium" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="field item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">Link de capacitación<span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <asp:TextBox ID="TextLInk" CssClass="form-control" runat="server" TextMode="Url"></asp:TextBox>
                        </div>
                    </div>
                    <div class="field item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">
                            Documentación<span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <div class="pnUpLoadArch text-center">
                                <div class="boxUpLoadArch">
                                    <svg width="9em" height="9em" viewBox="0 0 16 16" class="bi bi-cloud-arrow-up-fill pt-3" fill="currentColor" xmlns="http://www.w3.org/2000/svg" id="imgUpLoad">
                                        <path fill-rule="evenodd" d="M8 2a5.53 5.53 0 0 0-3.594 1.342c-.766.66-1.321 1.52-1.464 2.383C1.266 6.095 0 7.555 0 9.318 0 11.366 1.708 13 3.781 13h8.906C14.502 13 16 11.57 16 9.773c0-1.636-1.242-2.969-2.834-3.194C12.923 3.999 10.69 2 8 2zm2.354 5.146l-2-2a.5.5 0 0 0-.708 0l-2 2a.5.5 0 1 0 .708.708L7.5 6.707V10.5a.5.5 0 0 0 1 0V6.707l1.146 1.147a.5.5 0 0 0 .708-.708z"></path>
                                    </svg>
                                </div>
                                <asp:FileUpload ID="FileUpload1" CssClass="active fuArchivo" for="customFileLang" runat="server" />
                            </div>
                            <table class="table mt-3" id="tbArchivos">
                                <thead>
                                    <tr>
                                        <th>Nombre</th>
                                        <th>Extención</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>

                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="field item form-group">
                        <label class="col-form-label col-md-3 col-sm-3  label-align">
                            Documento para el examen<span class="required">*</span></label>
                        <div class="col-md-6 col-sm-6">
                            <label class="form-control" for="ContentPlaceHolder_fuExamen"></label>
                            <asp:FileUpload ID="fuExamen" CssClass="d-none" runat="server" />
                        </div>
                    </div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="field item form-group">
                                <label class="col-form-label col-md-3 col-sm-3  label-align">
                                    Descripciónn de Usuarios a matricular<span class="required">*</span></label>
                                <div class="col-md-6 col-sm-6">
                                    <div class="row">
                                        <div class="col col-4">
                                            <div class="form-group">
                                                <asp:DropDownList runat="server" ID="ddlUsuariosM" CssClass="form-control" OnSelectedIndexChanged="ddlUsuariosM_SelectedIndexChanged" AutoPostBack="true" />
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" TextMode="MultiLine" Rows="8" ID="txtUsuarios" CssClass="form-control" />
                                            </div>
                                        </div>
                                        <div class="col col-4">
                                            <div class="form-group">
                                                <asp:DropDownList runat="server" ID="ddlCargosM" CssClass="form-control" OnSelectedIndexChanged="ddlCargosM_SelectedIndexChanged" AutoPostBack="true" />
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="txtCargos" Rows="8" CssClass="form-control" TextMode="MultiLine" />
                                            </div>
                                        </div>
                                        <div class="col col-4">
                                            <div class="form-group">
                                                <asp:DropDownList runat="server" ID="ddlUnidadesM" CssClass="form-control" OnSelectedIndexChanged="ddlUnidadesM_SelectedIndexChanged" AutoPostBack="true" />
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox runat="server" ID="txtUnidades" Rows="8" CssClass="form-control" TextMode="MultiLine" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="ln_solid">
                        <div class="form-group">
                            <div class="col-md-6 offset-md-3">
                                <asp:Button ID="btnguardar" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="btnguardar_Click" />
                                <asp:Button ID="Button2" CssClass="btn btn-success" runat="server" Text="Cancelar" OnClick="Button2_Click" />
                            </div>
                        </div>
                        <asp:Label ID="LabelResult" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js/CrearSolicitudJS.js"></script>
</asp:Content>
