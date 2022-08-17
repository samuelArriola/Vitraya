<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CrearIndicador.aspx.cs" Inherits="Presentacion.GestionDocumental.CrearIndicador" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal" tabindex="-1" role="dialog" id="event-modal">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">¿Esta seguro de iniciar el Proceso de Revisión?</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <p>Una vez el indicador inicie el flujo de aprobación  no se prodra volver a realizar cambios</p>
                </div>
                <div class="modal-footer">
                    <asp:Button Text="Guardar Cambios" runat="server" ID="btnGuardarInd" CssClass="btn btn-success"/>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <link href="css/CrearProcedimientoCss.css" rel="stylesheet" />
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Crear Indicador: <span id="txtNomProcedimientotitle" runat="server"></span></h6>
            </div>
        </div>
        <div class="x_content">

            <%--Header--%>
            <asp:UpdatePanel runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div class="row justify-content-center">
                         <div class="col col-10">
                            <div class="form-group">
                                <label>Versión</label>
                                <input type="text" value="" class="form-control" id="txtVersion"/>
                            </div>
                        </div>

                        <div class="col col-10">
                            <div class="form-group">
                                <label>Proceso</label>
                                <asp:DropDownList runat="server" ID="ddlProcesos" CssClass="form-control">
                                    <asp:ListItem Text="Seleccione" Value="-1"/>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtAlcance">Tipo de Indicador:</label>
                                <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-control">
                                    <asp:ListItem Text="Proceso" Value="Proceso" />
                                    <asp:ListItem Text="Resultado" Value="Resultado"/>
                                    <asp:ListItem Text="Producto" Value="Producto"/>
                                    <asp:ListItem Text="Eficacia" Value="Eficacia"/>
                                    <asp:ListItem Text="Eficiencia" Value="Eficiencia"/>
                                    <asp:ListItem Text="Calidad" Value="Calidad"/>
                                    <asp:ListItem Text="Insumo" Value="Insumo"/>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtNombre">Nombre:</label>
                                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control"/>
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtJustificacion">Justificación:</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="form-control" ID="txtJustificacion" />
                            </div>
                        </div>
                        
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_ddlDominio">Dominio:</label>
                                <asp:DropDownList runat="server" ID="ddlDominio" CssClass="form-control">
                                    <asp:ListItem Text="Seleccione..." Value="-1" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtCodSOGC">Código en el SOGC:</label>
                                <asp:TextBox runat="server" ID="txtCodSOGC" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col col-12">
                            <div class="x_title">
                                <div class="clearfix"></div>
                            </div>
                        </div>
                        <div class="col col-5">
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <h6>Numerador</h6>
                                    </div>
                                </div>
                                <div class="col col-12">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_txtDescNum">Descripción</label>
                                        <asp:TextBox runat="server" ID="txtDescNum" CssClass="form-control" TextMode="MultiLine" />
                                    </div>
                                </div>
                                <div class="col col-12">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_txtOrInfoNum">Origen de la Información:</label>
                                        <asp:TextBox runat="server" ID="txtOrInfoNum" CssClass="form-control" TextMode="MultiLine" />
                                    </div>
                                </div>
                                <div class="col col-12">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_txtFuenNum">Fuente Primaria</label>
                                        <asp:TextBox runat="server" ID="txtFuenNum" CssClass="form-control" TextMode="MultiLine" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col col-5" style="border-left: solid 2px #E6E9ED">
                            <div class="row">
                                <div class="col col-12">
                                    <div class="form-group">
                                        <h6>Denominador</h6>
                                    </div>
                                </div>
                                <div class="col col-12">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_txtDescDen">Descripción</label>
                                        <asp:TextBox runat="server" ID="txtDescDen" CssClass="form-control" TextMode="MultiLine" />
                                    </div>
                                </div>
                                <div class="col col-12">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_txtOrInfoDen">Origen de la Información:</label>
                                        <asp:TextBox runat="server" ID="txtOrInfoDen" CssClass="form-control" TextMode="MultiLine" />
                                    </div>
                                </div>
                                <div class="col col-12">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_txtFuenDen">Fuente Primaria</label>
                                        <asp:TextBox runat="server" ID="txtFuenDen" CssClass="form-control" TextMode="MultiLine" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col col-12">
                            <div class="x_title">
                                <div class="clearfix"></div>
                            </div>
                        </div>
                        <div class="col col-10">
                            <div class="row">
                                <div class="col col-3">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_txtUnidad">Unidad de Medida:</label>
                                        <asp:TextBox runat="server" ID="txtUnidad" CssClass="form-control" pattern="[0-9]" />
                                    </div>
                                </div>
                                <div class="col col-3">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_txtFactor">Factor:</label>
                                        <asp:TextBox runat="server" ID="txtFactor" CssClass="form-control"  />
                                    </div>
                                </div>
                                <div class="col col-3">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_ddlPeriodicidad">Periodicidad:</label>
                                        <asp:DropDownList runat="server" ID="ddlPeriodicidad" CssClass="form-control">
                                            <asp:ListItem Text="Mensual" Value="Mensual"/>
                                            <asp:ListItem Text="Bimestral" Value="Bimestral"/>
                                            <asp:ListItem Text="Trimestral" Value="Trimestral"/>
                                            <asp:ListItem Text="Semestral" Value="Semestral"/>
                                            <asp:ListItem Text="Anual" Value="Anual"/>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col col-3">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_ddlResponsable">Responsable:</label>
                                        <asp:DropDownList runat="server" ID="ddlResponsable" CssClass="form-control">
                                            <asp:ListItem Text="Seleccione ..." Value="-1" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col col-3">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_txtFormula">Formula de Cálculo:</label>
                                        <asp:TextBox runat="server" ID="txtFormula" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col col-3">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_txtEstandar">Estandar:</label>
                                        <asp:TextBox runat="server" ID="txtEstandar" CssClass="form-control" />
                                    </div>
                                </div>
                                <div class="col col-3">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_ddlTendencia">Tendencia Ideal:</label>
                                       <asp:DropDownList runat="server" ID="ddlTendencia" CssClass="form-control">
                                            <asp:ListItem Text="Ascendente" Value="Ascendente"/>
                                            <asp:ListItem Text="Descendente" Value="Descendente"/>
                                            <asp:ListItem Text="Estacional" Value="Estacional"/>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col col-3">
                                    <div class="form-group">
                                        <label for="ContentPlaceHolder_txtTipGrafica">Tipo de Graficas</label>
                                        <asp:TextBox runat="server" ID="txtTipGrafica" CssClass="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtInterpretacion">Interpretacion:</label>
                                <asp:TextBox runat="server" ID="txtInterpretacion" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>


                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_ddlResponsableMed">Responsable de Medición:</label>
                                <asp:DropDownList runat="server" ID="ddlResponsableMed" CssClass="form-control">
                                    <asp:ListItem Text="Seleccione ..." Value="-1" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_ddlResponsableAna">Responsable de Análisis:</label>
                                <asp:DropDownList runat="server" ID="ddlResponsableAna" CssClass="form-control">
                                    <asp:ListItem Text="Seleccione ..." Value="-1" />
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_ddlActores">Actores Interesados en el Resuldato</label>
                                <asp:DropDownList runat="server" ID="ddlActores" CssClass="form-control">
                                    <asp:ListItem Text="Seleccione ..." Value="-1" />
                                </asp:DropDownList>
                                <div id="lstActores"></div>
                            </div>
                        </div>

                         <div class="col col-10">
                            <div class="form-group">
                                <label for="ContentPlaceHolder_txtVigencia">Vigencia y Control:</label>
                                <asp:TextBox runat="server" ID="txtVigencia" CssClass="form-control" TextMode="MultiLine" />                                
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="row justify-content-center">
                <div class="col col-10">
                    <div class="form-group">
                        <asp:Button Text="Guardar" runat="server" ID="btnGuardar" CssClass="btn btn-success" Style="float: left" />
                        <button class="btn btn-primary" id="btnViewIndicador" type="button"><i class="fa fa-file-pdf-o"></i></button>
                        <asp:Button Text="Iniciar flujo de apobación" runat="server" ID="btnEnviarRevision" CssClass="btn btn-primary" Style="float: right" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../proceedings/js/tinymce.min.js"></script>
    <script src="js/CrearIndicardorJs.js"></script>
</asp:Content>
