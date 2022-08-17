<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PanelBloqueos.aspx.cs" Inherits="Presentacion.General.PanelBloqueos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row">
            <div class="col col-12">
                <div class="page-title">
                    <div class="title_left">
                        <h3>Panel de Bloqueos</h3>
                    </div>
                </div>
            <div class="x_content">
                <div class="x_panel">
                    
                    <div class="row">
                        <div class="col col-12">
                            <div >
                                <table class="table" style="overflow:auto; width:100%;" id="tableDocs">
                                    <thead>
                                        <tr>
                                            <th>Código</th>
                                            <th>Nombre de bloqueo</th>
                                            <th>Estado de bloqueo</th>
                                        </tr>  
                                    </thead>
                                    <tbody id="tbBloqueos">

                                    </tbody>   
                                </table>
                                <h5>Opciones bloqueadas 
                                    <button class="btnMostrarOB btn btn-primary" type="button" data-toggle="modal" data-target="#exampleModal" >
                                      <i class="fa fa-unlock-alt"> Gestionar </i>
                                    </button>
                                </h5>
                            </div>
                        </div>
                    </div>
                    
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Gestion de bloquear opciones</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">

                  <table class="table" style="overflow:auto; width:100%;" id="tableOB">
                        <thead>
                            <tr>
                                <th>Codigo</th>
                                <th>Nombre de opcion</th>
                                <th>Estado de bloqueo</th>
                            </tr>  
                        </thead>
                        <tbody id="tbOBloqueos">

                        </tbody>   
                   </table>
                
              </div>
            </div>
          </div>
    </div>


    <script src="js/PanelBloqueosJS.js"></script>
</asp:Content>
