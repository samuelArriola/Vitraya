using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using Entidades.Generales;

namespace Persistencia.Generales 
{ 
    public class DAOUnidadFuncional
    {
        private static DAOUnidadFuncional dAOUnidadFuncional;
        private DAOUnidadFuncional() { }

        public static DAOUnidadFuncional GetInstance()
        {
            if (dAOUnidadFuncional == null)
                dAOUnidadFuncional = new DAOUnidadFuncional();
            return dAOUnidadFuncional;
        }

        /// <summary>
        /// retornar unidad funcional por id*
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  static UnidadFuncional GetUnidadFuncional(int id)
        {
            UnidadFuncional unidadFuncional = null;


            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  *, (select NomDir from GNDireccion where OidGnDir = D.OidGnDir) as GnNomAra FROM [dbo].[Departamento] as D where GnDcDep = @id and isnull(Eliminado, 0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    unidadFuncional = new UnidadFuncional();
                    unidadFuncional.GnCdAra1 = Convert.ToInt32(reader["OidGNDir"].ToString());
                    unidadFuncional.GnDcDep1 = Convert.ToInt32(reader["GnDcDep"].ToString());
                    unidadFuncional.GnEsDep1 = reader["GnEsDep"].ToString();
                    unidadFuncional.GnNomDep1 = reader["GnNomDep"].ToString();
                    unidadFuncional.GnSiglaUnf1 = reader["GnSiglaUnf"].ToString();
                    unidadFuncional.GnNomArea1 = reader["GnNomAra"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conexion.CloseConnection();
            }
            return unidadFuncional;
        }
        
        /// <summary>
        /// retorna la lista de unidades funcionales.
        /// </summary>
        /// <returns></returns>
        public List<UnidadFuncional> listar()
        {
            List<UnidadFuncional> unidadFuncionales = new List<UnidadFuncional>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  *, (select NomDir from GNDireccion where OidGnDir = D.OidGnDir) as GnNomAra FROM [dbo].[Departamento] as D where isnull(Eliminado, 0) = 0  ORDER by D.GnNomDep", conexion.OpenConnection());
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UnidadFuncional unidadFuncional = new UnidadFuncional();
                    unidadFuncional.GnCdAra1 = Convert.ToInt32(reader[5].ToString());
                    unidadFuncional.GnDcDep1 = Convert.ToInt32(reader["GnDcDep"].ToString());
                    //unidadFuncional.GnIdDep1 = Convert.ToInt32(reader["GnIdDep"].ToString());
                    unidadFuncional.GnEsDep1 = reader["GnEsDep"].ToString();
                    unidadFuncional.GnNomDep1 = reader["GnNomDep"].ToString();
                    unidadFuncional.GnSiglaUnf1 = reader["GnSiglaUnf"].ToString();
                    unidadFuncional.GnNomArea1 = reader["GnNomAra"].ToString();


                    unidadFuncionales.Add(unidadFuncional);
                }
            }
            catch (Exception ex)
            {
                Page page = new Page();
                ScriptManager.RegisterClientScriptBlock(page, page.GetType(),"com1", "error(\"Error\",\"El campo Documento no puede estar vacio\")", true);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return unidadFuncionales;
        }
        
        /// <summary>
        /// consultar unidades funcional usando el nombre.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public static List<UnidadFuncional> GetUnidadesFuncionales(string nombre)
        {
            List<UnidadFuncional> unidadFuncionales = new List<UnidadFuncional>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT  *, (select NomDir from GNDireccion where OidGnDir = D.OidGnDir) as GnNomAra FROM [dbo].[Departamento] as D where GnNomDep like '%'+@GnNomDep+'%' and isnull(Eliminado,0) = 0 ORDER by D.GnNomDep", conexion.OpenConnection());
                command.Parameters.AddWithValue("@GnNomDep", nombre);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UnidadFuncional unidadFuncional = new UnidadFuncional();
                    unidadFuncional.GnCdAra1 = Convert.ToInt32(reader["OidGnDir"].ToString());
                    unidadFuncional.GnDcDep1 = Convert.ToInt32(reader["GnDcDep"].ToString());
                    //unidadFuncional.GnIdDep1 = Convert.ToInt32(reader["GnIdDep"].ToString());
                    unidadFuncional.GnEsDep1 = reader["GnEsDep"].ToString();
                    unidadFuncional.GnNomDep1 = reader["GnNomDep"].ToString();
                    unidadFuncional.GnSiglaUnf1 = reader["GnSiglaUnf"].ToString();
                    unidadFuncional.GnNomArea1 = reader["GnNomAra"].ToString();


                    unidadFuncionales.Add(unidadFuncional);
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

            return unidadFuncionales;
        }
        
        /// <summary>
        /// guardar unidad funcional
        /// </summary>
        /// <param name="unidad"></param>
        public static void setUnidadFuncional(UnidadFuncional unidad)
        {

            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("INSERT INTO [dbo].[Departamento]"+
                                         "          ([GnNomDep]"+
                                         "          ,[GnEsDep]"+
                                         "          ,[GnSiglaUnf]"+
                                         "          ,[OidGnDir])" +
                                         "    VALUES"+
                                         "          (@GnNomDep"+
                                         "          ,@GnEsDep"+
                                         "          ,@GnSiglaUnf"+
                                         "          ,@OidGnDir) Select SCOPE_IDENTITY()", conexion.OpenConnection());

                command.Parameters.AddWithValue("@GnNomDep", unidad.GnNomDep1);
                command.Parameters.AddWithValue("@GnEsDep", unidad.GnEsDep1);
                command.Parameters.AddWithValue("@GnSiglaUnf", unidad.GnSiglaUnf1);
                command.Parameters.AddWithValue("@OidGnDir", unidad.GnCdAra1);

                int OidInstancia = Convert.ToInt32(command.ExecuteScalar().ToString());
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se Crea la unidad funcional {unidad.GnNomDep1}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "Departamento"
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

        public static bool DeleteDepartamento(int idDepartamento)
        {

            bool isDeleted = true;

            SqlCommand command;
            Conexion conexion = new Conexion();
            SqlDataReader reader;
            try
            {
                command = new SqlCommand(@"update Departamento set Eliminado = 1 WHERE GnDcDep = @GnDcDep", conexion.OpenConnection());
                command.Parameters.AddWithValue("@GnDcDep", idDepartamento);
                reader = command.ExecuteReader();

                if (reader.Read()) {
                    DAOGNHistorico.SetHistorico(new GNHistorico
                    {
                        intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                        intInstancia = idDepartamento,
                        intOidGNHistorico = 0,
                        strAccion = "Eliminar",
                        strDetalle = $"Se elimina la unidad funcional {reader["GnNomDep"].ToString()}",
                        dtmFecha = DateTime.Now,
                        strEntidad = "Departamento"
                    });
                }
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

        public static void UpdateDepatamento(UnidadFuncional unidad)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE [dbo].[Departamento] SET [GnNomDep] = @GnNomDep, [GnEsDep] = @GnEsDep, " +
                                         "   [GnSiglaUnf] = @GnSiglaUnf, [OidGnDir] = @OidGnDir WHERE GnDcDep = @GnDcDep ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@GnNomDep", unidad.GnNomDep1);
                command.Parameters.AddWithValue("@GnEsDep", unidad.GnEsDep1);
                command.Parameters.AddWithValue("@GnSiglaUnf", unidad.GnSiglaUnf1);
                command.Parameters.AddWithValue("@OidGnDir",unidad.GnCdAra1);
                command.Parameters.AddWithValue("@GnDcDep", unidad.GnIdDep1);
                command.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = unidad.GnDcDep1,
                    intOidGNHistorico = 0,
                    strAccion = "Modificar",
                    strDetalle = $"Se modifica la unidad funcional {unidad.GnNomDep1}",
                    dtmFecha = DateTime.Now,
                    strEntidad = "GNDireccion"
                });
            }
            catch(Exception wi)
            {
                System.Windows.Forms.MessageBox.Show(wi.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

        }
    }
}