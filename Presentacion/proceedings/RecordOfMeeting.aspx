<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecordOfMeeting.aspx.cs" Inherits="Generales_1._0.Home.dashboard.production.screens.proceedings.RecordOfMeeting" MaintainScrollPositionOnPostback="true" %>
<% Server.ScriptTimeout = 3600; %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>DataTables | Gentelella</title>

    <!-- Bootstrap -->
 <%--   <link href="cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css">
    <link href="../../../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="../../../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">
    <!-- NProgress -->
    <link href="../../../vendors/nprogress/nprogress.css" rel="stylesheet">
    <!-- iCheck -->
    <link href="../../../vendors/iCheck/skins/flat/green.css" rel="stylesheet">
    <!-- Datatables -->
    
    <link href="../../../vendors/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet">
    <link href="../../../vendors/datatables.net-buttons-bs/css/buttons.bootstrap.min.css" rel="stylesheet">
    <link href="../../../vendors/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css" rel="stylesheet">
    <link href="../../../vendors/datatables.net-responsive-bs/css/responsive.bootstrap.min.css" rel="stylesheet">
    <link href="../../../vendors/datatables.net-scroller-bs/css/scroller.bootstrap.min.css" rel="stylesheet">

    <!-- Custom Theme Style -->
    <link href="../../../build/css/custom.min.css" rel="stylesheet"/>--%>
    <link href="../../../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet"/>
        <asp:PlaceHolder runat="server">
            <%: Styles.Render("~/bundles/WebformsCSS") %>
        </asp:PlaceHolder>
    
    <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.0.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.22/jquery-ui.js"></script>
  </head>

  <body class="nav-md">
   <form id="form1" runat="server">
      <div class="container body">
         <div class="main_container">
            <div class="col-md-3 left_col">
               <div class="left_col scroll-view">
                  <div class="navbar nav_title" style="border: 0;">
                     <a href="../index.aspx" class="site_title"><i class="fa fa-hospital-o"></i> <span>Clinica Crecer</span></a>
                  </div>
                  <div class="clearfix"></div>
                  <!-- menu profile quick info -->
                  <div class="profile clearfix">
                     <div class="profile_pic">
                        <asp:Image ID="Image1" runat="server" class="img-circle profile_img"/>
                     </div>
                     <div class="profile_info">
                        <span>Welcome,</span>
                        <h2>
                           <asp:Label ID="Label2" runat="server" Text="" style="font-weight: bold; font-size: 15px"></asp:Label>
                        </h2>
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
                              <asp:Image ID="Image2" runat="server" />
                              <asp:Label ID="Label6" runat="server" Text="" style="font-weight: bold; font-size: 15px"></asp:Label>
                           </a>
                           <div class="dropdown-menu dropdown-usermenu pull-right" aria-labelledby="navbarDropdown">
                              <a class="dropdown-item"  href="../profile.aspx"> Profile</a>
                              <asp:LinkButton ID="LinkButton1" runat="server" CssClass="dropdown-item" OnClick="LinkButton1_Click"><i class="fa fa-sign-out pull-right"></i>Log Out</asp:LinkButton>
                           </div>
                        </li>
                     </ul>
                  </nav>
               </div>
            </div>
            <!-- page content -->
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
           
         </div>
      </div>
      <!-- footer content -->
      <footer>
      <div class="pull-right">
      Ⓒ Clínica Crecer 2020. Todos los derechos reservados.
      </div>
      <div class="clearfix"></div>
      </footer>
      <!-- /footer content -->
      <!-- jQuery -->
      <%--    <script src="../../../vendors/jquery/dist/jquery.min.js"></script>
         <!-- Bootstrap -->
         <script src="../../../vendors/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
         <!-- FastClick -->
         <script src="../../../vendors/fastclick/lib/fastclick.js"></script>
         <!-- NProgress -->
         <script src="../../../vendors/nprogress/nprogress.js"></script>
         <!-- iCheck -->
         <script src="../../../vendors/iCheck/icheck.min.js"></script>
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
      <asp:Label ID="Label14" runat="server" Visible="false"></asp:Label>
      <asp:Label ID="Label5" runat="server" Text="Label" Visible="false"></asp:Label>
      <asp:Label ID="Label8" runat="server" Text="Label" Visible="false"></asp:Label>
      <asp:Label ID="Label3" runat="server" Text="id" Visible="False"></asp:Label>
      <asp:Label ID="Label4" runat="server" Text="Label" Visible="false"></asp:Label>
      </ContentTemplate>
      </asp:UpdatePanel>
      <asp:TextBox ID="TextBox4" runat="server"  Visible="False"></asp:TextBox>
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
      <asp:PlaceHolder runat="server">
         <%: Scripts.Render("~/bundles/Webformsjs") %>
      </asp:PlaceHolder>
      <script type="text/javascript">
          function error1() {
              new PNotify({
                  title: 'Oh no!',
                  text: 'No se enmcontraron temas!',
                  type: 'error',
                  styling: 'bootstrap3',
                  delay: 1000
              });
          }
          function exito1() {
              new PNotify({
                  title: 'Success',
                  text: 'Registro exitoso',
                  type: 'success',
                  styling: 'bootstrap3',
                  delay: 1000
              });
          }
      </script>
   </form>
</body>
</html>
