<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ConsultarLotes.aspx.cs" Inherits="Presentacion.Vacunacion.ConsultarLotes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Consultar lotes y entradas</h6>
            </div>
        </div>
        <div class="x_content">
            <ul class="nav nav-tabs bar_tabs" id="myTab" role="tablist">
                <li class="nav-item">
                    <a class="nav-link active" id="tab-panel-lotes" data-toggle="tab" href="#panel-lotes" role="tab" aria-controls="panel-lotes" aria-selected="true">Lotes</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" id="tab-panel-entradas" data-toggle="tab" href="#panel-entradas" role="tab" aria-controls="panel-entradas" aria-selected="true">Entradas</a>
                </li>
            </ul>
            <div class="tab-content ">
                <div class="tab-pane fade active show" id="panel-lotes" role="tabpanel" aria-labelledby="tab-panel-lotes">
                    <table class="table" id="tbLotes">
                        <thead>
                            <tr>
                                <th>Insumo</th>
                                <th>Número del lote</th>
                                <th>Cantidad Ingresada</th>
                                <th>Existencias</th>
                            </tr>
                            <tr>
                                <th>
                                    <asp:DropDownList runat="server" ID="slcInsumo" CssClass="form-control form-control-sm">
                                    </asp:DropDownList>
                                </th>
                                <th></th>
                                <th></th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody></tbody>
                    </table>
                </div>
                <div class="tab-pane fade" id="panel-entradas" role="tabpanel" aria-labelledby="tab-panel-entradas">
                    asdfasdfasd
                </div>
            </div>
        </div>
    </div>
    <script src="js/ConsultarLotesJS.js"></script>
</asp:Content>
