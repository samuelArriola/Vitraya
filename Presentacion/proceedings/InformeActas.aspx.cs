using Entidades.PlanAccion;
using Persistencia.proceedings;
using System;
using System.Collections.Generic;
using System.Web.Services;

namespace Presentacion.proceedings
{
    public partial class InformeActas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod]
        public static List<object> GetActas(string nombre, string codigo, DateTime fecha1, DateTime fecha2, string coordinador, string estado)
        {
            //se obtiene una lista de todas las actas segun los parametros de consulta
            List<ARActasC> actas = DAOARActasC.GetActas(nombre, codigo, fecha1, fecha2, coordinador, estado);

            List<object> datos = new List<object>();
            foreach (var acta in actas)
            {
                //se obtine una lista de todos los que participan en el comite
                List<ARActasDM> participantes = DAOARactasDM.getParticipantes(acta.IntOidARActas);

                bool isTotalFirmado = true;

                List<ARActasDM> asistentesSinFirma = new List<ARActasDM>();
                foreach (var participante in participantes)
                {
                    if (!participante.BlnFirmado && participante.IntEstUsuario == 1)
                    {
                        isTotalFirmado = false;
                        asistentesSinFirma.Add(participante);
                    }
                }
                if (isTotalFirmado)
                    acta.IntEstado = 3;

                datos.Add(new object[] { acta, asistentesSinFirma });
            }

            return datos;
        }
    }
}