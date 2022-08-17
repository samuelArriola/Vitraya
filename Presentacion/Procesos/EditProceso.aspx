<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EditProceso.aspx.cs" Inherits="Presentacion.Procesos.EditProceso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <link href="CSS/CrearProcesosCSS.css" rel="stylesheet" />
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6><strong>Editar Proceso: </strong> <span id="NomProTitle" runat="server"></span> </h6>
            </div>
        </div>
        <div class="x_content">
                <asp:UpdatePanel runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="row justify-content-center">

                            <%--Tipo de proceso--%>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_ddlTipPro">Tipo De Proceso</label>
                                    <asp:DropDownList runat="server" ID="ddlTipPro" CssClass="form-control">
                                        <asp:ListItem Text="Seleccione..." Value="-1" />
                                        <asp:ListItem Text="Estratégicos" Value="Estratégicos" />
                                        <asp:ListItem Text="Misionales" Value="Misionales" />
                                        <asp:ListItem Text="Apoyo " Value="Apoyo " />
                                        <asp:ListItem Text="Evaluación" Value="Evaluación" />
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <%--Nombre proceso--%>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtNomPro">Nombre del Proceso:</label>
                                    <asp:TextBox runat="server" ID="txtNomPro" CssClass="form-control" />
                                </div>
                            </div>

                            <%--proceso padre--%>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_ddlPadrePro">Proceso Padre:</label>
                                    <asp:DropDownList runat="server" ID="ddlPadrePro" CssClass="form-control">
                                        <asp:ListItem Text="N/A" Value="-1" />
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <%--Prefijo--%>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtPrefijo">Prefijo:</label>
                                    <asp:TextBox runat="server" ID="txtPrefijo" CssClass="form-control" />
                                </div>
                            </div>
                            
                            <%--objetivo--%>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtObjetivo">Objetivo:</label>
                                    <asp:TextBox runat="server" ID="txtObjetivo" CssClass="form-control" Style="height:80px" TextMode="MultiLine" />
                                </div>
                            </div>

                            <%--Alcance--%>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtAlcance">Alcance  :</label>
                                    <asp:TextBox runat="server" ID="txtAlcance" CssClass="form-control" Style="height:80px" TextMode="MultiLine" />
                                </div>
                            </div>

                            <%--Indicadores
                            Espacio de indicadores
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtIndicadores">Indicadores:</label>
                                    <asp:TextBox runat="server" ID="txtIndicadores" CssClass="form-control" TextMode="MultiLine" />
                                </div>
                            </div>
                            Espacio de indicadores--%>

                            <%--Responsables--%>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_ddlResponsables">Responsables:</label>
                                    <asp:DropDownList runat="server" ID="ddlResponsables" CssClass="form-control">
                                        <asp:ListItem Text="Seleccione..." Value="-1" />
                                    </asp:DropDownList>
                                </div>
                                <div id="lstddlResponsables" runat="server">
                                </div>
                            </div>

                            <div class="col col-12 mt-3 mb-4">
                                <div class="x_title">
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>

                            <%--SIPOC--%>
                            <div class="col col-12">
                                <div class="form-group">
                                    <h6>Diagrama SIPOC </h6>
                                </div>
                            </div>

                            <%--Entradas--%>
                            <div class="col col-11">
                                <div class="form-group">
                                    <h6>Entradas</h6>
                                </div>
                            </div>
                            <div class="col col-5">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtProvedores">Provedores:</label>
                                    <asp:TextBox runat="server" ID="txtProvedores" CssClass="form-control" Style="height:80px" TextMode="MultiLine" />
                                </div>
                            </div>
                            <div class="col col-5">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtEntradas">Entradas:</label>
                                    <asp:TextBox runat="server" ID="txtEntradas" CssClass="form-control" Style="height:80px" TextMode="MultiLine" />
                                </div>
                            </div>

                            <%--Salidas--%>
                            <div class="col col-11">
                                <div class="form-group">
                                    <h6>Gestión PHVA</h6>
                                </div>
                            </div>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_ddlTipoActividad">Tipo Actividad:</label>
                                    <asp:DropDownList runat="server" ID="ddlTipoActividad" CssClass="form-control">
                                        <asp:ListItem Text="Seleccione..." Value="-1" />
                                        <asp:ListItem Text="Planear" Value="Planear" />
                                        <asp:ListItem Text="Hacer" Value="Hacer" />
                                        <asp:ListItem Text="Verificar " Value="Verificar" />
                                        <asp:ListItem Text="Actuar" Value="Actuar" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtActividad">Descripcion Actividad:</label>
                                    <asp:TextBox runat="server" ID="txtActividad" CssClass="form-control" Style="height:80px" TextMode="MultiLine" />
                                </div>
                            </div>
                            <div class="col col-11">
                                <div class="form-group">
                                    <h6>Salidas</h6>
                                </div>
                            </div>
                            <div class="col col-5">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtClientes">Clientes:</label>
                                    <asp:TextBox runat="server" ID="txtClientes" CssClass="form-control" Style="height:80px" TextMode="MultiLine" />
                                </div>
                            </div>
                            <div class="col col-5">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtClientes">salidas:</label>
                                    <asp:TextBox runat="server" ID="txtSalidas" CssClass="form-control" Style="height:80px" TextMode="MultiLine" />
                                </div>
                            </div>
                            <div class="col col-10">
                                <div class="form-group">
                                    <asp:Button Text="Agregar" runat="server" ID="btnCrearActividad" CssClass="btn btn-success" Style="float: right" />
                                </div>
                            </div>

                            <div class="col col-10">
                                <div class="" id="">
                                    <table class="table" id="tbActividades">
                                        <thead>

                                        </thead>
                                        <tbody>

                                        </tbody>
                                    </table>
                                </div>
                            </div>

                            <div class="col col-12 mb-4">
                                <div class="x_title">
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>

                            <%--Recursos finacieros--%>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtRecFinacieros">Recursos Finacieros:</label>
                                    <asp:TextBox runat="server" ID="txtRecFinacieros" CssClass="form-control" Style="height:80px" TextMode="MultiLine" />
                                </div>
                            </div>

                            <%--Recursos humanos: --%>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_ddlRecHumanos">Recursos humanos: </label>
                                    <asp:DropDownList runat="server" ID="ddlRecHumanos" CssClass="form-control">
                                        <asp:ListItem Text="Seleccione..." Value="-1" />
                                    </asp:DropDownList>
                                </div>
                                <div id="lstddlRecHumanos" runat="server">

                                </div>
                            </div>

                            <%--Normas--%>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtNormas">Normas:</label>
                                    <asp:TextBox runat="server" ID="txtNormas" CssClass="form-control" Style="height:80px" TextMode="MultiLine" />
                                </div>
                            </div>

                            <%--Riesgos--%>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtRiesgos">Riesgos:</label>
                                    <asp:TextBox runat="server" ID="txtRiesgos" CssClass="form-control" Style="height:80px" TextMode="MultiLine" />
                                </div>
                            </div>

                            <%--Documentos relacionados--%>
                            <div class="col col-10">
                                <div class="form-group">
                                    <label for="ContentPlaceHolder_txtDocRelacionados">Documentos relacionados:</label>
                                    <asp:TextBox runat="server" ID="txtDocRelacionados" CssClass="form-control" Style="height:80px" TextMode="MultiLine" />
                                </div>
                            </div>

                            <div class="col col-12 mt-3">
                                <div class="x_title">
                                    <div class="clearfix">
                                    </div>
                                </div>
                            </div>

                            <%--Flujograma--%>
                            <div class="col col-12">
                                <div class="form-group">
                                    <h6>Flujograma</h6>
                                </div>
                            </div>
                            <div class="col col-10 text-center">
                                <div id="imageFlujo" class="d-inline-block">
                                    <svg width="11em" id="imgUpLoad" height="11em" viewBox="0 0 16 16" class="bi bi-cloud-arrow-up-fill pt-3" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                                        <path fill-rule="evenodd" d="M8 2a5.53 5.53 0 0 0-3.594 1.342c-.766.66-1.321 1.52-1.464 2.383C1.266 6.095 0 7.555 0 9.318 0 11.366 1.708 13 3.781 13h8.906C14.502 13 16 11.57 16 9.773c0-1.636-1.242-2.969-2.834-3.194C12.923 3.999 10.69 2 8 2zm2.354 5.146l-2-2a.5.5 0 0 0-.708 0l-2 2a.5.5 0 1 0 .708.708L7.5 6.707V10.5a.5.5 0 0 0 1 0V6.707l1.146 1.147a.5.5 0 0 0 .708-.708z" />
                                    </svg>
                                    <label for="ContentPlaceHolder_fuImageFlujo">&nbsp;</label>
                                </div>
                                <asp:FileUpload runat="server" ID="fuImageFlujo" Style="display: none" accept=".jpg, .png, .jpeg, .JPG, .PNG, .JPEG" />
                            </div>

                            <%--boton Crear Proceso--%>
                            <div class="col col-12">
                                <div class="form-group">
                                    <asp:Button Text="Crear" runat="server" ID="btnCrearPro" CssClass="btn btn-success"/>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
