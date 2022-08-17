using Entidades.Generales;
using Entidades.trainings;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.trainings
{
    public class DAOCPExamen
    {
        public static void setExamen(CPEXAMEN examen)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            SqlDataReader reader;
            try
            {
                command = new SqlCommand(@"INSERT INTO [dbo].[CPEXAMEN] ([Nombre] ,[OidInstancia] , [NumApro], [Contexto]) VALUES (@Nombre ,@IDCAPACITACION, @NumApro, @Contexto) 
                                           select SCOPE_IDENTITY() instancia, TEMA from CPCAPACITACION where OidCPCAPACITACION = @IDCAPACITACION", conexion.OpenConnection());

                command.Parameters.AddWithValue("@Nombre",examen.StrNombre);
                command.Parameters.AddWithValue("@IDCAPACITACION", examen.IntOidInstancia);
                command.Parameters.AddWithValue("@NumApro", examen.IntNumApro);
                command.Parameters.AddWithValue("@Contexto", examen.IntContexto);

                reader = command.ExecuteReader();
                reader.Read();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = Convert.ToInt32(reader["instancia"]),
                    strAccion = "Crear",
                    strDetalle = $"Se crea Examen para la capacitacion con tema {reader["TEMA"].ToString()}",
                    strEntidad = "CPEXAMEN"
                });
            }
            catch (Exception wi)
            {
                System.Windows.Forms.MessageBox.Show(wi.Message);
            }finally
            {
                conexion.CloseConnection();
            }
        }

        public static CPEXAMEN getExamenUltimo()
        {
            SqlCommand command;
            SqlDataReader reader;
            CPEXAMEN CPExamen = null;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(" select top(1) * from CPEXAMEN order by OidCPEXAMEN desc ", conexion.OpenConnection());
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    CPExamen = new CPEXAMEN
                    {
                        StrNombre = reader["Nombre"].ToString(),
                        IntOidCPEXAMEN = Convert.ToInt32(reader["OidCPEXAMEN"]),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        IntNumApro = Convert.ToInt32(reader["NumApro"]),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString())
                    };
                }
            }
            catch (Exception wi)
            {
                System.Windows.Forms.MessageBox.Show(wi.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return CPExamen;
        }

        public static CPEXAMEN getExamen(int IdCPCapacitacion, int contexto)
        {
            SqlCommand command;
            SqlDataReader reader;
            CPEXAMEN CPExamen = null;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select top(1)  * from CPEXAMEN where OidInstancia = @OidInstancia and Contexto = @Contexto order by OidCPEXAMEN desc ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidInstancia", IdCPCapacitacion);
                command.Parameters.AddWithValue("@Contexto", contexto);
                reader = command.ExecuteReader();
                if(reader.Read())
                {
                    CPExamen = new CPEXAMEN
                    {
                        StrNombre = reader["Nombre"].ToString(),
                        IntOidCPEXAMEN = Convert.ToInt32(reader["OidCPEXAMEN"]),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        IntNumApro = Convert.ToInt32(reader["NumApro"]),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString())
                    };
                }
            }
            catch (Exception wi)
            {
                System.Windows.Forms.MessageBox.Show(wi.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return CPExamen;
        }

        public static CPEXAMEN getExamen(int idAgenda)
        {
            SqlCommand command;
            SqlDataReader reader;
            CPEXAMEN CPExamen = null;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select E.* from CPEXAMEN E inner join CPAgenda A on A.OidCPExamen = E.OidCPEXAMEN and A.OidCPAgenda = @OidCPAgenda", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OidCPAgenda", idAgenda);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    CPExamen = new CPEXAMEN
                    {
                        StrNombre = reader["Nombre"].ToString(),
                        IntOidCPEXAMEN = Convert.ToInt32(reader["OidCPEXAMEN"]),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        IntNumApro = Convert.ToInt32(reader["NumApro"]),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString())
                    };
                }
            }
            catch (Exception wi)
            {
                System.Windows.Forms.MessageBox.Show(wi.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return CPExamen;
        }


        public static void DeleleteExamen(int idCapacitacion, int contexto)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" select OidCPEXAMEN from CPEXAMEN where OidInstancia = @OidInstancia and Contexto = @Contexto
                                            Delete from CPEXAMEN where OidInstancia = @OidInstancia and Contexto = @Contexto", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidInstancia", idCapacitacion);
                command.Parameters.AddWithValue("Contexto", contexto);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Eliminar",
                    strDetalle = $"",
                    strEntidad = "CPEXAMEN"
                });
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

        public static CPEXAMEN GetExamenByAgenda(int idAgenda)
        {
            CPEXAMEN examen = null;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"Select E.* from CPEXAMEN E
	                                            left join CPAgenda A on A.OidCPExamen = E.OidCPEXAMEN 
                                            where A.OidCPAgenda = @OidCPAgenda", conexion.OpenConnection());

                command.Parameters.AddWithValue("@OidCPAgenda", idAgenda);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    examen = new CPEXAMEN {
                        StrNombre = reader["Nombre"].ToString(),
                        IntOidCPEXAMEN = Convert.ToInt32(reader["OidCPEXAMEN"]),
                        IntOidInstancia = Convert.ToInt32(reader["OidInstancia"].ToString()),
                        IntNumApro = Convert.ToInt32(reader["NumApro"]),
                        IntContexto = Convert.ToInt32(reader["Contexto"].ToString())
                    };
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return examen;
        }

    }
}