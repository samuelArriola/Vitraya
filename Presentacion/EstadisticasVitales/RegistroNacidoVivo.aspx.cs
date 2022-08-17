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
    public partial class RegistroNacidoVivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            CargarUsuRegViv();
            validarCodigosDisponibles();
            fechaNacimiento.Attributes.Add("max", DateTime.Now.ToString("yyyy-MM-ddThh:mm"));
        }

        public void validarCodigosDisponibles()
        {

            if (DAOCRCodRuaf.GetCodRuafNVVal().Count > 0)
            {
                lbCodDisp.InnerHtml = "" + "Còdigo RUAF Disponibles: " + "<strong >" + DAOCRCodRuaf.GetCodRuafNVVal().Count + "</ strong >";
                btnGuaRegNacVivFil.Visible = true;
            }
            else
            {
                lbCodDisp.InnerHtml = "" + "No hay códigos disponibles. esperar que admin los suministre al sistema";
                btnGuaRegNacVivFil.Visible = false;
            }
        }



        [WebMethod(enableSession: true)]
        public static bool VerificarIdMadre(string IdMadre)
        {
            bool estado = false;
            if (IdMadre != "")
                if (DAOCRRegNacViv.getRegNacViv(IdMadre, "Documento Madre").Count > 0)
                    estado = true;

            return estado;
        }

        [WebMethod(enableSession: true)]
        public static string guardarRegNacViv(CRRegNacViv RegNacViv)
        {
            //CRCodRuaf codRuaf = (CRCodRuaf) HttpContext.Current.Session["codRuaf"]; //variable session objetos global
            CRCodRuaf codRuaf = Community.index(DAOCRCodRuaf.GetCodRuafNVVal());
            RegNacViv.IntCRCodRuaf = codRuaf.IntOIdCRCodRuaf;
            DAOCRCodRuaf.cambEstCodRuaf(codRuaf.IntOIdCRCodRuaf);
            DAOCRRegNacViv.setRegNacViv(RegNacViv);
            object[] regNac = { RegNacViv, codRuaf.DoubleCRcodRuaf };
            Community.sentEmail(); //validar codigos disponibles, para enviar correo en caso de que hallan pocos.

            return JsonConvert.SerializeObject(regNac);
        }

        public void CargarUsuRegViv()
        {

            int idUsuario = Convert.ToInt32(Session["admin"]);
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(idUsuario);
            IdDocNacViv.Text = usuario.GNCodUsu1.ToString();
            NomDocNacViv.Text = usuario.GNNomUsu1.ToString();

        }
    }
}