<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Matricula.aspx.cs" Inherits="Presentacion.trainings.Matricula" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/MatriculaCSS.css" rel="stylesheet" />
    <div class="modal" tabindex="-1" role="dialog" id="modal-matricula">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Matricular usuarios</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row justify-content-center m-3 d-flex">
                        <div class="col col-3">
                            <label>Matricula por usuario</label>
                        </div>
                        <div class="col col-7">
                            <div style="width: 100%; position: relative">
                                <div class="d-flex justify-content-between form-control search-p">
                                    <input type="text" id="txtUsuario" placeholder="Seleccione" autocomplete="off" spellcheck="false" />
                                    <i class="fa fa-angle-down"></i>
                                </div>
                                <div class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                            </div>
                            <div id="lstUsuarios"></div>
                        </div>
                    </div>
                    <div class="row justify-content-center m-3 d-flex">
                        <div class="col col-3">
                            <label>Matricula por cargo</label>
                        </div>
                        <div class="col col-7">
                            <div style="width: 100%; position: relative">
                                <div class="d-flex justify-content-between form-control search-p">
                                    <input type="text" id="txtCargo" placeholder="Seleccione" autocomplete="off" spellcheck="false" />
                                    <i class="fa fa-angle-down"></i>
                                </div>
                                <div class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                            </div>
                            <div id="lstCargos"></div>
                        </div>
                    </div>
                    <div class="row justify-content-center m-3 d-flex">
                        <div class="col col-3">
                            <label>Matricula por unidad funcional</label>
                        </div>
                        <div class="col col-7">
                            <div style="width: 100%; position: relative">
                                <div class="d-flex justify-content-between form-control search-p">
                                    <input type="text" id="txtUnidadFuncional" placeholder="Seleccione" autocomplete="off" spellcheck="false" />
                                    <i class="fa fa-angle-down"></i>
                                </div>
                                <div class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                            </div>
                            <div id="lstUnididades"></div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" id="btnMatricular">Matricular</button>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_content">
            <h1 class="text-center" id="txtTitulo"></h1>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Participantes</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="text-right form-group">
                <button class="btn btn-primary" id="btn-matricula">Matricular usurios</button>
            </div>
            <div id="tbParticipantes">
            </div>
            <div class="form-group">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btnEnviarNotificacion" OnClick="btnEnviarNotificacion_Click" Text="Enviar notificaciones" runat="server" CssClass="btn btn-primary"/>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </div>
        </div>
    </div>
    <script src="js/Matricula.js"></script>
</asp:Content>
