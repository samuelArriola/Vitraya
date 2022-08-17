<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CrearProtocolo.aspx.cs" Inherits="Presentacion.GestionDocumental.CrearProtocolo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/CrearProtocoloCSS.css" rel="stylesheet" />
    <link href="css/CrearProcedimientoCss.css" rel="stylesheet" />
    <div class="modal" tabindex="-1" role="dialog" id="event-modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">¿Esta seguro de iniciar el Proceso de Revisión?</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Una vez el indicador sea enviado a revisión no se prodra volver a realizar cambios</p>
                </div>
                <div class="modal-footer">
                    <asp:Button Text="Guardar Cambios" runat="server" ID="btnGuardarDoc" CssClass="btn btn-success" type="button" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Creación del Protocolo: <span id="nomProtocolo"></span></h6>
            </div>
        </div>
        <div class="x_content">
            <div class="row justify-content-center">
                <div class="col col-10">
                    <div class="form-group">
                        <label>Version</label>
                        <input type="number" class="form-control" id="txtVersion" />
                    </div>
                    <div class="form-group">
                        <label>Procesos</label>
                        <asp:DropDownList runat="server" ID="ddlProcesos" CssClass="form-control">
                            <asp:ListItem Text="Seleccione" Value="-1"/>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Nombre del Protocolo</label>
                        <input type="text" class="form-control" id="txtNombre" />
                    </div>
                    <div class="form-group">
                        <label>Alcance</label>
                        <textarea id="txtAlcance" cols="" rows="" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Objetivo</label>
                        <textarea id="txtObjetivo" cols="" rows="" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Responsable</label>
                        <div class="d-flex justify-content-around">
                            <input type="text" id="txtResponsable" class="form-control d-none" />
                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlResponsable">
                            </asp:DropDownList>
                            <button type="button" class="btn btn-success" id="btnChangeResponsable"><i class="fa fa-refresh ml-1"></i></button>
                        </div>
                    </div>
                    <div id="lstResponsable"></div>

                    <div class="form-group">
                        <label>Recurso Humano</label>
                        <div class="d-flex justify-content-between">
                            <input type="text" id="txtTalentoHumano" class="form-control d-none" />
                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlTalentoHumano">
                                <asp:ListItem Text="Seleccione" Value="-1" />
                            </asp:DropDownList>
                            <button type="button" class="btn btn-success" id="btnChangeTalHum"><i class="fa fa-refresh ml-1" ></i></button>
                        </div>
                    </div>
                    <div id="lstTalHumano"></div>

                    <div class="form-group">
                        <label>Equipos Biomédicos</label>
                        <textarea  class="form-control" id="txtEquiposBiomedicos">
                        </textarea>
                    </div>
                    <div class="form-group">
                        <label>Medicamentos, dispositivos e insumos médicos</label>
                        <textarea class="form-control" id="txtMedicamentos">
                        </textarea>
                    </div>
                    <div class="form-group">
                        <label>Recursos Informáticos</label>
                        <textarea class="form-control" id="txtRecInformaticos">
                        </textarea>
                    </div>
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>Actividades</h6>
                        </div>
                    </div>
                    <div class="form-gorup mt-2">
                        <textarea id="txtAtividades" rows="7" class="form-control"></textarea>
                    </div>
                    <div class="x_title mb-2">
                        <div class="clearfix">
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <label>Definiciones</label>
                        <textarea id="txtDefiniciones"  rows="7" class="form-control"></textarea>
                    </div>
                    
                    
                    <div class="form-group">
                        <label>Recomendaciones Generales</label>
                        <textarea id="txtRecomendaciones" cols="" rows="" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Referencias Normativas</label>
                        <input type="text" name="name" value="" id="txtRefNorm" />
                    </div>
                    <div id="lstRefNormativas"></div>
                    <div class="form-group">
                        <label>Indicadores</label>
                        <input type="text" value="" id="txtIndicadores" />
                    </div>
                   
                    <div class="form-group">
                        <label>Anexos</label>
                        <textarea id="txtAnexos"></textarea>
                    </div>
                    <div class="justify-content-between d-flex">
                        <button class="btn btn-success" id="btnGuardar" type="button">Guardar</button>
                        <button class="btn btn-primary" id="btnViewProtocolo" type="button"><i class="fa fa-file-pdf-o"></i></button>
                        <button class="btn btn-primary" id="btnRevision" type="button">Enviar A Revisión</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../proceedings/js/tinymce.min.js"></script>
    <script src="js/CrearProtocoloJS.js"></script>
</asp:Content>
