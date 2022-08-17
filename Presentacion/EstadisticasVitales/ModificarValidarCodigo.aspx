<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ModificarValidarCodigo.aspx.cs" Inherits="Presentacion.EstadisticasVitales.ModificarValidarCodigo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="css/ConsultarRegistros.css" rel="stylesheet" />
    <div class="x_panel">
        <div class="x_title pr-5">
            <div class="clearfix text-center">
                <h3 style="font-weight: 700" class="pr-5">Modificar y Validar Códigos </h3>
            </div>
        </div>
        <div class="x_content">
            <section id="formRegValCod" class="mt-2">
                <div class="form-group  row pt-1 justify-content-center">
                    <div class="col-xl-2 col-lg-3 col-md-4 col-sm-6 col-8">
                        <div class="" runat="server" id="divValNacViv">
                            <input type="radio" name="btnTipValReg" runat="server" value="" id="TipValNacViv" class="inputCon inputCon" />
                            <label for="TipValNacViv" class="">Nacidos Vivos</label>
                        </div>

                        <div class="" runat="server" id="divValDef">
                            <input type="radio" name="btnTipValReg" runat="server" value="" id="TipValDef" class="inputCon inputCon" />
                            <label for="TipValDef" class="">Defunción</label>
                        </div>
                    </div>
                </div>
                <div class="form-group  row pt-2 justify-content-center">
                    <div class="col-xl-4 col-lg-5 col-md-6 col-sm-8 col-10 mb-2">
                        <input type="text" class="form-control inputCon inputCon" id="ParValReg" placeholder="Digite código" style="border-radius: 5px" />
                    </div>
                    <button type="button" class="col-xl-1 col-lg-2 col-md-2 col-sm-2 col-3 btn btn-primary" id="btnValReg">Buscar</button>
                </div>
            </section>


            <section id="TablaValReg">
            </section>

            <div class="row">
                <div class="col-12 pt-1  justify-content-center">
                    <label runat="server" class="mb-0" id="lbCodDispNacViv"></label>
                </div>
            </div>
            <div class="row">
                <div class="col-12 pt-4 justify-content-center">
                    <label runat="server" class="mb-0" id="lbCodDispDef"></label>
                </div>
            </div>
        </div>
    </div>
    <script src="js/ModificarValidarCodigo.js"></script>
</asp:Content>
