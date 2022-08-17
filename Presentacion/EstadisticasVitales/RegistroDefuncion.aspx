<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RegistroDefuncion.aspx.cs" Inherits="Presentacion.EstadisticasVitales.RegistroDefuncion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/RegistroDefuncion.css" rel="stylesheet" />
    <link href="css/ConsultarRegistros.css" rel="stylesheet" />
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix mr-5">
                <h6 style="font-weight: 700">Registrar Defunción</h6>
            </div>
        </div>
        <div class="x_content">
            <section id="formRegDef" class="mt-2 ">
                <div class="form-group  row  pt-1 justify-content-center">
                    <div class="form-group col-xl-2  col-lg-3 col-md-4 col-sm-4 col-12">
                        <%--<input type="text" class="form-control inpDef" id="idPacDef" placeholder="Id paciente"/>--%>
                        <label for="idPacDef" class="mb-0">Documento Paciente</label>
                        <asp:TextBox runat="server" class="form-control inpDef inputCon" ID="idPacDef" placeholder="Documento" />
                    </div>
                    <div class="form-group col-xl-4 col-lg-5 col-md-6 col-sm-8 col-12">
                        <label for="nomPacDef" class="mb-0">Nombre Paciente (Completo)</label>
                        <input type="text" class="form-control inpDef inputCon" id="nomPacDef" placeholder="Nombre" />
                    </div>
                </div>

                <div class="form-group  row pt-2 justify-content-center">
                    <div class="form-group col-xl-2 col-lg-3 col-md-4 col-sm-4 col-4">
                        <asp:TextBox runat="server" class="form-control inputCon" ID="IdDocDef" ReadOnly />
                        <%--<input type="text" class="form-control" id="" placeholder="Id del doctor"/>--%>
                    </div>
                    <div class="form-group col-xl-4 col-lg-5 col-md-6 col-sm-8 col-8">
                        <asp:TextBox runat="server" class="form-control inputCon" ID="NomDocDef" ReadOnly />
                        <%--<input type="text" class="form-control" id="NomDocDef" placeholder="Nombre doctor encargado" readonly/>--%>
                    </div>
                </div>

                <div class="form-group row pt-2 justify-content-center">
                    <div class="form-group col-xl-2 col-lg-3 col-md-4 col-sm-4 col-12">
                        <label for="fechaDefuncion" class="mb-0">Fecha Defunción</label>
                        <input type="datetime-local" runat="server" id="fechaDefuncion" class="form-control inpDef inputCon" />
                    </div>
                    <div class="form-group col-xl-4 col-lg-5 col-md-6 col-sm-8 col-12">
                        <label for="tipoNacimiento" class="mb-0">Tipo Defunción</label>
                        <select class="form-control inputCon" runat="server" style="border-radius: 5px" id="tipDef">
                            <option>Fetal</option>
                            <option>No fetal</option>
                        </select>
                    </div>
                </div>

                <div class="form-group d-flex row pt-2 justify-content-center">
                    <div class="form-group col-xl-2 col-lg-3 col-md-4 col-sm-4 col-4">
                         <label for="chkEstadoPaciente" class="mb-0">Fallecimiento 48 después del ingreso</label>
                         <div><input type="radio" id="chkEstadoPacienteSi" name="chkEstadoPaciente"/><label class="ml-2" for="chkEstadoPacienteSi">Si</label></div>
                         <div><input type="radio" id="chkEstadoPacienteNo" name="chkEstadoPaciente"/><label class="ml-2" for="chkEstadoPacienteNo">No</label></div>
                     </div>

                    <div class="form-group col-xl-4 col-lg-5 col-md-6 col-sm-8 col-8">
                        <label for="ServDef" class="mb-0">Servicio Defunción</label>
                        <select class="form-control inputCon" runat="server" style="border-radius: 5px" id="ServDef">
                            <option>CIRUGIA</option>
                            <option>EXTRAINSTITUCIONAL</option>
                            <option>HOSPITALIZACION GENERAL</option>
                            <option>HOSPITALIZACION COVID-19</option>
                            <option>UNIDAD DE CUIDADOS INTENSIVOS ADULTOS</option>
                            <option>UNIDAD DE CUIDADOS INTENSIVOS ADULTOS COVID-19</option>
                            <option>UNIDAD DE CUIDADOS INTENSIVOS NEONATAL</option>
                            <option>URGENCIAS</option>
                        </select>
                    </div>
                </div>

                <div class="row justify-content-center">
                    <div class="row justify-content-center col-12 ">
                        <!-- Button trigger modal -->
                        <button type="button" runat="server" class="btn btn-primary btnGuardarRegDef" id="btnGuaRegDefFil">
                            Guardar registro
                        </button>

                        <!-- Modal -->
                        <div class="modal fade" id="ModalRegDef" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="ModalLabel">Registro Guardado</h5>
                                    </div>
                                    <div class="modal-body" id="modalRegDefTex">
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" id="cerrarModal" data-dismiss="modal">Cerrar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12 justify-content-between pt-2">
                            <strong></strong>
                            <label runat="server" class="mb-0" id="lbCodDisp"></label>
                        </div>
                    </div>
                </div>
            </section>

        </div>
    </div>
    <script src="js/RegistroDefuncion.js"></script>
</asp:Content>
