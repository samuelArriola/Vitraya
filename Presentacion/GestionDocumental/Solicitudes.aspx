<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Solicitudes.aspx.cs" Inherits="Presentacion.GestionDocumental.Solicitudes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Solicitud de Documentos</h6>
            </div>
        </div>
        <div class="x_content">
            <asp:UpdatePanel runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="row">

                        <div class="col col-6">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_ddlTipDoc">Tipo Documento</label>
                                <asp:DropDownList runat="server" ID="ddlTipDoc" CssClass="form-control">
                                    <asp:ListItem Text="Seleccione..." Value="-1" />
                                    <asp:ListItem Text="Procedimiento" Value="Procedimiento" />
                                    <asp:ListItem Text="Indicador" Value="Indicador" />
                                    <asp:ListItem Text="Protocolo" Value="Protocolo" />
                                    <asp:ListItem Text="Manual" Value="Manual" />
                                    <asp:ListItem Text="Politica" Value="Politica" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col col-6 DivProcedimiento" style="display: none">
                            <div class="form-group">
                                <label class="d-block">Tipo Solicitud</label>
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" name="inlineRadioOptions" id="rdCrear" value="crear" runat="server" checked />
                                    <label class="form-check-label" for="inlineRadio1">Creación</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" name="inlineRadioOptions" id="reEliminar" value="eliminar" runat="server" />
                                    <label class="form-check-label" for="inlineRadio2">Eliminación</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" name="inlineRadioOptions" id="rdEditar" value="editar" runat="server" />
                                    <label class="form-check-label" for="inlineRadio3">Nueva versión</label>
                                </div>
                            </div>
                        </div>
                        <div class="col col-6 pnProceso" style="display: none">
                            <div class="form-group">
                                <label id="lblProceso" for="ContentPlaceHolder_ddlProcesos">Proceso</label>
                                <asp:DropDownList runat="server" ID="ddlProcesos" CssClass="form-control">
                                    <asp:ListItem Text="Seleccione ..." Value="-1"/>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col col-6 pnNombre" style="display: none" >
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtNomProc">Nombre</label>
                                <asp:TextBox runat="server" ID="txtNomProc" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="pnEditar col col-6" style="display:none">
                            <div class="form-group">
                                <label>Nombre del documento</label>
                                <select id="nomDoc" class="form-control" runat="server"></select>
                            </div>
                        </div>
                        <div class="col col-6 pnResponsable" style="display:none">
                            <div class="form-group">
                                <label>Seleccionar usuario responsable de la solicitud:</label>
                                <select id="usuarioResponsable" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example" >
                                    <option value="" selected>Seleccione</option>
                                </select>
                            </div>
                        </div>
                        <div class="col col-12 DivProcedimiento"  style="display: none">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtJus">Justificación/Descripción</label>
                                <asp:TextBox runat="server" ID="txtJus" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                            </div>
                        </div>
                        
                        <div class="col col-12 DivProcedimiento" style="display: none">
                            <div class="form-group">
                                <asp:Button Text="Solicitar" runat="server" ID="btnSolicitarPro" CssClass="btn btn-success"  />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Estado de Solicitudes</h6>
            </div>
        </div>
        <div class="x_content">
            <section class="tableContainer">
                <table class="table" id="tbSolicitud">
                    <thead>
                        <tr>
                            <th ></th>
                            <th>
                                <input type="text" id="txtTipoDoc" class="form-control" />
                            </th>
                            <th>
                                <input type="text" id="txtNomDoc" class="form-control" />
                            </th>
                            <th>
                                <select id="ddlTipoSol" class="form-control">
                                    <option value="">Todos</option>
                                    <option value="crear">Crear</option>
                                    <option value="eliminar">Eliminar</option>
                                    <option value="editar">Editar</option>
                                </select>
                            </th>
                            <th>
                                <input type="date" id="dtFecha" class="form-control" />
                            </th>
                            <th>
                                <select id="ddlEstado" class="form-control">
                                    <option value="">Todas</option>
                                    <option value="Pendiente...">Pendiente...</option>
                                    <option value="Aprobado">Aprobado</option>
                                    <option value="Rechazado">Rechazado</option>
                                </select>
                            </th>
                            <th></th>
                        </tr>
                        <tr>
                            <th style="width: 5%"></th>
                            <th style="width: 20%">Tipo Documento</th>
                            <th style="width: 20%">Nombre Documento</th>
                            <th style="width: 20%">Tipo Solicitud</th>
                            <th style="width: 20%">Fecha</th>
                            <th style="width: 10%">Estado</th>
                            <th style="width: 5%"></th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </section>
        </div>
    </div>
    <script src="js/SolicitudesJS.js"></script>
</asp:Content>
