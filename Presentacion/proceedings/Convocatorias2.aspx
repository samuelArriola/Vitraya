<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Convocatorias2.aspx.cs" Inherits="Presentacion.proceedings.Convocatorias2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="modal" tabindex="-1" role="dialog" id="loading-modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title text-center w-100">Cargando</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="text-center">
                        <div class="preloader" style="display: inline-block"></div>
                    </div>
                    <p class="text-center">Por favor espere ...</p>
                </div>
            </div>
        </div>
    </div>

    <div class="row">

        <div class="col col-4">
             <div class="page-title">
                <div class="title_center">
                    <h3>CONVOCATORIAS</h3>
                </div>
            </div>
        </div>
    </div>

    <div class="row">


        <div class="col col-6">

            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Convocatorias disponibles para iniciar</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">

                        <div class="col col-12">
                            <table class="table" style="overflow:auto; width:100%;" id="tableConvocatoriasD">
                                <thead>
                                    <tr>
                                        <th>Nombre de Convocatoria</th>
                                        <th>Fecha programada</th>
                                        <th style="display:none;" >Fecha sin formato</th>
                                        <th style="display:none;" >Lugar</th>
                                        <th style="width: 180px;" >Acciones</th>
                                    </tr>  
                                </thead>
                                <tbody id="tbConvD">

                                </tbody>   
                            </table>
                        </div>

                    </div>
                </div>
            </div>

            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Convocatorias realizadas disponibles para desarrollo</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">

                        <div class="col col-12">
                            <table class="table" style="overflow:auto; width:100%;" id="tableConvocatoriasP">
                                <thead>
                                    <tr>
                                        <th>Nombre de la Convocatoria</th>
                                        <th>Lugar</th>
                                        <th>Fecha</th>
                                        <th style="width: 180px;">Acciones</th>
                                    </tr>  
                                </thead>
                                <tbody id="tbConP">

                                </tbody>   
                            </table>
                        </div>

                    </div>
                </div>
            </div>

        </div>

        <div class="col col-6">
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Convocar reunión para gestión general o Comité</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">

                        
                            <div class="col col-6">
                                <div class="form-group">
                                    <label id="LnomReunion">* Nombre de la reunión o convocatoria:</label>
                                    <input type="text" class="form-control" id="nomReunion"/>
                                </div>
                            </div>

                            <div class="col col-6 DivunidadFReunion">
                                <div class="form-group ">
                                    <label>* Unidad funcional:</label>
                                    <select id="unidadFReunion" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example" >
                                        <option value="0" selected>Seleccione</option>
                                    </select>
                                </div>
                            </div>

                            <div class="col col-6 DivcoordReunion">
                                <div class="form-group">
                                    <label>* Coordinador:</label>
                                    <select id="coordReunion" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example" >
                                        <option value="0" selected>Seleccione</option>
                                    </select>
                                </div>
                            </div>

                            <div class="col col-6">
                                <div class="form-group">
                                    <label id="LfechaReunion">* Fecha y hora:</label>
                                    <input type="datetime-local" class="form-control" id="fechaReunion"/>
                                </div>
                            </div>

                            <div class="col col-6">
                                <div class="form-group">
                                    <label id="LlugarReunion">* Lugar:</label>
                                    <input type="text" class="form-control" id="lugarReunion"/>
                                </div>
                            </div>

                            <div class="col col-6">
                                <div class="form-group">
                                    <label id="LlinkReunion">Link de la reunion:</label>
                                    <input type="text" class="form-control" id="linkReunion"/>
                                </div>
                            </div>
                            
                            <div class="x_content">
                                <div class="x_title">
                                    <div class="clearfix">
                                        <h6>Agenda de la reunion o convocatoria</h6>
                                    </div>
                                </div>
                            </div>

                            <div class="col col-10">
                                <div class="form-group">
                                    <label id="LtemasReunion">* Temas a desarrollar en la reunion o convocatoria:</label>
                                    <input type="text" class="form-control" id="temasReunion"/>
                                </div>
                            </div>
                            <div class="col col-2">
                                <div class="form-group">
                                    <button style="margin-top: 27px;" type="button" class="btnAñadirTema btn btn-primary">AÑADIR</button>
                                </div>
                            </div>

                            <div class="col col-12">
                                <table class="table tableTemas" style="overflow:auto; width:100%; display:none;" id="tableTemas">
                                    <thead>
                                        <tr>
                                            <th>Nombre del tema</th>
                                            <th>Opciones</th>
                                        </tr>  
                                    </thead>
                                    <tbody id="tbInfoTemas">

                                    </tbody>   
                                </table>
                            </div>

                            <div class="x_content">
                                <div class="x_title">
                                    <div class="clearfix">
                                        <h6>Miembros e Invitados</h6>
                                    </div>
                                </div>
                            </div>

                            <div class="col col-10">
                                <div class="form-group">
                                    <label>Agregar invitados individualmente:</label>
                                    <select id="invitadosI" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example" >
                                        <option value="0" data-correo="0" selected>Seleccione</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col col-2">
                                <div class="form-group">
                                    <button style="margin-top: 27px;" type="button" class="btninvitadosI btn btn-primary">AÑADIR</button>
                                </div>
                            </div>

                            <div class="col col-10">
                                <div class="form-group">
                                    <label>Agregar invitados por cargos:</label>
                                    <select id="invitadosC" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example" >
                                        <option value="0" data-correo="0" selected>Seleccione</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col col-2">
                                <div class="form-group">
                                    <button style="margin-top: 27px;" type="button" class="btninvitadosC btn btn-primary">AÑADIR</button>
                                </div>
                            </div>

                            <div class="col col-10">
                                <div class="form-group">
                                    <label>Agregar invitados por unidad funcional:</label>
                                    <select id="invitadosUF" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example" >
                                        <option value="0" data-correo="0" selected>Seleccione</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col col-2">
                                <div class="form-group">
                                    <button style="margin-top: 27px;" type="button" class="btninvitadosUF btn btn-primary">AÑADIR</button>
                                </div>
                            </div>

                            <div class="col col-12">
                                <table class="table tableInvitados" style="overflow:auto; width:100%; display:none;" id="tableInvitados">
                                    <thead>
                                        <tr>
                                            <th>Identificacion</th>
                                            <th>Nombre</th>
                                            <th>Tipo</th>
                                            <th>Opciones</th>
                                        </tr>  
                                    </thead>
                                    <tbody id="tbInfoInvitados">

                                    </tbody>   
                                </table>
                            </div>

                            
                            <div class="col col-12">
                                <div class="text-center">
                                    <div class="form-group">
                                        <button style="margin-top: 27px;" type="button" class="btnGuardar btn btn-success">REALIZAR CONVOCATORIA <i class='btnGuardar fa fa-users'></i></button>
                                        <button style="margin-top: 27px; display:none;" type="button" class="btnCancelar btn btn-danger">CANCELAR <i class='btnCancelar fa fa-mail-reply '></i></button>
                                    </div>
                                </div>
                            </div>
                            

                    </div>
                </div>
            </div>
        </div>


    </div>

    <script src="js/Convocatorias2JS.js"></script>

</asp:Content>
