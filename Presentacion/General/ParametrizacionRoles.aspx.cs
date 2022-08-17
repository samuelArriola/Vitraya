using Entidades.Generales;
using Newtonsoft.Json;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentacion.General
{
    public partial class ParametrizacionRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarRblRoles();
            cargarOpciones();
            upPermisos.Update();
        }

        //evento del boton de crea rol
        protected void btnCrearRol_Click(object sender, EventArgs e)
        {
            //se valida que el nombre de rol no se encuentre vacio
            if (!(txtCrearRol.Text == null))
            {
                // se crea el rol en base de datos
                DAOGNRoles.set(txtCrearRol.Text);
                
                //se recarga el contenido de los roles 
                cargarRblRoles();
            }

            upPermisos.Update();
        }

        //metodo que carga la informacion de los roles en el panel correspondiente 
        public void cargarRblRoles()
        {
            //se consulta un listado de los roles en base de datos 
            List<GNRoles> roles = DAOGNRoles.listar();

            //se carga el listdo de los roles en el panel
            rblRoles.Items.Clear();
            foreach (GNRoles rol in roles)
            {
                rblRoles.Items.Add(new ListItem(rol.StrNombre, rol.IntOidGNRol.ToString()));
            }

        }
        

        //metodo que carga el listado de las opciones en el panel corespondiente
        public void cargarOpciones()
        {
            //se consulta el listado de las opciones que se encuentra en la base de datos 
            List<GNOpciones> opciones = DAOGNOpciones.listar();

            //variable esclablo de para verificar el cambio de los modulos en en la opciones 
            string flag = opciones[0].StrNombreModulo;
            
            //se muestra el primer modulo
            string contenido = "<div class=\"tModulo\">" + flag + "</div>";

            //se cargan las opciones en el panel ordenada por el modulo al cual correposponden 
            for (int i = 0; i < opciones.Count; i++)
            {
                if (opciones[i].StrNombreModulo != flag)
                {
                    flag = opciones[i].StrNombreModulo;
                    contenido += "<div class=\"tModulo\">" + flag + "</div>";
                }
                contenido += "<input type=\"radio\" id=\"rdopc" + i + "\" name=\"opcion\" value=\"" + opciones[i].IntOidGNOpcion + "\" />" +
                    "<label for=\"rdopc" + i + "\" class=\"ml-3\">" + opciones[i].StrNombre + "</label>";
            }

            pnOpciones.InnerHtml = contenido;

        }


        protected void rblRoles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// meto que actualiza los permisos de un rol para una opcion determinada 
        /// </summary>
        /// <param name="eliminar">permiso para la accion eliminar</param>
        /// <param name="crear">permiso para la accion crear</param>
        /// <param name="modificar">permiso para la accion modificar</param>
        /// <param name="confirmar">permiso para la accion confirmar</param>
        /// <param name="idRol">id del rol</param>
        /// <param name="idOpcion">id de la opcion </param>
        /// <returns></returns>
        [WebMethod]
        public static string ModificarPermisos(bool eliminar, bool crear, bool modificar, bool confirmar, int idRol, int idOpcion)
        {
            //se consula los permisos por el rol y opcion definida
            GNPermisos permisos = DAOGNPermisos.get(idRol, idOpcion);
            
            //se valida que los permisos existan
            if (permisos == null)
            {
                //sen caso de que los perminsos no existan se crean en base de datos
                permisos = new GNPermisos
                {
                    BlnConfirmar = confirmar,
                    BlnEliminar = eliminar,
                    BlnCrear = crear,
                    BlnModificar = modificar,
                    IntOidGNOpcion = idOpcion,
                    ItnOidRol = idRol
                };
                //se crean en base de datos
                DAOGNPermisos.set(permisos);
            }
            else
            {
                //sen caso de que los permisos existan se modifican de acuerdo a lo indicado
                permisos.BlnConfirmar = confirmar;
                permisos.BlnEliminar = eliminar;
                permisos.BlnCrear = crear;
                permisos.BlnModificar = modificar;

                //se guarda dicha informacion en la base de datos
                DAOGNPermisos.upadate(permisos);
            }
            return "";
        }

        //metodo web que roetorna un listado de los permisos filtrados por el id del rol y el id de la opcion
        [WebMethod]
        public static string GetPermisos(int idRol, int idOpcion)
        {
            GNPermisos perminso = DAOGNPermisos.get(idRol, idOpcion);
            return JsonConvert.SerializeObject(perminso);
        }
    }
}