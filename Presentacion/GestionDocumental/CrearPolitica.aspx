 <%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CrearPolitica.aspx.cs" Inherits="Presentacion.GestionDocumental.CrearPolitica" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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
                <h6>Creación de la Politca:</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="row justify-content-center">
                <div class="col col-10">
                    <div class="form-group">
                        <label>Nombre</label>
                        <input id="txtVersion" class="form-control" type="number"/>
                    </div>
                    <div class="form-group">
                        <label>Nombre</label>
                        <input id="txtNombre" class="form-control" type="text"/>
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
                        <label>Objetivos Especificos</label>
                        <textarea id="txtObjetivosEsp" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Alcance</label>
                        <textarea id="txtAlcance" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Marco Legal</label>
                        <textarea id="txtMarcoLegal" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <label>Desarrollo</label>
                        <textarea id="txtDesarrollo" class="form-control"></textarea>
                    </div>
                    <div class="x_title">
                        <div class="clearfix">
                            <h6>Glosario de terminos</h6>
                        </div>
                    </div>
                    <div class="form-group">
                        <label>Terminio</label>
                        <input type="text" value="" class="form-control" id="txtTermino" />
                    </div>
                    <div class="form-group">
                        <label>Definición</label>
                        <textarea id="txtGlosario" class="form-control"></textarea>
                    </div>
                    <div class="form-group">
                        <button class="btn btn-success" id="btnGuardarDef">Guardar</button>
                    </div>
                    <ul id="lstDefiniciones">

                    </ul>
                    <div class="x_title">
                        <div class="clearfix">
                         
                        </div>
                    </div>
                    <div class="form-group">
                        <label>Anexos</label>
                        <textarea id="txtEditor" class="form-control"></textarea>
                    </div>
                    <div class="d-flex justify-content-between">
                        <button class="btn btn-success" id="btnGuardar">Guardar</button>
                        <button class="btn btn-primary" id="btnRevision">Enviar a revisión</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../proceedings/js/tinymce.min.js"></script>
    <script src="js/CrearPoliticaJS.js"></script>
</asp:Content>
