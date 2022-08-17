using Entidades.EncuestaCovid;
using Persistencia.EncuestaCovid;
using System;
using System.Web.Services;

namespace Presentacion.LinkExternos
{
    public partial class EncuestaCovid : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static void InsertarEncuesta(string adinamia, string temperatura, string valorTemperatura, string tos, string dificultadRespiratoria, string odinofagia, string dolorLumbar,
            string dolorToracico, string malestarGeneral, string perdidaOlfato, string perdidaGusto, string elementosBioseguridad, string contactoEstrecho, string nombreContactoEstrecho,
            string idContactoEstrecho, string tipoCaso)
        {

            string eps = PEncuestaCovid.GetEPS();

            PEncuestaCovid.SetEncuestaCovid(eps, adinamia, temperatura, valorTemperatura, tos, dificultadRespiratoria, odinofagia, dolorLumbar, dolorToracico, malestarGeneral, perdidaOlfato,
                perdidaGusto, elementosBioseguridad, contactoEstrecho, nombreContactoEstrecho, idContactoEstrecho, tipoCaso);
        }
    }
}