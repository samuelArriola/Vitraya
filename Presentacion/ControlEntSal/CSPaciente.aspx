<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CSPaciente.aspx.cs" Inherits="Presentacion.ControlEntSal.CSPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    
    <link rel="stylesheet" href="CSS/style.css" /> 

    <div class="row no-gutters ">
        <!-- CONTROL ENTRADA-SALIDA -->
        <div class="col col-4  x_panel">
            <div class="d-flex flex-column ">
                <!-- ----------CONTROL DE SALIDA---------------- -->
                <div class="p-2 p-2-divide">
                  
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
                             <div class="row"> <!-- style="display: none" --> 
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="Edad" class="form-control form-control-sm" id="CSedad" />
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
                                Salida Menores de Edad
                            </button>
                        </div>

                         

                      </div>
                    </div>

                </div>
                      
                <!-- ------------CONTROL DE SALIDA DE PACIENTE ------------------ -->
                <div class="p-2 p-2-divide">

                    <div class=" card-header">
                        <div class="title_center">
                            <h5>CONTROL VISITA DE PACIENTES</h5>
                        </div>
                    </div>
                
                    <div class="row" style="padding: 7px ;">
                      <!--DATOS DEL PACIENTE -->
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
                                        <h6>DATOS DEL PACIENTE</h6>
                                    </div>
                                </div>
                            </div>
                        
                             <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="* Codigo de Cama" class="form-control form-control-sm" id="CSVPcodCama" readonly/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="* Ingreso:" min="1" max="5" type="number" class="form-control form-control-sm" id="CSVPingreso"  readonly/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="* Identificacion:" type="text" class="form-control form-control-sm" id="CSVPiden" readonly/>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <input placeholder="Nombres y Apellidos:" type="text" class="form-control form-control-sm" id="CSVPnom" readonly/>
                                    </div>
                                </div>
                            </div>
                           
                        
                        </div>
                   </div>
                      
                      <!--DATOS DEL VISITANTE -->
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
                                        <h6>DATOS DEL VISITANTE</h6>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="row">
                                  <div class="col col-12">
                                        <div class="form-group">
                                             <input type="text" class="form-control form-control-sm" id="CSVViden"   placeholder="* Escanear Cedula:" />
                                        </div>
                                    </div>
                                    <div class="col col-12">
                                        <div class="form-group">
                                            <input type="text" class="form-control form-control-sm" id="CSVVnom"   placeholder="* Nombres y Apellidos:" />
                                        </div>
                                    </div>

                            </div>
                        </div>
                         <div class="d-flex justify-content-center" style="margin-top: 15px">
                            <button type="button" class="btn btn-primary" id="CSVbtnLimpiar" >
                                Limpiar
                            </button>
                        </div>


                      </div>
                    </div>

                </div>
            </div> 
        </div>

           <div class="col col-8 x_panel" style="height: 100%"> 
                   

                 <div class="x_content">

                    <div class="row d-flex justify-content-center">

                        <div class="col col-12">

                            <div class="x_title">
                                <div class="clearfix">
                                    <h6 style = "float: left;" >Filtro de busquedas</h6>
                                </div>
                            </div>
                                 <div>
                                    <div class="row">                                    
                                        <div class="col col-2">
                                            <div class="form-group">
                                                <label>Buscador</label>
                                                <input type="text" class="form-control" id="CSVFbuscar"/>
                                            </div>
                                        </div> 
                                        <div class="col col-2">
                                            <div class="form-group">
                                                <label>Grupo:</label>
                                                <select id="CSVFGrupo" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example">
                                                    <option value="" selected>Seleccione</option>
                                                    <option value="01">URGENCIAS</option>
                                                    <option value="03">UCI ADULTO</option>
                                                    <option value="04">UCI NEONATAL</option>
                                                    <option value="05">HOSPITALIZACION</option>
                                                   
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col col-2">
                                            <div class="form-group">
                                                <label>Subgrupo:</label>
                                                <select id="CSVFSubGrupo" class="form-control form-select form-select-lg mb-3" aria-label=".form-select-sm example">
                                                    <option value="" selected>Seleccione</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col col-1">
                                            <div class="form-group">
                                                <button style="margin-top: 27px;" id="CSVFlimpiar" type="button" class=" btn btn-primary">LIMPIAR</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                                <table class="table" style="overflow:auto; width:98%; min-height:440px" id="CSVtableCenso">
                                    <thead>
                                        <tr>
                                           <th>Grupo</th>
                                            <th>Subgrupo</th>
                                            <th>Documento</th>
                                            <th >Nombre del Paciente</th>
                                            <th>Visita</th>
                                            <th>Ingreso</th>
                                            <th>Cod. Cama</th>
                                        </tr>  
                                    </thead>
                                    <tbody id="CSVtableCensoBody">
                                        <div class="spinner-border text-success" role="status" style="position: absolute; top: 320px; width: 4rem; height: 4rem">
                                          <span class="visually-hidden"></span>
                                        </div>
                                          <template id="CSVtableCensoBodyTemplate">
                                                <tr >
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>       
                                                    <td></td>       
                                                    <td></td>       
                                                </tr>
                                           </template>
                                    </tbody>   
                                </table>

                                <div>
                                   
                                </div>
    
                        </div>

                    </div>
                </div>

             </div>




           

           </div>
         
           <%-- <div id="carouselExampleIndicators" class="carousel slide" data-bs-ride="carousel" >
             
              <div class="carousel-inner">
                <div class="carousel-item active">
                  <img src="https://cdn.pixabay.com/photo/2022/07/30/14/07/butterfly-7353884_960_720.jpg" class="d-block w-100" alt=""/>
                </div>
                <div class="carousel-item">
                  <img src="https://cdn.pixabay.com/photo/2022/07/31/17/07/bird-7356346__340.jpg" class="d-block w-100" alt=""/>
                </div>
                <div class="carousel-item">
                  <img src="https://cdn.pixabay.com/photo/2022/07/22/18/50/helenium-7338764__340.jpg" class="d-block w-100" alt=""/>
                </div>
              </div>
              <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
                <h1 class="text-white" aria-hidden="true">&#10094</h1>
               
              </button>
              <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
                <h1 class="text-black" aria-hidden="true">&#10095</h1> 
              </button>
            </div>
               --%>

   
           
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
                <h4 class="modal-body-h4" id="MCSExitoH4">La salida ha sido exitosa.</h4>
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
                <h4 class="modal-body-h4" id="MCSErrorH4">La identificacion de la BOLETA no es igual al de la MANILLA</h4>
              </div>
              <div class="modal-footer">
                  <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" style=" margin: 0px 5px 5px 4px">Close</button>
              </div>
            </div>
          </div>
        </div>
     
     <!-- CONTROL SALIDA DE MENORES DE EDAD -->
        <div class="modal fade" id="exampleModalBB" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="max-width: 95% !important">
            <div class="modal-content">
                <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">CONTROL SALIDA PARA MENORES DE EDAD </h5>
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
                                <label> Identificacion del Acudiente:</label>
                                <input type="number" class="form-control" id="CSAidenCCBB" disabled/>
                            </div>
                        </div>       
                        <div class="col col-3">
                            <div class="form-group">
                                <label> Nombre del Acudiente:</label>
                                <input type="text" class="form-control" id="CSACCNombreBB" disabled/>
                            </div>
                        </div>                                                     
                    </div>
                      
                       <table class="table" style="overflow: auto; width: 100%;" id="tableBitacora">
                            <thead>
                                <tr>
                                    <th>OID</th>
                                    <th>Nombre Completo de la madre</th>
                                    <th>Identificacion del Acudiente</th>
                                    <th>Tipo de Acudiente</th>
                                    <th>Nombre Completo del Acudiente</th>
                                    <th>Nombre Del Menor de Edad</th>
                                    <th>Salida servicio</th>
                                    <th class="ColumnaOpciones" style="width: 110px">Opciones</th>
                                </tr>
                            </thead>
                            <tbody id="CStable">

                                <template id="CStableTemplate">
                                    <tr>
                                        <td scope="row">234</td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td >
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

   
     <%--MODAL RESPUESTA ACECPTADA DE VISITA--%> 
        <div class="modal fade " id="MCVExito" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
          <div class="modal-dialog modal-dialog-centered" role="document" >
            <div class="modal-content bg-success" >
              <div class="modal-header">
                <h3 class="modal-title text-white" id="exampleModalLongTitleAceptar2">EXITO</h3>
                <button type="button" class="close" data-bs-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body text-white">
                <h4 class="modal-body-h4" id="MCSExitoH42">La salida ha sido exitosa.</h4>
              </div>
              <div class="modal-footer">
                 <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" style=" margin: 0px 5px 5px 4px">Close</button>
              </div>
            </div>
          </div>
        </div>

     <%--MODAL RESPUESTA ERROR DE VISITA--%> 
        <div class="modal fade " id="MCVError" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
          <div class="modal-dialog modal-dialog-centered" role="document" >
            <div class="modal-content bg-danger" >
              <div class="modal-header">
                <h3 class="modal-title text-white" id="exampleModalLongTitleError2">ADVERTENCIA DE SEGURIDAD</h3>
                <button type="button" class="close text-white" data-bs-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body text-white">
                <h4 class="modal-body-h4" id="MCSErrorH42">La identificacion de la BOLETA no es igual al de la MANILLA</h4>
              </div>
              <div class="modal-footer">
                  <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" style=" margin: 0px 5px 5px 4px">Close</button>
              </div>
            </div>
          </div>
        </div>

     <!-- MODAL CAMBIO DE ACUDIENTE Y LISTA DE VISITAS  -->
        <div class="modal fade" id="MCVCambioVisita" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="max-width: 95% !important">
            <div class="modal-content">
                <div class="modal-header">
                <h5 class="modal-title" id="">CAMBIO DE VISITA </h5>
                <button type="button" class="close text-white" data-bs-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
                     
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col col-2">
                            <div class="form-group">
                                <label>*Ingreso:</label>
                                <input type="text" class="form-control" id="MCVingreso" />
                            </div>
                        </div>     
                        <div class="col col-2">
                            <div class="form-group">
                                <label> Identificacion del Acudiente:</label>
                                <input type="number" class="form-control" id="MCViden" disabled/>
                            </div>
                        </div>       
                        <div class="col col-3">
                            <div class="form-group">
                                <label> Nombre del Acudiente:</label>
                                <input type="text" class="form-control" id="MCVNombre" disabled/>
                            </div>
                        </div>                                                     
                    </div>
                      
                       <table class="table" style="overflow: auto; width: 100%;" id="MCVTablaVisita">
                            <thead>
                                <tr>
                                    <th>OID</th>
                                    <th>Nombre Completo de la madre</th>
                                    <th>Identificacion del Acudiente</th>
                                    <th>Tipo de Acudiente</th>
                                    <th>Nombre Completo del Acudiente</th>
                                    <th>Nombre Del Menor de Edad</th>
                                    <th>Salida servicio</th>
                                    <th class="ColumnaOpciones" style="width: 110px">Opciones</th>
                                </tr>
                            </thead>
                            <tbody id="MCVTablaVisitaBody">

                                <template id="MCVTablaVisitaTemplate">
                                    <tr>
                                        <td scope="row">234</td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td >
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
