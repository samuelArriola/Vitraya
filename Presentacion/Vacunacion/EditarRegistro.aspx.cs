using Entidades.Generales;
using Entidades.Vacunacion;
using Persistencia.Generales;
using Persistencia.Vacunacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Vacunacion
{
    public partial class EditarRegistro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargartxtAseguradoraAC();
        }

        [WebMethod]
        public static bool UpdateRegistro(RegistroDiarioVac registro)
        {
            //Se realiza la actualizacion del registro con la iformacion ingresada por el usuario
            var result =  DAORegistroDiarioVac.updateRegistroDiarioVac(registro);

            //En caso de que la actualizacion se haya realizado exitosamente
            if (result)
            {
                //se consulta la  informacion de usuario que realiza la actualizacion a traves la sesion de usuario
                Usuario usuarioEditor = DAOUsuario.getInstance().GetUsuario(Convert.ToInt32(HttpContext.Current.Session["Admin"]));

                //se crea una instancia de historico con la informacion del usuario que realizo la actualizacion
                VCHistorico historico = new VCHistorico
                {
                    IntOidRegistroDiarioVac = registro.IntOidRegistroDiarioVac,
                    DtmFecha = DateTime.Now,
                    IntOidUsuario = usuarioEditor.GNCodUsu1,
                    StrNombre = usuarioEditor.GNNomUsu1,
                    StrAccion = "Editar"
                };

                //se gurada el historico ela base de datos
                DAOVCHistorico.SetHistorico(historico);
            }

            //se retorna el resultado de la actualizacion del registro en base de datos 
            return result;
        }
        protected void CargartxtAseguradoraAC()
        {
            // se limpia la informacion de los drops
            txtAseguradora.Items.Clear();
            txtAseguradoraAC.Items.Clear();

            //se consulta un listado de las eps en la base de datos
            List<GNEps> EpsList = DAOGNEps.ListarEps("");

            //se crea el item seleccione, se desactiva y queda seleccionado por promira vez 
            ListItem seleccione = new ListItem("Seleccione", "-1");
            seleccione.Attributes.Add("disabled", "");
            seleccione.Attributes.Add("selected", "");

            //se crea la opcion para pobre no asegurado, en caso de que el contro de regimen se selecione la  opcion de pobre no asegurado
            ListItem pobreNoAsegurado = new ListItem("Pobre no asegurado");
            pobreNoAsegurado.Attributes.Add("style", "display:none");


            //se agrega el item seleccione a los drops
            txtAseguradoraAC.Items.Add(seleccione);
            txtAseguradora.Items.Add(seleccione);


            //por cada eps en la base de datos se agrega la opcion a los drops de las aseguradoras
            foreach (var eps in EpsList)
            {
                txtAseguradoraAC.Items.Add(new ListItem(eps.StrNomEps));
                txtAseguradora.Items.Add(new ListItem(eps.StrNomEps));
            }

            //se grega la opcion de pobre no seguradp a los drops
            txtAseguradoraAC.Items.Add(pobreNoAsegurado);
            txtAseguradora.Items.Add(pobreNoAsegurado);
        }
        /// <summary>
        /// Metoddo web que retorna un listado de los departamentos de Colombia que se encuentran en base de datos 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<dynamic> GetDepartamentos(string name)
        {
            List<dynamic> datos = new List<dynamic>();
            DAOGNDepartamento.GetDeptoByNombre(name).ForEach(departamento =>
                datos.Add(new { text = departamento.StrNombreDepartamento, value = departamento.IntOidGNDepartamento })
            );
            return datos;
        }

        /// <summary>
        /// Metodo web
        /// </summary>
        /// <param name="nombreDepartamento"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<dynamic> GetMunicipio(string nombreDepartamento, string name)
        {
            List<dynamic> datos = new List<dynamic>();

            DAOGNMunicipio.GetMunicipiosByNombreDepto(nombreDepartamento, name).ForEach(municipio =>
                datos.Add(new { text = municipio.StrNombreMunicipio, value = municipio.IntOidGNMunicipio })
            );

            return datos;
        }

        [WebMethod]
        public static RegistroDiarioVac GetRegistro(int idRegistro)
        {
            var registro = DAORegistroDiarioVac.GetRegistroDiarioVac(idRegistro);
            return registro;
        }
    }
}