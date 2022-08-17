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
    public partial class ActualizarDef : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        public void cargarDatos()
        {
            //se consulta el id del codigo ruaf que se encuentra como paramentro en la url del navegador 
            int idCodRuaf = Convert.ToInt32(Request["OIdCRCodRuaf"].ToString());
            
            //se gurda la informacion del id en la variable de sesion 
            Session["codRuaf"] = idCodRuaf;
            CRCodRuaf codRuaf = DAOCRCodRuaf.getCodRuafId(Convert.ToString(idCodRuaf));
            //se consulta el registro de defuncion en la base de datos a traves de id
            List<CRRegDef> RefDef = DAOCRRegDef.getRegDef(Convert.ToString(idCodRuaf), "Código RUAF");

            //se consulta el id del usuario que se encuentra logueado
            int idUsuario = Convert.ToInt32(Session["admin"]);
            //se consulta el usuario en la base de datos 
            Usuario usuario = DAOUsuario.getInstance().GetUsuario(idUsuario);

            //se muestra la informacion consultada en el formulario
            NomDocDef.Text = usuario.GNNomUsu1.ToString();
            IdDocDef.Text = usuario.GNCodUsu1.ToString();
            nomPacDef.Text = RefDef[0].StrNomPac.ToString();
            idPacDef.Text = RefDef[0].DoubleIdPaciente.ToString();
            ServDef.Value = RefDef[0].StrServicio.ToString();
            tipDef.Value = RefDef[0].StrTipDef.ToString();
            fechaDefuncion.Value = RefDef[0].DateFecDef.ToString("yyyy-MM-ddTHH:mm");
        }

        /// <summary>
        /// metodo web que actualiza la informacion de un registr de defunción 
        /// </summary>
        /// <param name="RegDef"></param>
        /// <returns></returns>
        [WebMethod(enableSession: true)]
        public static string ActualizarRegDef(CRRegDef RegDef)
        {
            //se consulta el id de del codigo a actualizar  en la variable de sesion el cual se habia guradado previamente
            string codRuaf = Convert.ToString(HttpContext.Current.Session["codRuaf"]);
            
            //se actualiza el codigo ruaf con la informacion suminstrada por el usuario logueado  
            DAOCRRegDef.updateRegDef(RegDef, codRuaf);

            //se envia una notificacion al usuario administrador
            DAOCRCodRuaf.sentEmail(DAOCRCodRuaf.GetCodRuafTotVal()); //validar codigos disponibles, para enviar correo en caso de que hallan pocos.

            object[] datos = { RegDef, DAOCRCodRuaf.getCodRuafId(codRuaf).DoubleCRcodRuaf };

            return JsonConvert.SerializeObject(datos);
        }
    }
}