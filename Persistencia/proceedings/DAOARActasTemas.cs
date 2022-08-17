
using Entidades.Generales;
using Entidades.PlanAccion;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.proceedings
{
    public class DAOARActasTemas :Conexion, DAOInterfaz<ARActasTemas>
    {
        public ARActasTemas Get(int id)
        {

            SqlCommand consult;
            SqlDataReader reader;
            ARActasTemas aRActas = null;

            try
            {
                consult = new SqlCommand("SELECT * FROM ARActasTemas where OidARActasTemas = @id and isnull(Eliminado, 0) = 0", OpenConnection());

                //consult.CommandType = CommandType.Text;
                consult.Parameters.AddWithValue("@id", id);
                consult.ExecuteNonQuery();
                reader = consult.ExecuteReader();
                if (reader.Read())
                {
                    string strFlag = reader["OidGNListaArchivos"].ToString();
                    aRActas = new ARActasTemas
                    {
                        StrDesarrollo = reader["Desarrollo"].ToString(),
                        StrAdjuntar = reader["Adjuntar"].ToString(),
                        StrNomTema = reader["NomTema"].ToString(),
                        IntOidARActasTemas = int.Parse(reader["OidARActasTemas"].ToString()),
                        IntOidARActasC = int.Parse(reader["OidARActasC"].ToString()),
                        IntOidGNListaArchivos = (strFlag == "") ? 0 : int.Parse(strFlag),
                        IntPosicion = Convert.ToInt32(reader["Posicion"])
                    };
                }

                CloseConnection();
                return aRActas;
            }
            catch (Exception ex)
            {
                CloseConnection();
                return null;
            }
        }

        /// <summary>
        /// Metodo que consulta un listado de los temas que pertenecen a un acta
        /// </summary>
        /// <param name="id">oid del acta de reunion que se desea consultar </param>
        /// <returns></returns>
        public List<ARActasTemas> Listar(int id)
        {
            List<ARActasTemas> aRActasTemas = new List<ARActasTemas>();


            SqlCommand consult;
            SqlDataReader reader;

            try
            {
                consult = new SqlCommand("select *  from ARActasTemas where OidARActasC = @id and isnull(Eliminado, 0) = 0 order by Posicion", OpenConnection());

                //consult.CommandType = CommandType.Text;
                consult.Parameters.AddWithValue("@id", id);
                consult.ExecuteNonQuery();
                reader = consult.ExecuteReader();
                while (reader.Read())
                {
                    string strFlag = reader["OidGNListaArchivos"].ToString();
                    ARActasTemas aRActas = new ARActasTemas
                    {
                        StrDesarrollo = reader["Desarrollo"].ToString(),
                        StrAdjuntar = reader["Adjuntar"].ToString(),
                        StrNomTema = reader["NomTema"].ToString(),
                        IntOidARActasTemas = int.Parse(reader["OidARActasTemas"].ToString()),
                        IntOidARActasC = int.Parse(reader["OidARActasC"].ToString()),
                        IntOidGNListaArchivos = (strFlag == "") ? 0 : int.Parse(strFlag),
                        IntPosicion = Convert.ToInt32(reader["Posicion"])
                    };

                    aRActasTemas.Add(aRActas);
                }
                CloseConnection();
                return aRActasTemas;
            }
            catch (Exception ex)
            {
                CloseConnection();
                return null;
            }
        }

        public bool set(ARActasTemas data)
        {
            SqlCommand consult;

            try
            {
                consult = new SqlCommand("INSERT INTO [dbo].[ARActasTemas]" +
                                               "([OidARActasC]" +
                                               ",[Desarrollo]" +
                                               ",[NomTema]" +
                                               ",[Eliminado]" +
                                               ",[Posicion])" +
                                         "VALUES" +
                                               "(@OidARActasC," +
                                               "@Desarrollo," +
                                               "@NomTema," +
                                               "0," +
                                               "(select iif(MAX(Posicion) is null, 0, MAX(Posicion))  +1 from ARActasTemas where OidARActasC =  @OidARActasC)) SELECT scope_identity()", OpenConnection());

                consult.Parameters.AddWithValue("@OidARActasC",data.IntOidARActasC);
                consult.Parameters.AddWithValue("@Desarrollo",data.StrDesarrollo);
                consult.Parameters.AddWithValue("@NomTema",data.StrNomTema);
                int OidInstancia = Convert.ToInt32(consult.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $"Se crea el tema {data.StrNomTema} para el acta con código {data.IntOidARActasC}",
                    strEntidad = "ARActasTemas"
                });

                CloseConnection();
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                CloseConnection();
                return false;
            }
        }

        public bool set(List<ARActasTemas> datas)
        {
            throw new NotImplementedException();
        }

        public ARActasTemas setUltiomo()
        {
            SqlCommand consult;
            SqlDataReader reader;
            ARActasTemas aRActasTemas;

            try
            {
                consult = new SqlCommand("SELECT top 1 * FROM dbo.ARActasTemas where isnull(Eliminado, 0) = 0 order by OidARActasTemas desc", OpenConnection());
                consult.ExecuteNonQuery();
                reader = consult.ExecuteReader();
                reader.Read();
                string strFlag = reader["OidGNListaArchivos"].ToString();
                aRActasTemas = new ARActasTemas
                {
                    StrDesarrollo = reader["Desarrollo"].ToString(),
                    StrAdjuntar = reader["Adjuntar"].ToString(),
                    StrNomTema = reader["NomTema"].ToString(),
                    IntOidARActasTemas = int.Parse(reader["OidARActasTemas"].ToString()),
                    IntOidARActasC = int.Parse(reader["OidARActasC"].ToString()),
                    IntOidGNListaArchivos = (strFlag == "") ? 0 : int.Parse(strFlag),
                    IntPosicion = Convert.ToInt32(reader["Posicion"])
                };
                CloseConnection();
                return aRActasTemas;
            }
            catch (Exception ex)
            {
                CloseConnection();
                return null;
            }
        }

        public bool update(int id)
        {
            throw new NotImplementedException();
        }

        public void update(ARActasTemas data)
        {
            SqlCommand consult;

            try
            {
                consult = new SqlCommand(" UPDATE [dbo].[ARActasTemas]"+
                                         " SET[Desarrollo] = @Desarrollo"+
                                              ",[NomTema] = @NomTema"+
                                              ",[OidGNListaArchivos] = @OidGNListaArchivos " +
                                              ",[Posicion] = @Posicion" +
                                        " WHERE OidARActasTemas = @OidARActasTemas", OpenConnection());

                consult.Parameters.AddWithValue("@Desarrollo", data.StrDesarrollo);
                consult.Parameters.AddWithValue("@NomTema", data.StrNomTema);
                consult.Parameters.AddWithValue("@OidGNListaArchivos", data.IntOidGNListaArchivos);
                consult.Parameters.AddWithValue("@OidARActasTemas", data.IntOidARActasTemas);
                consult.Parameters.AddWithValue("@Posicion", data.IntPosicion);

                consult.ExecuteNonQuery();
               
                CloseConnection();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                CloseConnection();
            }
        }

        public bool update(List<ARActasTemas> data)
        {
            throw new NotImplementedException();
        }

        public void delete(int id)
        {
            SqlCommand consult;
            SqlDataReader reader;
            try
            {
                consult = new SqlCommand(@" UPDATE [dbo].[ARActasTemas] set Eliminado = 1 WHERE OidARActasTemas = @OidARActasTemas
                                            Select NomTema, OidARActasC  from ARActasTemas where OidARActasTemas = @OidARActasTemas", OpenConnection());

                consult.Parameters.AddWithValue("@OidARActasTemas", id);

                reader = consult.ExecuteReader();

                reader.Read();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = id,
                    strAccion = "Eliminar",
                    strDetalle = $"Se elimina el tema {reader["NomTema"].ToString()} del acta con el código {reader["OidARActasC"].ToString()}",
                    strEntidad = "ARActasTemas"
                });

                CloseConnection();
            }
            catch (Exception ex)
            {
                CloseConnection();
            }
        }
    }
}