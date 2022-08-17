<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="SolicitarAutorizaciones.aspx.cs" Inherits="Presentacion.Facturacion.SolicitarAutorizaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="modal" tabindex="-1" role="dialog" id="loading-modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-center w-100">Buscando Información</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="text-center">
                        <div class="preloader" style="display:inline-block"></div>
                    </div>
                    <p class="text-center">Por favor espere mientras se cargan los datos del paciente</p>
                </div>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="col col-12">
             <div class="page-title">
                <div class="title_left">
                    <h3>Registro de Solicitud de Autorización</h3>
                </div>
            </div>
            <div class="x_panel">
                <div class="x_content">
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>INFORMACIÓN DEL AFILIADO</h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* Tipo de identificación:</label>
                                <select id="tipoId" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example">
                                    <option value="0" selected>Seleccione</option>
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
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* Número de Identificación:</label>
                                <input type="number" class="form-control" id="numId"/>
                            </div>
                        </div>
                        <%--<div class="col col-1">
                                <div class="form-group">
                                    <button style="margin-top: 27px;" type="button" class="btnBuscarInfoID btn btn-primary">BUSCAR</button>
                                </div>
                        </div>--%>
                    </div>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* Nombres y Apellidos:</label>
                                <input type="text" class="form-control" id="nombres"/>
                            </div>
                        </div>
                    </div>
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>SOLICITUD DE AUTORIZACIÓN</h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* Numero de la solicitud:</label>
                                <input type="number" class="form-control" id="numSolicitud" />
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* Fecha y Hora de Radicación:</label>
                                <input type="datetime-local" class="form-control" id="fechaSolicitud" />
                            </div>
                        </div>
                    </div>
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>INFORMACIÓN DE LA ATENCIÓN</h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* 1. Origen atención:</label>
                                <input type="text" class="form-control" id="origenAtencion" readonly/>
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* 4. Ubicación Paciente:</label>
                                <input type="text" class="form-control" id="ubicacion" readonly/>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* 2. Tipo de Servicios Solicitados:</label>
                                <input type="text" class="form-control" id="tipoServicio" readonly/>
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* 5. Numero de Ingreso:</label>
                                <input type="number" class="form-control" id="numeroIngreso" readonly/>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* 3. Prioridad Atención:</label>
                                <input type="text" class="form-control" id="prioridad" readonly/>
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* 6. Fecha y Hora de Ingreso:</label>
                                <input type="datetime-local" class="form-control" id="fechaIngreso" readonly/>
                            </div>
                        </div>
                    </div>
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>INFORMACIÓN DE SERVICIOS SOLICITADOS</h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* Contrato Prestación:</label>
                                <input type="text" class="form-control" id="contrato" readonly/>
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* Servicio:</label>
                                <input type="text" class="form-control" id="servicio" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* Numero de cama:</label>
                                <input type="text" class="form-control" id="numCama" readonly/>
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* Diagnóstico Principal:</label>
                                <input type="text" class="form-control" id="diagPrincipal" readonly/>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>Diagnóstico Rel 1:</label>
                                <input type="text" class="form-control" id="diag1" />
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>Diagnóstico Rel 2:</label>
                                <input type="text" class="form-control" id="diag2" />
                            </div>
                        </div>
                    </div>
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>NIT DE LA IPS SOLICITANTE</h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* Nombre de IPS:</label>
                                <input type="text" class="form-control" id="nombreIps" value="Centro Medico Crecer Ltda." readonly/>
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* Dirección IPS:</label>
                                <select id="direccionIps" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example">
                                    <option value="" selected>Seleccione</option>
                                    <option value="0-CARTAGENA-BARRIO AMBERES TERCER CALLEJON CRA 44 N 27-113">0-CARTAGENA-BARRIO AMBERES TERCER CALLEJON CRA 44 N 27-113</option>
                                    <option value="1-CARTAGENA-URGENCIAS_CIRUGIAS_PRADO CL 30 34-22 AV PEDRO DE HEREDIA" selected>1-CARTAGENA-URGENCIAS_CIRUGIAS_PRADO CL 30 34-22 AV PEDRO DE HEREDIA</option>
                                    <option value="2-CARTAGENA-LABORATORIO_BRUSELAS TV. 39 26C-90">2-CARTAGENA-LABORATORIO_BRUSELAS TV. 39 26C-90</option>
                                    <option value="3-CARTAGENA-CONSULTA EXTERNA_BARRIO AMBERES TERCER CALLEJON CRA 44 N 27-113">3-CARTAGENA-CONSULTA EXTERNA_BARRIO AMBERES TERCER CALLEJON CRA 44 N 27-113</option>
                                    <option value="4-CARTAGENA-IMAGENES DIAG_PRADO CL 30 34-22 AV PEDRO DE HEREDIA">4-CARTAGENA-IMAGENES DIAG_PRADO CL 30 34-22 AV PEDRO DE HEREDIA</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>JUSTIFICACIÓN CLÍNICA</h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-12">
                            <div class="form-group">
                                <label>* Justificación Clínica:</label>
                                <textarea class="form-control" id="justificacionClinica" ></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>TECNOLOGÍAS EN SALUD</h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-3">
                            <div class="form-group">
                                <label>* Clasificación:</label>
                                <input type="text" class="form-control" id="clasificacionT"/>
                            </div>
                        </div>
                        <div class="col col-7">
                            <div class="form-group">
                                <label>* Tecnología en Salud:</label>
                                <input type="text" class="form-control" id="tecnologiaT" list="listaCups" />
                                <datalist id="listaCups"></datalist>
                            </div>
                        </div>
                        <div class="col col-2">
                            <div class="form-group">
                                <label>* Cantidad Solicitada:</label>
                                <input type="number" class="form-control" id="cantidadT" />
                            </div>
                        </div>
                        <%--<div class="col col-1">
                            <div class="form-group">
                                <button style="margin-top: 27px;" type="button" class="btnAgregarT btn btn-primary">AÑADIR</button>
                            </div>
                        </div>--%>
                    </div>
                    <%--<table class="table" style="overflow:auto; width:100%;" id="tableTecnologias">
                        <thead>
                            <tr>
                                <th>Clasificación</th>
                                <th>Tecnología en Salud</th>
                                <th>Cantidad Solicitada</th>
                            </tr>  
                        </thead>
                        <tbody id="tbInfoT">

                        </tbody>   
                    </table>--%>
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>INFORMACION DEL USUARIO SOLICITANTE</h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>* Nombre Profesional de la Salud:</label>
                                <input type="text" class="form-control" id="profesionalSalud" readonly/>
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>Cargo:</label>
                                <input type="text" class="form-control" id="cargoProfesional" />
                            </div>
                        </div>
                    </div>
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>SOPORTES DE LA SOLICITUD</h6>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>Soporte para Autorización:</label>
                                <input type="file" class="form-control" id="ordenMedica" />
                            </div>
                        </div>
                        <div class="col col-6">
                            <div class="form-group">
                                <label>Ocurrencia Posible Siniestro:</label>
                                <input type="file" class="form-control" id="posibleSiniestro" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-12">
                            <div class="text-center">
                                <div class="form-group">
                                    <button style="margin-top: 27px;" type="button" class="btnGuardar btn btn-primary">REGISTRAR</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="result"></div>
                </div>
            </div>
        </div>

    </div>

    <script src="JS/RegistroAutorizacionesJS.js"></script>

</asp:Content>
