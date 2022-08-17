using Entidades.Procesos;
using Persistencia.procesos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.General
{
    public partial class Processes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridProcesos.Columns[0].Visible = false;
                SetListProcesos("");
            }
        }

        /// <summary>
        /// Capturar click al boton guardar proceso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "error", "error(\"error\",\"Completar campos\")", true);
                return;
            }
            if (DAOProceso.BuscarProceso(TextBox1.Text) != null)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "error", "error(\"error\",\"Proceso registrado\")", true);
                TextBox1.Text = "";
                return;
            }

            PCProceso proceso = new PCProceso
            {
                StrNomPro = TextBox1.Text,
                StrEstado = "Activo",
            };

            DAOProceso.setProceso(proceso);
            SetListProcesos("");
            TextBox1.Text = "";
        }

        /// <summary>
        /// Listar procesos filtrando por nombre.
        /// </summary>
        /// <param name="nombre"></param>
        public void SetListProcesos(string nombre)//metodo para llenar la tabla desde la base de datos
        {
            List<PCProceso> procesos = DAOProceso.listarFiltro(nombre);

            GridProcesos.DataSource = procesos;
            GridProcesos.DataBind();
            UpdatePanel1.Update();
        }

        /// <summary>
        /// Castear a mayus todo el contenido del texbox1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox1.Text = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(TextBox1.Text.ToLower()));
        }

        /// <summary>
        /// capturar los parametros digitados al texbox2 para busqueda de procesos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            SetListProcesos(TextBox2.Text);
        }

        /// <summary>
        /// Cancelar modo edicion de filas en la tabla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridArea_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridProcesos.EditIndex = -1;
            SetListProcesos(TextBox2.Text);
        }

        /// <summary>
        /// Eliminar procesos.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridArea_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int codigo = Convert.ToInt32(GridProcesos.DataKeys[e.RowIndex].Values[0]);
            if (!DAOProceso.DeleteProceso(codigo))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "error(\"Error\",\"Este Campo no se puede Eliminar porque esta siendo usuado\")", true);
            }
            SetListProcesos(TextBox2.Text);
        }

        /// <summary>
        /// Activar modo edicion en una fila de la tabla.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridArea_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = e.NewEditIndex;
            GridProcesos.EditIndex = index;
            SetListProcesos(TextBox2.Text);
        }

        /// <summary>
        /// Actualizar proceso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridArea_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow fila = GridProcesos.Rows[e.RowIndex];
            int codigo = Convert.ToInt32(GridProcesos.DataKeys[e.RowIndex].Values[0]);
            string nombre = (fila.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;

            PCProceso proceso = new PCProceso
            {
                IntOIdProceso = codigo,
                StrNomPro = nombre,
                StrEstado = "Activo",
            };

            DAOProceso.setUpProceso(proceso);
            GridProcesos.EditIndex = -1;
            SetListProcesos(TextBox2.Text);
        }
    }
}