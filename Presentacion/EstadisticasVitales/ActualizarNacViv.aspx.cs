using Entidades.EstadisticasVitales;
using Entidades.Generales;
using Newtonsoft.Json;
using Persistencia.EstadisticasVitales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace Presentacion.EstadisticasVitales
{
    public partial class ActualizarNacViv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        public void cargarDatos()
        {
            int idCodRuaf = Convert.ToInt32(Request["OIdCRCodRuaf"].ToString());
            Session["codRuaf"] = idCodRuaf;
            CRCodRuaf codRuaf = DAOCRCodRuaf.getCodRuafId(Convert.ToString(idCodRuaf));
            List<CRRegNacViv> NacViv = DAOCRRegNacViv.getRegNacViv(Convert.ToString(idCodRuaf), "Código RUAF");
            //CodRuafNacViv.Text = codRuaf.DoubleCRcodRuaf.ToString();

            int idUsuario = Convert.ToInt32(Session["admin"]);
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(idUsuario);

            tallaRN.Value = NacViv[0].FloatTallaRN.ToString();
            pesoRN.Text = NacViv[0].IntPesoRn.ToString();
            NomDocNacViv.Text = usuario.GNNomUsu1.ToString();
            IdDocNacViv.Text = usuario.GNCodUsu1.ToString();
            edadGesNacimiento.Text = NacViv[0].IntEdGesNac.ToString();
            fechaNacimiento.Value = NacViv[0].DateFecNac.ToString("yyyy-MM-ddTHH:mm");
            NomMadNV.Text = NacViv[0].StrNomMadre.ToString();
            idMadreNV.Text = NacViv[0].DoubleIdMadre.ToString();
            tipoNacimiento.Value = NacViv[0].StrTipNac.ToString();
            Sexo.Value = NacViv[0].StrSexo.ToString();
        }

        [WebMethod(enableSession: true)]
        public static string updateRegNacViv(CRRegNacViv RegNacViv)
        {

            string codRuaf = Convert.ToString(HttpContext.Current.Session["codRuaf"]);
            DAOCRRegNacViv.updateRegNacViv(RegNacViv, codRuaf);

            DAOCRCodRuaf.sentEmail(DAOCRCodRuaf.GetCodRuafTotVal()); //validar codigos disponibles, para enviar correo en caso de que hallan pocos.

            object[] datos = { RegNacViv, DAOCRCodRuaf.getCodRuafId(codRuaf).DoubleCRcodRuaf };

            return JsonConvert.SerializeObject(datos);
        }
    }
}