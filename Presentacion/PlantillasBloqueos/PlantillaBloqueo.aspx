<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PlantillaBloqueo.aspx.cs" Inherits="Presentacion.PlantillasBloqueos.PlantillaCapacitaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

  <div class="container">

      <div class="container">

        <div class="row">

            <div class="col col-2"></div>

            <div class="col col-8">

                <div style="margin-top: 140px;" class="alert alert-danger" role="alert">

                      <h4 class="alert-heading">ALERTA DE PESTAÑA BLOQUEADA!</h4>
                      <p>El acceso a la página <span id="nombreOpcion"></span> se encuentra bloqueado por tareas pendientes, coloquese a paz y salvo para poder acceder.</p>
                      <hr>
                      <p class="mb-0">Pendientes:</p>
                      <div id="ListPendientes"></div>

                </div>

            </div>

            <div class="col col-2"></div>

        </div>

    </div>

  </div>

    <script src="JS/PlnatillasBloqueos2JS.js"></script>

</asp:Content>
