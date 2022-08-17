<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Restores.aspx.cs" Inherits="Generales_1._0.Home.Admin.Log_in.Restore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>Clinica Crecer - Recuperar Contraseña</title>
<link href="../../../Style/StyleLogin.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
     <script type="text/javascript">
        $(document).ready(function () {
            $('#show_password').hover(function show() {
                //Cambiar el atributo a texto
                $('#txtPassword').attr('type', 'text');
                //$('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
                $('#TextBox2').attr('type', 'text');
            },
            function () {
                //Cambiar el atributo a contraseña
                $('#txtPassword').attr('type', 'password');
                //$('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash'); 
                $('#TextBox2').attr('type', 'password');
            });
            //CheckBox mostrar contraseña
            $('#ShowPassword').click(function () {
                $('#Password').attr('type', $(this).is(':checked') ? 'text' : 'password');
                $('#TextBox2').attr('type', $(this).is(':checked') ? 'text' : 'password');
            });
        });

         $(document).ready(function () {
             $('#CheckBox1').hover(function show() {
                 //Cambiar el atributo a texto
                 $('#txtPassword').attr('type', 'text');
                 //$('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
             },
                 function () {
                     //Cambiar el atributo a contraseña
                     $('#txtPassword').attr('type', 'password');
                     //$('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
                 });
             //CheckBox mostrar contraseña
             $('#CheckBox1').click(function () {
                 $('#TextBox2').attr('type', $(this).is(':checked') ? 'text' : 'password');
             });
         });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-page">
        <div class="form">
            <img src="../../../Image/logocrecer.png" alt=""/>
          <form class="login-form">
              <div>
                <asp:TextBox CssClass="input" ID="TextBox1" runat="server" placeholder="usuario"></asp:TextBox>
              </div>
              <div>
                <asp:TextBox CssClass="input" ID="TextBox2" runat="server" placeholder="Contraseña nueva" TextMode="Password"></asp:TextBox>
              </div>
              <div>
                <asp:TextBox CssClass="input" ID="Password" runat="server" placeholder="Confirmar contraseña" TextMode="Password"></asp:TextBox>
              </div>
              <div style="margin-bottom: 10px; display:flex; margin-right: 70px;">
                <asp:CheckBox  ID="ShowPassword" runat="server" Text="Ocultar/Ver Contraseña"/>
              </div>
              <div>
                <asp:Button CssClass="button" ID="Button1" runat="server" Text="Cambiar" OnClick="Button1_Click"/>
              </div>
              <br />
              <asp:Label ID="Label1" Text="Usuario invalido" runat="server" Visible="False" />
          </form>
        </div>
      </div>
    </form>
    </body>
</html>
