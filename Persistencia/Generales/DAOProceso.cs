using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Entidades.Generales;

namespace Persistencia.Generales
{
    public class DAOProceso
    {
        /// <summary>
        /// guardar un proceso a bases de datos
        /// </summary>
        /// <param name="proceso"></param>
        public static void setProceso(Proceso proceso)
        {
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[GNProcesos]"+
                                                            " ([NomPro], "+
                                                            " [Estado]) "+
                                                            " VALUES "+
                                                            " (@NomPro"+
                                                            " ,@Estado)", conexion.OpenConnection());
                command.Parameters.AddWithValue("@NomPro", proceso.StrNomPro);
                command.Parameters.AddWithValue("@Estado", proceso.StrEstado);
                reader = command.ExecuteReader();


                
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

        /// <summary>
        /// Actualizar un proceso
        /// </summary>
        /// <param name="proceso"></param>
        public static void setUpProceso(Proceso proceso)
        {
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[GNProcesos] SET " +
                                                            " [NomPro] = @NomPro, " +
                                                            " [Estado] =  @Estado " +
                                                            " where OIdProceso = @OIdProceso", conexion.OpenConnection());
                command.Parameters.AddWithValue("@NomPro", proceso.StrNomPro);
                command.Parameters.AddWithValue("@Estado", proceso.StrEstado);
                command.Parameters.AddWithValue("@OIdProceso", proceso.IntOIdProceso);
                reader = command.ExecuteReader();



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

        /// <summary>
        /// listar todos los procesos
        /// </summary>
        /// <returns></returns>
        public static List<Proceso> listar()
        {
            List<Proceso> procesos = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GNProcesos", conexion.OpenConnection());
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();

                procesos = new List<Proceso>();
                while (reader.Read())
                {
                    Proceso proceso = new Proceso {
                        IntOIdProceso = Convert.ToInt32(reader["OIdProceso"].ToString()),
                        StrNomPro = reader["NomPro"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                    };
                    procesos.Add(proceso);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
            return procesos;
        }

        /// <summary>
        /// Listar procesos filtrando por el nombre
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static List<Proceso> listarFiltro(string nombre)
        {
            List<Proceso> procesos = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GNProcesos where NomPro like '%'+@NomPro+'%'", conexion.OpenConnection());
                command.Parameters.AddWithValue("@NomPro", nombre);

                reader = command.ExecuteReader();

                procesos = new List<Proceso>();
                while (reader.Read())
                {
                    Proceso proceso = new Proceso
                    {
                        IntOIdProceso = Convert.ToInt32(reader["OIdProceso"].ToString()),
                        StrNomPro = reader["NomPro"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                    };
                    procesos.Add(proceso);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }
            return procesos;
        }

        /// <summary>
        /// buscar un procesos por el nombre.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static Proceso BuscarProceso(string nombre)
        {
            Proceso proceso = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * from GNProcesos where NomPro = @NomPro", conexion.OpenConnection());
                command.Parameters.AddWithValue("@NomPro", nombre);

                reader = command.ExecuteReader();

                
                if(reader.Read())
                {
                    proceso = new Proceso
                    {
                        IntOIdProceso = Convert.ToInt32(reader["OIdProceso"].ToString()),
                        StrNomPro = reader["NomPro"].ToString(),
                        StrEstado = reader["Estado"].ToString(),
                    }; 
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conexion.CloseConnection();
            }

            return proceso;
        }

        /// <summary>
        /// Eliminar un proceso tomando como parametro su id!!
        /// </summary>
        /// <param name="OIdProceso"></param>
        /// <returns></returns>
        public static bool DeleteProceso(int OIdProceso)
        {

            bool isDeleted = true;

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("DELETE FROM GNProcesos WHERE OIdProceso = @OIdProceso", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OIdProceso", OIdProceso);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                isDeleted = false;
            }
            finally
            {
                conexion.CloseConnection();
            }
            return isDeleted;
        }

    }
}