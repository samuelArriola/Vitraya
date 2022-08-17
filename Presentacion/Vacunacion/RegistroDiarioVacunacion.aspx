<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="RegistroDiarioVacunacion.aspx.cs" Inherits="Presentacion.LinksExternos.RegistroDiarioVacunacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="modal" tabindex="-1" role="dialog" id="modal1">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Lugar de registro</h5>
                </div>
                <div class="modal-body">
                    <h6 class="text-center">Por favor seleccione el area de registro</h6>
                    <div class="custom-control custom-radio">
                        <input autocomplete="off" type="radio" class="custom-control-input" id="defaultGroupExample1" name="rdsAreas" value="Cuidado seguro en casa" />
                        <label class="custom-control-label" for="defaultGroupExample1">Cuidado seguro en casa</label>
                    </div>


                    <div class="custom-control custom-radio">
                        <input autocomplete="off" type="radio" class="custom-control-input" id="defaultGroupExample2" name="rdsAreas" value="Clínica Crecer" />
                        <label class="custom-control-label" for="defaultGroupExample2">Clínica Crecer</label>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnAceptarModal" class="btn btn-primary">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6 class="text-center">Registro diario de Vacunación</h6>
            </div>
        </div>
        <div class="x_content" id="panel1">
            <div class="row justify-content-center d-flex">
                <div class="row col col-12 col-xl-11">
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Fecha de Vacunación</label>
                            <input autocomplete="off" type="date" class="form-control" id="dtFechaVacunacion" />
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Tipo de documento</label>
                            <select class="form-control" id="slcTipoDocumento">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>CC</option>
                                <option>PA</option>
                                <option>CE</option>
                                <option>TI</option>
                                <option>RC</option>
                                <option>AS</option>
                                <option>PEP</option>
                                <option>PPT</option>
                                <option>DE</option>
                            </select>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Documento</label>
                            <input autocomplete="off" type="text" class="form-control" id="txtDocumento" />
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <label>Fecha de Nacimiento</label>
                        <input autocomplete="off" type="date" class="form-control" id="dtFechaNacimiento" />
                    </div>

                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Departamento de Nacimiento</label>
                            <input autocomplete="off" type="text" value="" class="form-control" id="txtDeptoNacimiento" />
                            <div id="lstDeptos2" class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Municipio de Nacimiento</label>
                            <input autocomplete="off" type="text" value="" class="form-control" id="txtMunicipioNacimiento" />
                            <div id="lstMunicipios2" class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                        </div>
                    </div>

                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Edad</label>
                            <input autocomplete="off" type="text" class="form-control" id="txtEdad" disabled />
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Género</label>
                            <select class="form-control" id="slcSexo">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>F</option>
                                <option>M</option>
                            </select>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Primer apellido</label>
                            <input autocomplete="off" type="text" class="form-control" id="txtPrimerApellido" />
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Segundo apellido</label>
                            <input autocomplete="off" type="text" class="form-control" id="txtSegundoApellido" />
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Nombre(s)</label>
                            <input autocomplete="off" type="text" class="form-control" id="txtNombre" />
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Regimen</label>
                            <select class="form-control" id="slcRegimen">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>1=Contributivo</option>
                                <option>2=Subsidiado</option>
                                <option>3=Pobre no asegurado</option>
                                <option>4=Especial y de excepción</option>
                            </select>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Aseguradora</label>
                            <asp:DropDownList runat="server" ID="txtAseguradora" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Departamento de Residencia</label>
                            <input autocomplete="off" type="text" value="" class="form-control" id="txtDeptoResidencia" />
                            <div id="lstDeptos" class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Municipio de residencia</label>
                            <input autocomplete="off" type="text" value="" class="form-control" id="txtMunicipioResidencia" />
                            <div id="lstMunicipios" class="box-autocomplete" style="max-height: 0; border: 0; z-index: 100"></div>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Área residencia</label>
                            <select class="form-control" id="txtAreaResidencia">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>1= Cabecera</option>
                                <option>2= Centro Poblado</option>
                                <option>3= Rural</option>
                            </select>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Barrio / Centro poblado o Vereda de residencia</label>
                            <input autocomplete="off" type="text" value="" class="form-control" id="txtBarrio" />
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Dirección</label>
                            <input autocomplete="off" type="text" value="" class="form-control" id="txtDireccion" />
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Teléfono</label>
                            <input autocomplete="off" type="tel" class="form-control" id="txtTelefono" />
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Grupo étnico</label>
                            <select class="form-control" id="slcGrupoEtnico">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>1 = Indígena</option>
                                <option>2 = Gitano</option>
                                <option>3 = Raizal</option>
                                <option>4 = Palenquero</option>
                                <option>5 = Negro, Mulato ó Afrocolombiano</option>
                                <option>6 =Sin pertenencia étnica</option>
                            </select>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Condición de desplazamiento</label>
                            <select class="form-control" id="slcDesplazamiento">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>1 = Si</option>
                                <option>2 = No</option>
                            </select>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Condición de discapacidad</label>
                            <select class="form-control" id="slcDiscapacidad">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>1 = Si</option>
                                <option>2 = No</option>
                            </select>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Correo electrónico</label>
                            <input autocomplete="off" type="email" class="form-control" id="txtEmail" />
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Condición de usuaria</label>
                            <select class="form-control" id="slcGestacion">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>1 = Gestante</option>
                                <option>2 = No Gestante</option>

                            </select>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <label>Fecha Probable del parto</label>
                        <div class="justify-content-between d-flex">
                            <input autocomplete="off" type="date" class="form-control" id="dtFechaParto" style="width: 80%" />
                            <button type="button" class="btn btn-info ml-1" style="width: 20%" id="btnInputFechaParto"><i class="fa fa-refresh"></i></button>
                        </div>
                    </div>
                    <!--<div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Etapa de vacunación</label>
                            <select class="form-control" id="slcEtapa">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>ETAPA1</option>
                                <option>ETAPA2</option>
                                <option>ETAPA3</option>
                                <option>ETAPA4</option>
                                <option>ETAPA5</option>
                            </select>
                        </div>
                    </div>-->

                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Tipo de población</label>
                            <input autocomplete="off" type="text" class="form-control" id="slcTipoPoblacion" list="ETAPA" disabled="disabled" />
                        </div>
                    </div>

                    <div class="col col-12 col-sm-3 groupNaciMenor" style="display: none">
                        <div class="form-group">
                            <label>Semanas de gestacion del menor</label>
                            <input autocomplete="off" type="number" class="form-control" id="txtSemanasGestacionMenor"/>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3 groupNaciMenor" style="display: none">
                        <div class="form-group">
                            <label>Peso al nacer del menor(Kg)</label>
                            <input autocomplete="off" type="number" class="form-control" id="txtPesoAlNacerMenor"/>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3 groupNaciMenor" style="display: none">
                        <div class="form-group">
                            <label>Lugar atención del parto del menor</label>
                            <select class="form-control" id="slcLugarPartoMenor">
                                <option value="" disabled selected>Seleccione</option>
                                <option>1 = Hospitalario</option>
                                <option>2 = Domiciliario</option>
                                <option>3 = Via Publica</option>
                            </select>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3 groupNaciMenor" style="display: none">
                        <div class="form-group">
                            <label>Nombre lugar atención del parto del menor</label>
                            <input autocomplete="off" type="text" class="form-control" id="txtNombreLugarPartoMenor" />
                        </div>
                    </div>

                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Dosis aplicada</label>
                            <select class="form-control" id="slcDosis">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>1 = Primera Dosis</option>
                                <option>2 = Segunda Dosis</option>
                                <option>3 = Única</option>
                                <option>4 = Refuerzo</option>
                                <option>5 = Segundo Refuerzo</option>
                            </select>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Biológico</label>
                            <select class="form-control" id="slcBiologico">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>AstraZeneca</option>
                                <option>Pfizer</option>
                                <option>Sinovac</option>
                                <option>Janssen</option>
                                <option>Moderna</option>
                            </select>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Lote del biológico</label>
                            <input autocomplete="off" type="text" class="form-control" id="txtLote" />

                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Jeringa</label>
                            <select class="form-control" id="txtJeringa">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>22G1 1/2 "Convencional</option>
                                <option>23G1 "Convencional</option>
                                <option>22G1 1/2" AD</option>
                                <option>23G1" AD</option>
                            </select>
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <div class="form-group">
                            <label>Lote de la Jeringa</label>
                            <input class="form-control" id="txtLoteJeringa" type="text" />
                        </div>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <label>Nombre del vacunador</label>
                        <select class="form-control" id="txtNombreVacunador">
                            <option value="-1" disabled selected>Seleccione</option>
                            <option>ALEJANDRA MARIA BUELVAS ALCALÁ</option>
                            <option>ANTONIA SALGADO MALDONADO</option>
                            <option>ELVIRA ROSA CAICEDO RODRIGUEZ</option>
                            <option>JAZMIN BATISTA DE AVILA</option>
                            <option>JESSICA ESTHER BERDUGO MALDONADO</option>
                            <option>JHORSELIS KARINA MATOS DIAZ</option>
                            <option>JOEL DEL CARMEN BARRIOS SECA</option>
                            <option>MARELYS ZAMBRANO MONTERO</option>
                            <option>MARIBIS PUERTA ARIAS</option>
                            <option>MARYORIS MOLINA REALES</option>
                            <option>MÓNICA FERIA CERVANTES</option>
                            <option>PAOLA KATERINE VELASQUEZ PALMERA</option>
                            <option>PAOLA VELASQUEZ PALMERA</option>
                            <option>ROSA ELVIRA CAICEDO RODRIGUEZ</option>
                            <option>YESSICA PAOLA VASQUEZ NIÑO</option>
                            <option>YULIS GÓMEZ DEL TORO</option>
                            <option>ZAIDY PATRICIA GARCES COTTA</option>
                        </select>
                    </div>
                    <div class="col col-12 col-sm-3">
                        <label>Nombre de la IPS</label>
                        <input autocomplete="off" type="text" class="form-control" id="txtNombreIps" disabled value="Centro Médico Crecer Ltda." />
                    </div>
                </div>
            </div>
        </div>
        <div class="mt-3" id="panel-ac" style="display: none">
            <div class="x_title">
                <div class="clearfix">
                    <h6 class="text-center">Datos del acudiente del menor de edad</h6>
                </div>
            </div>
            <div class="x_content">
                <div class="row justify-content-center d-flex">
                    <div class="col col-12 col-xl-11 row">
                        <div class="col col-12 col-sm-3">
                            <label>Tipo de documento</label>
                            <select class="form-control" id="slcTipoDocumentoAC">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>CC</option>
                                <option>PA</option>
                                <option>CE</option>
                                <option>TI</option>
                                <option>RC</option>
                                <option>AS</option>
                            </select>
                        </div>
                        <div class="col col-12 col-sm-3">
                            <label>Documento</label>
                            <input autocomplete="off" type="text" class="form-control" id="txtDocumentoAC" />
                        </div>

                        <div class="col col-12 col-sm-3">
                            <label>Parentesco</label>
                            <input autocomplete="off" type="text" class="form-control" id="txtParentescoAC" />
                        </div>
                        <div class="col col-12 col-sm-3">
                            <label>Nombre(s) y apellidos</label>
                            <input autocomplete="off" type="text" class="form-control" id="txtNombreAC" />
                        </div>
                        <div class="col col-12 col-sm-3">
                            <label>Regimen</label>
                            <select class="form-control" id="slcRegimenAC">
                                <option value="-1" disabled selected>Seleccione</option>
                                <option>1=Contributivo</option>
                                <option>2=Subsidiado</option>
                                <option>3=Pobre no asegurado</option>
                                <option>4=Especial y de excepción</option>
                            </select>
                        </div>
                        <div class="col col-12 col-sm-3">
                            <div class="form-group">
                                <label>Aseguradora</label>
                                <asp:DropDownList runat="server" ID="txtAseguradoraAC" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col col-12 col-sm-3">
                            <div class="form-group">
                                <label>Teléfono</label>
                                <input autocomplete="off" type="tel" class="form-control" id="txtTelefonoAC" />
                            </div>
                        </div>
                        <div class="col col-12 col-sm-3">
                            <div class="form-group">
                                <label>Condición de desplazamiento</label>
                                <select class="form-control" id="slcDesplazamientoAC">
                                    <option value="-1" disabled selected>Seleccione</option>
                                    <option>1 = Si</option>
                                    <option>2 = No</option>

                                </select>
                            </div>
                        </div>
                        <div class="col col-12 col-sm-3">
                            <div class="form-group">
                                <label>Condición de discapacidad</label>
                                <select class="form-control" id="slcDiscapacidadAC">
                                    <option value="-1" disabled selected>Seleccione</option>
                                    <option>1 = Si</option>
                                    <option>2 = No</option>
                                </select>
                            </div>
                        </div>
                        <div class="col col-12 col-sm-3">
                            <div class="form-group">
                                <label>Grupo étnico</label>
                                <select class="form-control" id="slcGrupoEtnicoAC">
                                    <option value="-1" disabled selected>Seleccione</option>
                                    <option>1 = Indígena</option>
                                    <option>2 = Gitano</option>
                                    <option>3 = Raizal</option>
                                    <option>4 = Palenquero</option>
                                    <option>5 = Negro, Mulato ó Afrocolombiano</option>
                                    <option>6 =Sin pertenencia étnica</option>
                                </select>
                            </div>
                        </div>
                        <div class="col col-12 col-sm-3">
                            <div class="form-group">
                                <label>Correo Eletrónico</label>
                                <input autocomplete="off" type="text" value="" class="form-control" id="txtEmailAC" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="row justify-content-center mt-3">
            <div class="col col-12 col-xl-11 row">
                <div class="d-flex justify-content-between col col-12 col-xl-12">
                    <button type="button" class="btn btn-danger" id="btnCancelar">Cancelar</button>
                    <button type="button" class="btn btn-success" id="btnRegistrar">Registrar</button>
                </div>
            </div>
        </div>
    </div>
    <script src="js/RegistroDiarioVacunacionJS.js"></script>
</asp:Content>
