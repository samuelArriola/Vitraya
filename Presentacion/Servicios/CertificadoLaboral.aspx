<%@ Page Title="" Language="C#"  MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CertificadoLaboral.aspx.cs" Inherits="Presentacion.Servicios.certificadoLaboral" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <script src="../build/js/FileSaver.min.js"></script>
    <script src="../build/js/xlsx.full.min.js"></script>
    <script src="../build/js/tableexport.min.js"></script>

    <div class="row">

        <div class="col col-12">
             <div class="page-title">
                <div class="title_left">
                    <h3>Consulta y Generacion de Certificados Laborales</h3>
                </div>
            </div>

            <div runat="server" class="x_panel" id="panelEmpleado">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Recuerde que la falsificacion de documentos es un delito. Abstengase de editar o modificar el Certificado Laboral</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col col-12">

                           <button type="button" id="V" class="btnGenerarCertificado btn btn-primary btn-lg">VISUALIZAR CERTIFICADO</button>
                            <button type="button" id="D" class="btnGenerarCertificado btn btn-secondary btn-lg">DESCARGAR CERTIFICADO</button>
                           
                        </div>
                    </div>
                </div>
             </div>
            <div runat="server" class="x_panel" id="panelAdministrador1">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Consulta y descarga de certificados laborales. ACCESO ADMINISTRADOR.</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col col-md-6 col-sm-12">
                            <div class="form-group">
                                <label for="id">Numero de identificacion o nombre del usuario</label>
                                <div class="d-flex justify-content-around">
                                    <input type="text" class="form-control" id="id" list="listaCoincidencias" placeholder="Ingrese ID o nombre" />
                                    <button type="button" class="btnGenerarCertificadoA btn btn-primary ml-2">Generar</button>
                                </div>
                                <datalist id="listaCoincidencias">
                                </datalist>
                            </div>
                        </div>
                        
                    </div>
                </div>
             </div>

            <div runat="server" class="x_panel" id="panelAdministrador2">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>OPCIONES USUARIO ADMINISTRADOR.</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col col-12">

                            <div class="x_title">
                                <div class="clearfix">
                                    <h6>CONFIGURACION DE FIRMA</h6>
                                </div>  
                                 <div>
                                    <div class="row justify-content-between">
                                        <div class="col col-12 col-sm-6 col-md-5">
                                            <div class="form-group">
                                                <label for="idF">Número de identificación o nombre del usuario que firmará los certificados laborales</label>
                                                <div class="d-flex justify-content-between">
                                                    <input type="text" class="form-control" id="idF" list="listaCoincidenciasF" placeholder="Ingrese ID o nombre" />
                                                    <button type="button" class="btnCambioFirma btn btn-primary ml-2">Seleccionar</button>
                                                </div>
                                                <datalist id="listaCoincidenciasF">

                                                </datalist>
                                             </div>
                                        </div>
                                        <div class="col col-12 col-sm-6 col-md-5">
                                           <label for="idF">Usuario que firma certificados laborales</label>
                                           <input type="text" class="form-control" id="inputFirma" disabled/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col col-12">
                            <div class="x_title">
                                <div class="clearfix">
                                    <h6>HISTORICO</h6>
                                </div>
                                 <div>
                                    <div class="row justify-content-between">    
                                        <div class="col col-12 col-md-3">
                                            <div class="form-group">
                                                <label>Fecha Inicial</label>
                                                <input type="month" class="form-control" id="txtFecha1"/>
                                            </div>
                                        </div>
                                        <div class="col col-12 col-md-3">
                                            <div class="form-group">
                                                <label>Fecha Final</label>
                                                <input type="month" class="form-control" id="txtFecha2"/>
                                            </div>
                                        </div>
                                        <div class="col col-12 col-md-3">
                                            <div class="form-group">
                                                <label>Tipo de informacion</label>
                                                <select id="TipoI" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example">
                                                  <option value="0" selected>Seleccione</option>
                                                  <option value="1">En detalle por mes</option>
                                                  <option value="2">Cantidad por mes</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div>
                                            <div class="form-group">
                                                <label class="d-none d-md-block ">&nbsp;</label>
                                                <button  type="button" class="BuscarHistorico btn btn-primary">Buscar</button>
                                                <button  type="button" class="ExportarHistorico btn btn-secondary">Exportar</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tableContainer">
                                <table class="table" style="overflow:auto; width:100%;" id="tableDocs">
                                    <thead>
                                        <tr>
                                            <th>ID de empleado</th>
                                            <th>Nombre de empleado</th>
                                            <th>Tipo de accion</th>
                                            <th>Fecha y Hora</th>
                                        </tr>  
                                    </thead>
                                    <tbody id="tbHistorico">

                                    </tbody>   
                                </table>
                            </div>
                            <div class="tableContainer">
                                <table class="table" style="overflow:auto; width:100%;" id="table2">
                                    <thead>
                                        <tr>
                                            <th>Año</th>
                                            <th>Mes</th>
                                            <th>Cantidad descargas y visualizaciones</th>
                                        </tr>  
                                    </thead>
                                    <tbody id="tb2">

                                    </tbody>   
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
             </div>
        </div>
    </div>

    <script src="JS/CertificadoLaboralJS.js"></script>

</asp:Content>
