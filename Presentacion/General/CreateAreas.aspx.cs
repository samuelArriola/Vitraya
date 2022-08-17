using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.General
{
    public partial class CreateAreas : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CargarTbDireccion(TextBox2.Text); //se carga la tabla por primera ves
                tbDireciones.Columns[0].Visible = false; // se oculta la la columna que muestra el id de la Direccion
                //TextBox1.Attributes.Add("autocomplete", "off");
                TextBox2.Attributes.Add("autocomplete", "off");
                TextBox4.Attributes.Add("autocomplete", "off");

            }
        }

        private void verification()
        {
            try
            {
                Session["admin"].ToString();
            }
            catch (Exception)
            {
                Response.Redirect("~/Log%20in/Login.aspx?Sesion=Debe Iniciar Sesion");
            }
        }


        //Metodo que carga la tabla de los direcciones 
        public void CargarTbDireccion(string nomDireccion)
        {
            // se obtine una lista de la direcciones creadas
            List<GNDireccion> direcciones = DAOGNDireccion.GetDirecciones(nomDireccion);

            //se pasan los datos de la lista a la tabla de direcciones
            tbDireciones.DataSource = direcciones;


            tbDireciones.DataBind();

            //se realiza la actulizacion del panel para visualizacion de los datos
            UpdatePanel1.Update();
        }


        //evento que busca las direciones
        protected void GridArea_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            tbDireciones.EditIndex = -1;
            CargarTbDireccion(TextBox2.Text);
        }

        //evento que realiza la eliminacion de los regitros en la tabla de la direcciones
        protected void GridArea_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //se obtiene el id de la direcion que se desea eliminar
            int codigo = Convert.ToInt32(tbDireciones.DataKeys[e.RowIndex].Values[0]);

            //se elimina la direccion y se comprueva que se haya realizado dicha elimnacion
            if (!DAOGNDireccion.DeleteDireccion(codigo))
            {
                //se manda un mensaje en caso de que no se haya podido eliminar
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "error(\"Error\",\"Este Campo no se puede Eliminar porque esta siendo usuado\")", true);
            }
            CargarTbDireccion(TextBox2.Text);
        }


        //metodo que realiza la actualizacion de los campos de las direcciones
        protected void GridArea_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //se obtiene la fila que a la cual se requiere actualizar
            GridViewRow fila = tbDireciones.Rows[e.RowIndex];

            //se obtine el id de la direccion a actualizar
            int codigo = Convert.ToInt32(tbDireciones.DataKeys[e.RowIndex].Values[0]);

            //se optinen los valores nuevos de la Direccion
            string nombre = (fila.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;
            string Sigla = (fila.FindControl("TextBox3") as System.Web.UI.WebControls.TextBox).Text;
            Sigla = Sigla.ToUpper();

            //se crea una instancia de la direccion para poder realizar la actualizacion
            GNDireccion direccion = new GNDireccion
            {
                StrEstado = "Activo",
                StrNomDir = nombre,
                StrSiglaDir = Sigla,
                IntOidGNDir = codigo,
            };

            //se realisa la actualizacion
            DAOGNDireccion.UpdateGNDireccion(direccion);

            //se sale del modo de edicion
            tbDireciones.EditIndex = -1;

            //se actualizan los datos de la tabla
            CargarTbDireccion(TextBox2.Text);

        }


        //metodo que pone la tabla en modo de edicion
        protected void GridArea_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //se indica el indice de la fila que se requiere actualizar
            tbDireciones.EditIndex = e.NewEditIndex;

            // se actualiza los datos de la tabla
            CargarTbDireccion(TextBox2.Text);

        }


        //metodo que filtra los datos de la tabla por el nombre de la direccion
        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            CargarTbDireccion(TextBox2.Text);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //se verifica que los datos para la creacion de una direccion esten completos
            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "error(\"Error\",\"Se requiere el campo Nombre de Dirección\")", true);

                return;
            }
            if (string.IsNullOrEmpty(TextBox4.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "error(\"Error\",\"Se Requiere el campo Sigla de la Dirección\")", true);
                return;
            }

            //se crea la direcion con los datos ingresados en el formario
            DAOGNDireccion.SetGNDireccion(new GNDireccion
            {
                StrNomDir = TextBox1.Text,
                StrSiglaDir = TextBox4.Text,
                StrEstado = "Activo"
            });

            //se actualizan los datos de la tabla de la direccion
            CargarTbDireccion(TextBox2.Text);

        }
    }
}