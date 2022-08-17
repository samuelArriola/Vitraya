<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="InformeRepetidos.aspx.cs" Inherits="Presentacion.EstadisticasVitales.InformeRepetidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Informe de registros repetidos</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="x_content">
                <ul class="nav nav-tabs bar_tabs" id="myTab" role="tablist">
                    <li class="nav-item">
                        <a class="nav-link" id="nacidos-vivos-tab" data-toggle="tab" href="#nacidos-vivos" role="tab" aria-controls="nacidos-vivos" aria-selected="false"><font style="vertical-align: inherit;"><font style="vertical-align: inherit;">Nacidos vivos</font></font></a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="defuncion-tab" data-toggle="tab" href="#defuncion" role="tab" aria-controls="defuncion" aria-selected="false"><font style="vertical-align: inherit;"><font style="vertical-align: inherit;">Defunciónn</font></font></a>
                    </li>
                    <li class="nav-item">
                    </li>
                </ul>
                <div class="tab-content" id="myTabContent">
                    <div class="tab-pane fade" id="nacidos-vivos" role="tabpanel" aria-labelledby="nacidos-vivos-tab">
                        <table class="table" id="tbRegistrosNV">
                            <thead>
                                <tr>
                                    <th>Código</th>
                                    <th>Documento de la madre</th>
                                    <th>Nombre de la madre</th>
                                    <th>Doctor</th>
                                    <th>Fecha de registro</th>
                                </tr>
                                <tr>
                                    <th><input type="text" class="form-control" id="txtCodigo"/></th>
                                    <th><input type="text" class="form-control" id="txtDocumentoMadre"/></th>
                                    <th><input type="text" class="form-control" id="txtNomMadre"/></th>
                                    <th><input type="text" class="form-control" id="txtNomDoctor"/></th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>

                            </tbody>
                        </table>
                    </div>
                    <div class="tab-pane fade" id="defuncion" role="tabpanel" aria-labelledby="defuncion-tab">
                       
                    </div>
                    
                </div>
            </div>
        </div>
    </div>
    <script src="js/InformeRepetidosJS.js"></script>
</asp:Content>
