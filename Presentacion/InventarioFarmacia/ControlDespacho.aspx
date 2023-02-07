<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ControlDespacho.aspx.cs" Inherits="Presentacion.InventarioFarmacia.ControlDespacho" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
      <link rel="stylesheet" href="css/Style.css" /> 
   

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
                        <div class="col col-10">
                            <div class="row">                             
                                <div class="col col-3">
                                    <div class="form-group">
                                        <label>*Buscar Suministro:</label>
                                        <input type="text" class="form-control" id="CDBusSuministro" value="" />
                                    </div>
                                </div>
                                <input type="text" class="form-control" id="CDconsec"  style="display:none" disabled/>
                                <div class="col col-3">
                                    <div class="form-group">
                                        <label>* Fec. Documento:</label>
                                        <input type="text" class="form-control" id="CDfec_doc" disabled/>
                                    </div>
                                </div> 
                                    <div class="col col-3">
                                    <div class="form-group">
                                        <label>* Area Servicio:</label>
                                        <input type="text" class="form-control" id="CDarea_servicio" disabled />
                                    </div>
                                </div>
                                <div class="col col-2">
                                    <div class="form-group">
                                        <label>* Cama:</label>
                                        <input type="text" class="form-control" id="CDcama" disabled/>
                                    </div>
                                </div> 
                                <div class="w-100 divider"></div>
                                    <div class="col col-3">
                                    <div class="form-group">
                                        <label>* Identificación:</label>
                                        <input type="text" class="form-control" id="CDiden" disabled />
                                    </div>
                                </div>
                                <div class="col col-6">
                                    <div class="form-group">
                                        <label>* Nombre y Apellido:</label>
                                        <input type="text" class="form-control" id="CDInoms" disabled/>
                                    </div>
                                </div> 
                                <div class="w-100"></div>
                                <div class="d-flex flex-row bd-highlight pt-2">
                                    <label class="p-2 bd-highlight">
                                        <input type="checkbox" class="filled-in" name="checkTip" id="cdCPAC" value="1"/>
                                        <span>* Paciente Correcto</span>
                                    </label>
                                    <label class="p-2 bd-highlight">
                                        <input type="checkbox" class="filled-in" name="checkTip" id="cdCCANT" value="1"/>
                                        <span>* Cantidad de medicamento Correcto</span>
                                    </label>
                                    <label class="p-2 bd-highlight">
                                        <input type="checkbox" class="filled-in" name="checkTip" id="cdCVIAADMIN" value="1" />
                                        <span>* Via de administracion correcta</span>
                                    </label>
                                    <label class="p-2 bd-highlight">
                                        <input type="checkbox" class="filled-in" name="checkTip" id="cdCDOSIS" value="1" />
                                        <span>* Dosis de administración Correcta</span>
                                    </label>
                                </div>
                                <div class="w-100"></div>
                                <div class="d-flex flex-row bd-highlight mb-3" id="cdOber_prolon">
                                   
                                </div>
                         </div>
                              <%--FIRMA--%>
                        </div>
                         <div class="col col-2 d-flex align-items-center justify-content-around">       
                            <div class="form-group" style="visibility: hidden"  id="div_firmar">
                                <button data-bs-toggle="modal" type="button" data-bs-target="#MCDFirma"  id="ModalbtnFirma" class=" btn  btn-outline-success" onclick="  $('#CD_user').focus();"   >Firmar</button>
                            </div>    
                        </div>
                    </div>
                       
                    <div class="x_title">
                        <div class="clearfix">
                        </div>
                    </div>

                    <table class="table" style="overflow: auto; max-width: 100%;" id="CDtablaSuministro">
                        <thead>
                            <tr>
                                <th>Codigo</th>
                                <th>Descripción</th>
                                <th>Indicación Medica</th>
                                <th>Dosis</th>
                                <th>Unidad Medida</th>
                                <th>Frecuencia</th>
                                <th>Via Administración</th>
                                <th>Lote/Serie</th>
                                <th>Cantidad</th>
                            </tr>
                        </thead>
                        <tbody id="CDBodySuministro">
                                <div class="spinner-border text-success spinner-border_list_suministro" role="status" style="position: absolute; top: 320px; right:50%; width: 4rem; height: 4rem">
                                <span class="visually-hidden"></span>
                            </div>
                            <template id="CDSuministroTemplate">
                                <tr >
                                    <td scope="row">234</td>
                                    <td></td>
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
                 </div> 
            </div>

        </div>
      </div>
  


    
     <!--MODAL FIRMA DE SUMINISTYRO-->
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
                                        <label for="CD_user" class="text-secondary" >* Username</label>
                                        <input type="text" class="form-control" id="CD_user" value="" placeholder=""/>
                                      </div>
                                      <div class="w-100"></div>
                                      <div class="form-group col-md-6">
                                        <label for="CD_pass" class="text-secondary" >* Password</label>
                                        <input type="password" class="form-control" id="CD_pass" value="" placeholder=""/>
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
                    <button type="button" class="btn btn-outline-success" id="CD_btn_firmar">Firmar</button>
                </div>
            </div>
            </div>
         </div>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.3/jquery.min.js" crossorigin="anonymous" ></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js" integrity="sha384-IQsoLXl5PILFhosVNubq5LC7Qb9DXgDA9i+tQ8Zj3iwWAwPtgFTxbJ8NT4GN1R8p" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.min.js" integrity="sha384-cVKIPhGWiC2Al4u+LWgxfKTRIcfu0JTxR+EQDz/bgldoEyl4H0zUF0QKbrJ0EcQF" crossorigin="anonymous"></script>
    <script src="JS/ControlDespachoJS.js"></script>
</asp:Content>
