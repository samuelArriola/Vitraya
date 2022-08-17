<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CensoDiario.aspx.cs" Inherits="Presentacion.Facturacion.CensoDiario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

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
                        <div class="preloader" style="display:inline-block"></div>
                    </div>
                    <p class="text-center">Por favor espere mientras se cargan los datos del Censo</p>
                </div>
            </div>
        </div>
    </div>

    <div class="modal" tabindex="-1" role="dialog" id="modal2">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Detalles de Paciente</h5>
                </div>
                <div class="modal-body" id="modalBody2">

                    <p id="PNombrePaciente"></p>
                    <p id="PIdentificacionPaciente"></p>

                    <table class="table" style="overflow:auto;" id="tableDetalles">
                        <thead>
                            <tr>
                                
                                <th>Numero de Ingreso</th>
                                <th style="display:none;" >Numero de Identificacion</th>
                                <th>Tipo de Servicio</th>
                                <th>Unidad Funcional</th>
                                <th>Cups</th>
                                <th>Cups Descripcion</th>
                                <th>Cama</th>
                                <th>Opciones</th>

                            </tr>  
                        </thead>
                        <tbody id="tbInfoDetalles">

                        </tbody>   
                    </table>

                    <div id="DivCerrar" ></div>
                    
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">SALIR</button>
                </div>
            </div>
        </div>
    </div>

     <div class="row">

        <div class="col col-12">
             <div class="page-title">
                <div class="title_left">
                    <h3>Censo Diario de Pacientes</h3>
                </div>
            </div>

            <div runat="server" class="x_panel" id="panelAdministrador">

                <div class="x_content">

                    <div class="row">

                        <div class="col col-12">

                            <div class="x_title">
                                <div class="clearfix">
                                    <h6 style = "float: left;" >Filtro de busquedas</h6>
                                </div>
                            </div>
                                 <div>
                                    <div class="row">
                                        <div class="col col-2">
                                            <div class="form-group">
                                                <label>Identificacion:</label>
                                                <input type="number" class="form-control" id="filtroNumId"/>
                                            </div>
                                        </div>
                                        <div class="col col-2">
                                            <div class="form-group">
                                                <label>Nombres y Apellidos:</label>
                                                <input type="text" class="form-control" id="filtroNombres"/>
                                            </div>
                                        </div>
                                        <div class="col col-2">
                                            <div class="form-group">
                                                <label>Ingreso:</label>
                                                <input type="number" class="form-control" id="filtroNumIngreso"/>
                                            </div>
                                        </div>
                                        <div class="col col-2">
                                            <div class="form-group">
                                                <label>Fecha de Ingreso:</label>
                                                <input type="date" class="form-control" id="filtroFecha"/>
                                            </div>
                                        </div>
                                        <div class="col col-1">
                                            <div class="form-group">
                                                <button style="margin-top: 27px;" type="button" class="btnBuscarFec btn btn-primary">BUSCAR</button>
                                            </div>
                                        </div>
                                        <div class="col col-1">
                                            <div class="form-group">
                                                <label>Grupo:</label>
                                                <select id="filtroGrupo" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example">
                                                    <option value="" selected>Seleccione</option>
                                                    <option value="URGENCIAS">URGENCIAS</option>
                                                    <option value="SALA DE PARTO">SALA DE PARTO</option>
                                                    <option value="UCI ADULTO">UCI ADULTO</option>
                                                    <option value="UCI NEONATAL">UCI NEONATAL</option>
                                                    <option value="HOSPITALIZACION">HOSPITALIZACION</option>
                                                    <option value="CIRUGIA">CIRUGIA</option>
                                                    <option value="CAMAS DE EMERGENCIA">CAMAS DE EMERGENCIA</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col col-1">
                                            <div class="form-group">
                                                <label>Subgrupo:</label>
                                                <select id="filtroSubGrupo" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example">
                                                    <option value="" selected>Seleccione</option>
                                                    <option value="URGENCIA OBSERVACION">URGENCIA OBSERVACION</option>
                                                    <option value="URGENCIA PROCEDIMIENTOS">URGENCIA PROCEDIMIENTOS</option>
                                                    <option value="URGENCIA OBSERVACION PEDIATRICA">URGENCIA OBSERVACION PEDIATRICA</option>
                                                    <option value="URGENCIA OBSERVACION REANIMACION">URGENCIA OBSERVACION REANIMACION</option>
                                                    <option value="URGENCIA ENFERMERIA">URGENCIA ENFERMERIA</option>
                                                    <option value="SEGUNDO PISO HOSPITALIZACION">SEGUNDO PISO HOSPITALIZACION</option>
                                                    <option value="TERCER PISO UCI ADULTO INTERMEDIO">TERCER PISO UCI ADULTO INTERMEDIO</option>
                                                    <option value="TERCER PISO UCI ADULTO INTENSIVO">TERCER PISO UCI ADULTO INTENSIVO</option>
                                                    <option value="TERCER PISO UCI NEONATAL BASICA">TERCER PISO UCI NEONATAL BASICA</option>
                                                    <option value="TERCER PISO UCI NEOTAL INTERMEDIA">TERCER PISO UCI NEOTAL INTERMEDIA</option>
                                                    <option value="TERCER PISO UCI NEONATAL INTENSIVA">TERCER PISO UCI NEONATAL INTENSIVA</option>
                                                    <option value="CUARTO PISO PEDIATRIA">CUARTO PISO PEDIATRIA</option>
                                                    <option value="CUARTO PISO HOSPITALIZACION">CUARTO PISO HOSPITALIZACION</option>
                                                    <option value="QUINTO PISO HOSPITALIZACION">QUINTO PISO HOSPITALIZACION</option>
                                                    <option value="QUINTO PISO AISLADO">QUINTO PISO AISLADO</option>
                                                    <option value="SEGUNDO PISO SALA DE RECUPERACION">SEGUNDO PISO SALA DE RECUPERACION</option>
                                                    <option value="SEXTO PISO HOSPITALIZACION">SEXTO PISO HOSPITALIZACION</option>
                                                    <option value="SEPTIMO PISO HOSPITALIZACION GENERAL">SEPTIMO PISO HOSPITALIZACION GENERAL</option>
                                                    <option value="URGENCIA V">URGENCIA V</option>
                                                    <option value="SEGUNDO PISO SALA DE PARTO">SEGUNDO PISO SALA DE PARTO</option>
                                                    <option value="SEGUNDO PISO TRABAJO DE PARTO">SEGUNDO PISO TRABAJO DE PARTO</option>
                                                    <option value="SEGUNDO PISO QUIROFANOS">SEGUNDO PISO QUIROFANOS</option>
                                                    <option value="CIRUGIA V">CIRUGIA V</option>
                                                    <option value="EMERGENCIAS">EMERGENCIAS</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col col-1">
                                            <div class="form-group">
                                                <button style="margin-top: 27px;" type="button" class="btnBuscar btn btn-primary">BUSCAR</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="x_title">
                                <div class="clearfix">
                                </div>
                            </div>

                                <table class="table" style="overflow:auto; width:100%;" id="tableCenso">
                                    <thead>
                                        <tr>
                                            <th>Grupo</th>
                                            <th>Subgrupo</th>
                                            <th style="width: 90px;" >Admisión</th>
                                            <th style="display:none;" >Tipo de Documento</th>
                                            <th>Documento</th>
                                            <th>Nombres y Apellidos Paciente</th>
                                            <th>Fecha de Ingreso</th>
                                            <th>Opciones</th>
                                            <th>Egreso</th>

                                        </tr>  
                                    </thead>
                                    <tbody id="tbInfoCenso">

                                    </tbody>   
                                </table>

                                <div>
                                    <div class="col col-12">
                                        <div class="form-group">
                                            <%--<button style="margin-top: 10px; margin-left: 1550px;" type="button" class="btnVerHistorico btn btn-secondary">VER HISTORICO DE CIERRE DE PACIENTES</button>--%>
                                            <button type="button" class="btnVerHistorico btn btn-secondary">VER HISTORICO DE CIERRE DE PACIENTES</button>
                                        </div>
                                    </div>
                                </div>
    
                        </div>

                    </div>
                </div>

             </div>

        </div>

    <script src="JS/CensoDiarioJS.js"></script>

</asp:Content>
