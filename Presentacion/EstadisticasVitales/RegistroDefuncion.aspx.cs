using Entidades.EstadisticasVitales;
using Entidades.Generales;
using Logica.EstadisticasVitales;
using Newtonsoft.Json;
using Persistencia.EstadisticasVitales;
using Persistencia.Generales;
using System;
using System.Web.Services;

namespace Presentacion.EstadisticasVitales
{
    public partial class RegistroDefuncion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            CargarUsuDef();
            validarCodigosDisponibles();
            fechaDefuncion.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-ddThh:mm"));
        }

        public void validarCodigosDisponibles()
        {
            if (DAOCRCodRuaf.GetCodRuafDefVal().Count > 0)
            {
                lbCodDisp.InnerHtml = "" + "Còdigo RUAF Disponibles: " + "<strong >" + DAOCRCodRuaf.GetCodRuafDefVal().Count + "</ strong >";
                btnGuaRegDefFil.Visible = true;
            }
            else
            {
                lbCodDisp.InnerHtml = "" + "No hay códigos disponibles. esperar que admin los suministre al sistema";
                btnGuaRegDefFil.Visible = false;
            }
        }

        public void CargarUsuDef()
        {
            int idUsuario = Convert.ToInt32(Session["admin"]);
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(idUsuario);
            IdDocDef.Text = usuario.GNCodUsu1.ToString();
            NomDocDef.Text = usuario.GNNomUsu1.ToString();

        }

        [WebMethod(enableSession: true)]
        public static string guardarRegDef(CRRegDef RegDef)
        {
            object[] RegDefCod = new object[2];

            if (DAOCRRegDef.getRegDef((RegDef.DoubleIdPaciente).ToString(), "Documento Paciente").Count == 0)
            {
                //CRCodRuaf codRuaf = (CRCodRuaf)HttpContext.Current.Session["codRuaf"];
                CRCodRuaf codRuaf = Community.index(DAOCRCodRuaf.GetCodRuafDefVal());
                RegDef.IntOIdCRCodRuaf = codRuaf.IntOIdCRCodRuaf;
                DAOCRCodRuaf.cambEstCodRuaf(codRuaf.IntOIdCRCodRuaf);
                DAOCRRegDef.setRegDef(RegDef);

                RegDefCod[0] = RegDef;
                RegDefCod[1] = codRuaf.DoubleCRcodRuaf;

                Community.sentEmail(); //validar codigos disponibles, para enviar correo en caso de que hallan pocos.
            }
            else
            {
                return null;
            }


            return JsonConvert.SerializeObject(RegDefCod);
        }
    }
}