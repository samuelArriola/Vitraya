<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MisPlanes.aspx.cs" Inherits="Presentacion.PlanAccion.MisPlanes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="../build/css/canvas.css" rel="stylesheet" />
    <link href="css/MisPlanesCSS.css" rel="stylesheet" />

    <div class="modal fade bd-example-modal-sm" id="modal-alert" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Cerrar del Plan de Acción</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>¿Esta seguro de cerrar el Plan de Acción?, al hacer clic en Enviar no podrá hacer mas avances</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="btn-cerrar-plan">Enviar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade bd-example-modal-lg" id="modal-view-plan" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Información del Plan de Acción</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">

                    <table class="table-inf">
                        <tr>
                            <td>Origen</td>
                            <td><p id="txtOperMej"></p></td>
                        </tr>
                        <tr>
                            <td>Fuente</td>
                            <td><p id="txtNoConf"></p></td>
                        </tr>
                        <tr>
                            <td>Acción de mejora</td>
                            <td><p id="txtActividad"></p></td>
                        </tr>
                        <tr>
                            <td>¿Cómo?</td>
                            <td><p id="txtComo"></p></td>
                        </tr>
                        <tr>
                            <td>¿Por Qué?</td>
                            <td><p id="txtPorQue"></p></td>
                        </tr>
                        <tr>
                            <td>¿Cuándo?</td>
                            <td><p id="txtCuando"></p></td>
                        </tr>
                        <tr>
                            <td>¿Dónde?</td>
                            <td><p id="txtDonde"></p></td>
                        </tr>
                        <tr>
                            <td>¿Cuanto Costará?</td>
                            <td><p id="txtCuanto"></p></td>
                        </tr>
                        <tr>
                            <td>¿Cómo se Soporta?</td>
                            <td><p id="txtSoporte"></p></td>
                        </tr>
                        <tr>
                            <td>¿Quién realiza seguimiento?</td>
                            <td><p id="txtQuienSeguimiento"></p></td>
                        </tr>
                        <tr>
                            <td>Proceso</td>
                            <td><p id="txtProceso"></p></td>
                        </tr>
                        <tr>
                            <td>Usuario creador del plan de acción</td>
                            <td><p id="txtUsuCreador"></p></td>
                        </tr>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="col col-4">
             <div class="page-title">
                <div class="title_center">
                    <h3>MIS PLANES DE ACCIÓN</h3>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col col-12">
            <div class="canvas-container">
                <div class="columna">
                    <div class="title_k">
                        <h2><i class="fa fa-tasks mr-3"></i>Planes de Acción</h2>
                    </div>

                    <div class="p-3">
                        <div class="form-control search-p d-flex justify-content-between">
                            <input type="text" placeholder="Buscar..." id="txtSearchAsignado"/>
                            <i class="fa fa-search"></i>
                        </div>
                    </div>
                </div>
                <div class="columna">
                    <div class="title_k">
                        <h2><i class="fa fa-gears mr-3"></i>En Proceso</h2>
                    </div>
                    <div class="p-3">
                        <div class="form-control search-p d-flex justify-content-between">
                            <input type="text" placeholder="Buscar..." id="txtSearchProceso"/>
                            <i class="fa fa-search"></i>
                        </div>
                    </div>
                </div>
                <div class="columna">
                    <div class="title_k">
                        <h2><i class="fa fa-square-o mr-3"></i>En Evaluación</h2>
                    </div>
                    <div class="p-3">
                        <div class="form-control search-p d-flex justify-content-between">
                            <input type="text" placeholder="Buscar..." id="txtSearchEvaluacion"/>
                            <i class="fa fa-search"></i>
                        </div>
                    </div>
                </div>
                <div class="columna">
                    <div class="title_k">
                        <h2><i class="fa fa-check-square-o mr-3"></i>Teminados</h2>
                    </div>
                    <div class="p-3">
                        <div class="form-control search-p d-flex justify-content-between">
                            <input type="text" placeholder="Buscar..." id="txtSearchTerminado"/>
                            <i class="fa fa-search"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js/MisPlanesJS.js"></script>
</asp:Content>
