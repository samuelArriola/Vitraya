<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CSPacienteBB.aspx.cs" Inherits="Presentacion.ControlEntSal.CSPacienteBB" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
     <link rel="stylesheet" href="CSS/CspacienteBB.css" /> 

      
     <div class="row">

        <div class="col col-12">
            <div class="page-title">
                <div class="title_left">
                    <h3>Control Salida Para Menores de Edad</h3>
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
                                    <div class="col col-2">
                                        <div class="form-group">
                                            <label>* Orden de salida:</label>
                                            <input type="text" class="form-control" id="CSIordenBB" disabled/>
                                        </div>
                                    </div>  
                                    <div class="col col-1" >
                                        <div class="form-group">
                                            <label>* Edad:</label>
                                            <input type="text" class="form-control" id="CSIedadBB" disabled/>
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

                        <table class="table" style="overflow: auto; max-width: 100%;" id="tableBitacora">
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
                                        <button  type="button" class=" btn btn-success" title="EDITAR"><i class="hidenIcom fa fa-pencil text-white"></i></button>
                                        <button  type="button" class=" btn btn-danger" title="ELIMINAR"><i class="hidenIcom fa fa-ban" ></i></button>
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


    <!-- MODAL EDITAR SALIDA -->
     <div class="modal fade" id="ModalEditaSalidaBB" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="max-width: 90% !important">
            <div class="modal-content">
                <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">EDITAR ACUDIENTE</h5>
                <button type="button" class="close text-white" data-bs-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
                     
                </div>
                <div class="modal-body">
                    <div class="row">
                         <div class="col col-1">
                            <div class="form-group">
                                <label>* OID:</label>
                                <input type="number" class="form-control" id="CSAoidEdiBB" disabled/>
                            </div>
                        </div>
                        <div class="col col-2">
                            <div class="form-group">
                                <label>* Identificacion:</label>
                                <input type="text" class="form-control" id="CSAidenIdiBB" />
                            </div>
                        </div>     
                        <div class="col col-2">
                             <div class="form-group">
                                <label>* Tipo de Acudiente:</label>
                                <select id="CSATpResEdiCCBB" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example">
                                    <option value="PADRE">Padre</option>
                                    <option value="MADRE">Madre</option>
                                    <option value="OTRO">Otro</option>
                                </select>
                            </div>
                        </div>       
                        <div class="col col-3">
                            <div class="form-group">
                                <label>* Nombre:</label>
                                <input type="text" class="form-control" id="CSACCNombreEdiBB" />
                            </div>
                        </div>     
                        <div class="col col-2">
                            <div class="form-group">
                                <button style="margin-top: 27px;" type="button" id="btnCSEditoBB" class="btnFiltroSolic btn  btn-outline-success">Editar</button>
                            </div>
                        </div>

                    </div>
                      
                       
                </div>
                <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" style=" margin: 0px 5px 5px 4px">Close</button>
                </div>
            </div>
            </div>
         </div>

     <%--MODAL RESPUESTA ERROR DE VISITA--%> 
        <div class="modal fade " id="MCSErrorBB" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
          <div class="modal-dialog modal-dialog-centered" role="document" >
            <div class="modal-content" >
              <div class="modal-header bg-danger">
                     <input type="number" class="form-control" id="MCSIdenBB" disabled  style="display:none"/>
                <h3 class="modal-title " id=""></h3>
                <button type="button" class="close text-white" data-bs-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                <h4 class="modal-body-h4" id="">¿Estás seguro que desea eliminar este registro?</h4>
              </div>
              <div class="modal-footer">
                  <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" style="">Close</button>
                  <button style="margin-top: 5px;" type="button" id="btnMCSEliminarEBB" class=" btn  btn-danger">Eliminar</button>

              </div>
            </div>
          </div>
        </div>
      


    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
    <script src="js/CSPacienteBB.js"></script>

</asp:Content>
