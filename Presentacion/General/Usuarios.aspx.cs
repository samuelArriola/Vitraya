using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;

namespace Presentacion
{
    public partial class Usuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Usuario> usuarios = DAOUsuario.getUsuarios();
            string table = "<table>" +
                "<thead>" +
                "   <tr>" +
                "       <td>Documento</td>" +
                "       <td>Nombre</td>" +
                "       <td>Cargo</td>" +
                "       <td>Firma</td>" +
                "   </tr>" +
                "</thead>" +
                "<tbody>";

            foreach (var usuario in usuarios)
            {
                if (usuario.GnUnfun1.ToLower().Contains("adulto".ToLower()))
                    table += "<tr>" +
                        "   <td>" + usuario.GNCodUsu1 + "</td>" +
                        "   <td>" + usuario.GNNomUsu1 + "</td>" +
                        "   <td>" + usuario.GnCargo1 + "</td>" +
                        "   <td><img src=\"data:image/png;base64," + Convert.ToBase64String(usuario.GNFmUsu1) + "\" width=\"70px\" /></td>" +
                        "<tr>";
            }

            table += "</tbody></thead>";

            panelUsuarios.InnerHtml = table;

        }
    }
}