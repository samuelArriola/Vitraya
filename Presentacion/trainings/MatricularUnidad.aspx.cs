using Comunes;
using Entidades.Generales;
using Entidades.trainings;
using Persistencia.Generales;
using Persistencia.trainings;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.trainings
{
    public partial class MatricularUnidad : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDropUnidad();
            }
            txtidcapa.Text = Request["idCapacitacion"];
            lblfecha.Text = DateTime.Now.ToShortDateString();
            cargarUsuariosMatriculados();
        }

        //Se carga la tabla de los usuarios matriculados
        public void cargarUsuariosMatriculados()
        {
            //lista de los usuarios matriculado a la capacitacion
            List<CPMatricula> matriculas = DAOCPMatricula.GetMatriculasCapacitaciones(Convert.ToInt32(Request["idCapacitacion"]));

            //se pasa la información de la lista a la tabla de las matricula 
            tbUsuariosMatriculados.DataSource = matriculas;

            //se actualiza la tabla
            tbUsuariosMatriculados.DataBind();

            upUsuariosMatricuculados.Update();
        }
        //metodo de validacion de los permisos de los roles


        //metodo que carga el drop de las unidades funcionales
        public void CargarDropUnidad()
        {
            //se limpia la informacion del drop
            DropUnidad.Items.Clear();

            //lista de unidades funcionales
            List<UnidadFuncional> unidadesFuncionales = DAOUnidadFuncional.GetInstance().listar();

            //se pasa la informacion de la lista al drop
            foreach (var unidad in unidadesFuncionales)
            {
                DropUnidad.Items.Add(new ListItem(unidad.GnNomDep1, unidad.GnDcDep1.ToString()));
            }
        }

        //evento del boton que realiza las matriculaciones por unidad funcional
        protected void Button4_Click(object sender, EventArgs e)
        {
            //matricular usuario por unidad funcional

            try
            {
                // busco una lista de los usuarios que se encuentran en la unidad funcional seleccionada 
                List<Usuario> UsuariosUnidad = DAOUsuario.GetUsuariosByUnidad(Convert.ToInt32(DropUnidad.Text));

                //se crea una lista de usuarios que no se pueden matricular por que se encuentran matriculados en otras capacitaciones que se crusan en tiempo
                List<Usuario> UsuariosInvalidos = new List<Usuario>();

                //se obtiene La capacitacion
                CPCAPACITACION cap = DAOCPCapacitacion.GetCapacitacion(Convert.ToInt32(Request["idCapacitacion"]));

                // lista de correos a los que se les va a enviar la notificación
                List<string> correos = new List<string>();

                foreach (var usuario in UsuariosUnidad)
                {
                    //se valida cada usuario en la unidad y  se guardan los datos segun corresponda 
                    if (DAOCPMatricula.validarUsuarioMatriculado(usuario.GNCodUsu1, Convert.ToInt32(Request["idCapacitacion"])))
                        UsuariosInvalidos.Add(usuario);
                    else
                    {
                        //DAOCPMatricula.matricularUsuarios(usuario.GNCodUsu1.ToString(), "unitario", Convert.ToInt32(Request["idCapacitacion"]), DateTime.Now);
                        //en caso de que la capacitaion se de modalidad virtual documental se marca enseguida la aistencia al usuario
                        if (cap.StrMODALIDAD == "Virtual documental")
                        {
                            DAOCPAsistencia.SetAsistencia(usuario.GNCodUsu1, DateTime.Now, DateTime.Now, cap.IntOidCPCAPACITACION);
                        }
                        correos.Add(usuario.GNCrusu1);
                    }
                }

                // se envia la ntotificacion de la matricula a los correos correspondientes 
                Enviarcorreo(correos, cap.StrCODIGO, cap.StrLINK, cap.StrTEMA, cap.DtmFECHA.ToString("d") + " " + cap.DtmHORAINICIAL.ToString("t"));
            }
            catch (Exception ex)
            {
                Label2.Text = ex.ToString();
            }
            cargarUsuariosMatriculados();
        }

        //metodo que notifica por correo elecetronico la matriculacion de la cpacitacion
        public void Enviarcorreo(List<string> correos, string idcapa, string link, string tema, string fecha)
        {

            //mensaje con la infomacion que se a enviar por correo electronico
            string msj = "<p><img class=\" preload-me\" style=\"float: left;\" src=\"https://www.clinicacrecer.com/home/wp-content/uploads/2019/05/logocrecer.png\" sizes=\"323px\" srcset=\"https://www.clinicacrecer.com/home/wp-content/uploads/2019/05/logocrecer.png 323w\" alt=\"Clinica Crecer\" width=\"323\" height=\"158\" />&nbsp; Usted ha sido matriculado a una Capacitaci&oacute;n</p>" +
                            "<p style=\"text-align: left;\"><strong> &nbsp; Codigo de la capacitaci&oacute;n</strong>:&nbsp; " + idcapa + " <strong>Tema:&nbsp; " + tema + "</strong>&nbsp;<strong> Fecha:" + fecha + "</strong></p>" +
                            "<p style=\"text-align: left;\"><span style=\"0background-color: #ffffff;\"><span style=\"0color: #3598db;\"><a style=\"0background-color: #ffffff; color: #3598db;\"> Ingresar al aplicativo</a> </span>&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;<span style=\"0color: #236fa1;\">&nbsp;<span style=\"0color: #3598db;\"> <a href=\"" + link + "\">Link de Microsoft Team</a></span></span></p>";

            //se intenta envoa el correo
            if (Email.SendMail(correos, msj, "Notificaficación de Solicitud de capacitación aceptada"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "exito(\"Correo Enviado\",\"Se ha enviado la notificación de matricula por correo eletrónico\")", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "error(\"Error al Enviar El Correo\",\"Hay problemas par enviar el correo de notificación de matriculación\")", true);
            }
        }


        //evento de redirecion a la opcion de matricula
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Matricula.aspx?idCapacitacion=" + Request["idCapacitacion"].ToString());
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Session.Remove("id");
            Session.Remove("eje");
            Session.Remove("tema");

            Response.Redirect("~/Log%20in/Login.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                //Se buscar el id del usuario que se requiere eliminar de la matricula
                int idUsuario = Convert.ToInt32(tbUsuariosMatriculados.SelectedRow.Cells[0].Text);

                //se elimina el usuario de la lista de los matriculados a la capacitación
                if (!DAOCPMatricula.eliminarMatricula(idUsuario, Convert.ToInt32(Request["idCapacitacion"].ToString())))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "error(\"No se puede eliminar esta matricula\",\"No se puede elimanr esta matricula porque ya se marco asistencia\")", true);
                }
                cargarUsuariosMatriculados();

            }
            catch
            {

            }
        }
    }
}