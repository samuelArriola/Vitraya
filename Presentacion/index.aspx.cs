using Comunes;
using Entidades.Generales;
using Persistencia.Generales;
using Persistencia.proceedings;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;

namespace Presentacion
{
    public partial class index : System.Web.UI.Page
    {



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                verification();
            }
        }

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
            }
        }


        [WebMethod]
        public static bool ValidarPass(string pass)
        {
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            return usuario.GNConUsu1 == pass;
        }
        [WebMethod]
        public static bool ChangePass(string pass)
        {
            //Expresion regular que valida la condiciones necesarias para que una contraseña se segura
            var rgl = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d\w\W]{8,}$");

            //se valida que el usuario se encuentre logueado y que la ocntraseña cumpla con las condiciones de la espesion regular
            if (HttpContext.Current.Session["Admin"] == null || !(rgl.IsMatch(pass)))
            {
                return false;
            }

            //se consulta al usuario logueado en la base de datos
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

            //se le asigna la nueva contraseña al usuario
            usuario.GNConUsu1 = pass;

            //se amplia el plaso para el cambio de contraseña al usuario
            usuario.FechaCambioPass = DateTime.Now.AddDays(180);

            //se actualiza la nueva informacion del usuario en la base de datos
            DAOUsuario.update(usuario);
            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = usuario.GNCodUsu1,
                intOidGNHistorico = 0,
                strAccion = "Modificar",
                strDetalle = $"Se actualiza la contraseña de usuario {usuario.GNNomUsu1} por el cambio obligatorio de contraseña cada 180 días",
                dtmFecha = DateTime.Now,
                strEntidad = "Usuario"
            });

            return true;
        }

        [WebMethod]
        public static List<dynamic> GetInfoMenu(string nombre)
        {
            //Listado de datos en el que se guardara la informacion para la creacion del  menu principal
            List<dynamic> opcionesMenu = new List<dynamic>();

            //Se consulta el id del usuario que se encuentra logueado a traves de la variable de sesion 
            int idUser = Convert.ToInt32(HttpContext.Current.Session["Admin"]);

            //se consulta la informacion del usuario logueado
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(idUser);

            //se consulta un listado de los modulos a los cuales el usuario tiene permiso
            List<GNModulos> modulos = DAOGNModulos.GetModulosByRol(usuario.CodigoR, nombre);

           
            List<ScriptBloqueo> bloqueos = DAONABloqueos.GetBloqueos();
            bloqueos = bloqueos.OrderBy(bloqueo => bloqueo.strEstado).ToList();
            foreach (var modulo in modulos)
            {
                List<GNOpciones> opciones = DAOGNOpciones.GetGNOpcionesByIdModulo(modulo.IntOidGNModulo, usuario.CodigoR);
                var UrlBloqueo = "../PlantillasBloqueos/PlantillaBloqueo.aspx";
                var UrlBloqueoPedagogico = "../PlantillasBloqueos/PlantillaBloqueoPedagogico.aspx";

                
                foreach (var opcion in opciones)
                {
                    int flag = 0;
                    if (opcion.IntEstadoBloqueo == 1){
                        foreach (var bloqueo in bloqueos) {
                            if(bloqueo.IntValidacion == 1)
                                if(bloqueo.strEstado == "Restrictivo")
                                {
                                    flag = 2;
                                    break;
                                }
                                else 
                                {
                                    flag = flag == 2 ? 2 : 1;
                                }
                        }
                        if (flag == 1)
                            opcion.StrPrefijo = UrlBloqueoPedagogico + $"?opcion={opcion.StrNombre}&idOpcion={opcion.IntOidGNOpcion}";
                        else if (flag == 2)
                            opcion.StrPrefijo = UrlBloqueo + $"?opcion={opcion.StrNombre}&idOpcion={opcion.IntOidGNOpcion}";
                    }
                    
                }
               
                opcionesMenu.Add(new
                {
                    Modulo = modulo.StrNombre,
                    Icono = modulo.StrIcono,
                    Opciones = opciones
                });
            }

            return opcionesMenu;
        }
        /// <summary>
        /// Metodo que retorna un objeto un con los numeros de los planes de accion segun sea su estado
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static dynamic GetEstadisticas()
        {
            string idUsuario = HttpContext.Current.Session["Admin"].ToString();
            
            dynamic datos = DAOPAPlanAccion.GetEstadisticas(idUsuario);
           
            return datos;
        }
        /// <summary>
        /// metodo web que retorna un objeto con los numeros de la capacitaciones del usuario loguedo segun sea
        /// el estado de de la matricula  de las capacitaciones
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static dynamic GetEstadisticasCapacitaciones()
        {
            return DAOCPMatricula.GetEstadisticaCapacitaciones();
        }

        /// <summary>
        /// metodo web que retorna un un objeto con los numeros de con los estados de las actas de reunion del usuario
        /// que se encuentra logueado
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public static dynamic GetEstadisticaActas() {
            return DAOARactasDM.GetEstadisticasActas();
        }
    }
}