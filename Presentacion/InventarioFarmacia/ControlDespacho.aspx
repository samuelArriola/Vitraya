<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ControlDespacho.aspx.cs" Inherits="Presentacion.InventarioFarmacia.ControlDespacho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
      <link rel="stylesheet" href="css/Style.css" /> 
    InventarioFarmacia
    <div class="row">

        <div class="col col-12">
            <div class="page-title">
                <div class="title_left">
                    <h3>Control De Despacho</h3>
                </div>
            </div>

            <div runat="server" class="x_panel" id="panelAdministrador">

                <div class="x_content">

                    <div class="row">
                        <div class="col col-8">

                            <div class="row">
                                
                                    <div class="col col-3">
                                        <div class="form-group">
                                            <label>* Suministro:</label>
                                            <input type="number" class="form-control" id="" />
                                        </div>
                                    </div>
                                   
                                </div>
                            
                        </div>

                        <%--FIRMA--%>
                        <div class="col col-4 d-flex align-items-center justify-content-center">        
                            
                                <div class="form-group">
                                    <button data-bs-toggle="modal" type="button" data-bs-target="#MCDFirma"  id="ModalSalBB" class=" btn  btn-outline-success">Firmar</button>
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


    
     <!-- CONTROL SALIDA DE MENORES DE EDAD  BTN-->
        <div class="modal fade" id="MCDFirma" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="max-width: 50% !important">
            <div class="modal-content">
                <div class="modal-body">
                   
                     <div class="container">
                      <div class="row justify-content-center">
                          <div class="card text-white fondo-card mb-3 " style="max-width: 45rem;">
                                <div class="card-header text-center ">                           
                                     <img src="../Images/LogoVitraya21.png" height="200" width="200" />
                                </div>
                  
                              <div class="card-body">

                                  <div class="form-row ">
                                    <div class="row d-flex justify-content-center">

                                      <div class="form-group col-md-6">
                                        <label for="Descripcion" class="text-secondary" >* Username</label>
                                        <input type="text" class="form-control" id="" value="1002188186" placeholder="Descripción"/>
                                      </div>
                                      <div class="w-100"></div>
                                      <div class="form-group col-md-6">
                                        <label for="precio" class="text-secondary" >* Password</label>
                                        <input type="password" class="form-control" id="" value="1002188186" placeholder="Precio"/>
                                      </div>
                                    </div>
                                  </div>
              
                              </div>
                          </div>
                      </div>
                    </div>
                      
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" style=" margin: 0px 5px 5px 4px">Close</button>
                    <button type="button" class="btn btn-outline-success" id="">Firmar</button>
                </div>
            </div>
            </div>
         </div>


    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
   
</asp:Content>
