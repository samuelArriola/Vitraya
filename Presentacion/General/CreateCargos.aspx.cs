using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.General
{
    public partial class CreateCargos : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                GridCargo.Columns[0].Visible = false;
                TextBox1.Attributes.Add("autocomplete", "off");
                TextBox2.Attributes.Add("autocomplete", "off");
                CargarTablaCargos("");
                UnidadFuncional_Departamento();
            }
        }

        //metodo que pasa los datos de una lista de direcciones a un DropDownList
        public void UnidadFuncional_Departamento()
        {
            List<UnidadFuncional> unidades = DAOUnidadFuncional.GetInstance().listar();
            DropDownList1.Items.Clear();
            DropDownList1.Items.Add(new ListItem("Seleccione ...", "-1"));
            foreach (var unidad in unidades)
            {
                DropDownList1.Items.Add(new ListItem(unidad.GnNomDep1, unidad.GnDcDep1.ToString()));
            }
        }

        //metodo que caga la informacion de los cargos en una tabla 
        public void CargarTablaCargos(string nomCargo)
        {
            // se consulta el listado de los cargos en la base de datos
            List<Cargo> cargos = DAOCargo.GetCargos(nomCargo);
            
            //se pasa la informacion de la lista de los cargos a la tabla
            GridCargo.DataSource = cargos;
            GridCargo.DataBind();
            UpdatePanel1.Update();
        }

        //metodo de la tabla que actualiza la informacion de un cargo
        protected void GridCargo_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //se busca el indice de la fila que genero el evento para la actualizacion del cargo
            GridViewRow fila = GridCargo.Rows[e.RowIndex];
            int codigo = Convert.ToInt32(GridCargo.DataKeys[e.RowIndex].Values[0]);

            //se optiene la iformacion de los campos de actualizadion correspondientes a la fila que genero la actualizacion 
            string nombre = (fila.FindControl("TextBox2") as System.Web.UI.WebControls.TextBox).Text;
            string selec = (fila.FindControl("DropDownList2") as System.Web.UI.WebControls.DropDownList).Text;

            //se actuliza el cargo con la informacion registrada
            Cargo cargo = new Cargo
            {
                IntGnDcCgo = codigo,
                IntGnIdCgo = 0,
                StrGnEsCgo = "Activo",
                StrGnNomCgo = nombre,
                StrGnDcDep = selec,
            };
            DAOCargo.updateCargo(cargo);

            //se hace salida del modo de ecicion 
            GridCargo.EditIndex = -1;

            //se recarga la tabla con la informacion de los cargos
            CargarTablaCargos("");
        }

        /// <summary>
        /// Metodo que iniciael modo edicion de Gridview en el indice selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridCargo_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //se obtiene el inddce del la fila a la cual se le va a realizar la actualizacion
            int index = e.NewEditIndex;

            //se ponme la tabla en modo edicion la fila que genero el evento
            GridCargo.EditIndex = index;

            //se carga la tabla
            CargarTablaCargos(TextBox2.Text);


            // se obtiene la fila
            GridViewRow fila = GridCargo.Rows[index];

            // de obtiene el odi del cargo contenido en la fila de la tabla en modo edicion
            int codigo = Convert.ToInt32(GridCargo.DataKeys[index].Values[0]);

            //se optiene el drop el cual tendra una lista de los unidades funcionales 
            DropDownList DropDownList2 = fila.FindControl("DropDownList2") as System.Web.UI.WebControls.DropDownList;
            DropDownList2.Items.Clear();
            DropDownList2.Items.Add(new ListItem("Seleccione ...", "-1"));
            //Crea una lista de todas la unidades funcionales
            List<UnidadFuncional> unidades = DAOUnidadFuncional.GetInstance().listar();

            foreach (var unidad in unidades)
            {
                //se pasan los datos de la lista de unidades funcionales al drop de las unidades funcionales
                DropDownList2.Items.Add(new ListItem(unidad.GnNomDep1, unidad.GnDcDep1.ToString()));
            }


            Cargo cargo = DAOCargo.getCargo(codigo);
            try
            {
                DropDownList2.Text = cargo.StrGnDcDep;
            }
            catch { }
        }

        //evento de la tabla de los cargos que elimina un cargo en la base de datos 
        protected void GridCargo_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // se obtiene el id del cargo que se va eliminar a traves de la fila que genero el evento de eliminacion 
            int codigo = Convert.ToInt32(GridCargo.DataKeys[e.RowIndex].Values[0]);
            
            //se eliminda el cargo de la base de datos a traves de id de este
            if (!DAOCargo.deleteCargo(codigo))
            {
                //en caso de que haya un error eliminado el cargo se muestra un mensanje que idica que hubo un error
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "error(\"Error\",\"No se puede eliminar este campo porque está siendo usado\")", true);
            }

            //se recarga la tabla de los cargos 
            CargarTablaCargos(TextBox2.Text);
        }

        //metodo que cancela el modo edicion de la tabla de los cargos 
        protected void GridCargo_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridCargo.EditIndex = -1;
            CargarTablaCargos(TextBox2.Text);

        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            CargarTablaCargos(TextBox2.Text);
        }


        //evento del boton crear cargo
        protected void Button1_Click(object sender, EventArgs e)
        {
            //se valida que la infomacion para la creacion de un nuevo cargo esta completa
            if (string.IsNullOrEmpty(TextBox1.Text))
            {
                //en caso de que la informacion esta invompleta se muestra un mensaje
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "error(\"Error\",\"Se requiere el campo Nombre de Cargo\")", true);
                return;
            }
            //se crea una nueva instancia del cargo con la informacion registrada
            Cargo cargo = new Cargo
            {
                StrGnDcDep = DropDownList1.Text,
                StrGnEsCgo = "Activo",
                StrGnNomCgo = TextBox1.Text,
            };

            //se guarda esta informacion en la base de datos 
            DAOCargo.setCargo(cargo);

            TextBox1.Text = "";
            DropDownList1.Text = "-1";
            //se vuelve a cargar la tabla de los cargos 
            CargarTablaCargos(TextBox2.Text);
            //se muetra un mensaje indicando que el cargo se ha actualizado correctamente 
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "exito(\"Cargo creado\",\"El Cargo ha sido creado satisfactoriamente\")", true);
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
