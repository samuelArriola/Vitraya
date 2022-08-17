using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.General
{
    public partial class CreateDepartamentos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetListDeparment("");
                SetDropDownList1();
            }
        }

        //boton gurdar departamento!! 
        protected void Button1_Click(object sender, EventArgs e) //evento clic boton guardar
        {
            if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox3.Text))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "error", "error(\"error\",\"Completar campos\")", true);
                return;
            }
            if (DropDownList1.Text == "-1")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "error", "error(\"error\",\"Seleccione Dirección\")", true);
                return;
            }

            if (DAOUnidadFuncional.GetUnidadesFuncionales(TextBox1.Text).Count > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "error", "error(\"Error\",\"Unidad registrada\")", true);
                return;
            }

            UnidadFuncional unidad = new UnidadFuncional
            {
                GnCdAra1 = Convert.ToInt32(DropDownList1.Text),
                GnEsDep1 = "Activo",
                GnNomDep1 = TextBox1.Text,
                GnSiglaUnf1 = TextBox3.Text,
                GnNomArea1 = DropDownList1.SelectedItem.Text,
            };

            DAOUnidadFuncional.setUnidadFuncional(unidad);
            SetListDeparment("");
            TextBox1.Text = "";
            TextBox3.Text = "";
        }

        public void SetListDeparment(string nombre)//metodo para llenar la tabla desde la base de datos
        {
            List<UnidadFuncional> unidadesFuncionales = DAOUnidadFuncional.GetUnidadesFuncionales(nombre);

            GridDepartamento.DataSource = unidadesFuncionales;
            GridDepartamento.DataBind();
            UpdatePanel1.Update();
        }

        public void SetDropDownList1()
        {
            List<GNDireccion> direcciones = DAOGNDireccion.GetDirecciones();
            DropDownList1.Items.Clear();
            foreach (var direccion in direcciones)
            {
                DropDownList1.Items.Add(new ListItem(direccion.StrNomDir, direccion.IntOidGNDir.ToString()));

            }
        }

        //validar entrada de datos para activar boton guardar departamento 
        protected void TextBox1_TextChanged(object sender, EventArgs e) // lebel (nombre unidad funcional)
        {
            TextBox1.Text = (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(TextBox1.Text.ToLower()));
        }

        //verificar que se selecciones una direccion a la cual pertenece. 
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        //verificar entrada de siglas unidad funcional 
        protected void TextBox3_TextChanged(object sender, EventArgs e) // leber (siglas unidad funcional)
        {
            TextBox3.Text = (CultureInfo.InvariantCulture.TextInfo.ToUpper(TextBox3.Text.ToUpper()));
        }

        //cancelar modo edición.
        protected void GridDepartamento_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridDepartamento.EditIndex = -1;
            SetListDeparment(TextBox2.Text);
        }

        // eliminar departamento.
        protected void GridDepartamento_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int codigo = Convert.ToInt32(GridDepartamento.DataKeys[e.RowIndex].Values[0]);
            if (!DAOUnidadFuncional.DeleteDepartamento(codigo))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "error(\"Error\",\"Este Campo no se puede Eliminar porque esta siendo usuado\")", true);
            }
            SetListDeparment(TextBox2.Text);
        }

        protected void GridDepartamento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = e.NewEditIndex;

            //se indica el indice de la fila que se requiere actualizar
            GridDepartamento.EditIndex = index;

            // se actualiza los datos de la tabla
            SetListDeparment(TextBox2.Text);

            GridViewRow fila = GridDepartamento.Rows[index];

            int codigo = Convert.ToInt32(GridDepartamento.DataKeys[index].Values[0]);


            DropDownList DropDownList2 = fila.FindControl("DropDownList2") as System.Web.UI.WebControls.DropDownList;
            List<GNDireccion> direcciones = DAOGNDireccion.GetDirecciones();
            DropDownList2.Items.Clear();

            UnidadFuncional unidad = DAOUnidadFuncional.GetUnidadFuncional(codigo);

            //DropDownList2.Items.Add(new ListItem("seleccione...", "-1"));
            foreach (var direccion in direcciones)
            {
                DropDownList2.Items.Add(new ListItem(direccion.StrNomDir, direccion.IntOidGNDir.ToString()));
            }
            DropDownList2.Text = unidad.GnCdAra1.ToString();

        }

        protected void GridDepartamento_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //se obtiene la fila que a la cual se requiere actualizar
            GridViewRow fila = GridDepartamento.Rows[e.RowIndex];

            //se obtine el id de la direccion a actualizar
            int codigo = Convert.ToInt32(GridDepartamento.DataKeys[e.RowIndex].Values[0]);

            //se optinen los valores nuevos de la Direccion
            string nombre = (fila.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;
            string Sigla = (fila.FindControl("TextBox3") as System.Web.UI.WebControls.TextBox).Text;

            DropDownList dropDownList2 = fila.FindControl("DropDownList2") as System.Web.UI.WebControls.DropDownList;
            Sigla = (CultureInfo.InvariantCulture.TextInfo.ToUpper(Sigla.ToUpper()));

            //se crea una instancia de la direccion para poder realizar la actualizacion
            UnidadFuncional unidad = new UnidadFuncional
            {
                GnCdAra1 = Convert.ToInt32(dropDownList2.Text),
                GnEsDep1 = "Activo",
                GnNomDep1 = nombre,
                GnSiglaUnf1 = Sigla,
                GnIdDep1 = codigo,
            };

            DAOUnidadFuncional.UpdateDepatamento(unidad);
            GridDepartamento.EditIndex = -1;
            SetListDeparment(TextBox2.Text);
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            SetListDeparment(TextBox2.Text);
        }
    }
}