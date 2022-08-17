<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CSPacienteBB.aspx.cs" Inherits="Presentacion.ControlEntSal.CSPacienteBB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
      
     <div class="row">

        <div class="col col-12">
            <div class="page-title">
                <div class="title_left">
                    <h3>Control Salida De Bebés</h3>
                </div>
            </div>

            <div runat="server" class="x_panel" id="panelAdministrador">

                <div class="x_content">

                    <div class="row">

                        <div class="col col-12">

                            <%--<div class="x_title">
                                <div class="clearfix">
                                    <h6></h6>
                                </div>
                            </div>--%>
                            <div>
                                <div class="row">
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>* Ingreso:</label>
                                            <input type="number" class="form-control" id="CSingresoBB" />
                                        </div>
                                    </div>
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>* Identificación:</label>
                                            <input type="number" class="form-control" id="CSIidenBB" disabled />
                                        </div>
                                    </div>
                                    <div class="col col-3">
                                        <div class="form-group">
                                            <label>* Nombre y Apellido:</label>
                                            <input type="text" class="form-control" id="CSInomsBB" disabled/>
                                        </div>
                                    </div>
                                    <div class="w-100 divider"></div>

                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>* Identificacion del Acudiente:</label>
                                            <input type="number" class="form-control" id="CSAidenBB" />
                                        </div>
                                    </div>                     
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>* Tipo de Acudiente:</label>
                                            <select id="CSAtipoBB" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example">
                                                <option value="" selected disabled> </option>
                                                <option value="PADRE">Padre</option>
                                                <option value="MADRE">Madre</option>
                                                <option value="OTRO">Otro</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col col-3">
                                        <div class="form-group">
                                            <label>* Nombre Completo:</label>
                                            <input type="text" class="form-control" id="CSAnomsBB" />
                                        </div>
                                    </div>
                                    
                                </div>
                                <div class="row">                               
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>Numero de Registro</label>
                                            <input type="number" class="form-control" id="CSBnumeroRBB" />
                                        </div>
                                    </div>
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <button style="margin-top: 27px;" type="button" id="btnCSregistroBB" class="btnFiltroSolic btn  btn-outline-success">Registrar</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="x_title">
                            <div class="clearfix">
                            </div>
                        </div>

                        <table class="table table-responsive" style="overflow: auto; width: 100%;" id="tableBitacora">
                            <thead>
                                <tr>
                                    <th>OID</th>
                                    <th>Nombre Completo de la madre</th>
                                    <th>Identificacion del Acudiente</th>
                                    <th>Tipo de Acudiente</th>
                                    <th>Nombre Completo del Acudiente</th>
                                    <th>Nombre del Bebe</th>
                                    <th>Salida servicio</th>
                                    <th class="ColumnaOpciones" style="width: 170px">Opciones</th>
                                </tr>
                            </thead>
                            <tbody id="CStableBB">
                                <template id="CStableBBTemplate">
                                    <tr >
                                        <td scope="row">234</td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                        <%--<button  type="button" class="btnEditar btn btn-success" data-toggle="tooltip" data-placement="top" title="EDITAR"><i class="fa fa-pencil text-white"></i></button>--%>
                                        <button  type="button" class="btnAnular btn btn-danger" data-toggle="tooltip" data-placement="top" title="ELIMINAR"><i class="fa fa-ban" ></i></button>
                                    </td>
                                    </tr>
                               </template>
                            </tbody>
                        </table>

                     
                    </div>

                </div>
            </div>
        </div>
    </div>
   
      


    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
    <script src="js/CSPacienteBB.js"></script>

</asp:Content>
