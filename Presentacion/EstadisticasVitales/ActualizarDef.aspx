<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ActualizarDef.aspx.cs" Inherits="Presentacion.EstadisticasVitales.ActualizarDef" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/ConsultarRegistros.css" rel="stylesheet" />
    <link href="css/RegistroDefuncion.css" rel="stylesheet" />
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix text-center mr-5">
                <h3 style="font-weight: 700">Actualizar Defunción</h3>
            </div>
        </div>
        <div class="x_content">

            <section id="formRegDef" class="mt-2">
                <div class="form-group  row  pt-2 justify-content-center">
                    <div class="form-group col-xl-2  col-lg-3 col-md-4 col-sm-4 col-12">
                        <label for="idPacDef" class="mb-0 ">Documento Paciente</label>
                        <%--<input type="text" class="form-control inpDef" id="idPacDef" placeholder="Id paciente"/>--%>
                        <asp:TextBox runat="server" class="form-control inpDef inputCon" ID="idPacDef" placeholder="Documento" />
                    </div>
                    <div class="form-group col-xl-4 col-lg-5 col-md-6 col-sm-8 col-12">
                        <label for="nomPacDef" class="mb-0">Nombre Paciente</label>
                        <%--<input type="text" class="form-control inpDef" id="nomPacDef" placeholder="Nombre del paciente"/>--%>
                        <asp:TextBox runat="server" class="form-control inpDef inputCon" ID="nomPacDef" placeholder="Nombre" />
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

                <div class="form-group  row pt-2 justify-content-center">
                    <div class="form-group col-xl-3 col-lg-3 col-md-4 col-sm-4 col-6">
                        <label for="fechaDefuncion" class="mb-0">Fecha Defunción</label>
                        <input type="datetime-local" runat="server" id="fechaDefuncion" class="form-control inpDef inputCon" />
                    </div>
                    <div class="form-group col-xl-2 col-lg-3 col-md-4 col-sm-4 col-6">
                        <label for="tipoNacimiento" class="mb-0">Tipo Defunción</label>
                        <select class="form-control inputCon" runat="server" style="border-radius: 5px" id="tipDef">
                            <option>Fetal</option>
                            <option>No fetal</option>
                        </select>
                    </div>
                    <%--<div class="col-2">
                                            <label for="CodRuafDef" class="mb-0">Código RUAF</label>
                                            <asp:TextBox runat="server"  class="form-control" id="CodRuafDef" readonly/>
                                            <input type="text" id="CodRuafDef" class="form-control" readonly/>
                                        </div>--%>
                </div>

                <asp:GridView runat="server"></asp:GridView>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=DESKTOP-S6LMVO3\SQLEXPRESS;Initial Catalog=Vitraya1;Integrated Security=True" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>

                <div class="form-group  row pt-0 justify-content-center">
                    <div class="form-group col-xl-4 col-lg-5 col-md-4 col-sm-5 col-12">
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
                        <button type="button" class="btn btn-primary btnGuardarRegDef" id="btnGuaRegDefFil">
                            Guardar registro
                        </button>

                        <!-- Modal -->
                        <div class="modal fade" id="ModalRegDef" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog modal-dialog-centered">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="ModalLabel">Registro a Actualizar</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
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
                </div>
            </section>
        </div>
    </div>
    <script src="js/ActualizarDef.js"></script>
</asp:Content>
