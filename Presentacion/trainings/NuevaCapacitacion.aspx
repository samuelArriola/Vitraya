<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="NuevaCapacitacion.aspx.cs" Inherits="Presentacion.trainings.NuevaCapacitacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal" tabindex="-1" role="dialog" id="modal-facilitador">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Modal title</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Modal body text goes here.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary">Save changes</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" tabindex="-1" role="dialog" id="modal-ejetematico">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Ejes temáticos</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Nuevo eje temático</label>
                        <div class="d-flex justify-content-between">
                            <input type="text" placeholder="Nuevo eje temático" class="form-control" id="txtCreateEjeTematico" />
                            <button class="btn btn-primary ml-3" id="btnCreateEjeTematico"><i class=" fa fa-plus"></i></button>
                        </div>
                    </div>
                    <table class="table-inf" id="tbEjesTematicos">
                        <thead>
                            <tr>
                                <th>Nombre del eje temático</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Nueva Capacitación</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Responsable<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <div style="width: 100%; position: relative">
                        <div class="d-flex justify-content-between form-control search-p">
                            <input type="text" id="txtResponsable" placeholder="Seleccione" autocomplete="off" spellcheck="false" />
                            <i class="fa fa-angle-down"></i>
                        </div>
                        <div class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                    </div>
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Eje Temático<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <div class="d-flex justify-content-between">
                        <div style="width: 100%; position: relative">
                            <div class="d-flex justify-content-between form-control search-p">
                                <input type="text" id="txtEjeTematico" placeholder="Tema de la capacitación" autocomplete="off" spellcheck="false" />
                                <i class="fa fa-angle-down"></i>
                            </div>
                            <div class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                        </div>
                        <button class="btn btn-primary ml-3" id="btnEjeTematico"><i class="fa fa-plus"></i></button>
                    </div>
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Tema<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <input type="text" class="form-control" id="txtTema" placeholder="Seleccione" autocomplete="off" spellcheck="false" />
                    <div style="position: relative; width: 100%">
                        <div class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                    </div>
                </div>
            </div>
            <div class="x_title">
                <div class="clearfix">
                    <h6>Agenda</h6>
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Fecha de Inicio<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <input type="date" class="form-control" id="FechaInicio" autocomplete="off" spellcheck="false" />
                    <div style="position: relative; width: 100%">
                        <div class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                    </div>
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Hora de Inicio<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <input type="time" class="form-control" id="HoraInicio" autocomplete="off" spellcheck="false" />
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Fecha de Finalización<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <input type="date" class="form-control" id="FechaFinal" autocomplete="off" spellcheck="false" />
                    <div style="position: relative; width: 100%">
                        <div class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                    </div>
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Hora de Finalizacion<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <input type="time" class="form-control" id="HoraFinal" autocomplete="off" spellcheck="false" />
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Tiempo requerido para la firma<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <input type="text" class="form-control" placeholder="Número de días requeridos para la firma" id="txtTempFirma" autocomplete="off" spellcheck="false" />
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Subtemas<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <div class="d-flex justify-content-between">
                        <input type="text" class="form-control" id="txtSubtemas" placeholder="Seleccione" autocomplete="off" spellcheck="false" />
                        <button class="btn btn-primary ml-3" id="btnAddSubtema"><i class="fa fa-plus"></i></button>
                    </div>
                    <div id="lstSubtemas"></div>
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Archivos<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <div id="pnUpLoadArch" class="text-center">
                        <div id="boxUpLoadArch">
                            <svg width="9em" height="9em" viewBox="0 0 16 16" class="bi bi-cloud-arrow-up-fill pt-3" fill="currentColor" xmlns="http://www.w3.org/2000/svg" id="imgUpLoad">
                                <path fill-rule="evenodd" d="M8 2a5.53 5.53 0 0 0-3.594 1.342c-.766.66-1.321 1.52-1.464 2.383C1.266 6.095 0 7.555 0 9.318 0 11.366 1.708 13 3.781 13h8.906C14.502 13 16 11.57 16 9.773c0-1.636-1.242-2.969-2.834-3.194C12.923 3.999 10.69 2 8 2zm2.354 5.146l-2-2a.5.5 0 0 0-.708 0l-2 2a.5.5 0 1 0 .708.708L7.5 6.707V10.5a.5.5 0 0 0 1 0V6.707l1.146 1.147a.5.5 0 0 0 .708-.708z"></path>
                            </svg>
                            <asp:FileUpload runat="server" ID="fuArchivo" class="fuArchivo" />
                        </div>
                    </div>
                    <div id="lsArchivos"></div>
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Modalidad<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <select class="form-control" id="slcModalidad">
                        <option value="-1" disabled="disabled" selected="selected">Seleccione</option>
                        <option>Presencial</option>
                        <option>Virtual con facilitador</option>
                        <option>Virtual documental</option>
                    </select>
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Link de la Reunión<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <input type="text" class="form-control" id="txtLinkReunion" placeholder="Link de la reunión" autocomplete="off" spellcheck="false" />
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Link de contenido externo</label>
                <div class="col-md-6 col-sm-6">
                    <input type="text" class="form-control" id="txtLinkExterno" placeholder="Link del contenido externo" autocomplete="off" spellcheck="false" />
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Lugar<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <select class="form-control" id="slcLugar">
                        <option value="-1" disabled="disabled" selected="selected">Seleccione</option>
                        <option>Crecer administración</option>
                        <option>Crecer consulta externa</option>
                        <option>Crecer asistencial</option>
                        <option>Virtual</option>
                    </select>
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">Facilitador<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <div class="d-flex justify-content-between">
                        <div style="width: 100%; position: relative">
                            <div class="d-flex justify-content-between form-control search-p">
                                <input type="text" id="txtCapacitador" placeholder="Seleccione" autocomplete="off" spellcheck="false" />
                                <i class="fa fa-angle-down"></i>
                            </div>
                            <div class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                        </div>
                        <button class="btn btn-primary ml-3"><i class="fa fa-plus"></i></button>
                    </div>
                </div>
            </div>
            <div class="field item form-group justify-content-center mt-5">
                <div class="col col-6 d-flex justify-content-between">
                    <button class="btn btn-danger">Cancerlar</button>
                    <button class="btn btn-success" id="btnCrearCapacitacion">Crear Capacitación</button>
                </div>
            </div>
        </div>
    </div>
    <script src="js/NuevaCapacitacionJs.js"></script>
</asp:Content>
