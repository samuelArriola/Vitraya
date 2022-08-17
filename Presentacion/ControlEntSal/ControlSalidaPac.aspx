<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ControlSalidaPac.aspx.cs" Inherits="Presentacion.ControlEntSal.ControlSalidaPac" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    
    <div class="page-title">
        <div class="title_center">
            <h3>CONTROL SALIDA DE PACIENTES</h3>
        </div>
    </div>

    <div class="row">
      <!--ESCANEAR BOLETA DE SALIDA -->
      <div class="col col-4">
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
         <form>
            <div class="row">
                <div class="col col-12">
                    <div class="form-group">
                        <label id="codigoRLabel">* Escanear Boleta:</label>
                        <input min="1" max="5" type="number" class="form-control" id="CScodigoR" autofocus />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col col-12">
                    <div class="form-group">
                        <label>* Identificacion:</label>
                        <input type="text" class="form-control" id="CSiden"/>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col col-12">
                    <div class="form-group">
                        <label>Nombres:</label>
                        <input type="text" class="form-control" id="CSnombreR"/>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col col-12">
                    <div class="form-group">
                        <label>Apellidos:</label>
                        <input class="form-control" id="CSapell" />
                    </div>
                </div>
            </div>
         </form>
        </div>
   </div>
      
      <!--ESCANEAR MANILLA -->
      <div class="col col-8">
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
                        <div class="col col-5">
                            <div class="form-group">
                                <label>* Escaner Manilla:</label>
                                <input type="text" class="form-control" id="CSmanilla" />
                            </div>
                        </div>
                    </div>
              
                </div>
        </div>
    </div>


    <script src="js/ControlSalida.js"></script>
</asp:Content>
