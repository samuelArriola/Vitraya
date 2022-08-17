<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="BitacoraAutorizaciones.aspx.cs" Inherits="Presentacion.Facturacion.BitacoraAutorizaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">


    <script src="../build/js/jspdf.min.js"></script>
    <script src="../build/js/jspdf.plugin.autotable.min.js"></script>
    <script src="../build/js/FileSaver.min.js"></script>
    <script src="../build/js/xlsx.full.min.js"></script>
    <script src="../build/js/tableexport.min.js"></script>

    <div class="modal" tabindex="-1" role="dialog" id="modal1">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Informacion General de Autorizaciónes</h5>
                </div>
                <div class="modal-body" id="modalBody1">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">CERRAR</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" tabindex="-1" role="dialog" id="modal2">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Aprobación de Solicitudes de Autorización</h5>
                </div>
                <div class="modal-body" id="modalBody2">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">CERRAR</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" tabindex="-1" role="dialog" id="modal3">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Anulación de Solicitudes de Autorización</h5>
                </div>
                <div class="modal-body" id="modalBody3">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">CERRAR</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" tabindex="-1" role="dialog" id="modal4">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Modificacion de Solicitudes de Autorización</h5>
                </div>
                <div class="modal-body" id="modalBody4">

                    <table class="table-inf" id="Editartablabitacora">
                        <tr>
                            <th class="text-center" colspan="2">Información del Paciente</th>
                        </tr>
                        <tr>
                            <td>Tipo de Identificación</td>
                            <td>
                                <select id="EtipoId" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example">
                                    <option value="RC">Registro Civil</option>
                                    <option value="TI">Tarjeta de Identidad</option>
                                    <option value="CC">Cedula de Ciudadania</option>
                                    <option value="CE">Cedula de Extranjeria</option>
                                    <option value="PT">Pasaporte</option>
                                    <option value="ASI">Adulto sin Identificación</option>
                                    <option value="MSI">Menor sin Identificación</option>
                                    <option value="PE">Permiso Especial</option>
                                    <option value="CNV">Certificado Nacido Vivo</option>
                                    <option value="SC">Salvo Conducto</option>
                                    <option value="N/A">Sin Tipo de Identificacion</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>Numero de Identificación</td>
                            <td>
                                <input type="number" class="form-control" id="EnumId" /></td>
                        </tr>
                        <tr>
                            <td>Nombre del paciente</td>
                            <td>
                                <input type="text" class="form-control" id="Enombres" /></td>
                        </tr>
                        <tr style="display: none;">
                            <td>Nombre del paciente</td>
                            <td>
                                <input type="number" class="form-control" id="EidAut" /></td>
                        </tr>
                        <tr>
                            <th class="text-center" colspan="2">Solicitud de Autorización</th>
                        </tr>
                        <tr>
                            <td>Numero de la solicitud</td>
                            <td>
                                <input type="number" class="form-control" id="EnumSolicitud" /></td>
                        </tr>
                        <tr>
                            <td>Fecha y Hora de Radicación</td>
                            <td>
                                <input type="datetime-local" class="form-control" id="EfechaSolicitud" /></td>
                        </tr>
                        <tr>
                            <th class="text-center" colspan="2">Información de la atención</th>
                        </tr>
                        <tr>
                            <td>Ubicacion Paciente</td>
                            <td>
                                <select id="EubicacionPaciente" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example">
                                    <option value="URGENCIAS">URGENCIAS</option>
                                    <option value="SALA DE PARTO">SALA DE PARTO</option>
                                    <option value="UCI ADULTO">UCI ADULTO</option>
                                    <option value="UCI NEONATAL">UCI NEONATAL</option>
                                    <option value="HOSPITALIZACION">HOSPITALIZACION</option>
                                    <option value="CIRUGIA">CIRUGIA</option>
                                    <option value="CAMAS DE EMERGENCIA">CAMAS DE EMERGENCIA</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td>Numero de Ingreso</td>
                            <td>
                                <input type="number" class="form-control" id="EnumeroIngreso" /></td>
                        </tr>
                        <tr>
                            <td>Fecha y Hora de Ingreso</td>
                            <td>
                                <input type="datetime-local" class="form-control" id="EfechaIngreso" /></td>
                        </tr>
                        <tr>
                            <th class="text-center" colspan="2">Información de Servicios Solicitados</th>
                        </tr>
                        <tr>
                            <td>Servicio</td>
                            <td>
                                <input type="text" class="form-control" id="Eservicio" /></td>
                        </tr>
                        <tr>
                            <td>Numero de cama</td>
                            <td>
                                <input type="text" class="form-control" id="EnumCama" /></td>
                        </tr>
                        <tr>
                            <td>Diagnóstico Principal</td>
                            <td>
                                <input type="text" class="form-control" id="EdiagPrincipal" /></td>
                        </tr>
                        <tr>
                            <th class="text-center" colspan="2">Tecnologías en Salud</th>
                        </tr>
                        <tr>
                            <td>Clasificacion Tecnologia</td>
                            <td>
                                <input type="text" class="form-control" id="EclasificacionT" /></td>
                        </tr>
                        <tr>
                            <td>Codigo y Nombre Tecnologia</td>
                            <td>
                                <input type="text" class="form-control" id="EtecnologiaT" list="listaCups" />
                                <datalist id="listaCups"></datalist>
                            </td>
                        </tr>
                        <tr>
                            <td>Cantidad</td>
                            <td>
                                <input type="number" class="form-control" id="EcantidadT" /></td>
                        </tr>
                    </table>
                    <div class="text-center">
                        <div class="form-group">
                            <button style="margin-top: 24px;" type="button" id="btnActualizar" class="btnActualizar btn btn-primary">ACTUALIZAR</button>
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">CERRAR</button>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" tabindex="-1" role="dialog" id="loading-modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-center w-100">Cargando Información</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="text-center">
                        <div class="preloader" style="display: inline-block"></div>
                    </div>
                    <p class="text-center">Por favor espere mientras se cargan los datos de la bitacora</p>
                </div>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="col col-12">
            <div class="page-title">
                <div class="title_left">
                    <h3>Bitácora de Autorizaciones</h3>
                </div>
            </div>

            <div runat="server" class="x_panel" id="panelAdministrador">

                <div class="x_content">

                    <div class="row">

                        <div class="col col-12">

                            <div class="x_title">
                                <div class="clearfix">
                                    <h6>Filtro de busquedas</h6>
                                </div>
                            </div>
                            <div>
                                <div class="row">
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>Ingreso:</label>
                                            <input type="number" class="form-control" id="filtroNumIngreso" />
                                        </div>
                                    </div>
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>Identificación:</label>
                                            <input type="number" class="form-control" id="filtroNumId" />
                                        </div>
                                    </div>
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>Nombre y Apellido</label>
                                            <input type="text" class="form-control" id="filtroNomPacien" />
                                        </div>
                                    </div>
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>Numero de Solicitud</label>
                                            <input type="number" class="form-control" id="filtroNumSolic" />
                                        </div>
                                    </div>
                                    <%--<div class="col col-2">
                                            <div class="form-group">
                                                <label>Profesional que solicita:</label>
                                                <input type="text" class="form-control" id="filtroNomProfe"/>
                                            </div>
                                        </div>--%>
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>Numero de Autorización:</label>
                                            <input type="text" class="form-control" id="filtroNumAut" />
                                        </div>
                                    </div>
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>Estado Autorización:</label>
                                            <select id="filtroEstAut" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example">
                                                <option value="" selected>Seleccione</option>
                                                <option value="Aprobado">Aprobadas</option>
                                                <option value="Pendiente">Pendientes</option>
                                                <option value="Anulado">Anuladas</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>Fecha Inicial Solicitud</label>
                                            <input type="date" class="form-control" id="fechIniSolic" />
                                        </div>
                                    </div>
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>Fecha Final Solicitud</label>
                                            <input type="date" class="form-control" id="fechFinSolic" />
                                        </div>
                                    </div>
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <button style="margin-top: 27px;" type="button" class="btnFiltroSolic btn btn-primary">BUSCAR</button>
                                        </div>
                                    </div>
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>Fecha Inicial Aprobación</label>
                                            <input type="date" class="form-control" id="fechIniAprob" />
                                        </div>
                                    </div>
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>Fecha Final Aprobación</label>
                                            <input type="date" class="form-control" id="fechFinAprob" />
                                        </div>
                                    </div>
                                    <div class="col col-1">
                                        <div class="form-group">
                                            <button style="margin-top: 27px;" type="button" class="btnFiltroAprob btn btn-primary">BUSCAR</button>
                                        </div>
                                    </div>
                                    <div class="col col-1">
                                        <div class="form-group">
                                            <button style="margin-top: 27px;" type="button" class="btnExportarInfo btn btn-secondary">EXPORTAR</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="x_title">
                            <div class="clearfix">
                            </div>
                        </div>

                        <table class="table table-responsive" style="overflow: auto; width: 100%;" id="tableBitacora">
                            <thead>
                                <tr>
                                    <th>Numero de Ingreso</th>
                                    <th>Numero de Identificación</th>
                                    <th>Nombre y Apellido</th>
                                    <th>Servicio</th>
                                    <th>EPS</th>
                                    <th>Numero de Solicitud</th>
                                    <th>Fecha de Solicitud</th>
                                    <th>Profesional que solicita</th>
                                    <th>Estado</th>
                                    <th>Fecha de Aprobación</th>
                                    <th>Numero de Autorización</th>
                                    <th>Detalles</th>
                                    <th class="ColumnaOpciones" style="width: 170px">Opciones</th>
                                </tr>
                            </thead>
                            <tbody id="tbInfo">
                            </tbody>
                        </table>

                        <table class="table table-responsive" style="overflow: auto; width: 100%;" id="tableBitacora2">
                            <thead>
                                <tr>
                                    <th>Numero de Ingreso</th>
                                    <th>Tipo de Identificación</th>
                                    <th>Numero de Identificación</th>
                                    <th>Nombre y Apellido</th>
                                    <th>Numero de Solicitud</th>
                                    <th>Fecha de Solicitud</th>
                                    <th>Origen de la Atención</th>
                                    <th>Tipo de Servicio</th>
                                    <th>Prioridad de la Atención</th>
                                    <th>Ubicacion del Paciente</th>
                                    <th>Fecha de Ingreso</th>
                                    <th>Contrato Prestación</th>
                                    <th>Servicio</th>
                                    <th>Numero de Cama</th>
                                    <th>Diagnostico Principal</th>
                                    <th>Nombre IPS</th>
                                    <th>Direccion IPS</th>
                                    <th>Profesional que solicita</th>
                                    <th>Cargo de Profesional que Solicita</th>
                                    <th>Estado Autorización</th>
                                    <th>Clasificacion Tecnologia</th>
                                    <th>Nombre Tecnologia</th>
                                    <th>Cantidad Tecnologia</th>
                                    <th>Fecha de Aprobación</th>
                                    <th>Numero de Autorización</th>
                                    <th>Motivo de Anulacion</th>
                                    <th>Fecha de Anulacion</th>

                                </tr>
                            </thead>
                            <tbody id="tbInfo2">
                            </tbody>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </div>
    </div>

    <script src="JS/BitacoraAutorizacionesJS.js"></script>

</asp:Content>
