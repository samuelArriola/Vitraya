<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Desprendible.aspx.cs" Inherits="Presentacion.Servicios.desprendible" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="row">

        <div class="col col-12">
            <div>
                <h3>Consulta y Descarga de Desprendibles de Nómina</h3>
            </div>
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Filtro de busqueda por fechas</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">

                        <div class="col col-12 col-sm-3">
                            <div class="form-group">
                                <label>Fecha Inicial</label>
                                <input type="month" class="form-control" id="txtFecha1" />
                            </div>
                        </div>
                        <div class="col col-sm-3 col-12">
                            <div class="form-group">
                                <label>Fecha Final</label>
                                <input type="month" class="form-control" id="txtFecha2" />
                            </div>
                        </div>
                        <div class="col col-sm-2 col-2">
                            <div class="form-group">
                                <label class="d-none d-sm-block d-sm">&nbsp;</label>
                                <button type="button" class="btnBuscarDesprendible btn btn-primary d-block">Buscar</button>
                            </div>
                        </div>

                        <div class="divcontrolOpcion col col-sm-3 col-3">
                            <div class="form-group">
                                <h5 class="d-sm-block d-sm" id="textoVisibilidad"></h5>
                                <button type="button" class="btnActivar btn btn-success">Habilitar</button>
                                <button type="button" class="btnInactivar btn btn-danger">Inhabilitar</button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="x_panel">
                <div class="x_title">
                    <div class="clearfix">
                        <h6>Lista de desprendibles disponibles</h6>
                    </div>
                </div>
                <div class="x_content">
                    <div class="row">
                        <div class="col col-12">
                            <ul id="ListaDesprendibles" class="list-group">
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="JS/DesprendibleJS.js"></script>

</asp:Content>
