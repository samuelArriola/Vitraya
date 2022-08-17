<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EliminarCodigos.aspx.cs" Inherits="Presentacion.EstadisticasVitales.EliminarCodigos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/CargarCodigosRUAF.css" rel="stylesheet" />
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix text-center">
                <h3 style="font-weight: 700">Eliminar Códigos</h3>
            </div>
        </div>
        <div class="x_content">
            <div class="text-center">
                <section>
                    <div class="sectionFile">
                        <asp:Label Text="Arrastrar Archivo Aquí" runat="server" Style="display: block" CssClass="mt-4 mb-2" />
                        <%-- <label for="fuArchivoACodigoRUAF" id="lbfuArchivoACodigoRUAF">Subir</label>--%>
                        <div class="seccionFileRUAF">
                            <svg width="10em" height="10em" viewBox="0 0 16 16" id="imgUpLoad" class="bi bi-cloud-arrow-up-fill pt-3" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                <path fill-rule="evenodd" d="M8 2a5.53 5.53 0 0 0-3.594 1.342c-.766.66-1.321 1.52-1.464 2.383C1.266 6.095 0 7.555 0 9.318 0 11.366 1.708 13 3.781 13h8.906C14.502 13 16 11.57 16 9.773c0-1.636-1.242-2.969-2.834-3.194C12.923 3.999 10.69 2 8 2zm2.354 5.146l-2-2a.5.5 0 0 0-.708 0l-2 2a.5.5 0 1 0 .708.708L7.5 6.707V10.5a.5.5 0 0 0 1 0V6.707l1.146 1.147a.5.5 0 0 0 .708-.708z" />
                            </svg>
                            <asp:Label Text="" runat="server" ID="txtNomArchivo" Style="display: block" CssClass="" />
                        </div>
                        <input type="file" id="fuArchivoACodigoRUAF" accept="text/plain, .txt, .TXT" />
                    </div>
                </section>
                <div class="row ">
                    <div class="col-6 pt-3" id="opcCodRuaf">
                        <h4 class="TitleOpcRUAF">Tipo de códigos</h4>
                        <div>
                            <input type="radio" name="btnTipoCodigosRUAF" value="" id="btnNacidosvivos" />
                            <label for="btnNacidosvivos" class="">Nacidos Vivos</label>
                        </div>

                        <div>
                            <input type="radio" name="btnTipoCodigosRUAF" value="" id="btnDefuncion" />
                            <label for="btnDefuncion" class="">Defunción</label>
                        </div>
                    </div>
                    <div class="col-6 pt-3">
                        <!-- Button trigger modal -->
                        <button type="button" class="btn btn-primary btnCargarCodigosRUAF" data-toggle="modal" id="BtnEliminarCodigos">
                            Eliminar códigos
                        </button>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title " id="exampleModalLabel">Verificar códigos a eliminar</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div>
                                <h3>Contenido del archivo:</h3>
                                <div id="contenido-archivo">Error, no a cargado archivo.</div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                            <button type="button" class="btn btn-primary" id="btnEliminarCodigosModal" data-dismiss="modal">Eliminar</button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="text-right mt-2">
            </div>
        </div>
    </div>
    <script src="js/EliminarCodigos.js"></script>
</asp:Content>
