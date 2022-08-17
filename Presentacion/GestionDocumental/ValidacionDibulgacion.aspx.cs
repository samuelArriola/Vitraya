using Entidades.Generales;
using Entidades.GestionDocumental;
using Entidades.trainings;
using Persistencia.Generales;
using Persistencia.GestionDocumental;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace Presentacion.GestionDocumental
{
    public partial class ValidacionDibulgacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDdlEjetematico();
            }
        }
        [WebMethod]
        public static List<Cargo> GetCargos(string name)
        {
            return DAOCargo.GetCargosByName(name);
        }

        public void CargarDdlEjetematico()
        {
            List<CPEJETEMATICO> ejes = DAOCPEjeTematico.ListarEjeTem();
            slctEjeTematico.Items.Clear();
            slctEjeTematico.Items.Add(new ListItem("Seleccione", "-1"));
            ejes.ForEach(eje =>
            {
                slctEjeTematico.Items.Add(new ListItem(eje.StrEJETEMATICO, eje.IntOidCPEJETEMATICO + ""));
            });
        }

        [WebMethod]
        public static void SetDivulgacion(GDDivulgacion divulgacion)
        {
            DAOGDDivulgacion.SetGDDivulgacion(divulgacion);
        }

        [WebMethod]
        public static List<GdDocProcedimiento> SetCapacitacion(string nombreC, string idSolicitud)
        {
            return DAOGDDivulgacion.SetCapacitacion(nombreC, idSolicitud);
        }

        [WebMethod]
        public static List<GdDocProcedimiento> SetAgenda(int idCapacitacion, string idSolicitud)
        {
            return DAOGDDivulgacion.SetAgenda(idCapacitacion, idSolicitud);
        }
    }
}