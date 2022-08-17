<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PlantillaBloqueoPedagogico.aspx.cs" Inherits="Presentacion.PlantillasBloqueos.PlantillaBloqueoPedagogico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="container">

        <div class="row">

            <div class="col col-2"></div>

            <div class="col col-8">

                <div style="margin-top: 150px;" class="alert alert-success" role="alert">

                  <h4 class="alert-heading">ALERTA DE TAREAS PENDIENTES!</h4>
                  <p>Se le recuerda que usted tiene tareas pendientes, coloquese a paz y salvo lo mas pronto posible.</p>
                  <p>Pendientes:</p>
                  <div id="ListPendientes"></div>
                  <hr/>
                  <p class="mb-0">Para continuar a la pestaña <span id="nombreOpcion"></span> de click en el botón siguiente.</p>

                </div>
                <div class="text-center">
                    <button type="button" class="btnContinuar btn btn-primary btn-lg">SIGUIENTE</button>
                </div>
            </div>

            <div class="col col-2"></div>

        </div>

    </div>

    <script src="JS/PlantillasBloqueosJS.js"></script>
  
</asp:Content>
