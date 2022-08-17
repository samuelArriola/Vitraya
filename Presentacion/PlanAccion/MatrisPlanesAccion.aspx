<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MatrisPlanesAccion.aspx.cs" Inherits="Presentacion.PlanAccion.MatrisPlanesAccion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal fade" tabindex="-1" role="dialog" id="modalEliminar">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Eliminar Plan de Acción</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>¿Esta seguro de eliminar el plan de acción?</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="btnConfirmarEliminar">Eliminar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" tabindex="-1" role="dialog" id="modalVerPlan">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Ver Plan de Acción</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <table id="tablePlansAcc" class="table-inf">
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <button type="button" class="btn btn-danger" id="btnEliminarPlan">Eliminar</button>
                    <button type="button" class="btn btn-primary" id="btnEditarPlan">Editar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="form-group">
        <button type="button" class="btn btn-primary" id="btnDownload"><i class="fa fa-download mr-3"></i>Descargar</button>
    </div>
    <div class="tableContainer">
        <table class="table" style="overflow: auto " id="tablePlanes">
            <thead>
                <tr>
                    <th><input type="text" class="form-control" id="slcOrigen" /></th>
                    <th><asp:DropDownList runat="server" ID="ddlProcesos" CssClass="form-control" >
                        <asp:ListItem Text="Tosdos" Value="" />
                    </asp:DropDownList></th>
                    <th></th>
                    <th>
                        <input type="text" class="form-control" id="txtUsuResp"/>
                    </th>
                    <th>
                        <div class="d-flex justify-content-between">
                            <input type="date" class="form-control" id="txtFecha1"/>
                            <input type="date" class="form-control" id="txtFecha2"/>
                        </div> 
                    </th>
                    <th><input type="text" class="form-control" id="txtUsuSeg"/></th>
                    <th>
                        <select class="form-control" id="slcEstado">
                            <option value="">TODOS</option>
                            <option value="1">ASIGNADO</option>
                            <option value="2">PROCESO</option>
                            <option value="3">EVALUACION</option>
                            <option value="4">TERMINADO</option>
                        </select>
                    </th>
                    <th></th>
                </tr>
                <tr>
                    <th>Origen</th>
                    <th>Proceso</th>
                    <th>¿Qué?</th>
                    <th>¿Quién?</th>
                    <th>¿Cuándo?</th>
                    <th>Responsable del Seguimiento</th>
                    <th>Estado</th>
                    <th></th>
                </tr>
            </thead>
            <tbody id="tbMatrisPlanesAccion">
            </tbody> 
        </table>
    </div> 
    <script src="js/MatrisPlanesAccionJS.js"></script>
</asp:Content>
