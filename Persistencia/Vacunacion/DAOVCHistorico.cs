using Entidades.Vacunacion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Vacunacion
{
    public class DAOVCHistorico
    {
        public static void SetHistorico(VCHistorico historico)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" INSERT INTO [dbo].[VCHistorico]
                                                   ([OidUsuario]
		                                           ,[Nombre]
                                                   ,[Fecha]
                                                   ,[Accion]
                                                   ,[OidRegistroDiarioVac])
                                             VALUES
                                                   (@OidUsuario
		                                           ,@Nombre
                                                   ,@Fecha
                                                   ,@Accion
                                                   ,@OidRegistroDiarioVac)", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidUsuario", historico.IntOidUsuario);
                command.Parameters.AddWithValue("Nombre", historico.StrNombre);
                command.Parameters.AddWithValue("Fecha", historico.DtmFecha);
                command.Parameters.AddWithValue("OidRegistroDiarioVac", historico.IntOidRegistroDiarioVac);
                command.Parameters.AddWithValue("Accion", historico.StrAccion);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }
    }
}