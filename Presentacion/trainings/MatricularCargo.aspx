<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="MatricularCargo.aspx.cs" Inherits="Presentacion.trainings.MatricularCargo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
   
   <div class="page-title">
      <div class="title_left">
         <h3>MATRICULAR USUARIOS POR CARGOS</h3>
      </div>
      <div class="title_right">
         <div class="col-md-5 col-sm-5   form-group pull-right top_search">
            <div class="input-group">
               <span class="input-group-btn">
                  <%--<asp:Button ID="Button1" runat="server" Text="Nueva Capacitacion" BackColor="#6666FF" Font-Bold="True" ForeColor="White" />--%>
                  <asp:Button ID="btnVolver" runat="server" CssClass="btn btn-round btn-success" Text="Volver" OnClick="btnVolver_Click" />
               </span>
            </div>
         </div>
      </div>
   </div>
   <br />
   <div>
      <asp:Label ID="lblfecha" runat="server" Text="Label" Visible="False"> </asp:Label>
   </div>
   <br />
   <br />
   <br />
   <asp:TextBox ID="txtidcapa" runat="server" Visible="False"></asp:TextBox>
   <div>
      <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control" Width="400px">
         <asp:ListItem>Seleccionar cargo</asp:ListItem>
      </asp:DropDownList>
      <br />
      <br />
      <br />
       <asp:UpdatePanel runat="server">
           <ContentTemplate>
               <div class="form-group">
                    <asp:Button Text="Matricular por Cargo" runat="server"  ID="btnMatricularCargo" CssClass="btn btn-success" OnClick="btnMatricularCargo_Click"/>
               </div>
           </ContentTemplate>
       </asp:UpdatePanel>
      <br />
      <br />
      <br />
      <asp:UpdatePanel runat="server" ID="upUsuariosMatriculados" UpdateMode="Conditional">
         <ContentTemplate>
            <asp:GridView ID="tbUsuariosMatriculados" runat="server" AutoGenerateColumns="False" Width="100%" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" CssClass="table">
               <Columns>
                  <asp:BoundField DataField="IntGNCodUsu" HeaderText="Documento" SortExpression="IDEMPLEADO" />
                  <asp:BoundField DataField="StrNOMUSUARIO" HeaderText="Nombre de Usuario" SortExpression="NOMUSUARIO" />
                  <asp:BoundField DataField="StrUNIDAD" HeaderText="Unidad Funcional" SortExpression="UNIDAD" />
                  <asp:BoundField DataField="StrCARGO" HeaderText="Cargo" SortExpression="CARGO" />
                  <asp:CommandField SelectText="Eliminar" ShowSelectButton="True">
                     <ItemStyle ForeColor="Red" />
                  </asp:CommandField>
               </Columns>
            </asp:GridView>
         </ContentTemplate>
      </asp:UpdatePanel>
      
      <asp:Label ID="Label2" runat="server"> </asp:Label>
   </div>
</asp:Content>