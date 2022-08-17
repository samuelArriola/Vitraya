using Entidades.Procesos;
using Persistencia.Procesos;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Presentacion.Procesos
{
    public partial class Normagramas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTablaNormagrama();
            }
        }

        public bool ValidarFormulario()
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "error(\"Datos incompletos\",\"El campo Descripcion se encutra vacio\")", true);
                return false;
            }
            if (string.IsNullOrEmpty(txtEmision.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "error(\"Datos incompletos\",\"El campo Emisión se encutra vacio\")", true);
                return false;
            }
            if (string.IsNullOrEmpty(txtFechEmision.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "error(\"Datos incompletos\",\"El campo Fecha de Emisión se encutra vacio\")", true);
                return false;
            }
            if (string.IsNullOrEmpty(txtNumNorma.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "error(\"Datos incompletos\",\"El campo Número de Norma se encutra vacio\")", true);
                return false;
            }
            if (string.IsNullOrEmpty(txtUrl.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "error(\"Datos incompletos\",\"El campo Dirección Electrónica se encutra vacio\")", true);
                return false;
            }
            if (ddlEstado.Text == "-1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "error(\"Datos incompletos\",\"Seleccione un Estado\")", true);
                return false;
            }
            if (ddlTipo.Text == "-1")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "error(\"Datos incompletos\",\"Seleccione un tipo\")", true);
                return false;
            }
            return true;
        }

        public void LimpiarFormulario()
        {
            txtUrl.Text = txtDescripcion.Text = txtEmision.Text = txtFechEmision.Text = txtNumNorma.Text = "";
            ddlTipo.Text = ddlEstado.Text = "-1";
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            PCNormagrama normagrama = new PCNormagrama
            {
                DtmFecEmision = Convert.ToDateTime(txtFechEmision.Text),
                IntNumNorma = Convert.ToInt32(txtNumNorma.Text),
                StrDescripcion = txtDescripcion.Text,
                StrEmision = txtEmision.Text,
                StrEstado = ddlEstado.Text,
                StrUrl = txtUrl.Text,
                StrTipo = ddlTipo.Text,
            };
            DAOPCNormagrama.SetNormagrama(normagrama);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "exito(\"Hecho\",\"El Normagrama ha sido creado correctamente\")", true);
            LimpiarFormulario();
            CargarTablaNormagrama();

        }

        public void CargarTablaNormagrama()
        {
            List<PCNormagrama> normagramas = DAOPCNormagrama.GetNormagramas();
            tbNormagrama.DataSource = normagramas;
            tbNormagrama.DataBind();
        }
    }
}