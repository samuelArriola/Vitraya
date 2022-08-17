<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateUsuarios.aspx.cs" Inherits="Generales_1._0.Home.dashboard.production.screens.general.CreateUsuarios" MaintainScrollPositionOnPostback="true" %>

<% Server.ScriptTimeout = 3600; %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../../../../../Image/logocrecer2.png" rel="icon" type="image/png" />

    <title>Clinica Crecer | Gestionar Usuarios</title>

    <%-- <!-- Bootstrap -->
    <link href="../../../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="../../../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <!-- NProgress -->
    <link href="../../../vendors/nprogress/nprogress.css" rel="stylesheet">
    <!-- bootstrap-daterangepicker -->
    <link href="../../../vendors/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet">
    <!-- bootstrap-datetimepicker -->
    <link href="../../../vendors/bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.css" rel="stylesheet">
    <!-- Ion.RangeSlider -->
    <link href="../../../vendors/normalize-css/normalize.css" rel="stylesheet">
    <link href="../../../vendors/ion.rangeSlider/css/ion.rangeSlider.css" rel="stylesheet">
    <link href="../../../vendors/ion.rangeSlider/css/ion.rangeSlider.skinFlat.css" rel="stylesheet">
    <!-- Bootstrap Colorpicker -->
    <link href="../../../vendors/mjolnic-bootstrap-colorpicker/dist/css/bootstrap-colorpicker.min.css" rel="stylesheet">

    <link href="../../../vendors/cropper/dist/cropper.min.css" rel="stylesheet">

    <!-- Custom Theme Style -->
    <link href="../../../build/css/custom.min.css" rel="stylesheet">--%>

    <link href="../../../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">

    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/bundles/WebformsCSS") %>
    </asp:PlaceHolder>


    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="//resources/demos/style.css" />
    <link href="css/CreateUserCSS.css" rel="stylesheet" />
    <%--    <link href="../../../Style/component.css" rel="stylesheet" />--%>
    <style>
        input[type=text], input[type=password], input[type=number], input[type=email] {
            border: 1px solid #808080
        }
    </style>
    <style>
        profile_pic {
            width: 200px;
            height: 300px;
            margin: 0 auto 0 auto;
            padding: 5px;
            margin-bottom: 10px;
        }

            profile_pic #Image1 {
                max-width: 100%;
            }
    </style>
</head>

<body class="nav-md">
    <form id="form1" runat="server">
        <div class="container body">
            <div class="main_container">
                <div class="col-md-3 left_col">
                    <div class="left_col scroll-view">
                        <div class="navbar nav_title" style="border: 0;">
                            <a href="../Index.aspx" class="site_title"><i class="fa fa-hospital-o"></i><span>Clinica Crecer</span></a>
                        </div>

                        <div class="clearfix"></div>

                        <!-- menu profile quick info -->
                        <div class="profile clearfix">
                            <div class="profile_pic">
                                <asp:Image ID="Image2" runat="server" class="img-circle profile_img" />
                            </div>
                            <div class="profile_info">
                                <span>Welcome,</span>
                                <h2>
                                    <asp:Label ID="Label2" runat="server" Text="" Style="font-weight: bold; font-size: 15px"></asp:Label></h2>
                            </div>
                        </div>
                        <!-- /menu profile quick info -->

                        <br />

                        <!-- sidebar menu -->
                        <div id="sidebar-menu" class="main_menu_side hidden-print main_menu">
                            <div class="menu_section">
                                <%-- <h3>General</h3>--%>
                                <ul class="nav side-menu">
                                    <li><a href="../Index.aspx"><i class="fa fa-home"></i>Home</a></li>
                                    <li id="menu_1" runat="server" visible="false"><a><i class="fa fa-desktop"></i>Generales y Seguridad<span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu">
                                            <li runat="server" id="sub_menu_1" visible="false"><a href="../general/CreateAreas.aspx">Gestionar Direcciones</a></li>
                                            <li runat="server" id="sub_menu_2" visible="false"><a href="../general/CreateCargos.aspx">Gestionar Cargos</a></li>
                                            <li runat="server" id="sub_menu_3" visible="false"><a href="../general/CreateDepartamentos.aspx">Gestionar Unidades Funcionales</a></li>
                                            <li runat="server" id="sub_menu_4" visible="false"><a href="../general/CreateEps.aspx">Gestionar Eps</a></li>
                                            <li runat="server" id="sub_menu_5" visible="false"><a href="../general/CreateUsuarios.aspx">Gestionar Usuarios</a></li>
                                            <li runat="server" id="sub_menu_6" visible="false"><a href="../general/ParametrizacionRoles.aspx">Gestionar Permisos</a></li>
                                            <li runat="server" id="sub_menu_7" visible="false"><a href="../general/Processes.aspx">Gestionar Procesos</a></li>
                                        </ul>
                                    </li>
                                    <li runat="server" id="menu_2" visible="false"><a><i class="fa fa-edit"></i>listas de chequeo<span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu">
                                            <li runat="server" id="sub_menu_8" visible="false"><a href="../lists/CreateListas.aspx">Gestionar Lista De Chequeo</a></li>
                                            <li runat="server" id="sub_menu_9" visible="false"><a href="../lists/ViewListas.aspx">Ver Lista De Chequeo</a></li>
                                            <li runat="server" id="sub_menu_10" visible="false"><a>Reporte Listas<span class="fa fa-chevron-down"></span></a>
                                                <ul class="nav child_menu">
                                                    <li class="sub_menu" runat="server"><a href="../lists/UnitReport.aspx">Reporte unitario</a>
                                                    </li>
                                                    <li class="sub_menu" runat="server"><a href="../lists/MassiveReport.aspx">Reporte masivo</a>
                                                    </li>
                                                </ul>
                                            </li>
                                        </ul>
                                    </li>
                                    <li runat="server" id="menu_3" visible="false"><a><i class="fa fa-table"></i>Gestion de Reuniones <span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu">
                                            <li runat="server" id="sub_menu_11" visible="false"><a href="../proceedings/CreationOfCommittees.aspx">Gestionar Reuniones</a></li>
                                            <li runat="server" id="sub_menu_12" visible="false"><a href="../proceedings/RecordOfMeeting.aspx">Agregar temas</a></li>
                                            <li runat="server" id="sub_menu_13" visible="false"><a href="../proceedings/RecordMinutes.aspx">Gestionar Actas</a></li>
                                        </ul>
                                    </li>
                                    <li runat="server" id="menu_4" visible="false"><a><i class="fa fa-table"></i>Planes de accion<span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu">
                                            <li runat="server" id="sub_menu_14" visible="false"><a href="../ImprovementAction/CreatePlans.aspx">Gestionar Planes de accion</a></li>
                                            <li runat="server" id="sub_menu_15" visible="false"><a href="../ImprovementAction/MyActionPlans.aspx">Mis planes de accion</a></li>
                                            <li runat="server" id="sub_menu_16" visible="false"><a href="../ImprovementAction/ApproveImprovementPlans.aspx">Seguimientos planes de accion</a></li>
                                        </ul>
                                    </li>
                                    <li runat="server" id="menu_5" visible="false"><a><i class="fa fa-home"></i>Capacitaciones <span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu">
                                            <li runat="server" id="sub_menu_17" visible="false"><a href="../trainings/AddCapacitacion.aspx">Administar mis capacitaciones</a></li>
                                            <li runat="server" id="sub_menu_18" visible="false"><a href="../trainings/Userprincipal.aspx">Mis Capacitaciones</a></li>
                                            <li runat="server" id="sub_menu_19" visible="false"><a href="../trainings/CapacitacionTerminada.aspx">Capacitaciones Terminadas</a></li>
                                            <li runat="server" id="sub_menu_20" visible="false"><a href="../trainings/AddEjeTematico.aspx">Ejes Tematicos</a></li>
                                            <li runat="server" id="sub_menu_21" visible="false"><a href="../trainings/NuevaCapacitacion.aspx">Nueva Capacitación</a></li>
                                            <li runat="server" id="sub_menu_22" visible="false"><a href="../trainings/InformesCapacitaciones.aspx">Informes</a></li>
                                        </ul>
                                    </li>
                                    <li runat="server" id="Li1" visible="false"><a><i class="fa fa-home"></i>Actas de reunión <span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu">
                                            <li runat="server" id="Li2" visible="false"><a href="../proceedings/ParametrizacionComites.aspx">Creación</a></li>
                                            <li runat="server" id="Li3" visible="false"><a href="../proceedings/CreationOfCommittees.aspx">Parametrización</a></li>
                                            <li runat="server" id="Li4" visible="false"><a href="../proceedings/Convocatorias.aspx">Convocatorias</a></li>
                                            <li runat="server" id="Li5" visible="false"><a href="../proceedings/MisActas.aspx">Mis actas</a></li>
                                        </ul>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <!-- /sidebar menu -->
                    </div>
                </div>
                <!-- top navigation -->
                <div class="top_nav">
                    <div class="nav_menu">
                        <div class="nav toggle">
                            <a id="menu_toggle"><i class="fa fa-bars"></i></a>
                        </div>
                        <nav class="nav navbar-nav">
                            <ul class=" navbar-right">
                                <li class="nav-item dropdown open" style="padding-left: 15px;">
                                    <a href="javascript:;" class="user-profile dropdown-toggle" aria-haspopup="true" id="navbarDropdown" data-toggle="dropdown" aria-expanded="false">
                                        <asp:Image ID="Image3" runat="server" /><asp:Label ID="Label9" runat="server" Text="" Style="font-weight: bold; font-size: 15px"></asp:Label>
                                    </a>
                                    <div class="dropdown-menu dropdown-usermenu pull-right" aria-labelledby="navbarDropdown">
                                        <a class="dropdown-item" href="../profile.aspx">Profile</a>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="dropdown-item" OnClick="LinkButton1_Click"><i class="fa fa-sign-out pull-right"></i>Log Out</asp:LinkButton>
                                    </div>
                                </li>
                            </ul>
                        </nav>
                    </div>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                <div class="right_col" role="main">
                    <div class="page-title">
                        <div class="title_left">
                            <h6>Comités</h6>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div class="row" id="Agregar" runat="server">
                        <div class="col-md-12 col-sm-12 ">
                            <div class="x_panel">
                                <div class="x_title">
                                    <ul class="nav navbar-right panel_toolbox">
                                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                        </li>
                                    </ul>
                                    <div class="clearfix">
                                        <h6>Nuevo Usuario</h6>
                                    </div>
                                </div>
                                <div class="x_content">
                                    <div class="row">
                                        <div class="col col-3 text-center">
                                            <div id="imagePerfil" class="d-inline-block">
                                                <label for="fuImagePerfil">&nbsp;</label>
                                            </div>
                                            <asp:FileUpload runat="server" ID="fuImagePerfil" Style="display: none" />
                                        </div>
                                        <div class="col col-9">
                                            <div class="row">
                                                <div class="col col-6">
                                                    <div class="form-group">
                                                        <asp:Label Text="Documento *" runat="server" />
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDocumento" />
                                                    </div>
                                                </div>
                                                <div class="col col-6">
                                                    <div class="form-group">
                                                        <asp:Label Text="Nombres Y Apellidos *" runat="server" />
                                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNombre" />
                                                    </div>
                                                </div>
                                                <div class="col col-6">
                                                    <div class="form-group">
                                                        <asp:Label Text="Unidad Funcional *" runat="server" />
                                                        <asp:DropDownList runat="server" ID="txtUnidadFuncional" CssClass="form-control">
                                                            <asp:ListItem Text="Seleccione" Value="-1" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col col-6">
                                                    <div class="form-group">
                                                        <asp:Label Text="Cargo *" runat="server" />
                                                        <asp:DropDownList runat="server" ID="txtCargo" CssClass="form-control">
                                                            <asp:ListItem Text="Seleccione" Value="-1" />
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col col-6">
                                                    <div class="form-group">
                                                        <asp:Label Text="Correo Electrónico *" runat="server" />
                                                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Email" ID="txtEmail" />
                                                    </div>
                                                </div>
                                                <div class="col col-6">
                                                    <div class="form-group">
                                                        <asp:Label Text="Contraseña *" runat="server" />
                                                        <asp:TextBox runat="server" CssClass="form-control" TextMode="Password" ID="txtPasssword" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col col-3">
                                            <div class="form-group">
                                                <asp:Label Text="EPS *" runat="server" />
                                                <asp:DropDownList runat="server" ID="ddlEps" CssClass="form-control">
                                                    <asp:ListItem Text="Seleccione" Value="-1" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col col-3">
                                            <div class="form-group">
                                                <asp:Label Text="Teléfono *" runat="server" />
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTelefono" />
                                            </div>
                                        </div>
                                        <div class="col col-3">
                                            <div class="form-group">
                                                <asp:Label Text="Firma *" runat="server" />
                                                <label class="form-control" for="txtFoto" id="lbFoto"></label>
                                                <asp:FileUpload runat="server" ID="txtFoto" CssClass="d-none" />
                                            </div>
                                        </div>
                                        <div class="col col-3">
                                            <div class="form-group">
                                                <asp:Label Text="Rol de Usuario *" runat="server" />
                                                <asp:DropDownList runat="server" ID="ddlRoles" CssClass="form-control">
                                                    <asp:ListItem Text="Seleccione" Value="-1" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col col-12">
                                            <div class="form-group text-right">
                                                <asp:Button Text="Crear Usuario" runat="server" CssClass="btn btn-success" ID="btnCrearUsuarios" OnClick="btnCrearUsuarios_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12 col-sm-12 ">
                            <div class="x_panel">
                                <div class="x_title">
                                    <ul class="nav navbar-right panel_toolbox">
                                        <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                        </li>
                                    </ul>
                                    <div class="clearfix">
                                        <h6>Listado de usuarios</h6>
                                    </div>
                                </div>
                                <div class="x_content">
                                    <table class="table table-hover " id="tbUsuarios">
                                        <thead>
                                            <tr>
                                                <th></th>
                                                <th>
                                                    <input type="text" class="form-control form-control-sm" name="documento" id="documento" /></th>
                                                <th>
                                                    <input type="text" class="form-control form-control-sm" name="nombre" id="nombre" /></th>
                                                <th>
                                                    <input type="text" class="form-control form-control-sm" name="cargo" id="cargo" /></th>
                                                <th>
                                                    <input type="text" class="form-control form-control-sm" name="email" id="email" /></th>
                                                <th>
                                                    <select class="form-control form-control-sm" name="estado" id="estado">
                                                        <option value="Activo" selected="selected">Activo</option>
                                                        <option value="Inactivo">Inactivo</option>
                                                    </select>
                                                </th>
                                                <th></th>
                                            </tr>
                                            <tr>
                                                <th>#</th>
                                                <th>Documento</th>
                                                <th>Nombre</th>
                                                <th>Cargo</th>
                                                <th>Correo Eletrónico</th>
                                                <th>Estado</th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tbdUsuarios">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>

                    </div>
                    
                </div>
                <footer>
                    <div class="pull-right">
                        <span class="fa fa-copyright"></span>Clínica Crecer 2020. Todos los derechos reservados.
                    </div>
                    <div class="clearfix"></div>
                </footer>
                <!-- /footer content -->
            </div>
        </div>
        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/Webformsjs") %>
        </asp:PlaceHolder>
        <script src="js/CreateUserJS.js"></script>
    </form>
</body>
</html>
