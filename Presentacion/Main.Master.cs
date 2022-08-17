using Entidades.Generales;
using Persistencia;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;

namespace Presentacion
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        Conexion con = new Conexion();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                verification();
                //CargarMenu();
            }
        }

        //public void CargarMenu()
        //{

        //    List<string> restricciones = new List<string>();
        //    List<int> IdsOpcionesBloqueadas = new List<int>();
        //    List<int> EstadosOpcionesBloqueadas = new List<int>();
        //    List<string> EstadosScriptsBloqueos = new List<string>();
        //    var UrlBloqueo = "../PlantillasBloqueos/PlantillaBloqueo.aspx";
        //    var UrlBloqueoPedagogico = "../PlantillasBloqueos/PlantillaBloqueoPedagogico.aspx";
        //    var acceso = "Libre";

        //    List<ScriptBloqueo> bloqueos = DAONABloqueos.GetBloqueos();

        //    //Recorro la lista de bloqueos
        //    foreach (var bloqueo in bloqueos)
        //    {
        //        //Valido si el bloqueo corresponde a la encuesta y si ya ha sido diligenciada
        //        if (bloqueo.intOidGnScriptsBloqueos == 1 && bloqueo.strResultConsulta != "0")
        //        {
        //            restricciones.Add("NoPendiente");
        //        }
        //        //Valido si el bloqueo corresponde a la encuesta  si no ha sido diligenciada
        //        else if (bloqueo.intOidGnScriptsBloqueos == 1 && bloqueo.strResultConsulta == "0")
        //        {
        //            if (bloqueo.StrEstado == "Restrictivo")
        //            {
        //                restricciones.Add("Pendiente");
        //            }
        //            else if (bloqueo.StrEstado == "Pedagogico")
        //            {
        //                restricciones.Add("PendienteP");
        //            }
                        
        //        }

        //        //Valido el resto de bloqueos que no tengan pendientes
        //        if (bloqueo.intOidGnScriptsBloqueos != 1 && bloqueo.strResultConsulta == "0")
        //        {
        //            restricciones.Add("NoPendiente");
        //        }
        //        //Valido el resto de bloqueos que tengan pendientes
        //        else if (bloqueo.intOidGnScriptsBloqueos != 1 && bloqueo.strResultConsulta != "0")
        //        {
        //            if(bloqueo.StrEstado == "Restrictivo")
        //            {
        //                restricciones.Add("Pendiente");
        //            }
        //            else if(bloqueo.StrEstado == "Pedagogico")
        //            {
        //                restricciones.Add("PendienteP");
        //            }
                        
        //        }

        //        EstadosScriptsBloqueos.Add(bloqueo.StrEstado);

        //    }

        //    //Valido si existe algo pendiente en los distintos bloqueos o si todo esta paz y salvo
        //    foreach (var restriccionR in restricciones)
        //    {
        //        if (restriccionR == "Pendiente")
        //        {
        //            acceso = "Restringido";
        //        }

        //    }

        //    if(acceso != "Restringido")
        //    {
        //        foreach (var restriccionP in restricciones)
        //        {
        //            if (restriccionP == "PendienteP")
        //            {
        //                acceso = "Pedagogico";
        //            }
        //        }
        //    }

        //    List<OpcionesBloqueadas> OpcionesBloqueadas = DAOGNOpcionesBloqueadas.GetOpcionesBloqueadas();

        //    foreach (var OpcionB in OpcionesBloqueadas)
        //    {
        //        IdsOpcionesBloqueadas.Add(OpcionB.IntOidGNOpcion);
        //        EstadosOpcionesBloqueadas.Add(OpcionB.intEstado);
        //    }

        //    Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(Session["Admin"].ToString()));
        //    var menuHtml = "";
        //    List<GNModulos> modulos = DAOGNModulos.GetModulosByRol(usuario.CodigoR);
        //    foreach (var modulo in modulos)
        //    {
        //        menuHtml += $"" +
        //        $"<li><a>{modulo.StrNombre}<i class=\"{modulo.StrIcono}\"></i></a>";
        //        //$" <ul class=\"nav child_menu\">";

        //        //List<GNOpciones> opciones = DAOGNOpciones.GetGNOpcionesByIdModulo(modulo.IntOidGNModulo, usuario.CodigoR);
        //        //foreach (var opcion in opciones)
        //        //{
        //        //    bool flag = true;
        //        //    //Bloqueo el acceso a las opciones
        //        //    if (acceso == "Restringido") 
        //        //    {
        //        //        for (var b = 0; b < IdsOpcionesBloqueadas.Count; b++)
        //        //        {
        //        //            if (IdsOpcionesBloqueadas[b] == opcion.IntOidGNOpcion && EstadosOpcionesBloqueadas[b] == 1)
        //        //            {
        //        //                menuHtml += $"" +
        //        //                $"<li><a href=\"{UrlBloqueo}?opcion={opcion.StrNombre}\">{opcion.StrNombre}</a></li>";
        //        //                flag = false;
        //        //            }
        //        //            else if (IdsOpcionesBloqueadas[b] == opcion.IntOidGNOpcion && EstadosOpcionesBloqueadas[b] == 0)
        //        //            {
        //        //                menuHtml += $"" +
        //        //                $"<li><a href=\"{opcion.StrPrefijo}\">{opcion.StrNombre}</a></li>";
        //        //                flag = false;
        //        //            }
                            
        //        //        }


        //        //    }

        //        //    if (acceso == "Pedagogico")
        //        //    {
        //        //        for (var b = 0; b < IdsOpcionesBloqueadas.Count; b++)
        //        //        {
        //        //            if (IdsOpcionesBloqueadas[b] == opcion.IntOidGNOpcion && EstadosOpcionesBloqueadas[b] == 1)
        //        //            {
        //        //                menuHtml += $"" +
        //        //                $"<li><a href=\"{UrlBloqueoPedagogico}?opcion={opcion.StrNombre}&idOpcion={opcion.IntOidGNOpcion}\">{opcion.StrNombre}</a></li>";
        //        //                flag = false;
        //        //            }
        //        //            else if (IdsOpcionesBloqueadas[b] == opcion.IntOidGNOpcion && EstadosOpcionesBloqueadas[b] == 0)
        //        //            {
        //        //                menuHtml += $"" +
        //        //                $"<li><a href=\"{opcion.StrPrefijo}\">{opcion.StrNombre}</a></li>";
        //        //                flag = false;
        //        //            }
        //        //        }
        //        //    }

        //        //    //dejo libre el acceso a las opciones
        //        //    if (acceso == "Libre" || flag)
        //        //    {
        //        //        menuHtml += $"" +
        //        //        $"<li><a href=\"{opcion.StrPrefijo}\">{opcion.StrNombre}</a></li>";
        //        //    }

        //        //}
        //        //menuHtml += $"</ul></li>";
        //    }
        //    menu.InnerHtml = menuHtml;
        //}

        private void verification()
        {
            try
            {
                Session["admin"].ToString();
               
            }
            catch (Exception)
            {
                Response.Redirect("~/Log%20in/Login.aspx?Sesion=Debe Iniciar Sesion");
            }
            finally
            {
                con.CloseConnection();
            }

            Usuario userLogedIn = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(Session["Admin"]));
            
            string link = Request.Url.AbsolutePath;

            GNPermisos permisos = DAOGNPermisos.GetPermiso(userLogedIn.CodigoR, link); 

            if (permisos == null || (permisos.BlnConfirmar || permisos.BlnCrear || permisos.BlnEliminar || permisos.BlnModificar))
            {
                lbUserName2.Text = userLogedIn.GNNomUsu1;
                profileIgame.Attributes["style"] = $@"background-image:url(""../Images/user.png"");";

                //if (userLogedIn.GNFtUsu1.Length > 0)
                //{
                //    profileIgame.Attributes["style"] = $@"background-image: url(""data:image/gif;base64,{Convert.ToBase64String(userLogedIn.GNFtUsu1)}"");";
                //}
                //else
                //{
                //    profileIgame.Attributes["style"] = $@"background-image:url(""../Images/user.png"");";
                //}

                if (userLogedIn.FechaCambioPass <= DateTime.Now)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "cambio", @"$(document).ready(function(){$(""#mdChangePass"").modal()})", true);
                }
            }
            else
            {
                Response.Redirect("~/page_403.aspx");
            }
            

        }

        protected void btnLogUot_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Response.Redirect("~/Log%20in/Login.aspx");
        }
    }
}