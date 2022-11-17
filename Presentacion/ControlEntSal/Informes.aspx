<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Informes.aspx.cs" Inherits="Presentacion.ControlEntSal.Informes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    


       <div class="row">

        <div class="col col-12">
            <div>
                <h3>INFORMES DE "CONTROL INGRESO-EGRESO" </h3>
            </div>
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Filtro de busqueda por fechas</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">

                        <div class="col col-12 col-sm-2">
                            <div class="form-group">
                                <label>Fecha Inicial</label>
                                <input type="date" class="form-control" id="txtFecha1"  />
                            </div>
                        </div>
                        <div class="col col-sm-2 col-12">
                            <div class="form-group">
                                <label>Fecha Final</label>
                                <input type="date" class="form-control" id="txtFecha2"  />
                            </div>
                        </div>
                        <div class="col col-sm-2 col-2">
                            <div class="form-group">
                                <label class="d-none d-sm-block d-sm">&nbsp;</label>
                                <button type="button" class="btnBuscarDesprendible btn btn-primary d-block">Buscar</button>
                            </div>
                        </div>

                       

                    </div>
                </div>
            </div>
           
            <%--INFORMES--%> 
            <div class="row">

                <%--INFORME SALIDA--%> 
                <div class="col col-12">

                    <div class="x_panel" style="height: 100%;">
                        <div class="x_title">
                            <div class="clearfix">
                                <h6>SALIDA DE PACIENTES</h6>
                            </div>
                        </div>
                        <div class="x_content">
                            <div class="row">
                                <div class="col col-12">
                                     <table class="display" style="width:100%" id="ICStableEgreso">
                                        <thead>
                                            <tr>
                                                <th>OID</th>
                                                <th>ORDENSALIDA</th>
                                                <th>INGRESO</th>
                                                <th>DOCUMENTO</th>
                                                <th>NOMBRE COMPLETO</th>
                                                <th>FECHA DE SALIDA</th>
                                            </tr>  
                                        </thead>
                                        <tbody >
                                        </tbody>   
                                      </table>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <%--INFORME MENOR DE EDAD--%>
                <div class="col col-12">
                     <div class="x_panel" style="height: 100%;">
                        <div class="x_title">
                            <div class="clearfix">
                                <h6>SALIDA DE MENORES DE EDAD</h6>
                            </div>
                        </div>
                        <div class="x_content">
                            <div class="row">
                                <div class="col col-12">
                                     <table class="display" style="width:100%" id="ICSMEtableEgreso">
                                        <thead>
                                            <tr>
                                                <th>OID</th>
                                                <th>ORDENSALIDA</th>
                                                <th>INGRESO</th>
                                                <th>DOCUMENTO</th>
                                                <th>NOMBRE COMPLETO</th>
                                                <th>FECHA DE SALIDA</th>
                                            </tr>  
                                        </thead>
                                        <tbody >
                                        </tbody>   
                                      </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
           
                 <%--INFORME FUGAS--%>
                <div class="col col-12">
                     <div class="x_panel" style="height: 100%;">
                        <div class="x_title">
                            <div class="clearfix">
                                <h6>NO REPORTADOS</h6>
                            </div>
                        </div>
                        <div class="x_content">
                            <div class="row">
                                <div class="col col-12">
                                     <table class="display" style="width:100%" id="ICStableFugas">
                                        <thead>
                                            <tr>
                                                <th>ORDENSALIDA</th>
                                                <th>INGRESO</th>
                                                <th>CAMA</th>
                                                <th>NOMBRE CAMA</th>
                                                <th>DOCUMENTO</th>
                                                <th>NOMBRE COMPLETO</th>
                                                <th>FECHA INGRESO</th>
                                                <th>FECHA EGRESO</th>
                                                <th>FECHA ORDEN</th>
                                               <%-- <th>OporMiIngreOrde </th>
                                                <th>OporMiOrdEgre</th>--%>
                                            </tr>  
                                        </thead>
                                        <tbody >
                                        </tbody>   
                                      </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>    
            
            </div>
        </div>
    </div>
  
                      
  
    <script src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.2.3/js/dataTables.buttons.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.2.3/js/buttons.html5.min.js"></script>
    <script src="https://cdn.datatables.net/buttons/2.2.3/js/buttons.print.min.js"></script>

    <script src="js/Informes.js"></script> 
</asp:Content>
