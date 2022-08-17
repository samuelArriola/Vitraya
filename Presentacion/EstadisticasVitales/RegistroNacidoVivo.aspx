<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RegistroNacidoVivo.aspx.cs" Inherits="Presentacion.EstadisticasVitales.RegistroNacidoVivo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/RegistroNacidosVivo.css" rel="stylesheet" />
    <link href="css/ConsultarRegistros.css" rel="stylesheet" />
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix text-center">
                <h3 style="font-weight: 700">Registrar Nacido Vivo</h3>
            </div>
        </div>
        <div class="x_content">

            <section id="formRegistroNacidoVivo" class="mt-4">

                <div class="form-group  row pt-xl-4  justify-content-center">
                    <div class="form-group col-xl-2  col-lg-3 col-md-4 col-sm-4 col-12">
                        <label for="idMadreNV" class="mb-0">Documento Madre</label>
                        <input type="text" class="form-control inpNacViv inputCon" id="idMadreNV" placeholder="Documento" />
                    </div>
                    <div class="form-group col-xl-4 col-lg-5 col-md-7 col-sm-8 col-12">
                        <label for="NomMadNV" class="mb-0">Nombre Madre (Completo)</label>
                        <input type="text" class="form-control inpNacViv inputCon" id="NomMadNV" placeholder="Nombre de la Madre" />
                    </div>
                </div>
                <br />
                <div class="form-group  row pt-xl-2   justify-content-center">
                    <div class="form-group col-xl-2 col-lg-3 col-md-4 col-sm-4 col-12">
                        <label for="fechaNacimiento" class="mb-0">Fecha Nacimiento RN</label>
                        <input type="datetime-local" id="fechaNacimiento" class="form-control inpNacViv inputCon" runat="server" />
                    </div>
                    <div class="form-group col-xl-2 col-lg-2 col-md-3 col-sm-4 col-6">
                        <label for="tipoNacimiento" class="mb-0">Tipo Nacimiento </label>
                        <select class="form-control inputCon" style="border-radius: 5px" id="tipoNacimiento">
                            <option>Cesárea</option>
                            <option>Vaginal</option>
                        </select>
                    </div>
                    <div class="form-group col-xl-2 col-lg-3 col-md-4 col-sm-4 col-6 ">
                        <label for="edadGesNacimiento" class="mb-0">Edad Gestacional RN</label>
                        <input type="text" id="edadGesNacimiento" class="form-control inpNacViv inputCon" placeholder="Semanas" />
                    </div>
                </div>

                <div class="form-group  row pt-xl-2 justify-content-center">
                    <div class="col-xl-2 col-lg-3 col-md-4 col-sm-4 col-4">
                        <asp:TextBox runat="server" class="form-control inputCon" ID="IdDocNacViv" ReadOnly />
                        <%--<input type="text" class="form-control" runat="server" id="IdDocNacViv " placeholder="Id del doctor"/>--%>
                    </div>
                    <div class="col-xl-4 col-lg-5 col-md-7 col-sm-8 col-8">
                        <asp:TextBox runat="server" class="form-control inputCon" ID="NomDocNacViv" ReadOnly />
                        <%--<input type="text" class="form-control" id="NomDocNacViv" placeholder="Nombre doctor encargado" readonly/>--%>
                    </div>
                </div>

                <div class="form-group  row pt-xl-2 mt-md-3 justify-content-center">
                    <div class="col-xl-2 col-lg-3 col-md-3 col-sm-4 col-6">
                        <label for="pesoRN" class="mb-0 ">Peso del RN </label>
                        <input type="text" id="pesoRN" class="form-control inpNacViv inputCon" placeholder="Gramos" />
                    </div>
                    <div class="col-xl-2  col-lg-2 col-md-4 col-sm-4 col-6">
                        <label for="tallaRN" class="mb-0">Talla RN (Cm)</label>
                        <input type="number" id="tallaRN" class="form-control inpNacViv inputCon" />
                    </div>
                    <div class="form-group  col-xl-2 col-lg-3 col-md-4 col-sm-4 col-6">
                        <label for="Sexo" class="mb-0">Sexo RN</label>
                        <select class="form-control inputCon" style="border-radius: 5px" id="Sexo">
                            <option>Masculino</option>
                            <option>Femenino</option>
                        </select>
                    </div>
                </div>

                <div class="row justify-content-center">
                    <div class=" row justify-content-center  col-12">
                        <!-- Button trigger modal -->
                        <button type="button" runat="server" class="btn btn-primary p-xl-2" id="btnGuaRegNacVivFil">
                            Guardar registro
                        </button>



                        <!-- Modal -->
                        <div class="modal fade" id="ModalRegNacViv" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="ModalLabel">Registro Guardado</h5>
                                        <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>--%>
                                    </div>
                                    <div class="modal-body" id="modalRegNacVivTex">
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" id="CerrarModal" data-dismiss="modal">Cerrar</button>
                                        <%--<button type="button" class="btn btn-primary" id="btnGuaRegNacViv" >Guardar registro</button>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12  pt-4 justify-content-center">
                            <strong></strong>
                            <label runat="server" class="mb-0" id="lbCodDisp"></label>
                        </div>
                    </div>
                </div>

            </section>


        </div>
    </div>
    <script src="js/RegistroNacidosVivos.js"></script>
</asp:Content>
