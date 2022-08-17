<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RegistroEntradas.aspx.cs" Inherits="Presentacion.Vacunacion.RegistroEntradas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <style>
        .form-group:nth-child(n+1){
            margin-left: 20px;
        }
    </style>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Registro de entradas</h6>
            </div>
        </div>
        <div class="x_content">
            <div class="form-group">
                <label>Fecha de ingreso</label>
                <input type="date" value="" class="form-control" id="txtFechaIngreso" style="width: 200px !important;" />
            </div>
            <div class="justify-content-between d-flex" id="formCreateLote">
                <div class="form-group w-100">
                    <label>Lote</label>
                    <div style="position: relative">
                        <div class="d-flex justify-content-between form-control search-p">
                            <input type="text" id="txtSerachLotes" placeholder="Seleccione" autocomplete="off" spellcheck="false" style="color: transparent; text-shadow: 0 0 black; cursor: pointer" />
                            <i class="fa fa-angle-down"></i>
                        </div>
                        <div class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100" id="lstLotes"></div>
                    </div>
                </div>
                <div class="form-group w-100">
                    <label>Tipo</label>
                    <select class="form-control" id="slcTipo" disabled>
                        <option value="-1" disabled selected>Seleccione</option>
                        <option>Vacuna covid</option>
                        <option>Jeringa</option>
                    </select>
                </div>
                <div class="form-group w-100">
                    <label>Nombre</label>
                    <select class="form-control" id="slcNombre" disabled>
                    </select>
                </div>
                <div class="form-group w-100">
                    <label>Númento de Lote</label>
                    <input type="text" class="form-control" id="txtNumLoteCrear"  disabled/>
                </div>
                <div class="form-group w-100">
                    <label>Diluyente</label>
                    <input type="text" class="form-control" id="txtDiluyente"  disabled/>
                </div>
                <div class="form-group w-100">
                    <label>Cantidad a ingresar</label>
                    <input type="text" class="form-control" id="txtCantidadCrear" disabled />
                </div>
                <div class="form-group">
                    <label>&nbsp;</label>
                    <button type="button" class="btn btn-primary" style="cursor:pointer" id="btnCrear" disabled><i class="fa fa-plus"></i></button>
                </div>
            </div>
            <div class="mt-3">
                <table class="table" id="tbEntradas">
                    <thead>
                        <tr>
                            <th>Tipo</th>
                            <th>Nombre</th>
                            <th>Número del lote</th>
                            <th>Diluyente</th>
                            <th>Existencias</th>
                            <th>Cantidad a ingresar</th>
                            <th style="width: 5%">Opciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        
                    </tbody>
                </table>
                <div class="justify-content-end d-flex mt-3 mb-3">
                    <button type="button" class="btn btn-success" id="btnAgregarEntrada">Agregar entrada</button>
                </div>
            </div>
        </div>
    </div>
    <script src="js/RegistroEntradasJS.js"></script>
</asp:Content>
