<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ValidacionDibulgacion.aspx.cs" Inherits="Presentacion.GestionDocumental.ValidacionDibulgacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/ValidacionDibulgacionCSS.css" rel="stylesheet" />

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
                    <p class="text-center">Por favor espere mientras se realiza el proceso de divulgacion del documento.</p>
                </div>
            </div>
        </div>
    </div>

    <div class="x_panel">
        <div class="x_titel">
            <div class="clearfix">
                <h6>Formato para la divulgación del documeto</h6>
            </div>
        </div>
        <button style="display: none"></button>
        <div class="x_content">
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">
                    Eje Tematico<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <asp:DropDownList runat="server" CssClass="form-control" ID="slctEjeTematico" />
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">
                    Agregar Subtemas<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6 ">
                    <div class="d-flex justify-content-between">
                        <input type="text" class="form-control" id="txtSubtema" placeholder="Subtema" />
                        <button class="btn btn-primary ml-1" id="btnGuardarSubtema">Agregar</button>
                    </div>
                    <div class="mt-3" id="lstSubtemas">
                    </div>
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">
                    Cargos<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <input type="text" class="form-control" id="txtCargo" placeholder="Cargos a los que aplica" autocomplete="off" spellcheck="false" />
                    <div style="position: relative; width: 100%">
                        <div id="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                    </div>
                    <div class="mt-3" id="lstCargos"></div>
                </div>
            </div>
            <div class="field item form-group">
                <label class="col-form-label col-md-3 col-sm-3  label-align">
                    Tiempo Estimado para la firma<span class="required">*</span></label>
                <div class="col-md-6 col-sm-6">
                    <input type="text" value="" class="form-control" id="txtTempFirma" placeholder="Días  requeridos para la firma de la capacitación"/> 
                </div>
            </div>
            <div class="field item form-group">
                <div class="col-md-6 col-sm-6">
                    <button id="btnGuardar" class="btn btn-success">Guardar</button>
                </div>
            </div>
        </div>
    </div>
    <script src="js/ValidacionDibulgacionJS.js"></script>
</asp:Content>
