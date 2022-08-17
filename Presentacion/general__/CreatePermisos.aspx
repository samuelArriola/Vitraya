<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatePermisos.aspx.cs" Inherits="Generales_1._0.Home.dashboard.production.screens.general.GestionarPermisos" MaintainScrollPositionOnPostback="true" %>

<% Server.ScriptTimeout = 3600; %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../../../../../Image/logocrecer2.png" rel="icon" type="image/png" />

    <title>Clinica Crecer | Gestionar Permisos</title>

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

    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="//resources/demos/style.css" />

    <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.0.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.22/jquery-ui.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" />
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <style>
        input[type=text], input[type=number] {
            border: 1px solid #808080
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

    <script type="text/javascript" language="javascript">
        function ShowPopup() {
            $('#mask').fadeIn();
            $('#<%=pnlpopup.ClientID %>').fadeIn();
        }
        function HidePopup() {
            $('#mask').fadeOut();
            $('#<%=pnlpopup.ClientID %>').fadeOut();
            //$('#DropDownList1').attr("style", "border-color: lightgray;");
        }
        $(".btnClose").live('click', function () {
            HidePopup();
            //$('#DropDownList1').attr("style", "border-color: lightgray;");
        });

        function ShowPopup2() {
            $('#mask').fadeIn();
            $('#<%=Panel1.ClientID %>').fadeIn();
        }
        function HidePopup2() {
            $('#mask').fadeOut();
            $('#<%=Panel1.ClientID %>').fadeOut();
            //$('#DropDownList1').attr("style", "border-color: lightgray;");
        }
        $(".Close").live('click', function () {
            HidePopup2();
            //$('#DropDownList1').attr("style", "border-color: lightgray;");
        });

        window.addEventListener("keyup", function (ev) {
            if (ev.ctrlKey && ev.keyCode === 71) alert(ev);//console.log(ev); // or do smth
        })

        document.fire("keyup", { ctrlKey: true, keyCode: 71, bubbles: true })

    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $(document).ready(function () {
                    SearchText3();
                });
                function SearchText3() {
                    $("#TextBox5").autocomplete({
                        source: function (request, response) {
                            $.ajax({
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                url: "GestionarPermisos.aspx/GetEmployeeName",
                                data: "{'empName':'" + document.getElementById('TextBox5').value + "'}",
                                dataType: "json",
                                success: function (data) {
                                    response(data.d);
                                },
                                error: function (result) {
                                    alert("No Match");
                                }
                            });
                        }
                    });
                }
            }
        });
    </script>

    <style type="text/css">
        #mask {
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 4;
            opacity: 0.4;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=40)"; /* first!*/
            filter: alpha(opacity=40); /* second!*/
            background-color: gray;
            display: none;
            width: 100%;
            height: 100%;
        }

        #popu tr:nth-child(1) {
            color: White;
            background-color: #2A3F54;
            font-weight: bold;
        }

        .centrado-porcentual {
            position: fixed;
            left: 50%;
            top: 50%;
            width: 70%;
            height: 70%;
            transform: translate(-50%, -50%);
            -webkit-transform: translate(-50%, -50%);
            z-index: 1000;
            display: none;
            background-color: White;
            /*position: absolute;*/
            border: outset 2px gray;
            
        }

        #GridView1 td:nth-child(1) {
            color: black;
            font-weight: bold;
            font-size: 15px;
        }
    </style>
</head>

<body class="nav-md">
    <form id="form1" runat="server">
        <div class="container body" id="DivAreaTrabajootro3">
            <input type="hidden" id="divPositionotro3" name="divPositionotro3" />
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
                                    <asp:Label ID="Label13" runat="server" Text="" Style="font-weight: bold; font-size: 15px"></asp:Label></h2>
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
                                        <asp:Image ID="Image2" runat="server" /><asp:Label ID="Label12" runat="server" Text="" Style="font-weight: bold; font-size: 15px"></asp:Label>
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
                    <div class="col-md-12 col-sm-12">
                        <div class="table-responsive">
                            <div id="mask">
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server" CssClass="centrado-porcentual" Style="overflow: scroll; height: 90%; width: 90%">
                                        <table cellpadding="0" cellspacing="5" id="popu" class="table table-striped jambo_table bulk_action">
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                                    <a id="Close" style="color: white; float: right; text-decoration: none; cursor: pointer" class="btnClose" onclick="HidePopup2()">X</a>
                                                </td>
                                            </tr>
                                        </table>
                                        <div class="col-md-6 col-sm-6" style="border: 1px solid rgba(170, 170, 170, 0.73); height: 500px; overflow:scroll" id="DivAreaTrabajoN">
                                            <input type="hidden" name="divPositionN" id="divPositionN" />
                                            <div class="col-md-2 col-sm-2" style="margin-bottom: 10px; height: 50px; margin-top: 10px">
                                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Todos" Style="font-weight: bold; font-size: 20px" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" />
                                            </div>
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <%--<script>
                                                        var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                        if (prm != null) {
                                                            prm.add_endRequest(function (sender, e) {
                                                                if (sender._postBackSettings.panelsToUpdate != null) {
                                                                    $(document).ready(function () {
                                                                        myFunction();
                                                                    });
                                                                    function myFunction() {
                                                                        //alert("ejemplo");
                                                                        var input, filter, table, tr, td, i, txtValue;
                                                                        input = document.getElementById("myInput");
                                                                        filter = input.value.toUpperCase();
                                                                        table = document.getElementById("GridView1");
                                                                        tr = table.getElementsByTagName("th");
                                                                        for (i = 0; i < tr.length; i++) {
                                                                            td = tr[i].getElementsByTagName("td")[3];
                                                                            if (td) {
                                                                                txtValue = td.textContent || td.innerText;
                                                                                if (txtValue.toUpperCase().indexOf(filter) > -1) {
                                                                                    tr[i].style.display = "";
                                                                                } else {
                                                                                    tr[i].style.display = "none";
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            });
                                                        };
                                                    </script>
                                                    <input type="text" id="myInput" onkeyup="myFunction()" placeholder="Search for names.." title="Type in a name">--%>
                                                    <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="false" CssClass="table table-striped order-table" OnRowCommand="GridView1_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="check">Seleccionar</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Codigo Usuario" DataField="GNCodUsu" HeaderStyle-Font-Size="Large" ItemStyle-Font-Size="Large" />
                                                            <asp:BoundField HeaderText="Nombre Usuario" DataField="GNNomUsu" HeaderStyle-Font-Size="Large" ItemStyle-Font-Size="Large" />
                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                                        <FooterStyle BackColor="#CCCCCC" />
                                                        <HeaderStyle BackColor="#2A3F54" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#0094ff" Font-Bold="True" ForeColor="White" />
                                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-md-6 col-sm-6" style="border: 1px solid rgba(170, 170, 170, 0.73); height: 500px; overflow:scroll">
                                            <asp:GridView ID="GridView3" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="false" CssClass="table table-striped order-table">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Quitar">Quitar</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Codigo Usuario" DataField="GNCodUsu" HeaderStyle-Font-Size="Large" ItemStyle-Font-Size="Large" />
                                                    <asp:BoundField HeaderText="Nombre Usuario" DataField="GNNomUsu" HeaderStyle-Font-Size="Large" ItemStyle-Font-Size="Large" />
                                                </Columns>
                                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="#2A3F54" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#0094ff" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#383838" />
                                            </asp:GridView>
                                        </div>
                                        <div class="col-md-12 col-sm-12" style="border: 1px solid rgba(170, 170, 170, 0.73); margin-top: 10px">
                                            <div class="col-md-6 col-sm-6">
                                                <asp:Label ID="Label6" runat="server" Text="Label" Visible="false"></asp:Label>
                                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" CssClass="form-control" DataTextField="nombreMod" DataValueField="codigoM"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-sm-12" style="margin-top: 10px">
                                            <div class="col-md-6 col-sm-6" style="">
                                                <asp:ListBox ID="ListBox1" runat="server" Style="width: 100%; height: 200px" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                            </div>
                                            <div class="col-md-6 col-sm-6">
                                                <asp:ListBox ID="ListBox2" runat="server" Style="width: 100%; height: 200px" OnSelectedIndexChanged="ListBox2_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="col-md-12 col-sm-12" style="margin-top: 10px; text-align: center">
                                            <asp:Label ID="Label14" runat="server" Text="" Style="font-weight: bold; font-size: 20px"></asp:Label>
                                            <br />
                                            <asp:Button ID="Button2" runat="server" Text="Agregar" OnClick="Button2_Click" CssClass="btn btn-info" />
                                            <asp:Button ID="Button3" runat="server" Text="Quitar" CssClass="btn btn-info" OnClick="Button3_Click" />
                                            <br />
                                            <asp:Button ID="Button5" runat="server" Text="Agregar Todos" OnClick="Button5_Click" CssClass="btn btn-info" />
                                            <asp:Button ID="Button6" runat="server" Text="Quitar Todos" OnClick="Button6_Click" CssClass="btn btn-info" />
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"/>
                                </Triggers>
                            </asp:UpdatePanel>
                            <%-- Style="z-index:10000; background-color: White; position: absolute; border: outset 2px gray;display:none; left: 25%;"
                                                    <asp:CheckBoxField HeaderText="check" ItemStyle-Font-Size="Large"/>
                                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="30" />
                                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-Width="150" />
                                                    <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" ItemStyle-Width="150" /> --%>
                            <asp:UpdatePanel runat="server" ID="panelPop" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlpopup" runat="server" CssClass="centrado-porcentual" Style="overflow: scroll">
                                        <table cellpadding="0" cellspacing="5" id="popu" class="table table-striped jambo_table bulk_action">
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:Label ID="Label20" runat="server" Text="Label"></asp:Label>
                                                    <a id="closebtn" style="color: white; float: right; text-decoration: none; cursor: pointer" class="btnClose" onclick="HidePopup()">X</a>
                                                </td>
                                            </tr>
                                        </table>
                                        <input type="button" name="name" value="Asignar" class="btn btn-info" style="float: right; margin-bottom: 10px; display: none" data-toggle="modal" data-target=".bs-example-modal-lg" runat="server" visible="false" />
                                        <input type="button" name="name" value="Eliminar" data-toggle="modal" data-target=".bs-example-modal-sm2" style="float: right; margin-bottom: 10px" class="btn btn-danger" runat="server" visible="false"/>
                                        <div class="col-md-12 col-sm-12">
                                            <div style="width: 100%; height: 90%; margin-bottom: 10px;" class="col-md-12 col-sm-12">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="col-md-6 col-sm-6">
                                                            <asp:Label ID="Label22" runat="server" Text="Label" Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control" DataTextField="nombreMod" DataValueField="codigoM"></asp:DropDownList>
                                                        </div>
                                                        <div class="col-md-12 col-sm-12" style="margin-top: 10px">
                                                            <div class="col-md-6 col-sm-6" style="">
                                                                <asp:ListBox ID="ListBox3" runat="server" Style="width: 100%; height: 200px" OnSelectedIndexChanged="ListBox3_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                            </div>
                                                            <div class="col-md-6 col-sm-6">
                                                                <asp:ListBox ID="ListBox4" runat="server" Style="width: 100%; height: 200px" OnSelectedIndexChanged="ListBox4_SelectedIndexChanged" AutoPostBack="true"></asp:ListBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-12 col-sm-12" style="margin-top: 10px; text-align: center">
                                                            <asp:Label ID="Label7" runat="server" Text="" Style="font-weight: bold; font-size: 20px"></asp:Label>
                                                            <br />
                                                            <asp:Button ID="Button10" runat="server" Text="Agregar" OnClick="Button10_Click" CssClass="btn btn-info" />
                                                            <asp:Button ID="Button9" runat="server" Text="Quitar" CssClass="btn btn-info" OnClick="Button9_Click" />
                                                            <br />
                                                            <asp:Button ID="Button11" runat="server" Text="Agregar Todos" OnClick="Button11_Click" CssClass="btn btn-info" />
                                                            <asp:Button ID="Button12" runat="server" Text="Quitar Todos" OnClick="Button12_Click" CssClass="btn btn-info" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:UpdateProgress runat="server" ID="pro" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0" Visible="false">
                                                    <ProgressTemplate>
                                                        <div class="overlay" />
                                                        <div class="overlayContent">
                                                            <h2>Cargando por favor espere...</h2>
                                                            <img src="../../../../../Image/09b24e31234507.564a1d23c07b4.gif" alt="Loading" border="1" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </div>
                                            <div style="text-align: center; -webkit-user-select: none; -moz-user-select: none; -ms-user-select: none; user-select: none;" class="col-md-12 col-sm-12">
                                                <asp:Label ID="Label17" runat="server" Text="" Style="color: transparent" Visible="false"></asp:Label>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" Visible="false">
                                                    <ContentTemplate>
                                                        <asp:Label ID="Label18" runat="server" Text="" Style="color: transparent"></asp:Label>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                                <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnUpdate_Click" />
                                                <input type="button" class="btn btn-danger" value="Cerrar" onclick="HidePopup()" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="RowCommand" />
                                    <asp:PostBackTrigger ControlID="btnUpdate" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <%--  --%>
                        </div>
                    </div>
                    <div class="">
                        <div class="page-title">
                            <div class="title_left">
                                <h3>Gestion de permisos</h3>
                            </div>

                            <%--<div class="title_right">
                <div class="col-md-5 col-sm-5   form-group pull-right top_search">
                  <div class="input-group">
                    <input type="text" class="form-control" placeholder="Search for...">
                    <span class="input-group-btn">
                      <button class="btn btn-default" type="button">Go!</button>
                    </span>
                  </div>
                </div>
              </div>--%>
                        </div>

                        <div class="clearfix"></div>

                        <div class="row" style="display: block;">
                            <div class="col-md-12 col-sm-12">
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-12 col-sm-12  " id="Div2" runat="server">
                                <div class="x_panel">
                                    <div class="x_title">
                                        <%--                    <h2>Table design <small>Custom design</small></h2>--%>
                                        <ul class="nav navbar-right panel_toolbox">
                                            <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                            </li>
                                            <li><a class="close-link"><i class="fa fa-close"></i></a>
                                            </li>
                                        </ul>
                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="col-md-12 col-sm-12" id="DivPermisos2">
                                        <div class="x_panel">
                                            <div class="x_title">
                                                <h2 style="font-weight: bold">Usuarios</h2>
                                                <ul class="nav navbar-right panel_toolbox">
                                                    <li><a class="collapse-link"><i class="fa fa-chevron-up"></i></a>
                                                    </li>
                                                    <li><a class="close-link"><i class="fa fa-close"></i></a>
                                                    </li>
                                                </ul>
                                                <div class="clearfix"></div>
                                            </div>
                                            <div class="x_content" style="overflow: scroll; height: 900px;" id="DivAreaTrabajo2">
                                                <input type="hidden" id="divPosition2" name="divPosition" />
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <script>
                                                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                            if (prm != null) {
                                                                prm.add_endRequest(function (sender, e) {
                                                                    if (sender._postBackSettings.panelsToUpdate != null) {
                                                                        $(document).ready(function () {
                                                                            SearchText2();
                                                                        });
                                                                        function SearchText2() {
                                                                            $("#TextBox6").autocomplete({
                                                                                source: function (request, response) {
                                                                                    $.ajax({
                                                                                        type: "POST",
                                                                                        contentType: "application/json; charset=utf-8",
                                                                                        url: "CreatePermisos.aspx/GetEmployeRol",
                                                                                        data: "{'empName':'" + document.getElementById('TextBox8').value + "'}",
                                                                                        dataType: "json",
                                                                                        success: function (data) {
                                                                                            response(data.d);
                                                                                        },
                                                                                        error: function (result) {
                                                                                            alert("No Match");
                                                                                        }
                                                                                    });
                                                                                }
                                                                            });
                                                                        }
                                                                    }
                                                                });
                                                            };
                                                        </script>
                                                        <div class="title_right">
                                                            <div class="col-md-7 col-sm-7   form-group pull-right">
                                                                <asp:Button ID="Button4" runat="server" Text="Desleccionar" CssClass="btn btn-info" Visible="false" />
                                                                <asp:Button ID="Button1" runat="server" Text="Asignar Permisos Masivos" CssClass="btn btn-info" OnClick="Button1_Click"/>
                                                            </div>
                                                        </div>
                                                        <div class="title_right">
                                                            <div class="col-md-5 col-sm-5   form-group pull-right">
                                                                <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="TextBox6_TextChanged" placeholder="Nombre del Usuario.." ></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <asp:Label ID="Label21" runat="server" Text="Label" Visible="false"></asp:Label>
                                                        <%--                        <table class="table table-striped">--%>
                                                        <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="false" CssClass="table table-striped" OnRowCommand="GridView2_RowCommand" DataKeyNames="GNCodUsu">
                                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                                            <RowStyle Height="50px" Width="90px" />
                                                            <AlternatingRowStyle Height="50px" Width="90px" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Codigo Usuario" DataField="GNCodUsu" HeaderStyle-Font-Size="Large" ItemStyle-Font-Size="Large" />
                                                                <asp:BoundField HeaderText="Nombre Usuario" DataField="GNNomUsu" HeaderStyle-Font-Size="Large" ItemStyle-Font-Size="Large" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Filtrar" CommandArgument='<%#Eval("GNCodUsu") %>' CssClass="btn btn-success">Asignar</asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <AlternatingRowStyle BackColor="#CCCCCC" />
                                                            <FooterStyle BackColor="#CCCCCC" />
                                                            <HeaderStyle BackColor="#2A3F54" Font-Bold="True" ForeColor="White" />
                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#0094ff" Font-Bold="True" ForeColor="White" />
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

        <%-- <!-- jQuery -->
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
                                              <script>
                                                  var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                  if (prm != null) {
                                                      prm.add_endRequest(function (sender, e) {
                                                          if (sender._postBackSettings.panelsToUpdate != null) {
                                                              $(document).ready(function () {
                                                                  SearchText3();
                                                              });
                                                              function SearchText3() {
                                                                  $("#TextBox5").autocomplete({
                                                                      source: function (request, response) {
                                                                          $.ajax({
                                                                              type: "POST",
                                                                              contentType: "application/json; charset=utf-8",
                                                                              url: "GestionarPermisos.aspx/GetEmployeeName",
                                                                              data: "{'empName':'" + document.getElementById('TextBox5').value + "'}",
                                                                              dataType: "json",
                                                                              success: function (data) {
                                                                                  response(data.d);
                                                                              },
                                                                              error: function (result) {
                                                                                  alert("No Match");
                                                                              }
                                                                          });
                                                                      }
                                                                  });
                                                              } 
                                                          }
                                                      });
                                                  };
                                              </script>
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

        <%--    <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.0.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.22/jquery-ui.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>  
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>

        <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.0.js"></script>
        <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.22/jquery-ui.js"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
        <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
        <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>


        
        <script type="text/javascript">
            var divN = document.getElementById("DivAreaTrabajoN"); var divPositionN = document.getElementById("divPositionN");
            var positionN = parseInt('<%=Request.Form["divPositionN"] %>');
            if (isNaN(positionN)) {
                positionN = 0;
            }
            divN.scrollTop = positionN;
            divN.onscroll = function () {
                divPositionN.value = divN.scrollTop;
            };
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                SearchText();
                SearchText2();
                SearchText3();
            });
            function SearchText() {
                $("#TextBox3").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "CreatePermisos.aspx/GetEmployeeName",
                            data: "{'empName':'" + document.getElementById('TextBox3').value + "'}",
                            dataType: "json",
                            success: function (data) {
                                response(data.d);
                            },
                            error: function (result) {
                                alert("No Match");
                            }
                        });
                    }
                });
            }
            function SearchText2() {
                $("#TextBox8").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "CreatePermisos.aspx/GetEmployeRol",
                            data: "{'empName':'" + document.getElementById('TextBox8').value + "'}",
                            dataType: "json",
                            success: function (data) {
                                response(data.d);
                            },
                            error: function (result) {
                                alert("No Match");
                            }
                        });
                    }
                });
            }
            function SearchText3() {
                $("#TextBox7").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "CreatePermisos.aspx/GetEmployeRol",
                            data: "{'empName':'" + document.getElementById('TextBox7').value + "'}",
                            dataType: "json",
                            success: function (data) {
                                response(data.d);
                            },
                            error: function (result) {
                                alert("No Match");
                            }
                        });
                    }
                });
            }
        </script>
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
        <script type="text/javascript">
            var div2 = document.getElementById("DivAreaTrabajootro"); var divPosition2 = document.getElementById("divPositionotro");
            var position2 = parseInt('<%=Request.Form["divPositionotro"] %>');
            if (isNaN(position2)) {
                position2 = 0;
            }
            div2.scrollTop = position2;
            div2.onscroll = function () {
                divPosition2.value = div2.scrollTop;
            };
        </script>

        <script type="text/javascript">
            var div3 = document.getElementById("DivAreaTrabajootro3"); var divPosition3 = document.getElementById("divPositionotro3");
            var position3 = parseInt('<%=Request.Form["divPositionotro3"] %>');
            if (isNaN(position3)) {
                position3 = 0;
            }
            div3.scrollTop = position3;
            div3.onscroll = function () {
                divPosition3.value = div3.scrollTop;
            };
        </script>

        <script type="text/javascript">

            function exitoRol4() {
                new PNotify({
                    title: 'Success',
                    text: 'Eliminacion Exitosa!',
                    type: 'success',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }

            function errorRol7() {
                new PNotify({
                    title: 'Oh No!',
                    text: 'Selccione una opcion.',
                    type: 'error',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
            function errorRol8() {
                new PNotify({
                    title: 'Oh No!',
                    text: 'Ya se encuntra con ese permiso.',
                    type: 'error',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
            function exitoRol() {
                new PNotify({
                    title: 'Success',
                    text: 'Agregado con exito!',
                    type: 'success',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

                function EndRequestHandler(sender, args) {
                    $(document).ready(function () {
                        SearchText3();
                    });
                    function SearchText3() {
                        $("#TextBox5").autocomplete({
                            source: function (request, response) {
                                $.ajax({
                                    type: "POST",
                                    contentType: "application/json; charset=utf-8",
                                    url: "CreatePermisos.aspx/GetEmployeeName",
                                    data: "{'empName':'" + document.getElementById('TextBox5').value + "'}",
                                    dataType: "json",
                                    success: function (data) {
                                        response(data.d);
                                    },
                                    error: function (result) {
                                        alert("No Match");
                                    }
                                });
                            }
                        });
                    }
                }
            });
        </script>

        <div runat="server" visible="false">
            <%-- function errorRol() {
                new PNotify({
                    title: 'Oh No!',
                    text: 'Seleccione el rol, las operaciones y los modulos.',
                    type: 'error',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
            function errorRol2() {
                new PNotify({
                    title: 'Oh No!',
                    text: 'Seleccione el rol y la operacion que desea Quitar.',
                    type: 'error',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
            function errorRol3() {
                new PNotify({
                    title: 'Oh No!',
                    text: 'Ingrese el codigo del usuario.',
                    type: 'error',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
                function errorRol5() {
                new PNotify({
                    title: 'Oh No!',
                    text: 'Seleccione el Rol que desea eliminar.',
                    type: 'error',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
            function errorRol6() {
                new PNotify({
                    title: 'Oh No!',
                    text: 'No hay filas seleccionadas.',
                    type: 'error',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
                function exitoRol2() {
                new PNotify({
                    title: 'Success',
                    text: 'Operacion realizada con exito!',
                    type: 'success',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
            function exitoRol3() {
                new PNotify({
                    title: 'Success',
                    text: 'Cambio Exitoso!',
                    type: 'success',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
            function exitoRol4() {
                new PNotify({
                    title: 'Success',
                    text: 'Eliminacion Exitosa!',
                    type: 'success',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
            function exitoRol6() {
                new PNotify({
                    title: 'Success',
                    text: 'Rol guardado con exito!',
                    type: 'success',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
            function exitoRol5() {
                new PNotify({
                    title: 'Oh No!',
                    text: 'Ingrese el nombre del rol!',
                    type: 'error',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }
            function errorRol9() {
                new PNotify({
                    title: 'Oh No!',
                    text: 'Ingrese el nombre del rol.',
                    type: 'error',
                    styling: 'bootstrap3',
                    delay: 1000
                });
            }--%>
        </div>
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
            <asp:Label ID="Label3" runat="server" Text="Label" Style="color: transparent"></asp:Label>
            <asp:Label ID="Label4" runat="server" Text="Label" Style="color: transparent"></asp:Label>
            <asp:Label ID="Label5" runat="server" Text="Label" Style="color: transparent"></asp:Label>
            <asp:Label ID="Label8" runat="server" Text="Label" Style="color: #000"></asp:Label>
            <asp:Label ID="Label9" runat="server" Text="Label" Visible="true" Style="color: #000"></asp:Label>
            <asp:Label ID="Label10" runat="server" Text="Label" Visible="true" Style="color: transparent"></asp:Label>
            <asp:Label ID="Label11" runat="server" Text="Label" Visible="true" Style="color: transparent"></asp:Label>
            <asp:Label ID="Label50" runat="server" Text="Label" Visible="true" Style="color: transparent"></asp:Label>
            <asp:Label ID="Label51" runat="server" Text="Label" Visible="true" Style="color: transparent"></asp:Label>
        </div>
    </form>
</body>
</html>
