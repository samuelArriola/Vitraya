<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdministrarCapacitaciones.aspx.cs" Inherits="Presentacion.trainings.AministrarCapacitaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="../build/css/canvas.css" rel="stylesheet" />
    <link href="css/AministrarCapacitacionesCSS.css" rel="stylesheet" />
   <%-- <div style="position:absolute; width: 100%; height: 100vh; background: #0005; z-index:1000; left:0; top: 0">

    </div> --%>
    <div class="modal" tabindex="-1" role="dialog" id="modal-iniciar">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Iniciar Capacitación</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>
                        ¿Esta seguro de iniciar la capacitación?, Si inicia ya no se podra editar la capacitación. 
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="btnIniciar">Iniciar</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="col col-12">
             <div class="page-title">
                <div class="title_center">
                    <h3>ADMINISTRAR CAPACITACIONES GENERALES</h3>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="btnShowMenuTemas">

        </div>
        <div class="col col-12 col-lg-3 p-0 d-none d-lg-inline-block" id="menuContainerTemas">
            <div class="" id="menuTemas">
                <div class="x_panel">
                    <div class="pt-2 pb-2" style="position:sticky; top:-10px; background: #fff; z-index:100">
                        <div class="btnHideMenuTemas">
                            <i class="fa fa-arrow-right"></i>
                        </div>
                    </div>
                    <div class="x_title">
                        <div class="clearfix">
                            <h6 class="text-center">Listado de Capacitaciones</h6>
                        </div>
                    </div>
                    <div class="x_content">
                        <div class="form-control search-p d-flex justify-content-between">
                            <input type="text" placeholder="Buscar..." id="txtSearch" />
                            <i class="fa fa-search"></i>
                        </div>  

                        <table id="tbCapacitciones" class="tbMenu">
                            <thead>
                                <tr>
                                    <th>Capacitaciones</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="col col-12 col-lg-9">
            <div class="row">
                <div class="col col-11 col-lg-12">
                    <div class="row">
                        <div class="animated flipInY col-lg-4 col-md-6 ">
                            <div class="tile-stats p-2">
                                <div class="d-flex justify-content-between mt-1">
                                    <div><strong><font style="vertical-align: inherit;"><font style="vertical-align: inherit; font-size: 20px !important">CÓDIGO</font></font></strong></div>
                                    <i class="fa fa-terminal fa-2x"></i>
                                </div>
                                <h6 class=""><font style="vertical-align: inherit;"><font style="vertical-align: inherit;"><span id="lbCodigo"></span></font></font></h6>
                            </div>
                        </div>
                        <div class="animated flipInY col-lg-4 col-md-6">
                            <div class="tile-stats p-2">
                                <div class="d-flex justify-content-between mt-1">
                                    <div><strong><font style="vertical-align: inherit;"><font style="vertical-align: inherit; font-size: 20px">RESPONSABLE</font></font></strong></div>
                                    <i class="fa fa-user fa-2x"></i>
                                </div>
                                <h6 class=""><font style="vertical-align: inherit;"><font style="vertical-align: inherit;"><span id="lbResponsable"></span></font></font></h6>
                            </div>
                        </div>
                        <div class="animated flipInY col col-lg-4 col-md-12">
                            <div class="tile-stats p-2">
                                <div class="d-flex justify-content-between mt-1">
                                    <div class=""><strong><font style="vertical-align: inherit; font-size: 20px"><font style="vertical-align: inherit;">TEMA</font></font></strong></div>
                                    <i class="fa fa-2x fa-file-o"></i>
                                </div>
                                <h6 class=""><font style="vertical-align: inherit;"><font style="vertical-align: inherit;"><span id="lbTema"></span></font></font></h6>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="panelAgendas">
                    </div>
                </div>
                <div class="col-1 text-center">
                    <button type="button" class="btn btn-secondary d-lg-none" id="btnMenuTemas"><i class="fa fa-bars"></i></button>
                </div>
            </div>
            
        </div>
        
    </div>

    <iframe name="actaAsistencia" style="display: none"></iframe>
    <script src="js/AministrarCapacitacionesJS.js"></script>
</asp:Content>
