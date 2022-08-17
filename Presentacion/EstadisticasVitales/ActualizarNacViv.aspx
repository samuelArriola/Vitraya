<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ActualizarNacViv.aspx.cs" Inherits="Presentacion.EstadisticasVitales.ActualizarNacViv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/RegistroNacidosVivo.css" rel="stylesheet" />
    <link href="css/ConsultarRegistros.css" rel="stylesheet" />
     <div class="x_panel" >
                        <div class="x_title">
                            <div class="clearfix text-center">
                                <h3 style="font-weight:700" > Actualizar Registrar Nacido Vivo</h3>
                            </div>
                        </div>
                        <div class="x_content">

                            <section id="formRegistroNacidoVivo" class="mt-1">   

                                    <div class="form-group  row pt-xl-4  justify-content-center">
                                        <div class="form-group col-xl-2  col-lg-3 col-md-4 col-sm-4 col-12">
                                            <%--<input type="text" class="form-control inpNacViv" id="idMadreNV" placeholder="Id de la madre"/>--%>
                                            <label for="idMadreNV" class="mb-0">Documento Madre</label>
                                            <asp:TextBox runat="server" class="form-control inpNacViv inputCon" id="idMadreNV" placeholder="Documento" />
                                        </div>
                                        <div class="form-group col-xl-4 col-lg-5 col-md-7 col-sm-8 col-12">
                                            <label for="NomMadNV" class="mb-0">Nombre Madre</label>
                                            <%--<input type="text" class="form-control inpNacViv" id="NomMadNV" placeholder="Nombre de la madre" />--%>
                                            <asp:TextBox runat="server" class="form-control inpNacViv inputCon" id="NomMadNV" placeholder="Nombre de la madre" />
                                        </div>
                                    </div>

                                    <div class="form-group  row pt-xl-2   justify-content-center">
                                        <div class="form-group col-xl-2 col-lg-3 col-md-4 col-sm-4 col-12">
                                            <label for="fechaNacimiento" class="mb-0">Fecha Nacimiento RN</label>
                                            <input type="datetime-local" runat="server" id="fechaNacimiento" class="form-control inpNacViv inputCon" />
                                        </div>
                                        <div class="form-group col-xl-2 col-lg-2 col-md-3 col-sm-4 col-6">
                                            <label for="tipoNacimiento" class="mb-0">Tipo Nacimiento</label>
                                            <select class="form-control inputCon" runat="server" style="border-radius: 5px" id="tipoNacimiento">
                                                <option>Cesárea</option>
                                                <option>Vaginal</option>                                                
                                            </select>                                            
                                        </div>
                                        <div class="form-group col-xl-2 col-lg-3 col-md-4 col-sm-4 col-6">
                                            <label for="edadGesNacimiento" class="mb-0">Edad Gestacional RN</label>
                                            <%--<input type="text" id="edadGesNacimiento" class="form-control inpNacViv" />--%>
                                            <asp:TextBox runat="server" class="form-control inpNacViv inputCon" id="edadGesNacimiento" placeholder="Semanas"/>
                                        </div>
                                    </div> 
                                    
                                    <div class="form-group  row pt-xl-2 justify-content-center" >
                                        <div class="col-xl-2 col-lg-3 col-md-4 col-sm-4 col-4" >
                                            <asp:TextBox runat="server" class="form-control inputCon" id="IdDocNacViv" readonly/>
                                            <%--<input type="text" class="form-control" runat="server" id="IdDocNacViv " placeholder="Id del doctor"/>--%>
                                        </div>
                                        <div class="col-xl-4 col-lg-5 col-md-7 col-sm-8 col-8">
                                            <asp:TextBox runat="server" class="form-control inputCon" id="NomDocNacViv" readonly/>
                                            <%--<input type="text" class="form-control" id="NomDocNacViv" placeholder="Nombre doctor encargado" readonly/>--%>
                                        </div>
                                    </div>
                                    
                                    <div class="form-group  row pt-xl-2 mt-md-3 justify-content-center" >
                                        <div class="col-xl-2 col-lg-3 col-md-3 col-sm-4 col-6" >
                                            <label for="pesoRN" class="mb-0 ">Peso RN</label>
                                            <%--<input type="text" id="pesoRN" class="form-control inpNacViv" />--%>
                                            <asp:TextBox runat="server" class="form-control inpNacViv inputCon" id="pesoRN" placeholder="Gramos"/>
                                        </div>
                                        <div class="col-xl-2  col-lg-2 col-md-4 col-sm-4 col-6">
                                            <label for="tallaRN" class="mb-0">Talla RN (Cm)</label>
                                            <input type="number" id="tallaRN" runat="server" class="form-control inpNacViv inputCon" />
                                                      
                                            <%--<asp:TextBox runat="server" class="form-control inpNacViv" id="tallaRN" />--%>
                                        </div>
                                        <div class="form-group  col-xl-2 col-lg-3 col-md-4 col-sm-4 col-6">
                                            <label for="Sexo" class="mb-0">Sexo RN</label>
                                            <select class="form-control inputCon" runat="server" style="border-radius: 5px" id="Sexo">
                                                <option>Masculino</option>
                                                <option>Femenino</option>                                                
                                            </select>                                            
                                        </div>
                                        <%--<div class="col-2">
                                            <label for="CodRuafNacViv" class="mb-0">Còdigo RUAF</label>
                                            <asp:TextBox runat="server" class="form-control" id="CodRuafNacViv" style="display:none;"  readonly/>
                                            <input type="text" id="CodigoRUAF" class="form-control " readonly />    
                                        </div>--%>
                                    </div>

                                    <div class="row justify-content-center">
                                        <div class=" row justify-content-center col-12">
                                            <!-- Button trigger modal -->
                                            <button type="button" class="btn btn-primary" id="btnGuaRegNacVivFil" >
                                                Guardar registro
                                            </button>

                                            <!-- Modal -->
                                            <div class="modal fade" id="ModalRegNacViv" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                <div class="modal-dialog modal-dialog-centered">
                                                    <div class="modal-content">
                                                        <div class="modal-header">
                                                            <h5 class="modal-title" id="ModalLabel">Registro Actualizado</h5>
                                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body" id ="modalRegNacVivTex">
                                                                
                                                        </div>
                                                        <div class="modal-footer">
                                                            <button type="button" class="btn btn-secondary"  id="closeModal" data-dismiss="modal">Cerrar</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                            </section>
                        </div>
                    </div>
    <script src="js/ActualizarNacViv.js"></script>
</asp:Content>
