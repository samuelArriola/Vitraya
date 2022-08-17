<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AprobarSolicitudes.aspx.cs" Inherits="Presentacion.GestionDocumental.AprobarSolicitudes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Aprobar Solicitudes</h6>
            </div>
        </div>
        <div class="x_content">
            <section class="tableContainer">
                <table class="table" id="tbSolicitud">
                    <thead>
                        <tr>
                            <th></th>
                            <th> <input type="text" id="txtTipoDoc" class="form-control"/> </th>
                            <th> <input type="text" id="txtNomDoc" class="form-control"/> </th>
                            <th> 
                                <select  id="ddlTipoSol" class="form-control">
                                    <option value="">Todos</option>
                                    <option value="crear">Crear</option>
                                    <option value="eliminar">Eliminar</option>
                                    <option value="editar">Editar</option>
                                </select> 
                            </th>
                            <th> <input type="date" id="dtFecha" class="form-control"/> </th>
                            <th> <select  id="ddlEstado" class="form-control">
                                    <option value="Pendiente...">Pendiente...</option>
                                    <option value="Aprobado">Aprobado</option>
                                    <option value="Rechazado">Rechazado</option>
                                </select>  
                                </th>
                            <th></th>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Tipo Documento</th>
                            <th>Nombre Documento</th>
                            <th>Tipo Solicitud</th>
                            <th>Fecha</th>
                            <th>Estado</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>

                    </tbody>
                </table>
            </section>
            
            <section>
                <!-- Modal -->
                <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                    <div class="modal-dialog modal-dialog-centered" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLongTitle">Revisión de solicitudes de documentos</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body" id="imodal-body">
                                ...
                            </div>
                            <div class="modal-footer">
                                <div class="form-group">
                                    <label for="txtIncidencia">Incidencia</label>
                                    <textarea class="form-control" id="txtIncidencia" style="width:300px; height:80px"></textarea>
                                </div>
                                <button type="button" class="btn btn-secondary" id="btnRechazar">Rechazar</button>
                                <button type="button" class="btn btn-primary" id="btnAprobar">Aprobar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            </div>
        </div>
        <script src="js/AprobarSolicitudesJS.js"></script>
</asp:Content>
