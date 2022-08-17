using Entidades.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Persistencia.Generales
{
    public class DAOCargo
    {

        public static List<Cargo> GetCargos()
        {
            List<Cargo> cargos = new List<Cargo>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * , (select GnNomDep from Departamento where GnDcDep = c.GnDcDep) as NomDep  from Cargo as C where  isnull(Eliminado,0) = 0  ORDER by GnNomCgo", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Cargo cargo = new Cargo
                    {
                        IntGnIdCgo = (reader["GnIdCgo"].ToString() == "") ? 0 : Convert.ToInt32(reader["GnIdCgo"].ToString()),
                        StrGnDcDep = reader["GnDcDep"].ToString(),
                        StrGnEsCgo = reader["GnEsCgo"].ToString(),
                        StrGnNomCgo = reader["GnNomCgo"].ToString(),
                        IntGnDcCgo = Convert.ToInt32(reader["GnDcCgo"].ToString()),
                        StrNomDep = reader["NomDep"].ToString(),

                    };
                    cargos.Add(cargo);
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
            return cargos;
        }

        public static List<Cargo> GetCargos(string nomCargo)
        {
            List<Cargo> cargos = new List<Cargo>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * , (select GnNomDep from Departamento where GnDcDep = c.GnDcDep) as NomDep  from Cargo as C  where GnNomCgo like '%'+@GnNomCgo+'%' and isnull(Eliminado,0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@GnNomCgo", nomCargo);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Cargo cargo = new Cargo
                    {
                        IntGnIdCgo = (reader["GnIdCgo"].ToString() == "") ? 0 : Convert.ToInt32(reader["GnIdCgo"].ToString()),
                        StrGnDcDep = reader["GnDcDep"].ToString(),
                        StrGnEsCgo = reader["GnEsCgo"].ToString(),
                        StrGnNomCgo = reader["GnNomCgo"].ToString(),
                        IntGnDcCgo = Convert.ToInt32(reader["GnDcCgo"].ToString()),
                        StrNomDep = reader["NomDep"].ToString(),
                    };
                    cargos.Add(cargo);
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
            return cargos;
        }

        public static void setCargo(Cargo cargo)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[Cargo]"+
                                         "      ([GnNomCgo]"+
                                         "      ,[GnEsCgo]"+
                                         "      ,[GnDcDep]" +
                                         "      ,Eliminado)"+
                                         " VALUES"+
                                         "     (@GnNomCgo"+
                                         "      ,@GnEsCgo"+
                                         "      ,@GnDcDep" +
                                         "      ,0)" +
                                         "select SCOPE_IDENTITY()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@GnNomCgo", cargo.StrGnNomCgo);
                command.Parameters.AddWithValue("@GnEsCgo", cargo.StrGnEsCgo);
                command.Parameters.AddWithValue("@GnDcDep", cargo.StrGnDcDep);

                int idInstancia = Convert.ToInt32(command.ExecuteScalar().ToString());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = idInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se crea el cargo {cargo.StrGnNomCgo}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "Cargo"
                }) ;

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
        public static Cargo getCargo(int idCargo)
        {
            Cargo cargo = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("Select * , (select GnNomDep from Departamento where GnDcDep = c.GnDcDep) as NomDep  from Cargo as C  WHERE GnDcCgo = @GnDcCgo and isnull(Eliminado,0) = 0 ORDER by GnNomCgo ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@GnDcCgo", idCargo);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    cargo = new Cargo
                    {
                        IntGnIdCgo = (reader["GnIdCgo"].ToString() == "")? 0 : Convert.ToInt32(reader["GnIdCgo"].ToString()),
                        StrGnDcDep = reader["GnDcDep"].ToString(),
                        StrGnEsCgo = reader["GnEsCgo"].ToString(),
                        StrGnNomCgo = reader["GnNomCgo"].ToString(),
                        IntGnDcCgo = Convert.ToInt32(reader["GnDcCgo"].ToString()),
                        StrNomDep = reader["NomDep"].ToString(),
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

            return cargo;
        }

        public static bool updateCargo(Cargo cargo)
        {
            bool isUpdate = true;
            
            SqlCommand command;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" UPDATE [dbo].[Cargo] SET [GnNomCgo] = @GnNomCgo ,[GnEsCgo] = @GnEsCgo,[GnDcDep] = @GnDcDep  WHERE GnDcCgo = @GnDcCgo and isnull(Eliminado,0) = 0", conexion.OpenConnection());

                command.Parameters.AddWithValue("@GnNomCgo", cargo.StrGnNomCgo);
                command.Parameters.AddWithValue("@GnEsCgo", cargo.StrGnEsCgo);
                command.Parameters.AddWithValue("@GnDcDep", Convert.ToInt32(cargo.StrGnDcDep));
                command.Parameters.AddWithValue("@GnDcCgo", cargo.IntGnDcCgo);

               command.ExecuteNonQuery();


                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = cargo.IntGnDcCgo,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"Se actuliza la información del cargo {cargo.StrGnNomCgo}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "Cargo"
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
            return isUpdate;
        }
        public static bool deleteCargo(int idCargo)
        {
            bool isDeleted = true;

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("update cargo set Eliminado = 1 where GnDcCgo = @GnDcCgo", conexion.OpenConnection());
                command.Parameters.AddWithValue("@GnDcCgo", idCargo);
                reader = command.ExecuteReader();

                if(reader.Read())
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = idCargo,
                    intOidGNHistorico = 0,
                    strAccion = "Eliminar",
                    strDetalle = $"Se elimina el cargo {reader["GnNomCgo"].ToString()}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "Cargo"
                });
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

        public static List<Cargo> GetCargosByName(string name)
        {
            List<Cargo> cargos = new List<Cargo>();

            SqlCommand command;
            SqlDataReader reader;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from Cargo where GnNomCgo like '%' + @GnNomCgo + '%' and isnull(Eliminado,0) = 0 ORDER by GnNomCgo", conexion.OpenConnection());
                command.Parameters.AddWithValue("GnNomCgo", name);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cargos.Add(new Cargo {
                        IntGnIdCgo = (reader["GnIdCgo"].ToString() == "") ? 0 : Convert.ToInt32(reader["GnIdCgo"].ToString()),
                        StrGnDcDep = reader["GnDcDep"].ToString(),
                        StrGnEsCgo = reader["GnEsCgo"].ToString(),
                        StrGnNomCgo = reader["GnNomCgo"].ToString(),
                        IntGnDcCgo = Convert.ToInt32(reader["GnDcCgo"].ToString()),
                    });
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

            return cargos;
        }
    }
}