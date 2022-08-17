using Entidades.Generales;
using Entidades.Procesos;
using Persistencia.Generales;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Presentacion.Procesos
{
    public partial class EditProceso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarddl();
                CargarTx();
            }

        }

        public void CargarTx()
        {
            string OIdProceso = Request["OIdProceso"];
            PCProceso proceso = DAOProceso.BuscarProceso(Convert.ToInt32(OIdProceso));

            NomProTitle.InnerText = proceso.StrNomPro;
            ddlTipPro.Text = proceso.StrTipo;
            txtNomPro.Text = proceso.StrNomPro;
            ddlPadrePro.Text = proceso.StrProcesoPadre;
            txtPrefijo.Text = proceso.StrPrefijo;

        }


        public void cargarddl()
        {
            List<PCProceso> procesos = DAOProceso.listar();
            List<Cargo> cargos = DAOCargo.GetCargos();
            ddlResponsables.Items.Clear();
            ddlRecHumanos.Items.Clear();
            ddlPadrePro.Items.Clear();
            foreach (var cargo in cargos)
            {

                ListItem item = new ListItem(cargo.StrGnNomCgo, cargo.IntGnDcCgo.ToString());
                ddlResponsables.Items.Add(item);
                ddlRecHumanos.Items.Add(item);
            }


            ddlPadrePro.Items.Add(new ListItem("Ninguno", "-1"));
            foreach (var proceso in procesos)
            {
                ddlPadrePro.Items.Add(new ListItem(proceso.StrNomPro, proceso.IntOIdProceso.ToString()));
            }
        }
    }
}