<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ConsultarRegistros.aspx.cs" Inherits="Presentacion.EstadisticasVitales.ConsultarRegistros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/ConsultarRegistros.css" rel="stylesheet" />
    <script src="js/xlsx.full.min.js"></script>
    <script src="js/FileSaver.min.js"></script>
    <script src="js/tableexport.min.js"></script>
    <div class="page-title">
        <div class="title_left">
            <h6>Consultar Registos</h6>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
            </div>
        </div>
        <div class="clearfix"></div>
        <div class="x_content">
            <div class="x_title">
                <div class="clearfix">
                </div>
            </div>
            <section id="formConReg" class="mt-4 mb-4">
                <div class="form-group row pt-2 justify-content-center">
                    <div class="col-xl-2 col-lg-3 col-md-2 col-6 mt-1">
                        <div class="">
                            <input type="radio" name="btnTipConReg" value="" id="TipNacViv" class="inputCon" />
                            <label for="TipNacViv" class="">Nacidos Vivos</label>
                        </div>
                        <div class="">
                            <input type="radio" name="btnTipConReg" value="" id="TipDef" class="inputCon" />
                            <label for="TipDef" class="">Defunción</label>
                        </div>
                    </div>
                    <div class="form-group col-xl-4 col-lg-5 col-md-8 col-12" id="divOpcBus">
                        <label for="tipConRegPar" class="mb-0">Parametro de Busqueda</label>
                        <select class="form-control inputCon " id="tipConRegPar">
                            <%--<option>Id Madre</option>
                                    <option>Nombre Madre</option>
                                    <option>Código ruaf</option>
                                    <option>Id Doctor</option>
                                    <option>Id paciente</option>
                                    <option>Fecha</option> --%>
                        </select>
                    </div>
                </div>
                <div class="form-group row pt-3  justify-content-center" id="columConRegFecha">
                    <%--<div class="col-xl-4 col-lg-5 col-md-6 col-12">
                                      <label for="fechaConReg1" class="mb-0">Fecha Mínima</label>
                                      <input type="datetime-local" id="fechaConReg1" class="form-control inputCon" />
                                  </div>
                                  <div class="col-xl-4 col-lg-5 col-md-6 col-12">
                                      <label for="fechaConReg2" class="mb-0">Fecha Máxima</label>
                                      <input type="datetime-local" id="fechaConReg2" class="form-control inputCon" visible="false" />
                                  </div>--%>
                </div>
                <div class="form-group row pt-1 justify-content-center">
                    <div class="col-xl-4 col-lg-7 col-md-8 col-12 mb-2">
                        <input type="text" class="form-control inputCon" id="ParBusReg" placeholder="Digite Parametros" />
                    </div>
                    <button type="button" class="col-xl-1 col-lg-2 col-md-2 col-3 btn btn-primary" id="btnBusReg">Buscar</button>
                </div>
            </section>


            <section id="TablaReg">
            </section>



            <%--espacio para el boton generar excel apartir de la consulta.--%>
            <div id="btnExcel">
            </div>

            <!-- Modal -->
            <div class="modal fade" id="ModalVerReg" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered modal-dialog-scrollable">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="ModalLabel">Registro</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body" id="ModalVerRegTex">
                            <!-- cuerpo modal. -->


                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js/ConsultarRegistros.js"></script>
</asp:Content>
