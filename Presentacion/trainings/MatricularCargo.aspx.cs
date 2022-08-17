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
    public partial class MatricularCargo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblfecha.Text = DateTime.Now.ToShortDateString();
            cargarUsuariosMatriculados();
            if (!IsPostBack)
            {
                cargarDropCargos();
            }
        }


        protected void btnMatricularCargo_Click(object sender, EventArgs e)
        {
            int idCapacitacion = Convert.ToInt32(Request["idCApacitacion"]);

            try
            {
                // se crea una lista de los usuarios que se encuentran en el cargo escogido
                List<Usuario> ListaUsuariosCargo = DAOUsuario.GetUsuariosByCargo(Convert.ToInt32(DropDownList2.Text));

                //se obtiene la capacitacion requerida par la matriculacion
                CPCAPACITACION cap = DAOCPCapacitacion.GetCapacitacion(idCapacitacion);

                //se crea una lista de usuarios que son invalidos
                List<Usuario> usuariosNoValidos = new List<Usuario>();

                //se crea una lista de los correos de los usuarios que se pueden matricular
                List<string> correos = new List<string>();

                //
                foreach (var usuario in ListaUsuariosCargo)

                    //se validan los usuarios que no se pueden matricular
                    if (DAOCPMatricula.validarUsuarioMatriculado(usuario.GNCodUsu1, idCapacitacion))
                    {
                        usuariosNoValidos.Add(usuario);
                    }
                    else
                    {
                        // se matricula los usuarios validos
                        //DAOCPMatricula.matricularUsuarios(usuario.GNCodUsu1.ToString(), "unitario", idCapacitacion, DateTime.Now);

                        //en caso de que la capacitaion se de modalidad virtual documental se marca enseguida la aistencia al usuario
                        if (cap.StrMODALIDAD == "Virtual documental")
                        {
                            DAOCPAsistencia.SetAsistencia(usuario.GNCodUsu1, DateTime.Now, DateTime.Now, cap.IntOidCPCAPACITACION);
                        }

                        //se grega el correo del usuario a la lista de los correos
                        correos.Add(usuario.GNCrusu1);
                    }

                //se envia un mensaje con la informacion de la matriculacion a cada uno de los usuarios que han sido matriculados 
                Enviarcorreo(correos, cap.StrCODIGO, cap.StrLINK, cap.StrTEMA, cap.DtmFECHA.ToString("d") + " " + cap.DtmHORAINICIAL.ToString("t"));

                //se actualiza la tabla de los usuarios matriculados
                cargarUsuariosMatriculados();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        //metodo que valida cada uno de los permisos a las opciones por rol



        public void Enviarcorreo(List<string> correos, string idcapa, string link, string tema, string fecha)
        {
            string msj = "<p><img class=\" preload-me\" style=\"float: left;\" src=\"https://www.clinicacrecer.com/home/wp-content/uploads/2019/05/logocrecer.png\" sizes=\"323px\" srcset=\"https://www.clinicacrecer.com/home/wp-content/uploads/2019/05/logocrecer.png 323w\" alt=\"Clinica Crecer\" width=\"323\" height=\"158\" />&nbsp; Usted ha sido matriculado a una Capacitaci&oacute;n</p>" +
                            "<p style=\"text-align: left;\"><strong> &nbsp; Codigo de la capacitaci&oacute;n</strong>:&nbsp; " + idcapa + " <strong>Tema:&nbsp; " + tema + "</strong>&nbsp;<strong> Fecha:" + fecha + "</strong></p>" +
                            "<p style=\"text-align: left;\"><span style=\"0background-color: #ffffff;\"><span style=\"0color: #3598db;\"><a style=\"0background-color: #ffffff; color: #3598db;\"> Ingresar al aplicativo</a> </span>&nbsp;&nbsp;</span>&nbsp;&nbsp;&nbsp;<span style=\"0color: #236fa1;\">&nbsp;<span style=\"0color: #3598db;\"> <a href=\"" + link + "\">Link de Microsoft Team</a></span></span></p>";
            if (Email.SendMail(correos, msj, "Notificación de Solictud de capacitación"))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "exito(\"Correo Enviado\",\"Se ha enviado la notificación de matricula por correo eletrónico\")", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Tema", "exito(\"Error de Notificación\",\"Se han presentado problemas con en Notificaciones\")", true);
            }
        }


        // evento que redirecion a la opcion de matricular usuario
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Matricula.aspx?idCapacitacion=" + Request["idCapacitacion"].ToString());
        }


        //eve3nto que cierra la sesion
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Session.Remove("admin");
            Session.Remove("id");
            Session.Remove("eje");
            Session.Remove("tema");

            Response.Redirect("~/Log%20in/Login.aspx");
        }

        //metodo que carga el drop con la lista de los cargo

        public void cargarDropCargos()
        {
            //se obriene una lista de los cargos registrados en base de datos
            List<Cargo> cargos = DAOCargo.GetCargos();

            //se limpia los  items  del drop de cargos
            DropDownList2.Items.Clear();

            //se grega la informacion de la lista de cargos al drop
            foreach (var cargo in cargos)
            {
                DropDownList2.Items.Add(new ListItem(cargo.StrGnNomCgo, cargo.IntGnDcCgo.ToString()));
            }
        }


        //metodo que carga la tabla de los usuarios matriculados
        public void cargarUsuariosMatriculados()
        {
            // se obtiene una lista de los usuarios que se encuentran matriculados en la capacitacion
            List<CPMatricula> matriculas = DAOCPMatricula.GetMatriculasCapacitaciones(Convert.ToInt32(Request["idCapacitacion"]));

            //se pasa la informacion de la lista de los usuarios a la tabla de usuarios
            tbUsuariosMatriculados.DataSource = matriculas;

            //se actualiza la tabla con la informacion
            tbUsuariosMatriculados.DataBind();
            upUsuariosMatriculados.Update();
        }

        //evento que se utiliza para eliminar un usario matriculado
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