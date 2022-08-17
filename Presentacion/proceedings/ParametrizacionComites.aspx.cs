using Entidades.Generales;
using Entidades.PlanAccion;
using Logica.proceedings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.proceedings
{
    public partial class ParametrizacionComites : System.Web.UI.Page
    {
        ActasReunionLogica actasReunionLogica = new ActasReunionLogica();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarddlUnidadeFuncional();
                cargarTablaComites();

                Session["codUnidadFuncional"] = "";
                Session["idComite"] = -1;
            }
        }

        //metodo que carga el ddlUnidadFuncional con un listado de todas las unidades funcionales
        private void cargarddlUnidadeFuncional()
        {
            //se crea un listado de todas las unidades funcionales
            List<UnidadFuncional> unidadFuncionales = actasReunionLogica.GetUnidadFuncionales();

            //se pasa la informacion de la lista al ddlUnidadesFuncionales
            foreach (UnidadFuncional unidadFuncional in unidadFuncionales)
            {
                ddlUnidadFuncional.Items.Add(new ListItem(unidadFuncional.GnNomDep1, unidadFuncional.GnDcDep1.ToString()));
            }
        }



        //metodo que validad que toda la informacion para la creacion de uncomite este completa 
        private bool ValidarFormulario()
        {
            if (
                (txtSigla.Text.Length != 3) ||
                (txtNombre.Text.Length == 0) ||
                (ddlUnidadFuncional.SelectedValue == "-1") ||
                (ddlTipo.SelectedValue == "-1")
            )
            {
                //en caso de que falte un datos se muesta un mensaje de error indicando que hay informacion faltante
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "error1", "error(\"Datos incompletos  o erroneos\",\"llene todos los datos correctamente para crear un comité\")", true);

                return false;
            }
            return true;
        }


        //metodo que crea un comiete
        protected void btnCrearComite_Click(object sender, EventArgs e)
        {
            //se  verifica que toda la informacion este completa
            if (ValidarFormulario())
            {
                //se crea un instancia de la clase AReunionC  con toda lainformacion dada previamente
                AReunionC reunion = new AReunionC();
                reunion.StrTipo = ddlTipo.Text;
                reunion.IntGnCdArea = Convert.ToInt32(ddlUnidadFuncional.SelectedValue);
                reunion.IntGNCodUsu = Convert.ToInt32(Session["admin"].ToString());
                reunion.StrNomReunion = txtNombre.Text;
                reunion.StrSigla = txtSigla.Text.ToUpper();
                reunion.StrCodReunion = txtSigla.Text.ToUpper();
                reunion.IntEstadoReu = 1;

                //se crea el comite en la base de datos
                actasReunionLogica.setAReunionC(reunion);
                cargarTablaComites();

                // se limpian los campos para la creacion del comité
                txtNombre.Text = "";
                txtSigla.Text = "";
                ddlTipo.Text = "-1";
                ddlUnidadFuncional.Text = "-1";

            }
        }


        //metodo que carga la tabla de los comites con la informacion requerida
        private void cargarTablaComites()
        {
            //se crea un lista de los comites
            List<AReunionC> comites = actasReunionLogica.GetAReunionCs(Convert.ToInt32(Session["admin"]));

            //se pasan los datos de la lista de los comites 
            tablaComites.DataSource = comites;
            tablaComites.DataBind();
            upComites.Update();
        }



        protected void tablaComites_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LinkButton buton = (LinkButton)e.CommandSource;
            GridViewRow row = (GridViewRow)buton.NamingContainer;
            if (e.CommandName == "editar")
            {
                DataTable dtComites = (DataTable)Session["dtComites"];
                int idComite = Convert.ToInt32(dtComites.Rows[row.RowIndex]["id"].ToString());
                AReunionC comite = actasReunionLogica.GetAReunionC(idComite);

                txtSigla.Text = comite.StrSigla;
                txtNombre.Text = comite.StrNomReunion;

                Session["idComite"] = comite.IntOidAReunionC;
                btnCrearComite.Text = "Modificar";
                upBtncrear.Update();
            }
        }

        //metodo que cancela el modo edicion de la tabla comites
        protected void tablaComites_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            tablaComites.EditIndex = -1;
            cargarTablaComites();
        }

        protected void tablaComites_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }


        //evento que inicia el mosedicion de la tabla de los comites
        protected void tablaComites_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //se seleciona la fila que se va a poner en modo edicion
            int index = e.NewEditIndex;

            //se indica el indice de la fila que se requiere actualizar
            tablaComites.EditIndex = index;

            // se actualiza los datos de la tabla
            cargarTablaComites();

            GridViewRow fila = tablaComites.Rows[index];

            //se optiene el id del comite a actualizar
            int codigo = Convert.ToInt32(tablaComites.DataKeys[index].Values[0]);

            //obtienen los dropDownList de unida funcional t tipo de comite
            DropDownList DropDownList2 = fila.FindControl("ddlUnidadFunionalTb") as System.Web.UI.WebControls.DropDownList;
            DropDownList DropDownList1 = fila.FindControl("ddlTipoTb") as System.Web.UI.WebControls.DropDownList;

            //se obtiene una lista de unidades funcionales
            List<UnidadFuncional> unidadFuncionales = actasReunionLogica.GetUnidadFuncionales();

            //se limpian los datos del drop de unidades funcionales
            DropDownList2.Items.Clear();

            //se obtiene el comite que se va a actualizar 
            ActasReunionLogica logica = new ActasReunionLogica();
            AReunionC comite = logica.GetAReunionC(codigo);

            //se agregan los datos al drop de tipo de comite
            DropDownList1.Items.Add(new ListItem("Asistencial", "Asistencial"));
            DropDownList1.Items.Add(new ListItem("Administrativo", "Administrativo"));
            DropDownList1.Items.Add(new ListItem("Primario", "Primario"));
            DropDownList1.Items.Add(new ListItem("Financiero", "Financiero"));
            DropDownList1.Items.Add(new ListItem("Sistema de Getión de la Calidad", "Sistema de Getión de la Calidad"));


            //se pasan los datos de la lista de unidades funcionales a drop de unidades funcionales
            foreach (var unidad in unidadFuncionales)
            {
                DropDownList2.Items.Add(new ListItem(unidad.GnNomDep1, unidad.GnDcDep1.ToString()));
            }

            //se selecciona la opcion en los drops que aparece en el comite a actualizar
            DropDownList2.Text = comite.IntGnCdArea.ToString();
            DropDownList1.Text = comite.StrTipo;

        }

        //evento que realiza la actualizacion de un comite
        protected void tablaComites_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //se optiene la fila que corresponde al cominte que se va actualizar
            GridViewRow fila = tablaComites.Rows[e.RowIndex];

            //se obtine el id del comite a actualizar
            int codigo = Convert.ToInt32(tablaComites.DataKeys[e.RowIndex].Values[0]);

            AReunionC comite = new AReunionC();





            comite.IntOidAReunionC = codigo;
            comite.StrSigla = (fila.FindControl("txtSiglaTb") as System.Web.UI.WebControls.TextBox).Text;
            comite.StrNomReunion = (fila.FindControl("txtNombreTb") as System.Web.UI.WebControls.TextBox).Text;
            comite.StrTipo = (fila.FindControl("ddlTipoTb") as System.Web.UI.WebControls.DropDownList).Text;
            comite.StrNomUnidadFuncional = (fila.FindControl("ddlUnidadFunionalTb") as System.Web.UI.WebControls.DropDownList).Text;
            comite.IntGnCdArea = Convert.ToInt32((fila.FindControl("ddlUnidadFunionalTb") as System.Web.UI.WebControls.DropDownList).Text);
            comite.IntPeriodicidad = 0;
            comite.StrCodReunion = comite.StrSigla;
            comite.IntEstadoReu = 1;
            comite.IntGNCodUsu = Convert.ToInt32(Session["Admin"]);

            ActasReunionLogica logica = new ActasReunionLogica();

            logica.updateComite(comite);

            tablaComites.EditIndex = -1;
            cargarddlUnidadeFuncional();
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }

}