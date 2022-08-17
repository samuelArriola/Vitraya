<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Presentacion.ResetPassword.ChangePassword" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../../../../Image/logocrecer2.png" rel="icon" type="image/png" />
    <link href="../build/css/styles.css" rel="stylesheet" />
    <title>Clinica Crecer | Login </title>

    <!-- Bootstrap -->
    <link href="../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Font Awesome -->
    <link href="../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <!-- NProgress -->
    <link href="../vendors/nprogress/nprogress.css" rel="stylesheet" />
    <!-- Animate.css -->
    <link href="../vendors/animate.css/animate.min.css" rel="stylesheet" />

    <!-- Custom Theme Style -->
    <link href="../build/css/custom.min.css" rel="stylesheet" />
    <!-- PNotify -->
    <link href="../vendors/pnotify/dist/pnotify.css" rel="stylesheet">
    <link href="../vendors/pnotify/dist/pnotify.buttons.css" rel="stylesheet">
    <link href="../vendors/pnotify/dist/pnotify.nonblock.css" rel="stylesheet">

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <link href="../build/css/styles.css" rel="stylesheet" />

    <style>
       
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
    </style>

    <style type="text/css">
        input:-webkit-autofill,
        input:-webkit-autofill:hover,
        input:-webkit-autofill:focus,
        input:-webkit-autofill:active {
            -webkit-box-shadow: 0 0 0 30px white inset !important;
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
            margin: 260px auto;
            width: 210px;
            height: 210px;
        }

            .overlayContent h2 {
                font-size: 20px;
                font-weight: bold;
                color: #000;
            }

            .overlayContent img {
                width: 200px;
                height: 200px;
                border-right: solid 1px
            }

        .search-p i {
            padding: 7px;
            background: #eee;
            margin: 0;
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
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="login_wrapper">
                <div class="animate form login_form">

                    <asp:UpdatePanel runat="server" ID="PanelLogin">
                        <ContentTemplate>
                            <section class="login_content">
                                <form>
                                    <h1><i class="fa fa-hospital-o mr-2"></i>Clinica Crecer</h1>
                                    <div>
                                        <div class="form-group">
                                            <asp:TextBox autocomplete="something-new" ID="txtUser" runat="server" placeholder="Usuario" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox autocomplete="something-new" ID="txtPass" runat="server" placeholder="Nueva contraseña" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        </div>
                                        <div class="form-group ">
                                            <asp:TextBox autocomplete="something-new" ID="txtRepeatPass" runat="server" placeholder="Repita la nueva contraseña" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />

                                    <br />
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnValidarC" runat="server" Text="Validar" CssClass="btn btn-info submit" OnClick="btnValidar_Click" />
                                            <div class="mt-3"></div>
                                            <a href="../Index.aspx" class="mt-3">Iniciar sesión</a>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div>
                                    </div>
                                    <div class="clearfix"></div>

                                    <div class="separator">
                                        <div>
                                            <p>©2020 All Rights Reserved. Privacy and Terms.</p>
                                        </div>
                                    </div>
                                </form>
                            </section>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="PanelLogin" DisplayAfter="0">
                        <ProgressTemplate>
                            <div class="overlay" />
                            <div class="overlayContent">
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
            </div>

            <%-- </ContentTemplate>
</asp:UpdatePanel>--%>
        </div>
        <!-- jQuery -->
        <script src="../vendors/jquery/dist/jquery.min.js"></script>
        <!-- Bootstrap -->
        <!-- PNotify -->
        <script src="../vendors/pnotify/dist/pnotify.js"></script>
        <script src="../vendors/pnotify/dist/pnotify.buttons.js"></script>
        <script src="../vendors/pnotify/dist/pnotify.nonblock.js"></script>

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

            function error(titulo, texto) {
                new PNotify({
                    title: titulo,
                    text: texto,
                    type: 'error',
                    styling: 'bootstrap3',
                    delay: 6000
                });
            }

            function exito(titulo, texto) {
                new PNotify({
                    title: titulo,
                    text: texto,
                    type: 'success',
                    styling: 'bootstrap3',
                    delay: 6000
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

            function alertaEncuesta(usuario) {
                new PNotify({
                    title: 'Alerta!',
                    text: 'No ha diligenciado la encuesta Covid del dia de hoy.',
                    type: 'alert',
                    styling: 'bootstrap3',
                    delay: 4000
                });
                setInterval(function () {
                    window.location.href = `http://190.242.128.206:8085/EncuestaCovid/formulario.php?identificacion=${usuario}`;
                }, 5000)
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

