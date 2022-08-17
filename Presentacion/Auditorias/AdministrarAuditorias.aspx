<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AdministrarAuditorias.aspx.cs" Inherits="Presentacion.Auditorias.AdministrarAuditorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Administrar Auditorias</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="x_content">
                <ul class="nav nav-tabs bar_tabs" id="myTab" role="tablist">
                    <li class="nav-item">
                        <a class="nav-link active" id="uditoria-interna-panel-tab" data-toggle="tab" href="#uditoria-interna-panel" role="tab" aria-controls="uditoria-interna-panel" aria-selected="true"><font style="vertical-align: inherit;"><font style="vertical-align: inherit;">Auditorias internas</font></font></a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" id="uditoria-externa-panel-tab" data-toggle="tab" href="#uditoria-externa-panel" role="tab" aria-controls="uditoria-externa-panel" aria-selected="false"><font style="vertical-align: inherit;"><font style="vertical-align: inherit;">Auditorias Externas</font></font></a>
                    </li>
                    
                </ul>
                <div class="tab-content" id="myTabContent">
                    <div class="tab-pane fade show active" id="uditoria-interna-panel" role="tabpanel" aria-labelledby="uditoria-interna-panel-tab">
                        <div id="panel-internas">

                        </div>
                    </div>
                    <div class="tab-pane fade" id="uditoria-externa-panel" role="tabpanel" aria-labelledby="uditoria-externa-panel-tab">
                        <div id="panel-externas">

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js/AdministrarAuditoriasJS.js"></script>
</asp:Content>
