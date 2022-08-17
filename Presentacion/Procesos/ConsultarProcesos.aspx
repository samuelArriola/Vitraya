<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ConsultarProcesos.aspx.cs" Inherits="Presentacion.Procesos.ConsultarProcesos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <div class="x_panel">
        <div class="x_title">
            <div class="clearfix">
                <h6>Procesos</h6>
            </div>
        </div>
        <div class="x_content">
            <section>
                <table class="table" id="tbProcesos">
                    <thead>
                        <tr>
                            <th></th>
                            <th> <asp:DropDownList runat="server" ID="ddlTipoPro" CssClass="form-control">
                                        <asp:ListItem Text="Seleccione..." Value="" />
                                        <asp:ListItem Text="Estratégicos" Value="Estratégicos" />
                                        <asp:ListItem Text="Misionales" Value="Misionales" />
                                        <asp:ListItem Text="Apoyo " Value="Apoyo " />
                                        <asp:ListItem Text="Evaluación" Value="Evaluación" />
                                    </asp:DropDownList> </th>
                            <th> <input type="text" id="txtNompro" class="form-control"/> </th>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                        <tr>
                            <th></th>
                            <th>Tipo Proceso</th>
                            <th>Nombre Proceso</th>
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>

                    </tbody>
                </table>
            </section>
            <iframe style="display:none" name="iFrameCPro">

            </iframe>
        </div>
        <script src="JS/ConsultarProceso.js"></script>
</asp:Content>
