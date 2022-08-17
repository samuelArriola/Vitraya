<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Generales_1._0.Log_in.ResetPassword" MaintainScrollPositionOnPostback="true" %>

<% Server.ScriptTimeout = 3600; %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <title>Clinica Crecer | Restaurar contraseña </title>

    <!-- Bootstrap -->
    <link href="../Home/dashboard/vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link href="../Home/dashboard/vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <!-- NProgress -->
    <link href="../Home/dashboard/vendors/nprogress/nprogress.css" rel="stylesheet" />
    <!-- Animate.css -->
    <link href="../Home/dashboard/vendors/animate.css/animate.min.css" rel="stylesheet" />

    <!-- Custom Theme Style -->
    <link href="../Home/dashboard/build/css/custom.min.css" rel="stylesheet" />
    <!-- PNotify -->
    <link href="../Home/dashboard/vendors/pnotify/dist/pnotify.css" rel="stylesheet">
    <link href="../Home/dashboard/vendors/pnotify/dist/pnotify.buttons.css" rel="stylesheet">
    <link href="../Home/dashboard/vendors/pnotify/dist/pnotify.nonblock.css" rel="stylesheet">

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />

    <style>
        input[type=text], input[type=password] {
            border: 1px solid #808080;
        }
    </style>

    <style>
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #fff;
            border: 3px solid #ccc;
            padding: 10px;
            width: 300px;
        }


        .overlay {
            position: fixed;
            z-index: 100;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: rgba(170, 170, 170, 0.73);
            filter: alpha(opacity=80);
            opacity: 0.8;
        }

        .overlayContent {
            z-index: 1001;
            margin: 250px auto;
            width: 200px;
            height: 200px;
        }

            .overlayContent h2 {
                font-size: 20px;
                font-weight: bold;
                color: #000;
            }

            .overlayContent img {
                width: 200px;
                height: 200px;
            }
    </style>

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
            height: 100px;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
        }
    </style>
</head>

<body class="login">
    <form id="form1" runat="server">
        <div>
            <a class="hiddenanchor" id="signup"></a>
            <a class="hiddenanchor" id="signin"></a>



            <%--
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>

            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="login_wrapper">
                <div class="animate form login_form">
                    <section class="login_content">
                        <form>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="reset_pass" Style="color: transparent;">Lost your password?</asp:LinkButton>
                                CancelControlID="Button3" BackgroundCssClass="modalBackground">
                            <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" Style="display: none; height: 50%; width: 80%">
                                <div class="col-md-12 col-sm-12">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="col-md-12 col-sm-12" id="solicitar" runat="server">
                                                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="Ingrese su usuario"></asp:TextBox>
                                                <br />
                                                <br />
                                                <asp:Button ID="Button2" runat="server" Text="solicitar cambio de contraseña" CssClass="btn btn-info" OnClick="Button2_Click" />
                                            </div>
                                            <div class="col-md-12 col-sm-12" runat="server" id="contraseña">
                                                <div class="col-md-12 col-sm-12 " id="Div1" runat="server">
                                                    <div class="alert alert-success" role="alert">
                                                        <h4 class="alert-heading">Success!</h4>
                                                        <asp:Label ID="Label2" runat="server" Text="Usted ah solicitado un cambio de contraseña tiene un limite maximo de una hora para poder efecutar el cambio,
                                            en caso de que se agote el tiempo limite no se podra realizar el cambio, Se eh enviado un correo electronico para poder realizar el cambio de contraseña"></asp:Label>
                                                        <hr>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12 col-sm-12 " id="requerido" runat="server">
                                                <div class="alert alert-danger" role="alert">
                                                    <h4 class="alert-heading">Warning!</h4>
                                                    <asp:Label ID="Label4" runat="server" Text="Label" Style="font-size: 15px; font-weight: bold;"></asp:Label>
                                                    <hr>
                                                </div>
                                            </div>
                                            <div class="col-md-12 col-sm-12 " id="Div2" runat="server">
                                                <div class="alert alert-danger" role="alert">
                                                    <h4 class="alert-heading">Warning!</h4>
                                                    <asp:Label ID="Label1" runat="server" Text="Limite de tiempo expirado" Style="font-size: 15px; font-weight: bold;"></asp:Label>
                                                    <hr>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdateProgress runat="server" ID="Progess" AssociatedUpdatePanelID="UpdatePanel3">
                                        <ProgressTemplate>
                                            <div class="overlay" />
                                            <div class="overlayContent">
                                                <h2>Cargando por favor espere...</h2>
                                                <img src="../Image/09b24e31234507.564a1d23c07b4.gif" alt="Loading" border="1" />
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </div>
                                <br />
                                <br />
                                <div>
                                    <asp:Button ID="Button4" runat="server" Text="Cerrar" CssClass="btn btn-success" OnClick="Button3_Click" />
                                </div>
                                <div class="col-md-12 col-sm-12">
                                    <asp:Button ID="Button3" runat="server" Text="Cerrar" CssClass="btn btn-light" OnClick="Button3_Click" Enabled="false" Style="color: transparent; cursor: auto" />
                                </div>
                            </asp:Panel>

                            <div class="separator">
                                <div>
                                    <p>©2020 All Rights Reserved. Privacy and Terms.</p>
                                </div>
                            </div>
                        </form>
                    </section>
                    <%-- 
                string fecha1 = DateTime.Now.ToString();
                string fecha2 = Encriptacion.Decrypt(HttpUtility.UrlDecode(Request.QueryString["ParametersQueryValidation"]).ToString());

                int resultado = DateTime.Compare(DateTime.Parse(fecha1), DateTime.Parse(fecha2));

                System.Windows.Forms.MessageBox.Show(resultado.ToString()); --%>
                </div>
            </div>

            <%-- </ContentTemplate>
</asp:UpdatePanel>--%>
        </div>
        <!-- jQuery -->
        <script src="../Home/dashboard/vendors/jquery/dist/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="../Home/dashboard/vendors/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
        <!-- FastClick -->
        <script src="../Home/dashboard/vendors/fastclick/lib/fastclick.js"></script>
        <!-- NProgress -->
        <script src="../Home/dashboard/vendors/nprogress/nprogress.js"></script>
        <!-- Chart.js -->
        <script src="../Home/dashboard/vendors/Chart.js/dist/Chart.min.js"></script>
        <!-- gauge.js -->
        <script src="../Home/dashboard/vendors/gauge.js/dist/gauge.min.js"></script>
        <!-- bootstrap-progressbar -->
        <script src="../Home/dashboard/vendors/bootstrap-progressbar/bootstrap-progressbar.min.js"></script>
        <!-- iCheck -->
        <script src="../Home/dashboard/vendors/iCheck/icheck.min.js"></script>
        <!-- Skycons -->
        <script src="../Home/dashboard/vendors/skycons/skycons.js"></script>
        <!-- Flot -->
        <script src="../Home/dashboard/vendors/Flot/jquery.flot.js"></script>
        <script src="../Home/dashboard/vendors/Flot/jquery.flot.pie.js"></script>
        <script src="../Home/dashboard/vendors/Flot/jquery.flot.time.js"></script>
        <script src="../Home/dashboard/vendors/Flot/jquery.flot.stack.js"></script>
        <script src="../Home/dashboard/vendors/Flot/jquery.flot.resize.js"></script>
        <!-- Flot plugins -->
        <script src="../Home/dashboard/vendors/flot.orderbars/js/jquery.flot.orderBars.js"></script>
        <script src="../Home/dashboard/vendors/flot-spline/js/jquery.flot.spline.min.js"></script>
        <script src="../Home/dashboard/vendors/flot.curvedlines/curvedLines.js"></script>
        <!-- DateJS -->
        <script src="../Home/dashboard/vendors/DateJS/build/date.js"></script>
        <!-- JQVMap -->
        <script src="../Home/dashboard/vendors/jqvmap/dist/jquery.vmap.js"></script>
        <script src="../Home/dashboard/vendors/jqvmap/dist/maps/jquery.vmap.world.js"></script>
        <script src="../Home/dashboard/vendors/jqvmap/examples/js/jquery.vmap.sampledata.js"></script>
        <!-- bootstrap-daterangepicker -->
        <script src="../Home/dashboard/vendors/moment/min/moment.min.js"></script>
        <script src="../Home/dashboard/vendors/bootstrap-daterangepicker/daterangepicker.js"></script>

        <!-- jquery.inputmask -->
        <script src="../vendors/jquery.inputmask/dist/min/jquery.inputmask.bundle.min.js"></script>
        <!-- jQuery Knob -->
        <script src="../vendors/jquery-knob/dist/jquery.knob.min.js"></script>


        <!-- Custom Theme Scripts -->
        <script src="../Home/dashboard/build/js/custom.min.js"></script>
        <!-- PNotify -->
        <script src="../Home/dashboard/vendors/pnotify/dist/pnotify.js"></script>
        <script src="../Home/dashboard/vendors/pnotify/dist/pnotify.buttons.js"></script>
        <script src="../Home/dashboard/vendors/pnotify/dist/pnotify.nonblock.js"></script>

        <%--    <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.0.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.22/jquery-ui.js"></script>    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script> 
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>

        <script type="text/javascript">
            function success() {
                PNotify.prototype.options.styling = "bootstrap3";
                PNotify.prototype.options.styling = "jqueryui";
                PNotify.prototype.options.styling = "fontawesome";
                new PNotify({
                    title: 'INFORMATION',
                    type: 'info',
                    text: 'Item Retrieved Successfully.',
                    after_init: function (notice) {
                        notice.attention('bounce');
                    }
                });
            }
        </script>
        <script type="text/javascript">         
            $(document).ready(function () {
                $('#show_password').hover(function show() {
                    //Cambiar el atributo a texto
                    $('#TextBox2').attr('type', 'text');
                    //$('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
                },
                    function () {
                        //Cambiar el atributo a contraseña
                        $('#TextBox2').attr('type', 'password');
                        //$('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
                    });
                //CheckBox mostrar contraseña
                $('#ShowPassword').click(function () {
                    $('#TextBox2').attr('type', $(this).is(':checked') ? 'text' : 'password');
                });
            });

            function error() {
                new PNotify({
                    title: 'Oh No!',
                    text: 'Usuario Invalido.',
                    type: 'error',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }

            function error2() {
                new PNotify({
                    title: 'Oh No!',
                    text: 'Complete los campos requeridos.',
                    type: 'error',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
            function nuevo() {
                var counter = 0;
                counter++;
                console.log(counter);
            }
            function cerrar(e) {
                setTimeout(function () {
                    //window.close("Login.aspx");
                    e.ctrlKey && e.keyCode == 87
                }, 2000);
            }
        </script>

        <%--<script>
          $(function () {
              new PNotify({
                  title: 'Oh No!',
                  text: 'Something terrible happened.',
                  type: 'error',
                  styling: 'bootstrap3'
              });
          });
      </script>--%>
        <div runat="server" visible="false">
            <asp:Label ID="Label5" runat="server" Text="Label" Style="color: transparent"></asp:Label>
            <asp:Label ID="Label6" runat="server" Text="Label" Style="color: transparent"></asp:Label>
        </div>
    </form>
</body>
</html>

