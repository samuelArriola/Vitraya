<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateEps.aspx.cs" Inherits="Generales_1._0.Home.dashboard.production.screens.general.CreateEps" MaintainScrollPositionOnPostback="true" %>

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

    <title>Clinica Crecer | Gestionar Eps</title>

    <%--  <!-- Bootstrap -->
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

    <style>
        input[type=text] {
            border: 1px solid #808080
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
                                <asp:Image ID="Image1" runat="server" class="img-circle profile_img" />
                            </div>
                            <div class="profile_info">
                                <span>Welcome,</span>
                                <h2>
                                    <asp:Label ID="Label6" runat="server" Text="" Style="font-weight: bold; font-size: 15px"></asp:Label></h2>
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
                                    <li id="menu_1" runat="server" visible=false><a><i class="fa fa-desktop"></i>Generales y Seguridad<span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu">
                                            <li runat="server" id ="sub_menu_1" visible="false"><a href="../general/CreateAreas.aspx">Gestionar Direcciones</a></li>
                                            <li runat="server" id ="sub_menu_2" visible="false"><a href="../general/CreateCargos.aspx">Gestionar Cargos</a></li>
                                            <li runat="server" id ="sub_menu_3" visible="false"><a href="../general/CreateDepartamentos.aspx">Gestionar Unidades Funcionales</a></li>
                                            <li runat="server" id ="sub_menu_4" visible="false"><a href="../general/CreateEps.aspx">Gestionar Eps</a></li>
                                            <li runat="server" id ="sub_menu_5" visible="false"><a href="../general/CreateUsuarios.aspx">Gestionar Usuarios</a></li>
                                            <li runat="server" id ="sub_menu_6" visible="false"><a href="../general/ParametrizacionRoles.aspx">Gestionar Permisos</a></li>
                                            <li runat="server" id ="sub_menu_7" visible="false"><a href="../general/Processes.aspx">Gestionar Procesos</a></li>
                                        </ul>
                                    </li>
                                    <li runat="server" id="menu_2"  visible="false"><a><i class="fa fa-edit"></i>listas de chequeo<span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu">
                                            <li runat="server" id="sub_menu_8" visible="false"><a href="../lists/CreateListas.aspx">Gestionar Lista De Chequeo</a></li>
                                            <li runat="server" id="sub_menu_9" visible="false"><a href="../lists/ViewListas.aspx">Ver Lista De Chequeo</a></li>
                                            <li runat="server" id="sub_menu_10" visible="false"><a>Reporte Listas<span class="fa fa-chevron-down"></span></a>
                                                <ul class="nav child_menu">
                                                    <li class="sub_menu" runat="server" ><a href="../lists/UnitReport.aspx" >Reporte unitario</a>
                                                    </li>
                                                    <li class="sub_menu" runat="server" ><a href="../lists/MassiveReport.aspx" >Reporte masivo</a>
                                                    </li>
                                                </ul>
                                            </li>
                                        </ul>
                                    </li>
                                    <li runat="server" id="menu_3"  visible="false"><a><i class="fa fa-table"></i>Gestion de Reuniones <span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu">
                                            <li runat="server" id="sub_menu_11"  visible="false"><a href="../proceedings/CreationOfCommittees.aspx">Gestionar Reuniones</a></li>
                                            <li runat="server" id="sub_menu_12"  visible="false"><a href="../proceedings/RecordOfMeeting.aspx">Agregar temas</a></li>
                                            <li runat="server" id="sub_menu_13" visible="false"><a href="../proceedings/RecordMinutes.aspx">Gestionar Actas</a></li>
                                        </ul>
                                    </li>
                                    <li runat="server" id="menu_4"  visible="false"><a><i class="fa fa-table"></i>Planes de accion<span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu">
                                            <li runat="server" id="sub_menu_14" visible="false"><a href="../ImprovementAction/CreatePlans.aspx">Gestionar Planes de accion</a></li>
                                            <li runat="server" id="sub_menu_15" visible="false"><a href="../ImprovementAction/MyActionPlans.aspx">Mis planes de accion</a></li>
                                            <li runat="server" id="sub_menu_16" visible="false"><a href="../ImprovementAction/ApproveImprovementPlans.aspx">Seguimientos planes de accion</a></li>
                                        </ul>
                                    </li>
                                    <li runat="server" id="menu_5"  visible="false"><a><i class="fa fa-home"></i>Capacitaciones <span class="fa fa-chevron-down"></span></a>
                                        <ul class="nav child_menu">
                                            <li runat="server" id="sub_menu_17" visible="false"><a href="../trainings/AddCapacitacion.aspx">Administar mis capacitaciones</a></li>
                                            <li runat="server" id="sub_menu_18" visible="false"><a href="../trainings/Userprincipal.aspx">Mis Capacitaciones</a></li>
                                            <li runat="server" id="sub_menu_19" visible="false"><a href="../trainings/CapacitacionTerminada.aspx">Capacitaciones Terminadas</a></li>
                                            <li runat="server" id="sub_menu_20" visible="false"><a href="../trainings/AddEjeTematico.aspx">Ejes Tematicos</a></li>
                                            <li runat="server" id="sub_menu_21" visible="false"><a href="../trainings/NuevaCapacitacion.aspx">Nueva Capacitación</a></li>
                                            <li runat="server" id="sub_menu_22" visible="false"><a href="../trainings/InformesCapacitaciones.aspx">Informes</a></li>
                                        </ul>
                                    </li>
                                    <li runat="server" id="Li1"  visible="false"><a><i class="fa fa-home"></i>Actas de reunión <span class="fa fa-chevron-down"></span></a>
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
                                        <asp:Image ID="Image2" runat="server" /><asp:Label ID="Label9" runat="server" Text="" Style="font-weight: bold; font-size: 15px"></asp:Label>
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
                <!-- /top navigation -->

                <!-- page content -->
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <div class="right_col" role="main">
                    <div class="">
                        <div class="page-title">
                            <div class="title_left">
                                <h3>Gestion de eps</h3>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="row" id="agregar" runat="server">
                            <div class="col-md-12 col-sm-12 ">
                                <div class="x_panel">
                                    <div class="x_title">
                                        <%--<h2>Form Design <small>different form elements</small></h2>--%>
                                        <ul class="nav navbar-right panel_toolbox">
                                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                            </li>
                                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                                            </li>
                                        </ul>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content">
                                        <br />
                                        <form id="demo-form2" data-parsley-validate class="form-horizontal form-label-left">
                                            <div class="item form-group">
                                                <label class="col-form-label col-md-3 col-sm-3 label-align" for="first-name">
                                                    nombre de la eps<span class="required">*</span>
                                                </label>
                                                <div class="col-md-6 col-sm-6 ">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="ln_solid"></div>
                                            <div class="item form-group">
                                                <div class="col-md-6 col-sm-6 offset-md-3">
                                                    <asp:Button ID="Button1" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="Button1_Click" />
                                                </div>
                                            </div>
                                        </form>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 ">
                                <div class="x_panel">
                                    <div class="x_title">
                                        <%--                    <h2>Responsive example<small>Users</small></h2>--%>
                                        <ul class="nav navbar-right panel_toolbox">
                                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                            </li>
                                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                                            </li>
                                        </ul>
                                        <div class="clearfix"></div>
                                    </div>
                                    <div class="x_content">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="card-box table-responsive">
                                                    <div id="datatable-responsive_wrapper" class="dataTables_wrapper container-fluid dt-bootstrap no-footer">
                                                        <div class="title_left">
                                                            <div id="datatable_filter" class="dataTables_filter">
                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                    <ContentTemplate>
                                                                        <label>
                                                                            Search:<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input-sm" OnTextChanged="TextBox2_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                            <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                                                        </label>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                        <div class="row" style="overflow: scroll; height: 800px" id="DivAreaTrabajo">
                                                            <input type="hidden" id="divPosition" name="divPosition" />
                                                            <div class="col-sm-12">
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:GridView ID="GridEps" runat="server" AutoGenerateColumns="False" DataKeyNames="GnCodEps" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="table table-striped table-bordered dt-responsive nowrap dataTable no-footer dtr-inline" OnPageIndexChanging="GridEps_PageIndexChanging" OnRowCancelingEdit="GridEps_RowCancelingEdit" OnRowDeleting="GridEps_RowDeleting" OnRowEditing="GridEps_RowEditing" OnRowUpdating="GridEps_RowUpdating">
                                                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                                                            <RowStyle Height="50px" Width="90px" />
                                                                            <AlternatingRowStyle Height="50px" Width="90px" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Codigo">
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="TextBox3" runat="server" ReadOnly="true" Text='<%# Bind("GnCodEps") %>'></asp:TextBox>
                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("GnCodEps") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Nombre">
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("GnNomEps") %>' CssClass="form-control"></asp:TextBox>
                                                                                    </EditItemTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("GnNomEps") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:CommandField ButtonType="Image" ShowEditButton="true" EditImageUrl="~/Image/Edit.png" UpdateImageUrl="~/Image/Edit.png" CancelImageUrl="~/Image/cancelar.png">
                                                                                    <ControlStyle Width="20px" />
                                                                                </asp:CommandField>
                                                                                <asp:CommandField ButtonType="Image" ShowDeleteButton="true" DeleteImageUrl="~/Image/Delete.png">
                                                                                    <ControlStyle Width="20px" />
                                                                                </asp:CommandField>
                                                                            </Columns>
                                                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                                                            <FooterStyle BackColor="#CCCCCC" />
                                                                            <HeaderStyle BackColor="#2A3F54" Font-Bold="True" ForeColor="White" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                                                        </asp:GridView>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- /page content -->

                <!-- footer content -->
                <footer>
                    <div class="pull-right">
                        <span class="fa fa-copyright"></span>Clínica Crecer 2020. Todos los derechos reservados.
                    </div>
                    <div class="clearfix"></div>
                </footer>
                <!-- /footer content -->
            </div>
        </div>

        <%--  <!-- jQuery -->
    <script src="../../../vendors/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap -->
    <script src="../../../vendors/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
    <!-- FastClick -->
    <script src="../../../vendors/fastclick/lib/fastclick.js"></script>
    <!-- NProgress -->
    <script src="../../../vendors/nprogress/nprogress.js"></script>
    <!-- bootstrap-daterangepicker -->
    <script src="../../../vendors/moment/min/moment.min.js"></script>
    <script src="../../../vendors/bootstrap-daterangepicker/daterangepicker.js"></script>
    <!-- bootstrap-datetimepicker -->    
    <script src="../../../vendors/bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js"></script>
    <!-- Ion.RangeSlider -->
    <script src="../../../vendors/ion.rangeSlider/js/ion.rangeSlider.min.js"></script>
    <!-- Bootstrap Colorpicker -->
    <script src="../../../vendors/mjolnic-bootstrap-colorpicker/dist/js/bootstrap-colorpicker.min.js"></script>
    <!-- jquery.inputmask -->
    <script src="../../../vendors/jquery.inputmask/dist/min/jquery.inputmask.bundle.min.js"></script>
    <!-- jQuery Knob -->
    <script src="../../../vendors/jquery-knob/dist/jquery.knob.min.js"></script>
    <!-- Cropper -->
    <script src="../../../vendors/cropper/dist/cropper.min.js"></script>
        <!-- Datatables -->
    <script src="../../../vendors/datatables.net/js/jquery.dataTables.min.js"></script>
    <script src="../../../vendors/datatables.net-bs/js/dataTables.bootstrap.min.js"></script>
    <script src="../../../vendors/datatables.net-buttons/js/dataTables.buttons.min.js"></script>
    <script src="../../../vendors/datatables.net-buttons-bs/js/buttons.bootstrap.min.js"></script>
    <script src="../../../vendors/datatables.net-buttons/js/buttons.flash.min.js"></script>
    <script src="../../../vendors/datatables.net-buttons/js/buttons.html5.min.js"></script>
    <script src="../../../vendors/datatables.net-buttons/js/buttons.print.min.js"></script>
    <script src="../../../vendors/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js"></script>
    <script src="../../../vendors/datatables.net-keytable/js/dataTables.keyTable.min.js"></script>
    <script src="../../../vendors/datatables.net-responsive/js/dataTables.responsive.min.js"></script>
    <script src="../../../vendors/datatables.net-responsive-bs/js/responsive.bootstrap.js"></script>
    <script src="../../../vendors/datatables.net-scroller/js/dataTables.scroller.min.js"></script>
    <script src="../../../vendors/jszip/dist/jszip.min.js"></script>
    <script src="../../../vendors/pdfmake/build/pdfmake.min.js"></script>
    <script src="../../../vendors/pdfmake/build/vfs_fonts.js"></script>

    <!-- Custom Theme Scripts -->
    <script src="../../../build/js/custom.min.js"></script>--%>

        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/Webformsjs") %>
        </asp:PlaceHolder>

        <script type="text/javascript">
            var div = document.getElementById("DivAreaTrabajo"); var divPosition = document.getElementById("divPosition");
            var position = parseInt('<%=Request.Form["divPosition"] %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                divPosition.value = div.scrollTop;
            };
        </script>

        <script>
            function error() {
                new PNotify({
                    title: "Oh No",
                    text: "Esta Eps ya se encuentra registrada",
                    type: "error",
                    styling: "bootstrap3",
                    delay: 1000
                });
            }

            function Error2() {
                new PNotify({
                    title: "Oh No",
                    text: "Ingrese el nombre de la Eps",
                    type: "error",
                    styling: "bootstrap3",
                    delay: 1000
                });
            }

            function Exito() {
                new PNotify({
                    title: "Success",
                    text: "Registro exitoso",
                    type: "success",
                    styling: "bootstrap3",
                    delay: 1000
                });
            }
        </script>

        <!-- Initialize datetimepicker -->
        <%--<script  type="text/javascript">
   $(function () {
                $('#myDatepicker').datetimepicker();
            });
    
    $('#myDatepicker2').datetimepicker({
        format: 'DD.MM.YYYY'
    });
    
    $('#myDatepicker3').datetimepicker({
        format: 'hh:mm A'
    });
    
    $('#myDatepicker4').datetimepicker({
        ignoreReadonly: true,
        allowInputToggle: true
    });

    $('#datetimepicker6').datetimepicker();
    
    $('#datetimepicker7').datetimepicker({
        useCurrent: false
    });
    
    $("#datetimepicker6").on("dp.change", function(e) {
        $('#datetimepicker7').data("DateTimePicker").minDate(e.date);
    });
    
    $("#datetimepicker7").on("dp.change", function(e) {
        $('#datetimepicker6').data("DateTimePicker").maxDate(e.date);
    });
</script>--%>
        <div runat="server" visible="false">
            <asp:Label ID="Label1" runat="server" Text="Label" Style="color: transparent"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="Label" Style="color: transparent"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="Label" Style="color: transparent"></asp:Label>
            <asp:Label ID="Label16" runat="server" Text="Label" Style="color: transparent"></asp:Label>
            <asp:Label ID="Label15" runat="server" Text="Label" Style="color: transparent"></asp:Label>
            <asp:Label ID="Label5" runat="server" Text="Label" Style="color: transparent"></asp:Label>
        </div>
    </form>
</body>
</html>
