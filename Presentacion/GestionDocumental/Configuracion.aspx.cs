using Entidades.GestionDocumental;
using Persistencia.GestionDocumental;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.GestionDocumental
{
    public partial class Configuracion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTablaDominio();
                tbDominio.Columns[0].Visible = false;
            }
        }

        /// <summary>
        /// Metodo que crea un Dominio en base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCrearDominio_Click(object sender, EventArgs e)
        {
            //se crea una instancia del dominio
            GDDominio dominio = new GDDominio
            {
                StrNombre = txtNomDominio.Text,
            };

            //se guarda la instacia del dominio en la base de datos
            DAOGDDominio.SetGDDominio(dominio);

            //se muestra un mensaje indicando que el dominio ha sido creado
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "crearDominio", "exito(\"Dominio Creado\",\"El Dominio ha sido creado exitosamente\")", true);

            //se carga el tabla de los dominios con los datos actualizados
            CargarTablaDominio();

            //se limpia el canpo del nombre del dominio
            txtNomDominio.Text = "";
        }

        /// <summary>
        /// metodo que carga la tabla de los dominios con los datos que se encuentran en la base de datos
        /// </summary>
        public void CargarTablaDominio()
        {
            // se obtine una lista de los dominios que se encuentran en base de datos
            List<GDDominio> dominios = DAOGDDominio.GetGDDominios();

            //se pasan los datose de la lista a la tabla de los dominios 
            tbDominio.DataSource = dominios;


            tbDominio.DataBind();
            upDominios.Update();
        }

        /// <summary>
        /// Evento que elimina un domino tanto en la base de datos como en la tabla de los dominios
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tbDominio_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //se obtiene el id del dominio que se desea eliminar 
            int codigo = Convert.ToInt32(tbDominio.DataKeys[e.RowIndex].Values[0]);

            //se elimina el domimio de la base de datos por su id
            DAOGDDominio.DeleteDominio(codigo);

            //se carga la tabla con los datos actualizados de los dominios en la base de datos
            CargarTablaDominio();
        }

        //metodo que cancela el estado de edicion 
        protected void tbDominio_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            tbDominio.EditIndex = -1;
            CargarTablaDominio();
        }

        //metodo que pone a la tabla de los dominios en etado de edicion 
        protected void tbDominio_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tbDominio.EditIndex = e.NewEditIndex;
            CargarTablaDominio();
        }

        //metodo que actualiza un dominio en la bas de datos 
        protected void tbDominio_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //se busca fila en edicion donde se encuentra la informacion del dominio que se desea actualizar 
            GridViewRow fila = tbDominio.Rows[e.RowIndex];

            //se obtine el id de la dominio a actualizar
            int codigo = Convert.ToInt32(tbDominio.DataKeys[e.RowIndex].Values[0]);

            //se captura el nuevo nombre del dominio a traves del contros TextBox2 en la tabla de los dominios 
            string nombre = (fila.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;


            //se crea una instacia de la clase domino
            GDDominio dominio = new GDDominio
            {
                IntOidGDDominio = codigo,
                StrNombre = nombre,
            };


            //se guardan los datos de la instancia en la base de datos
            DAOGDDominio.UpdateGDDominio(dominio);

            //se cancela el modo edicion en la tabla y se actualizan los datos de la tabla
            tbDominio.EditIndex = -1;
            CargarTablaDominio();
        }
    }
}