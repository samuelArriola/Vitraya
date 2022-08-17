<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdministrarReportes.aspx.cs" Inherits="Presentacion.Power_BI.JS.AdministrarReprotes" %>

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
                        <div class="preloader" style="display: inline-block"></div>
                    </div>
                    <p class="text-center">Por favor espere mientras se cargan los datos</p>
                </div>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="col col-4">
             <div class="page-title">
                <div class="title_center">
                    <h3>Administrar Reportes Power BI</h3>
                </div>
            </div>
            <div class="x_panel" style="float: left;">
                <div class="x_content">
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>INFORMACIÓN DEL REPORTE</h6>
                        </div>
                    </div>
                 </div>
                    <div class="row">
                        <div class="col col-12">
                            <div class="form-group">
                                <label>* Estado del reporte:</label>
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio1" value="1">
                                    <label class="form-check-label" for="inlineRadio1">ACTIVO</label>
                                </div>
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio2" value="0">
                                    <label class="form-check-label" for="inlineRadio2">INACTIVO</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-12">
                            <div class="form-group">
                                <label id="codigoRLabel">* Codigo del reporte:</label>
                                <input type="number" class="form-control" id="codigoR" readonly/>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-12">
                            <div class="form-group">
                                <label>* Nombre del reporte:</label>
                                <input type="text" class="form-control" id="nombreR"/>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-12">
                            <div class="form-group">
                                <label>* Enlace del reporte:</label>
                                <input type="text" class="form-control" id="enlaceR"/>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-12">
                            <div class="form-group">
                                <label>* Tipo de reporte:</label>
                                <select id="tipoR" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example" >
                                    <option value="0" selected>Seleccione</option>
                                    <option value="Cuadro de mando">Cuadro de mando</option>
                                    <option value="Tablero de control operativo">Tablero de control operativo</option>
                                    <option value="Informe">Informe</option>
                                    <option value="Informe en tiempo real">Informe en tiempo real</option>
                                    <option value="Aplicacion">Aplicacion</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-12">
                            <div class="form-group">
                                <label>* Descripcion general del reporte:</label>
                                <textarea class="form-control" id="descripcionR" ></textarea>
                            </div>
                        </div>
                    </div>
                
                    <div class="x_content">
                        <div class="x_title">
                            <div class="clearfix">
                                <h6>PERMISOS A REPORTE</h6>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-10">
                            <div class="form-group">
                                <label>Permisos por usuario:</label>
                                <select id="permisosU" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example" >
                                    <option value="0" selected>Seleccione</option>
                                </select>
                            </div>
                        </div>
                    
                        <div class="col col-2">
                            <div class="form-group">
                                <button style="margin-top: 27px;" type="button" class="btnpermisosU btn btn-primary">AÑADIR</button>
                            </div>
                        </div>
                     </div>
                    <div class="row">
                        <div class="col col-10">
                            <div class="form-group">
                                <label>Permisos por cargo:</label>
                                <select id="permisosC" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example" >
                                    <option value="0" selected>Seleccione</option>
                                </select>
                            </div>
                        </div>
                        <div class="col col-2">
                            <div class="form-group">
                                <button style="margin-top: 27px;" type="button" class="btnpermisosC btn btn-primary">AÑADIR</button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-10">
                            <div class="form-group">
                                <label>Permisos por unidad funcional:</label>
                                <select id="permisosUF" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example" >
                                    <option value="0" selected>Seleccione</option>
                                </select>
                            </div>
                        </div>
                        <div class="col col-2">
                            <div class="form-group">
                                <button style="margin-top: 27px;" type="button" class="btnpermisosUF btn btn-primary">AÑADIR</button>
                            </div>
                        </div>
                    </div>
                    <table class="table" style="overflow:auto; width:100%;" id="tablePermisos">
                        <thead>
                            <tr>
                                <th>Numero de Identificacion</th>
                                <th>Nombres y apellidos</th>
                                <th>Opciones</th>
                            </tr>  
                        </thead>
                        <tbody id="tbInfoP">

                        </tbody>   
                    </table>
                    <div class="row">
                        <div class="col col-12">
                            <div class="text-center">
                                <div class="form-group">
                                    <button style="margin-top: 27px;" type="button" id="btnGuardar" class="btnGuardar btn btn-primary">REGISTRAR</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col col-12">
                            <div class="text-center">
                                <div class="form-group">
                                    <button style="margin-top: 27px;" type="button" id="btnActualizar" class="btnActualizar btn btn-primary">ACTUALIZAR</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
   </div>

   <div class="col col-8">

       <div class="page-title">
                <div class="title_center">
                    <h3></h3>
                </div>
            </div>

            <div class="x_panel">
                <div class="x_content">
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>LISTADO DE REPORTES</h6>
                        </div>
                    </div>
                 </div>

                    <div class="row">
                        <div class="col col-6">
                            <div class="form-group">
                                <label>Filtro de busqueda por nombre de reporte:</label>
                                <input type="text" class="form-control" id="filtroNomReporte" />
                            </div>
                        </div>
                        <div class="col col-2">
                            <div class="form-group">
                                <button style="margin-top: 27px;" type="button" class="btnFiltroNombreR btn btn-primary">BUSCAR</button>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col col-12">
                            <table class="table" style="overflow:auto; width:100%;" id="tableReportes">
                        <thead>
                            <tr>
                                <th>Codigo</th>
                                <th>Nombre de reporte</th>
                                <th>Estado</th>
                                <th>Opciones</th>
                            </tr>  
                        </thead>
                        <tbody id="tbInfoR">

                        </tbody>   
                    </table>
                        </div>
                    </div>
               
                </div>

            </div>
     </div>

    <script src="JS/AdministraReportesJS.js"></script>

</asp:Content>
