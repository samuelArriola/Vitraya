<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CSPaciente.aspx.cs" Inherits="Presentacion.ControlEntSal.CSPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
     <link rel="stylesheet" href="CSS/style.css" /> 

    <div class="row no-gutters ">
        <!-- CONTROL ENTRADA-SALIDA -->
        <div class="col col-5  x_panel">
            <div class="d-flex flex-column ">
                <!-- ----------CONTROL DE SALIDA---------------- -->
                <div class="p-2">
                  
                    <div class=" card-header">
                        <div class="title_center">
                            <h5>CONTROL SALIDA DE PACIENTES</h5>
                        </div>
                    </div>
                
                    <div class="row" style="padding: 7px;">
                      <!--ESCANEAR BOLETA DE SALIDA -->
                      <div class="col ">
                        <div class="page-title">
                            <div class="title_center">
                                <h3></h3>
                            </div>
                        </div>
                        <div class="x_panel" style="float: left;">
                            <div class="x_content">
                                <div class="x_title">
                                    <div class="clearfix">
                                        <h6>ESCANEAR BOLETA DE SALIDA</h6>
                                    </div>
                                </div>
                            </div>
                        
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="* Escanear Boleta:" min="1" max="5" type="number" class="form-control form-control-sm" id="CScodigoR" autofocus />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="* Identificacion:" type="text" class="form-control form-control-sm" id="CSiden"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="Nombres:" type="text" class="form-control form-control-sm" id="CSnombreR"/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="Apellidos" class="form-control form-control-sm" id="CSapell" />
                                    </div>
                                </div>
                            </div>
                        
                        </div>
                      </div>
                      
                      <!--ESCANEAR MANILLA -->
                      <div class="col">
                        <div class="page-title">
                            <div class="title_center">
                                <h3></h3>
                            </div>
                        </div>
                
                        <div class="x_panel">
                            <div class="x_content">
                                <div class="x_title">
                                    <div class="clearfix">
                                        <h6>ESCANEAR MANILLA</h6>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="* Escaner Manilla:" type="text" class="form-control form-control-sm" id="CSmanilla" />
                                    </div>
                                </div>
                            </div>                       
                        </div>
                      
                        <div class="d-flex justify-content-center" style="margin-top: 35px">
                            <button type="button" class="btn btn-outline-success" id="ModalSalBB" >
                                Salida bebés
                            </button>
                         </div>

                         

                      </div>
                    </div>

                </div>
                      
                <!-- ------------CONTROL DE ENTRADA #2------------------ -->
                <div class="p-2">

                    <div class=" card-header">
                        <div class="title_center">
                            <h5>CONTROL VISITA DE PACIENTES</h5>
                        </div>
                    </div>
                
                    <div class="row" style="padding: 7px ;">
                      <!--ESCANEAR BOLETA DE SALIDA -->
                      <div class="col ">
                        <div class="page-title">
                            <div class="title_center">
                                <h3></h3>
                            </div>
                        </div>
                        <div class="x_panel" style="float: left;">
                            <div class="x_content">
                                <div class="x_title">
                                    <div class="clearfix">
                                        <h6>ESCANEAR BOLETA DE SALIDA</h6>
                                    </div>
                                </div>
                            </div>
                        
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="* Escanear Boleta:" min="1" max="5" type="number" class="form-control form-control-sm" id="" autofocus />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="* Identificacion:" type="text" class="form-control form-control-sm" id=""/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="Nombres:" type="text" class="form-control form-control-sm" id=""/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="Apellidos" class="form-control form-control-sm" id="" />
                                    </div>
                                </div>
                            </div>
                        
                        </div>
                   </div>
                      
                      <!--ESCANEAR MANILLA -->
                      <div class="col">
                       <div class="page-title">
                            <div class="title_center">
                                <h3></h3>
                            </div>
                        </div>
                
                        <div class="x_panel">
                            <div class="x_content">
                                <div class="x_title">
                                    <div class="clearfix">
                                        <h6>ESCANEAR MANILLA</h6>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="* Escaner Manilla:" type="text" class="form-control form-control-sm" id="" />
                                    </div>
                                </div>
                            </div>                       
                        </div>
                      </div>
                    </div>

                </div>
            </div> 
        </div>

        <!-- TABLERO DE CAMAS -->
        <div class="col col-7 x_panel" > 
              
           
        </div>
    </div>

     <%--MODAL RESPUESTA ACECPTADA--%> 
        <div class="modal fade " id="exampleModalCenterAceptar" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
          <div class="modal-dialog modal-dialog-centered" role="document" >
            <div class="modal-content bg-success" >
              <div class="modal-header">
                <h3 class="modal-title text-white" id="exampleModalLongTitleAceptar">EXITO</h3>
                <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body text-white">
                <h4 class="modal-body-h4">La salida ha sido exitosa.</h4>
              </div>
              <div class="modal-footer">
                 <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" style=" margin: 0px 5px 5px 4px">Close</button>
              </div>
            </div>
          </div>
        </div>

     <%--MODAL RESPUESTA ERROR--%> 
        <div class="modal fade " id="exampleModalCenterError" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
          <div class="modal-dialog modal-dialog-centered" role="document" >
            <div class="modal-content bg-danger" >
              <div class="modal-header">
                <h3 class="modal-title text-white" id="exampleModalLongTitleError">ADVERTENCIA DE SEGURIDAD</h3>
                <button type="button" class="close text-white" data-bs-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body text-white">
                <h4 class="modal-body-h4">La identificacion de la BOLETA no es igual al de la MANILLA</h4>
              </div>
              <div class="modal-footer">
                  <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" style=" margin: 0px 5px 5px 4px">Close</button>
              </div>
            </div>
          </div>
        </div>
     
     <!-- CONTROL SALIDA DE BEBES -->
        <div class="modal fade" id="exampleModalBB" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="max-width: 95% !important">
            <div class="modal-content">
                <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">CONTROL SALIDA DE BEBES</h5>
                <button type="button" class="close text-white" data-bs-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
                     
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col col-2">
                            <div class="form-group">
                                <label>* Identificacion del Acudiente:</label>
                                <input type="text" class="form-control" id="CSAidenBB" />
                            </div>
                        </div>     
                        <div class="col col-2">
                            <div class="form-group">
                                <label>* Identificacion del Acudiente:</label>
                                <input type="number" class="form-control" id="CSAidenCCBB" disabled/>
                            </div>
                        </div>       
                        <div class="col col-3">
                            <div class="form-group">
                                <label>* Identificacion del Acudiente:</label>
                                <input type="text" class="form-control" id="CSACCNombreBB" disabled/>
                            </div>
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
                                    <th class="ColumnaOpciones" style="width: 110px">Opciones</th>
                                </tr>
                            </thead>
                            <tbody id="CStable">
                                <template id="CStableTemplate">
                                    <tr >
                                        <td scope="row">234</td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td class="">
                                         <button  type="button" class="btnEditar btn btn-outline-success" > DAR SALIDA</button>
                                        </td>
                                    </tr>
                               </template>
                            </tbody>
                        </table>
                </div>
                <div class="modal-footer">
                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" style=" margin: 0px 5px 5px 4px">Close</button>
                </div>
            </div>
            </div>
         </div>

    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
    <script src="js/ControlSalida.js"></script>

</asp:Content>
