using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Entidades.Generales;
using System.Globalization;

namespace Persistencia.Generales
{
    public class DAOUsuario 
    {
        private static DAOUsuario  dAOUsuario = null ;
        private DAOUsuario() { }

        public static DAOUsuario getInstance()
        {
            if(dAOUsuario == null)
            {
                dAOUsuario = new DAOUsuario();
            }
            return dAOUsuario;
        }

        public static List<Usuario> getUsuarios()
        {
            List<Usuario> usuarios = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@"Select *
                                        FROM Usuario
                                        WHERE GnEtUsu = 'Activo' order by GNNomUsu", conexion.OpenConnection());
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                usuarios = new List<Usuario>();
                while (reader.Read())
                {
                    Usuario usuario = new Usuario()
                    {
                        CodigoR = Convert.ToInt32(reader["CodigoR"]),
                        GnCargo1 = reader["GnCargo"].ToString(),
                        GnCdAra1 = Convert.ToInt32(reader["GnCdAra"]),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"]),
                        GNConUsu1 = reader["GNConUsu"].ToString(),
                        GNCrusu1 = reader["GNCrusu"].ToString(),
                        GnDcCgo1 = Convert.ToInt32(reader["GnDcCgo"]),
                        GnDcDep1 = Convert.ToInt32(reader["GnDcDep"]),
                        GnEpsUsu1 = Convert.ToInt32(reader["GnEpsUsu"]),
                        GnEtUsu1 = reader["GnEtUsu"].ToString(),
                        GNFhUsu1 = Convert.ToDateTime(reader["GNFhUsu"]),
                        GnIdUsu1 = Convert.ToInt32(reader["GnIdUsu"]),
                        GNNomUsu1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
                        GnTlUsu1 = reader["GnTlUsu"].ToString(),
                        GnUnfun1 = reader["GnUnfun"].ToString()
                    };
                   
                    usuarios.Add(usuario);
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CloseConnection();
            }

            return usuarios;
        }

        public static List<Usuario> GetUsuariosAgregarAsitencia(int id)
        {
            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT  *
                                            from Usuario 
                                             where Usuario.GnEtUsu = 'Activo' and Usuario.GNCodUsu   not in
                                             (select GNCodUsu from ARActasDM where OidARActasC = @idActa ) order by Usuario.GNNomUsu", conexion.OpenConnection());
                command.Parameters.AddWithValue("@idActa",id);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario()
                    {
                        CodigoR = Convert.ToInt32(reader["CodigoR"]),
                        GnCargo1 = reader["GnCargo"].ToString(),
                        GnCdAra1 = Convert.ToInt32(reader["GnCdAra"]),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"]),
                        GNConUsu1 = reader["GNConUsu"].ToString(),
                        GNCrusu1 = reader["GNCrusu"].ToString(),
                        GnDcCgo1 = Convert.ToInt32(reader["GnDcCgo"]),
                        GnDcDep1 = Convert.ToInt32(reader["GnDcDep"]),
                        GnEpsUsu1 = Convert.ToInt32(reader["GnEpsUsu"]),
                        GnEtUsu1 = reader["GnEtUsu"].ToString(),
                        GNFhUsu1 = Convert.ToDateTime(reader["GNFhUsu"]),
                        GnIdUsu1 = Convert.ToInt32(reader["GnIdUsu"]),
                        GNNomUsu1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
                        GnTlUsu1 = reader["GnTlUsu"].ToString(),
                        GnUnfun1 = reader["GnUnfun"].ToString()
                    };
                    usuarios.Add(usuario);
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

            return usuarios;
        }

        public static List<Usuario> getUsuariosAsistentes(int id)
        {
            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@"SELECT  *
                                            from Usuario 
                                            where Usuario.GNCodUsu   not in
                                            (select GNCodUsu from AReunionD where AReunionD.OidAReunionC = @id) order by Usuario.GNNomUsu", conexion.OpenConnection());
                
                command.Parameters.AddWithValue("@id", id);
                reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    Usuario usuario = new Usuario()
                    {
                        CodigoR = Convert.ToInt32(reader["CodigoR"]),
                        GnCargo1 = reader["GnCargo"].ToString(),
                        GnCdAra1 = Convert.ToInt32(reader["GnCdAra"]),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"]),
                        GNConUsu1 = reader["GNConUsu"].ToString(),
                        GNCrusu1 = reader["GNCrusu"].ToString(),
                        GnDcCgo1 = Convert.ToInt32(reader["GnDcCgo"]),
                        GnDcDep1 = Convert.ToInt32(reader["GnDcDep"]),
                        GnEpsUsu1 = Convert.ToInt32(reader["GnEpsUsu"]),
                        GnEtUsu1 = reader["GnEtUsu"].ToString(),
                        GNFhUsu1 = Convert.ToDateTime(reader["GNFhUsu"]),
                        GnIdUsu1 = Convert.ToInt32(reader["GnIdUsu"]),
                        GNNomUsu1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
                        GnTlUsu1 = reader["GnTlUsu"].ToString(),
                        GnUnfun1 = reader["GnUnfun"].ToString()
                    };
                    usuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + " usuarios");
            }
            finally
            {
                conexion.CloseConnection();
            }

            return usuarios;
        }

        public Usuario GetUsuario(int id)
        {
            Usuario usuario = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@"SELECT  * from Usuario WHERE  GNCodUsu = @id order by GNNomUsu", conexion.OpenConnection());

                command.Parameters.AddWithValue("@id", id);
                
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new Usuario()
                    {
                        CodigoR = Convert.ToInt32(reader["CodigoR"]),
                        GnCargo1 = reader["GnCargo"].ToString(),
                        GnCdAra1 = Convert.ToInt32(reader["GnCdAra"]),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"]),
                        GNConUsu1 = reader["GNConUsu"].ToString(),
                        GNCrusu1 = reader["GNCrusu"].ToString(),
                        GnDcCgo1 = Convert.ToInt32(reader["GnDcCgo"]),
                        GnDcDep1 = Convert.ToInt32(reader["GnDcDep"]),
                        GnEpsUsu1 = Convert.ToInt32(reader["GnEpsUsu"]),
                        GnEtUsu1 = reader["GnEtUsu"].ToString(),
                        GNFhUsu1 = Convert.ToDateTime(reader["GNFhUsu"]),
                        GNFmUsu1 = (reader["GNFmUsu"].ToString() == "") ? new byte[0] : (byte[])reader["GNFmUsu"],
                        GnFtHull1 = (reader["GnFtHull"].ToString() == "") ? new byte[0] : (byte[])reader["GnFtHull"],
                        GNFtUsu1 = (reader["GNFtUsu"].ToString() == "") ? new byte[0] : (byte[])reader["GNFtUsu"],
                        GnIdUsu1 = Convert.ToInt32(reader["GnIdUsu"]),
                        GNNomUsu1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
                        GnTlUsu1 = reader["GnTlUsu"].ToString(),
                        FechaCambioPass = reader["FechaCambioPass"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["FechaCambioPass"]),
                        GnUnfun1 = reader["GnUnfun"].ToString()
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

            return usuario;
        }

        /// <summary>
        /// Metodo que busca y retorna un lista de usuarios consultandolos por el nombre
        /// </summary>
        /// <param name="nombre">Nombre los usuarios que se van a buscar</param>
        /// <returns></returns>
        public static List<Usuario> getUsuarios(string nombre)
        {
            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT  *
                                            from Usuario where GNNomUsu like '%"+nombre+"%'", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Usuario usuario = new Usuario()
                    {
                        CodigoR = Convert.ToInt32(reader["CodigoR"]),
                        GnCargo1 = reader["GnCargo"].ToString(),
                        GnCdAra1 = Convert.ToInt32(reader["GnCdAra"]),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"]),
                        GNConUsu1 = reader["GNConUsu"].ToString(),
                        GNCrusu1 = reader["GNCrusu"].ToString(),
                        GnDcCgo1 = Convert.ToInt32(reader["GnDcCgo"]),
                        GnDcDep1 = Convert.ToInt32(reader["GnDcDep"]),
                        GnEpsUsu1 = Convert.ToInt32(reader["GnEpsUsu"]),
                        GnEtUsu1 = reader["GnEtUsu"].ToString(),
                        GNFhUsu1 = Convert.ToDateTime(reader["GNFhUsu"]),
                        GnIdUsu1 = Convert.ToInt32(reader["GnIdUsu"]),
                        GNNomUsu1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
                        GnTlUsu1 = reader["GnTlUsu"].ToString(),
                        GnUnfun1 = reader["GnUnfun"].ToString()
                    };
                    usuarios.Add(usuario);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.CloseConnection();
            }
            return usuarios;
        }
        public static List<Usuario> getUsuarios(Usuario usuario)
        {
            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select top 10  * from Usuario WHERE GnEtUsu like '%" + usuario.GnEtUsu1 + "%'  " +
                    "and GNNomUsu like '%" + usuario.GNNomUsu1 + "%'  and GnCargo like '%" + usuario.GnCargo1 + "%' and GNCodUsu " +
                    "like '%" + usuario.GnUnfun1 + "%' and GNCrusu like '%" + usuario.GNCrusu1 + "%' order by GNNomUsu", conexion.OpenConnection());

                command.ExecuteNonQuery();


                reader = command.ExecuteReader();
                usuarios = new List<Usuario>();
                while (reader.Read())
                {
                    Usuario usuario1 = new Usuario()
                    {
                        CodigoR = Convert.ToInt32(reader["CodigoR"]),
                        GnCargo1 = reader["GnCargo"].ToString(),
                        GnCdAra1 = Convert.ToInt32(reader["GnCdAra"]),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"]),
                        GNConUsu1 = reader["GNConUsu"].ToString(),
                        GNCrusu1 = reader["GNCrusu"].ToString(),
                        GnDcCgo1 = Convert.ToInt32(reader["GnDcCgo"]),
                        GnDcDep1 = Convert.ToInt32(reader["GnDcDep"]),
                        GnEpsUsu1 = Convert.ToInt32(reader["GnEpsUsu"]),
                        GnEtUsu1 = reader["GnEtUsu"].ToString(),
                        GNFhUsu1 = Convert.ToDateTime(reader["GNFhUsu"]),
                        GnIdUsu1 = Convert.ToInt32(reader["GnIdUsu"]),
                        GNNomUsu1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
                        GnTlUsu1 = reader["GnTlUsu"].ToString(),
                        GnUnfun1 = reader["GnUnfun"].ToString()
                    };
                    usuarios.Add(usuario1);
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

            return usuarios;
        }

        public static void set(Usuario usuario)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(" INSERT INTO [dbo].[Usuario] " +
                                        "        ([GNCodUsu] " +
                                        "        ,[GNNomUsu] " +
                                        "        ,[GNConUsu] " +
                                        "        ,[GnDcDep] " +
                                        "        ,[GNFhUsu] " +
                                        "        ,[GnEtUsu] " +
                                        "        ,[GnCdAra] " +
                                        "        ,[GnDcCgo] " +
                                        "        ,[GNCrusu] " +
                                        "        ,[GNFtUsu] " +
                                        "        ,[GNFmUsu] " +
                                        "        ,[GnTlUsu] " +
                                        "        ,[GnEpsUsu] " +
                                        "        ,[GnUnfun] " +
                                        "        ,[GnCargo] " +
                                        "        ,[codigoR]) " +
                                        "  VALUES " +
                                        "        (@GNCodUsu, " +
                                        "        @GNNomUsu," +
                                        "        @GNConUsu," +
                                        "        @GnDcDep, " +
                                        "        @GNFhUsu, " +
                                        "        @GnEtUsu, " +
                                        "        @GnCdAra, " +
                                        "        @GnDcCgo," +
                                        "        @GNCrusu, " +
                                        "        @GNFtUsu," +
                                        "        @GNFmUsu,  " +
                                        "        @GnTlUsu,  " +
                                        "        @GnEpsUsu, " +
                                        "        @GnUnfun,  " +
                                        "        @GnCargo," +
                                        "        @codigoR) select SCOPE_IDENTITY()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@GNCodUsu", usuario.GNCodUsu1);
                command.Parameters.AddWithValue("@GNNomUsu", usuario.GNNomUsu1);
                command.Parameters.AddWithValue("@GNConUsu", usuario.GNConUsu1);
                command.Parameters.AddWithValue("@GnDcDep", usuario.GnDcDep1);
                command.Parameters.AddWithValue("@GNFhUsu", usuario.GNFhUsu1);
                command.Parameters.AddWithValue("@GnEtUsu", usuario.GnEtUsu1);
                command.Parameters.AddWithValue("@GnCdAra", usuario.GnCdAra1);
                command.Parameters.AddWithValue("@GnDcCgo", usuario.GnDcCgo1);
                command.Parameters.AddWithValue("@GNCrusu", usuario.GNCrusu1);
                command.Parameters.AddWithValue("@GNFtUsu", usuario.GNFtUsu1);
                command.Parameters.AddWithValue("@GNFmUsu", usuario.GNFmUsu1);
                command.Parameters.AddWithValue("@GnTlUsu", usuario.GnTlUsu1);
                command.Parameters.AddWithValue("@GnEpsUsu", usuario.GnEpsUsu1);
                command.Parameters.AddWithValue("@GnUnfun", usuario.GnUnfun1);
                command.Parameters.AddWithValue("@GnCargo", usuario.GnCargo1);
                command.Parameters.AddWithValue("@codigoR", usuario.CodigoR);

                int OidInstancia = Convert.ToInt32(command.ExecuteScalar().ToString());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se crea el usuario {usuario.GNNomUsu1}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "Usuario"
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

        public static void update(Usuario usuario)
        {
            SqlCommand command;

            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(" UPDATE[dbo].[Usuario] " +
                                        "    SET [GNNomUsu] = @GNNomUsu  " +
                                        "       ,[GNConUsu] = @GNConUsu  " +
                                        "       ,[GnDcDep] = @GnDcDep  " +
                                        "       ,[GNFhUsu] = @GNFhUsu " +
                                        "       ,[GnEtUsu] = @GnEtUsu" +
                                        "       ,[GnCdAra] = @GnCdAra" +
                                        "       ,[GnDcCgo] = @GnDcCgo" +
                                        "       ,[GNCrusu] = @GNCrusu" +
                                        "       ,[GNFtUsu] = @GNFtUsu" +
                                        "       ,[GNFmUsu] = @GNFmUsu" +
                                        "       ,[GnTlUsu] = @GnTlUsu" +
                                        "       ,[GnEpsUsu] = @GnEpsUsu" +
                                        "       ,[GnUnfun] = @GnUnfun" +
                                        "       ,[GnCargo] = @GnCargo" +
                                        "       ,[codigoR] = @codigoR" +
                                        "       ,[FechaCambioPass] = @FechaCambioPass" +
                                        "  WHERE GNCodUsu = @GNCodUsu ", conexion.OpenConnection());

                command.Parameters.AddWithValue("@GNCodUsu", usuario.GNCodUsu1);
                command.Parameters.AddWithValue("@GNNomUsu", usuario.GNNomUsu1);
                command.Parameters.AddWithValue("@GNConUsu", usuario.GNConUsu1);
                command.Parameters.AddWithValue("@GnDcDep", usuario.GnDcDep1);
                command.Parameters.AddWithValue("@GNFhUsu", usuario.GNFhUsu1);
                command.Parameters.AddWithValue("@GnEtUsu", usuario.GnEtUsu1);
                command.Parameters.AddWithValue("@GnCdAra", usuario.GnCdAra1);
                command.Parameters.AddWithValue("@GnDcCgo", usuario.GnDcCgo1);
                command.Parameters.AddWithValue("@GNCrusu", usuario.GNCrusu1);
                command.Parameters.AddWithValue("@GNFtUsu", usuario.GNFtUsu1);
                command.Parameters.AddWithValue("@GNFmUsu", usuario.GNFmUsu1);
                command.Parameters.AddWithValue("@GnTlUsu", usuario.GnTlUsu1);
                command.Parameters.AddWithValue("@GnEpsUsu", usuario.GnEpsUsu1);
                command.Parameters.AddWithValue("@GnUnfun", usuario.GnUnfun1);
                command.Parameters.AddWithValue("@GnCargo", usuario.GnCargo1);
                command.Parameters.AddWithValue("@codigoR", usuario.CodigoR);
                command.Parameters.AddWithValue("@FechaCambioPass", usuario.FechaCambioPass);

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
        /// <summary>
        /// metodo que devuelve una lista de Usuarios que se encuentran en una Unidad Funcional en especifico
        /// </summary>
        /// <param name="idUnidad">id de la unidad funcional por el cual se hace la consulta del listado de usuarios</param>
        /// <returns></returns>
        public static List<Usuario> GetUsuariosByUnidad(int idUnidad)
        {

            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@"SELECT  U.* FROM Usuario AS U LEFT JOIN Departamento AS D ON D.GnDcDep = U.GnDcDep WHERE U.GnDcDep = @GnDcDep and U.GnEtUsu = 'Activo'", conexion.OpenConnection());
                command.Parameters.AddWithValue("@GnDcDep", idUnidad);
                reader = command.ExecuteReader();
                usuarios = new List<Usuario>();
                while (reader.Read())
                {
                    Usuario usuario1 = new Usuario()
                    {
                        CodigoR = Convert.ToInt32(reader["CodigoR"]),
                        GnCargo1 = reader["GnCargo"].ToString(),
                        GnCdAra1 = Convert.ToInt32(reader["GnCdAra"]),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"]),
                        GNConUsu1 = reader["GNConUsu"].ToString(),
                        GNCrusu1 = reader["GNCrusu"].ToString(),
                        GnDcCgo1 = Convert.ToInt32(reader["GnDcCgo"]),
                        GnDcDep1 = Convert.ToInt32(reader["GnDcDep"]),
                        GnEpsUsu1 = Convert.ToInt32(reader["GnEpsUsu"]),
                        GnEtUsu1 = reader["GnEtUsu"].ToString(),
                        GNFhUsu1 = Convert.ToDateTime(reader["GNFhUsu"]),
                       
                        GnIdUsu1 = Convert.ToInt32(reader["GnIdUsu"]),
                        GNNomUsu1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
                        GnTlUsu1 = reader["GnTlUsu"].ToString(),
                        GnUnfun1 = reader["GnUnfun"].ToString()
                    };
                    usuarios.Add(usuario1);
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

            return usuarios;
        }

        public static List<Usuario> GetUsuariosByCargo(int idCargo)
        {
            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT  U.*
                                            from  Usuario u where U.GnDcCgo = @idCargo and U.GnEtUsu = 'Activo'", conexion.OpenConnection());
                command.Parameters.AddWithValue("@idCargo", idCargo);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Usuario usuario = new Usuario()
                    {
                        CodigoR = Convert.ToInt32(reader["CodigoR"]),
                        GnCargo1 = reader["GnCargo"].ToString(),
                        GnCdAra1 = Convert.ToInt32(reader["GnCdAra"]),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"]),
                        GNConUsu1 = reader["GNConUsu"].ToString(),
                        GNCrusu1 = reader["GNCrusu"].ToString(),
                        GnDcCgo1 = Convert.ToInt32(reader["GnDcCgo"]),
                        GnDcDep1 = Convert.ToInt32(reader["GnDcDep"]),
                        GnEpsUsu1 = Convert.ToInt32(reader["GnEpsUsu"]),
                        GnEtUsu1 = reader["GnEtUsu"].ToString(),
                        GNFhUsu1 = Convert.ToDateTime(reader["GNFhUsu"]),
                        
                        GnIdUsu1 = Convert.ToInt32(reader["GnIdUsu"]),
                        GNNomUsu1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
                        GnTlUsu1 = reader["GnTlUsu"].ToString(),
                        GnUnfun1 = reader["GnUnfun"].ToString()
                    };
                    usuarios.Add(usuario);
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

            return usuarios;
        }

        public static List<Usuario> GetUsuariosAprGD()
        {
            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT  U.*
                                            from Usuario as U
                                            inner join GNRoles as R on R.OidGNRol = U.codigoR
                                            inner join GNPermisos as P on p.OidRol = R.OidGNRol
                                            inner join GNOpciones As O on O.OidGNOpcion = P.OidGNOpcion
                                            where p.Modificar = 1 and O.Nombre = 'Aprobar Documento' and U.GnEtUsu = 'Activo'", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Usuario usuario = new Usuario()
                    {
                        CodigoR = Convert.ToInt32(reader["CodigoR"]),
                        GnCargo1 = reader["GnCargo"].ToString(),
                        GnCdAra1 = Convert.ToInt32(reader["GnCdAra"]),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"]),
                        GNConUsu1 = reader["GNConUsu"].ToString(),
                        GNCrusu1 = reader["GNCrusu"].ToString(),
                        GnDcCgo1 = Convert.ToInt32(reader["GnDcCgo"]),
                        GnDcDep1 = Convert.ToInt32(reader["GnDcDep"]),
                        GnEpsUsu1 = Convert.ToInt32(reader["GnEpsUsu"]),
                        GnEtUsu1 = reader["GnEtUsu"].ToString(),
                        GNFhUsu1 = Convert.ToDateTime(reader["GNFhUsu"]),
                       
                        GnIdUsu1 = Convert.ToInt32(reader["GnIdUsu"]),
                        GNNomUsu1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
                        GnTlUsu1 = reader["GnTlUsu"].ToString(),
                        GnUnfun1 = reader["GnUnfun"].ToString()
                    };
                    usuarios.Add(usuario);
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

            return usuarios;
        }

        public static List<Usuario> GetUsuariosRevpGD()
        {
            List<Usuario> usuarios = new List<Usuario>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT  U.*
                                            from Usuario as U
                                            inner join GNRoles as R on R.OidGNRol = U.codigoR
                                            inner join GNPermisos as P on p.OidRol = R.OidGNRol
                                            inner join GNOpciones As O on O.OidGNOpcion = P.OidGNOpcion
                                            where p.Modificar = 1 and O.Nombre = 'Revisar Documento' and U.GnEtUsu = 'Activo'", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Usuario usuario = new Usuario()
                    {
                        CodigoR = Convert.ToInt32(reader["CodigoR"]),
                        GnCargo1 = reader["GnCargo"].ToString(),
                        GnCdAra1 = Convert.ToInt32(reader["GnCdAra"]),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"]),
                        GNConUsu1 = reader["GNConUsu"].ToString(),
                        GNCrusu1 = reader["GNCrusu"].ToString(),
                        GnDcCgo1 = Convert.ToInt32(reader["GnDcCgo"]),
                        GnDcDep1 = Convert.ToInt32(reader["GnDcDep"]),
                        GnEpsUsu1 = Convert.ToInt32(reader["GnEpsUsu"]),
                        GnEtUsu1 = reader["GnEtUsu"].ToString(),
                        GNFhUsu1 = Convert.ToDateTime(reader["GNFhUsu"]),
                       
                        GnIdUsu1 = Convert.ToInt32(reader["GnIdUsu"]),
                        GNNomUsu1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
                        GnTlUsu1 = reader["GnTlUsu"].ToString(),
                        GnUnfun1 = reader["GnUnfun"].ToString()
                    };
                    usuarios.Add(usuario);
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

            return usuarios;
        }

        public static List<Usuario> GetUsuariosAdminGD()
        {
            List<Usuario> usuarios = new List<Usuario>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT U.*
                                            from Usuario U
                                            left join GNPermisos P on P.OidRol = U.codigoR
                                            left join GNOpciones O on O.OidGNOpcion = P.OidGNOpcion
                                            where O.Prefijo like '%GDAprobarSolicitudes%' and U.GnEtUsu = 'Activo'", conexion.OpenConnection());

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Usuario usuario = new Usuario()
                    {
                        CodigoR = Convert.ToInt32(reader["CodigoR"]),
                        GnCargo1 = reader["GnCargo"].ToString(),
                        GnCdAra1 = Convert.ToInt32(reader["GnCdAra"]),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"]),
                        GNConUsu1 = reader["GNConUsu"].ToString(),
                        GNCrusu1 = reader["GNCrusu"].ToString(),
                        GnDcCgo1 = Convert.ToInt32(reader["GnDcCgo"]),
                        GnDcDep1 = Convert.ToInt32(reader["GnDcDep"]),
                        GnEpsUsu1 = Convert.ToInt32(reader["GnEpsUsu"]),
                        GnEtUsu1 = reader["GnEtUsu"].ToString(),
                        GNFhUsu1 = Convert.ToDateTime(reader["GNFhUsu"]),
                        GNFmUsu1 = (reader["GNFmUsu"].ToString() == "") ? new byte[0] : (byte[])reader["GNFmUsu"],
                        GnFtHull1 = (reader["GnFtHull"].ToString() == "") ? new byte[0] : (byte[])reader["GnFtHull"],
                        GNFtUsu1 = (reader["GNFtUsu"].ToString() == "") ? new byte[0] : (byte[])reader["GNFtUsu"],
                        GnIdUsu1 = Convert.ToInt32(reader["GnIdUsu"]),
                        GNNomUsu1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
                        GnTlUsu1 = reader["GnTlUsu"].ToString(),
                        GnUnfun1 = reader["GnUnfun"].ToString()
                    };
                    usuarios.Add(usuario);
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
            return usuarios;
        }

        public static List<Usuario> GetListadoUsuarios(string nombre)
        {
            List<Usuario> usuarios = new List<Usuario>();
            
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@"SELECT  GNNomUsu, GNCodUsu FROM Usuario 
                                           WHERE GNNomUsu LIKE '%' + @GNNomUsu + '%' AND GnEtUsu = 'Activo' ORDER BY GNNomUsu", conexion.OpenConnection());

                command.Parameters.AddWithValue("GNNomUsu", nombre);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    usuarios.Add(new Usuario { 
                        GNNomUsu1 = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(reader["GNNomUsu"].ToString().ToLower()),
                        GNCodUsu1 = Convert.ToInt32(reader["GNCodUsu"])
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
            return usuarios;
        }
    }
}