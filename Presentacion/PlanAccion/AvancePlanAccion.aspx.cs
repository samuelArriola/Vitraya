using Entidades.Generales;
using Entidades.PlanAccion;
using Persistencia.Generales;
using Persistencia.proceedings;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentacion.PlanAccion
{
    public partial class AvancePlanAccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarArchivo();
            CargarTbArchivos();
        }

        public void CargarArchivo()
        {
            if (fuArchivo.HasFile)
            {
                //se obtiene el archivo subido
                HttpPostedFile file = fuArchivo.PostedFile;

                //se obtiene el nombre del archivo
                string nombre = file.FileName.Substring(0, file.FileName.LastIndexOf("."));

                //se obtiene la extencion del archivo
                string ext = fuArchivo.PostedFile.FileName;
                ext = ext.Substring(ext.LastIndexOf(".") + 1).ToLower();

                // Tipo de contenido
                string contentType = file.ContentType;
                // se obtine el array de bit del archivo
                byte[] imagen = new byte[file.InputStream.Length];


                file.InputStream.Read(imagen, 0, imagen.Length);


                // se crea un nuevo objeto de tipo GNArchivo con la informacion de del archivo subido por el input file
                GNArchivo archivo = new GNArchivo
                {
                    IntOidGNListaArchivos = 0,
                    StrContenido = contentType,
                    StrExt = ext,
                    StrNombre = nombre,
                    AbteArchivo = imagen
                };

                //se carga la lista de archivos que estan en la variable de session
                List<GNArchivo> archivos = (List<GNArchivo>)Session["archivos"];

                //en caso de que la informacion en la variable de session se nula
                if (archivos == null)
                {
                    // se crea una nueva lista de archivos
                    archivos = new List<GNArchivo>();
                }

                //se carga el archivo recion creado a la lista de archivos
                archivos.Add(archivo);

                //se gurada la lista de archivos en la variable de session 
                Session["archivos"] = archivos;

            }

        }

        public void CargarTbArchivos()
        {
            //se consulta la lista de archivos que se encuentra en la variable de session
            List<GNArchivo> archivos = (List<GNArchivo>)Session["archivos"];

            //en caso de que no exista una lista de archivos se termina la ejecucion del metodo 
            if (archivos == null)
                return;
            // se pasan los datos de la lista de los archivos a la tabla de los archivos
            tableArch.DataSource = archivos;

            //se actualiza la tabla de los archivos para que se muestra la infomacion agregada
            tableArch.DataBind();
        }

        protected void btnGuardarAvance_Click(object sender, EventArgs e)
        {
            int idPlanAccion = Convert.ToInt32(Request["idPlanAccion"]);

            //se crea una nueva lista de archivos que pertenece
            GNListaArchivos lista = new GNListaArchivos
            {
                IntOidGNModulo = 9
            };

            DAOGNListaArchivos.set(lista);
            lista = DAOGNListaArchivos.GetUltimo();

            // se consulta el listado de los archivos que se encuentran guardados en la variable de session;
            List<GNArchivo> archivos = (List<GNArchivo>)Session["archivos"];

            //se verifica que la lista de los archivos no se encuentre vacia
            if (archivos != null)
                foreach (var archivo in archivos)
                {
                    archivo.IntOidGNListaArchivos = lista.IntOidGNListaArchivos;

                    //se guardan los datos de los archivos en la base de datos
                    DAOGNArchivo.set(archivo);
                }

            //se crea una instancia  de avence para guardar la infomacion en la base de datos
            PAAvance avance = new PAAvance
            {
                IntOidPAPlanAccion = idPlanAccion,
                StrAvance = textEditor.Text,
                IntOidListaArch = lista.IntOidGNListaArchivos,
                DtmFecha = DateTime.Now,
                StrTitulo = txtTitulo.Text
            };

            //se guarda el avance en la base de datos
            DAOPAAvance.SetPAAvance(avance);

            PAPlanAccion planAccion = DAOPAPlanAccion.Get(idPlanAccion);

            planAccion.IntEstAct = PAPlanAccion.ESTADO.PROCESO;

            DAOPAPlanAccion.UpdateCompromiso(planAccion);

            DAOGNHistorico.SetHistorico(new GNHistorico
            {
                dtmFecha = DateTime.Now,
                intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                intInstancia = idPlanAccion,
                strAccion = "Modificar",
                strDetalle = "Se Cambia el estaod del plan de accion de Asignado a En proceso",
                strEntidad = "PAPlanAccion"
            });

            Usuario usuario = DAOUsuario.getInstance().GetUsuario((DAOPAUsuario.GetPAUsuarioByIdPlan(idPlanAccion, PAUsuario.SEGUIMIENTO)).IntOidGNUsuario);

            List<string> correos = new List<string>() { usuario.GNCrusu1 };

            Session.Remove("archivos");

            Response.Redirect("MisPlanes.aspx");
        }

        protected void tableArch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //se consulta la lista de archivos que se encuentra en la variable de session
            List<GNArchivo> archivos = (List<GNArchivo>)Session["archivos"];

            //en caso de que no exista una lista de archivos se termina la ejecucion del metodo 
            if (archivos == null)
                return;
            //En caso de que el commando enviado se eliminar
            if (e.CommandName == "eliminar")
            {
                //se optione el control que genero el evento
                LinkButton boton = (LinkButton)e.CommandSource;

                //se obtine le fila que de donde se genero el evento
                GridViewRow items = (GridViewRow)boton.NamingContainer;

                //se elimina el archivo que se encuentra en el indice de la fila seleccionada
                archivos.RemoveAt(items.RowIndex);

                //se carga otra vez la lista de los archivos en la variable de session
                Session["archivos"] = archivos;

                //se cargan otra vez los datos de la tabla de los archivos
                CargarTbArchivos();
            }
        }

        [WebMethod]
        public static object[] GetInfoPlanAccion(int idPlanAccion)
        {
            PAPlanAccion plan = DAOPAPlanAccion.Get(idPlanAccion);
            List<PAAvance> avances = DAOPAAvance.GetAvencesByIdPlan(idPlanAccion);

            //listado donde se guardaran todos los datos a retornarse
            List<object[]> datos = new List<object[]>();

            foreach (var avance in avances)
            {
                //se consulta el listado de archivos cargados para el plan de accion
                List<GNArchivo> archivos = DAOGNArchivo.Listar(avance.IntOidListaArch);
                foreach (var archivo in archivos)
                {
                    //se borra la informacion del archivo para evitar retornar datos muy pesados  
                    archivo.AbteArchivo = null;
                }

                //se agreban todos los datos a la lista a retornar
                datos.Add(new object[] { avance, archivos });
            }

            return new object[] { plan, datos };
        }
    }
}