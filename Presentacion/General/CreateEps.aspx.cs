using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.General
{
    public partial class CreateEps : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //se quita el autocompletado a los campos tipo textbox
                TextBox1.Attributes.Add("autocomplete", "off");
                TextBox2.Attributes.Add("autocomplete", "off");
                GridEps.Columns[0].Visible = false;
                cargarTablaEps("");
            }
        }


        protected void GridEps_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridEps.EditIndex = -1;
            GridEps.PageIndex = e.NewPageIndex;
            Label2.Text = "";
        }

        //metodo que carga un listado de las EPS en la tabla correspondiente
        public void cargarTablaEps(string nombre)
        {
            // se consulta un listado de todas las EPS de la base de datos
            List<GNEps> listaEps = DAOGNEps.ListarEps(nombre);

            //se pasa la informacion de las EPS a la tabla
            GridEps.DataSource = listaEps;
            GridEps.DataBind();
            UpdatePanel1.Update();

        }
        //Metodo que cancela la actualizacion de una EPS
        protected void GridEps_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridEps.EditIndex = -1;
            cargarTablaEps(TextBox2.Text);
        }

        //eveto que elimina una EPS desde la tabla.
        protected void GridEps_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //se obtiene el oid de la eps de la fila que genero el evento de eliminar
            int codigo = Convert.ToInt32(GridEps.DataKeys[e.RowIndex].Values[0]);

            //se elimina la eps de la base de datos 
            if (!DAOGNEps.DeleteEps(codigo))
            {
                //en caso de que la haya un error al eliminar la eps se muestra un mensaje con el error
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "error(\"Error\",\"Este Campo no se puede Eliminar porque esta siendo usuado\")", true);
            };

            //se recarga la tabla de las eps 
            cargarTablaEps(TextBox2.Text);
        }

        //evento que inicia la actualizacion de una eps
        protected void GridEps_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridEps.EditIndex = e.NewEditIndex;
            cargarTablaEps(TextBox2.Text);
        }

        //metodo que actualiza una eps 
        protected void GridEps_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //se obtiene la fila que genero el evento de edicion
            GridViewRow fila = GridEps.Rows[e.RowIndex];
            //se conuslta el id de la eps que se encuentra en la fila
            int codigo = Convert.ToInt32(GridEps.DataKeys[e.RowIndex].Values["IntOidGNEps"]);

            //se obtiene el nuevo nombre de la eps en el texbox que se encuentra en la eps
            string nombre = (fila.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;
            
            //se crea una instancia de la eps para su actualizacion en la base de datos
            DAOGNEps.UpdateEps(new GNEps
            {
                IntOidGNEps = codigo,
                StrEstado = "Activo",
                StrNomEps = nombre,
            });

            //se da salida al modo edicion 
            GridEps.EditIndex = -1;

            //se recarga la tabla 
            cargarTablaEps(TextBox2.Text);
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            cargarTablaEps(TextBox2.Text);
        }

        //evento de boton de crear eps
        protected void Button1_Click(object sender, EventArgs e)
        {

            // se valida que el campo para el nombre de la eps no este vacio
            if (TextBox1.Text == "")
            {
                //se muestra un mensaje de error en caso de que el campo se encuentre vacio
                ClientScript.RegisterStartupScript(this.GetType(), "error", "error('Error','El Campo Nombre de Eps no puede estar vacio')", true);
                return;
            }

            //se crea una nueva instacia de la eps para su creacion en base de datos
            DAOGNEps.SetGNEps(new GNEps
            {
                StrEstado = "Activo",
                StrNomEps = TextBox1.Text,
            });

            //se recarga la tabla de las eps
            cargarTablaEps(TextBox2.Text);
            TextBox1.Text = "";
        }

        //evento de busqueda de las eps por su nombre 
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox1.Text = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(TextBox1.Text.ToLower()));
        }
    }
}