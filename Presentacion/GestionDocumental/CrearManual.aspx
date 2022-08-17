<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CrearManual.aspx.cs" Inherits="Presentacion.GestionDocumental.CrearManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <style>
        #ContentPlaceHolder_ddlProcs option[value='Protocolos'], #ContentPlaceHolder_ddlProcs option[value='Procedimientos']{
            font-weight:900;
            font-size: 15px;
            text-align: center;
            background: #ccc;
        }
    </style>

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
                    <p>Una vez el Documento sea enviado a revisión no se prodra volver a realizar cambios</p>
                </div>
                <div class="modal-footer">
                    <asp:Button Text="Guardar Cambios" runat="server" ID="btnGuardarInd" CssClass="btn btn-success"/>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Creación del Manual:</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="row justify-content-center">
                <div class="col col-10">
                    <div class="form-group">
                        <label>Versión</label>
                        <input type="number" id="txtVersion" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>Nombre</label>
                        <input type="text" id="txtNombre" class="form-control" />
                    </div>
                    <div class="form-group">
                        <label>Proceso</label>
                        <asp:DropDownList runat="server" ID="ddlProcesos" CssClass="form-control">
                            <asp:ListItem Text="Seleccione" Value="-1" />
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Introducción</label>
                        <textarea id="txtIntroducion" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Objetivos</label>
                        <textarea id="txtObjetivos" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Alcance</label>
                        <textarea id="txtAlcance" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Marco Legal</label>
                        <input type="text" id="txtMarcoLegal"/>
                    </div>
                   
                    <div class="form-group">
                        <label>Desarrollo</label>
                        <textarea id="txtDesarrollo" class="form-control"></textarea>
                    </div>

                    <div class="form-group">
                        <label>Recursos Financieros</label>
                        <textarea  id="txtRecFin" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Talento Humano</label>
                        <textarea  id="txtTalHum" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Equipos Biomédicos</label>
                        <textarea  id="txtEquipos" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Medicamentos, Dispositivos e Insumos</label>
                        <textarea  id="txtMedicamentos" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Recursos Informáticos</label>
                        <textarea  id="txtRecInfo" class="form-control"></textarea>
                    </div>
                    
                    <div class="form-group">
                        <label>Glosario</label>
                        <textarea id="txtGlosario" class="form-control"></textarea>
                    </div>
                    
                    <div class="form-group">
                        <label>Anexos</label>
                        <textarea id="txtEditor" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Procedimientos o Protocolos Realcionados</label>
                        <div class="d-flex justify-content-between">
                            <input type="text" id="txtProcedimientos" class="form-control d-none"  placeholder="Ingrese el nombre del procedimiento o protocolo"/>
                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlProcs">
                                <asp:ListItem Text="Seleccione"  Value="-1"/>
                            </asp:DropDownList>   
                            <button type="button" class="btn btn-success ml-2" id="btnChangeControl"><i class="fa fa-refresh"></i></button>
                        </div>
                        <div id="lstProcs" class="mt-1"></div>
                    </div>
                    <div class="d-flex justify-content-between">
                        <button class="btn btn-success" id="btnGuardar" type="button">Guardar</button>
                        <button class="btn btn-primary" id="btnViewManual" type="button"><i class="fa fa-file-pdf-o"></i></button>
                        <button class="btn btn-primary" id="btnRevision" type="button">Enviar a revisión</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
   <script src="https://cdn.tiny.cloud/1/no-api-key/tinymce/5/tinymce.min.js" referrerpolicy="origin"></script>
    <script src="js/CrearManualJS.js"></script>
</asp:Content>
