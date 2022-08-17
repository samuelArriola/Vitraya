<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="VistaProcesos.aspx.cs" Inherits="Presentacion.Procesos.VistaProcesos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Crear Nuevo Proceso</h6>
            </div>
        </div>
        <div class="x_content">
            <label>Nombre del Proceso Nuevo</label>
            <div class="d-flex justify-content-between">
                <asp:TextBox runat="server" ID="txtNomProceso" CssClass="form-control" />
                <asp:Button Text="Crear" runat="server" CssClass="ml-2 btn btn-success" ID="btnGuardar" OnClick="btnGuardar_Click" />
            </div>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_tutle">
            <div class="clearfix">
                <h6>Listado de Procesos</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="tableContainer">
                <table class="table" id="tbProcesos">
                    <thead>
                        <tr>
                            <th>
                                <input type="text" class="form-control" id="txtNombreProceso" />
                            </th>
                            <th>
                                <input type="text" class="form-control" id="txtPrefijoProceso" />
                            </th>
                            <th>
                                <select class="form-control" id="slcTipo">
                                    <option value="">Todos</option>
                                    <option value="Misionales">Misionales</option>
                                    <option value="Estratégicos">Estratégicos</option>
                                    <option value="Apoyo">Apoyo</option>
                                    <option value="Evaluación">Evaluación</option>
                                </select>
                            </th>
                            <th>
                                <input type="text" class="form-control" id="txtPadreProceso" />
                            </th>
                            <th>
                                <select class="form-control" id="slcEstado">
                                    <option value="">Todos</option>
                                    <option value="1">Activo</option>
                                    <option value="0">Inactivo</option>
                                </select>
                            </th>
                            <th></th>
                        </tr>
                        <tr>
                            <th>Nombre del Proceso</th>
                            <th>Prefijo</th>
                            <th>Tipo</th>
                            <th>Padre</th>
                            <th>Estado</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody id="tbodoyTbProcesos">
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <script src="JS/VistaProcesosJS.js"></script>
</asp:Content>
