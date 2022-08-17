using Entidades.trainings;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace Presentacion.trainings
{
    public partial class AddEjeTematico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            tbEjes.Columns[0].Visible = false;
            CargarTablaEjeTematico();
        }

        //SqlConnection cn = new SqlConnection("Data Source=10.244.1.154\\SQLCRECER;Initial Catalog=Vitraya;User ID=sa;Password=XXxx_1");

        public void CargarTablaEjeTematico()
        {
            List<CPEJETEMATICO> ejes = DAOCPEjeTematico.ListarEjeTem();
            tbEjes.DataSource = ejes;
            tbEjes.DataBind();
            upTablaEjes.Update();
        }


        protected void Button1_Click1(object sender, EventArgs e)
        {
            DAOCPEjeTematico.setEjeTem(new CPEJETEMATICO { StrEJETEMATICO = TextBox1.Text });
            CargarTablaEjeTematico();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCapacitacion.aspx");
        }


        protected void tbEjes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            tbEjes.EditIndex = -1;
            CargarTablaEjeTematico();
        }
        protected void TbEjes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int codigo = Convert.ToInt32(tbEjes.DataKeys[e.RowIndex].Values[0]);
            DAOCPEjeTematico.deleteEjeTem(codigo);
            CargarTablaEjeTematico();

        }

        protected void tbEjes_RowEditing(object sender, GridViewEditEventArgs e)
        {
            tbEjes.EditIndex = e.NewEditIndex;
            CargarTablaEjeTematico();
        }

        protected void tbEjes_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //se obtiene la fila que a la cual se requiere actualizar
            GridViewRow fila = tbEjes.Rows[e.RowIndex];

            //se obtine el id del eje tematico a actualizar
            int codigo = Convert.ToInt32(tbEjes.DataKeys[e.RowIndex].Values[0]);

            //se optinen los valores nuevos del eje tematico
            string nombre = (fila.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;


            //se crea una instancia del eje tematico para poder realizar la actualizacion
            CPEJETEMATICO eje = new CPEJETEMATICO
            {
                StrEJETEMATICO = nombre,
                IntOidCPEJETEMATICO = codigo,
            };

            //se realisa la actualizacion
            DAOCPEjeTematico.updateEjeTem(eje);

            //se sale del modo de edicion
            tbEjes.EditIndex = -1;

            //se actualizan los datos de la tabla
            CargarTablaEjeTematico();
        }
    }
}