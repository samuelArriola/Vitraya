using Entidades.Auditorias;
using Persistencia.Auditorias;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Auditorias
{
    public partial class EntesAuditores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTbEntesAuditores();
            }
        }
        //evento que cancela la edicion de un ente auditor en el grid de lso entes auditores
        protected void tbEnteAuditor_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            tbEnteAuditor.EditIndex = -1;
            CargarTbEntesAuditores();
        }

        //evento que elimina un ente auditor de la base de datos
        protected void tbEnteAuditor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //se consulta el id del ente auditor a traves de la fila que disparo el evendo de eliminacion 
            int idEnteAuditor = Convert.ToInt32(tbEnteAuditor.DataKeys[e.RowIndex].Value);
            
            //se elimina el ente auditor de la base de datos a traves de su id
            DAOEnteAuditor.DeleteEnteAuditor(idEnteAuditor);

            //se da da salida al modo de edicion
            tbEnteAuditor.EditIndex = -1;

            //se recarga la grid de los entes auditores 
            CargarTbEntesAuditores();
        }

        //evento que inicia el modo edicion de la grid de entes auditores
        protected void tbEnteAuditor_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tbEnteAuditor.EditIndex = e.NewEditIndex;
            CargarTbEntesAuditores();
        }

        //evento que actualiza la informacion de un ente auditor en base de datos 
        protected void tbEnteAuditor_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //se consulta la fila que genero el evento
            GridViewRow row = tbEnteAuditor.Rows[e.RowIndex];

            //se obtiene el id del ente uditor que se encuentra en la fila que genero el evento
            int idEnteAuditor = Convert.ToInt32(tbEnteAuditor.DataKeys[e.RowIndex].Value);

            //se obtiene la informacion que se encuentra en los controles de la fila en edicion 
            string nombre = ((TextBox)row.FindControl("TxtNombre")).Text;
            string codigo = ((TextBox)row.FindControl("TxtCodigo")).Text;
            string sigla = ((TextBox)row.FindControl("TxtSigla")).Text;

            // se crea una instancia de EnteAuditor con la inforacion registrada y se actualiza en base de datos
            DAOEnteAuditor.UpdateEnteAuditor(new EnteAuditor
            {
                IntOidAUEnteAuditor = idEnteAuditor,
                StrCodigo = codigo,
                StrNombre = nombre,
                StrSigla = sigla
            });

            //se cancela el modo edicion
            tbEnteAuditor.EditIndex = -1;

            //se recarga el grid de los entes auditores
            CargarTbEntesAuditores();
        }


        //metodo que valida que la informacion para la creacion de un nuevo ente se encutre completa
        protected bool ValidarFormulario()
        {
            //se valida el codigo
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "error(\"No se ha Indicado un Código\",\"Por favor diligencie el campo Código\")", true);
                return false;
            }
            //se valida el nombre
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error2", "error(\"No se ha Indicado un Nombre\",\"Por favor diligencie el campo Nombre\")", true);
                return false;
            }

            //se valida la sigla
            if (string.IsNullOrEmpty(txtSigla.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error3", "error(\"No se ha Indicado un Sigla\",\"Por favor diligencie el campo Sigla\")", true);
                return false;
            }

            return true;
        }

        //evento del boton crear ente auditor
        protected void btnCrear_Click(object sender, EventArgs e)
        {
            //se verifica que la informacion para la creacion de un ente auditor este completa
            if (!ValidarFormulario())
                return;

            //se crea una instancia de EnteAuditor y se guarda su informacion en base de datos
            DAOEnteAuditor.SetEnteAuditor(new EnteAuditor
            {
                StrCodigo = txtCodigo.Text,
                StrNombre = txtNombre.Text,
                StrSigla = txtSigla.Text
            });

            //se recarga la tabla de los entes auditores 
            CargarTbEntesAuditores();
        }

        protected void CargarTbEntesAuditores()
        {
            List<EnteAuditor> entes = DAOEnteAuditor.GetEntesAuditores();
            tbEnteAuditor.DataSource = entes;
            tbEnteAuditor.DataBind();
            //upEntesAuditores.Update();
        }
    }
}