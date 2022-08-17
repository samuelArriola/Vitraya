
using Entidades.Generales;
using Entidades.PlanAccion;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.proceedings
{
    public class DAOAReunionC : Conexion, DAOInterfaz<AReunionC>
    {

        public AReunionC Get(int id)
        {
            AReunionC reunion = null;


            SqlCommand consult;
            SqlDataReader reader;

            try
            {
                consult = new SqlCommand("SELECT  [OidAReunionC]"+
                                              ",[CodReunion]"+
                                              ",[NomReunion]"+
                                              ",[EstadoReu]"+
                                              ",[GNCodUsu]"+
                                              ",[Periodicidad]"+
                                              ",[GnCdArea]"+
                                              ",[Tipo]"+
                                              ",[Sigla]"+
                                              ", a.GnNomDep "+
                                          "FROM[dbo].[AReunionC]"+
                                          "LEFT JOIN Departamento as a ON  a.GnIdDep = GnCdArea  where AReunionC.OidAReunionC =  '" + id + "'", OpenConnection());

                consult.CommandType = CommandType.Text;
                consult.ExecuteNonQuery();
                reader = consult.ExecuteReader();


                if (reader.Read())
                {
                    reunion = new AReunionC();
                    reunion.StrCodReunion = reader["CodReunion"].ToString();
                    reunion.StrNomReunion = reader["NomReunion"].ToString();
                    reunion.StrTipo = reader["Tipo"].ToString();
                    reunion.StrSigla = reader["Sigla"].ToString();
                    reunion.IntEstadoReu = Convert.ToInt32(reader["EstadoReu"].ToString());
                    reunion.IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString());
                    reunion.IntPeriodicidad = Convert.ToInt32(reader["Periodicidad"].ToString());
                    reunion.IntGnCdArea = Convert.ToInt32(reader["GnCdArea"].ToString());
                    reunion.StrNomUnidadFuncional = reader["GnNomDep"].ToString();
                    reunion.IntOidAReunionC = Convert.ToInt32(reader["OidAReunionC"].ToString());
                }

                CloseConnection();
                return reunion;
            }
            catch (Exception ex)
            {
                CloseConnection();
                return null;
            }
        }

        public static List<AReunionC> ListarReunionesSinConvocar(int id)
        {
            List<AReunionC> aReunionC = new List<AReunionC>();


            SqlCommand consult;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                consult = new SqlCommand("SELECT ar.[OidAReunionC]"+
                                                  ",[CodReunion]"+
                                                  ",[NomReunion]"+
                                                  ",[EstadoReu]"+
                                                  ",ar.[GNCodUsu]"+
                                                  ",[Periodicidad]"+
                                                  ",[GnCdArea]"+
                                                  ",[Tipo]"+
                                                  ",[Sigla]"+
                                                  ",[OidGNCronograma]" +
                                                  ",de.GnNomDep" +
                                             " FROM[dbo].[AReunionC] as ar"+
                                             " left join AReunionD as ad on ad.OidAReunionC = ar.OidAReunionC"+
                                             " left join Departamento as de on de.GnDcDep = ar.GnCdArea" +
                                             " where ad.GNCodUsu = @id and ad.TipoUsuario = 1 and"+
                                             " ar.OidAReunionC not in (select OidAReunionC from ARActasC where ARActasC.Estado = 1 )", conexion.OpenConnection());


                consult.CommandType = CommandType.Text;
                consult.Parameters.AddWithValue("@id", id);
                consult.ExecuteNonQuery();
                reader = consult.ExecuteReader();


                while (reader.Read())
                {
                    AReunionC reunion = new AReunionC();

                    reunion.IntOidAReunionC = Convert.ToInt32(reader["OidAReunionC"].ToString());
                    reunion.StrCodReunion = reader["CodReunion"].ToString();
                    reunion.StrNomReunion = reader["NomReunion"].ToString();
                    reunion.StrTipo = reader["Tipo"].ToString();
                    reunion.StrSigla = reader["Sigla"].ToString();
                    reunion.IntEstadoReu = Convert.ToInt32(reader["EstadoReu"].ToString());
                    reunion.IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString());
                    reunion.IntPeriodicidad = Convert.ToInt32(reader["Periodicidad"].ToString());
                    reunion.IntGnCdArea = Convert.ToInt32(reader["GnCdArea"].ToString());
                    reunion.StrNomUnidadFuncional = reader["GnNomDep"].ToString();

                    aReunionC.Add(reunion);
                }
               
                conexion.CloseConnection();
                return aReunionC;
            }
            catch (Exception ex)
            {
                conexion.CloseConnection();
                return null;
            }
        }

        public   List<AReunionC> Listar(int id)
        {
            List<AReunionC> aReunionC = new List<AReunionC>();


            SqlCommand consult;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                consult = new SqlCommand("SELECT [OidAReunionC] ,[CodReunion] ,[NomReunion] ,[EstadoReu] ,[GNCodUsu] " +
                                                ",[Periodicidad],[GnCdArea],[Tipo],[Sigla], a.GnNomDep " +
                                          "FROM[dbo].[AReunionC] as reunion " +
                                                "LEFT JOIN Departamento as a ON  a.GnDcDep = GnCdArea where OidAReunionC <> 6017", conexion.OpenConnection());


                consult.CommandType = CommandType.Text;
                consult.ExecuteNonQuery();
                reader = consult.ExecuteReader();


                while (reader.Read())
                {
                    AReunionC reunion = new AReunionC();

                    reunion.IntOidAReunionC = Convert.ToInt32(reader["OidAReunionC"].ToString());
                    reunion.StrCodReunion = reader["CodReunion"].ToString();
                    reunion.StrNomReunion = reader["NomReunion"].ToString();
                    reunion.StrTipo = reader["Tipo"].ToString();
                    reunion.StrSigla = reader["Sigla"].ToString();
                    reunion.IntEstadoReu = Convert.ToInt32(reader["EstadoReu"].ToString());
                    reunion.IntGNCodUsu = Convert.ToInt32(reader["GNCodUsu"].ToString());
                    reunion.IntPeriodicidad = Convert.ToInt32(reader["Periodicidad"].ToString());
                    reunion.IntGnCdArea = Convert.ToInt32(reader["GnCdArea"].ToString());
                    reunion.StrNomUnidadFuncional = reader["GnNomDep"].ToString();

                    aReunionC.Add(reunion);
                }

                conexion.CloseConnection();
                return aReunionC;
            }
            catch (Exception ex)
            {
                conexion.CloseConnection();
                return null;
            }
        }

        public  bool set(AReunionC data)
        {
            SqlCommand consult;
            Conexion conexion = new Conexion();
            try
            {
                consult = new SqlCommand("INSERT INTO [dbo].[AReunionC]"+
                                              " ([CodReunion]"+
                                              " ,[NomReunion]"+
                                              " ,[EstadoReu]"+
                                              " ,[GNCodUsu]"+
                                              " ,[Periodicidad]"+
                                              " ,[GnCdArea]"+
                                              " ,[Tipo]" +
                                              " ,[Sigla])" +
                                        " VALUES"+
                                              " (@CodReunion,"+
                                              " @NomReunion,"+
                                              " @EstadoReu,"+
                                              " @GNCodUsu,"+
                                              " @Periodicidad,"+
                                              " @GnCdArea,"+
                                              " @Tipo," +
                                              " @Sigla) select scope_identity()", conexion.OpenConnection());


                consult.Parameters.AddWithValue("@CodReunion", data.StrCodReunion);
                consult.Parameters.AddWithValue("@NomReunion", data.StrNomReunion);
                consult.Parameters.AddWithValue("@EstadoReu", data.IntEstadoReu);
                consult.Parameters.AddWithValue("@GNCodUsu", data.IntGNCodUsu);
                consult.Parameters.AddWithValue("@Periodicidad", data.IntPeriodicidad);
                consult.Parameters.AddWithValue("@GnCdArea", data.IntGnCdArea);
                consult.Parameters.AddWithValue("@Tipo", data.StrTipo);
                consult.Parameters.AddWithValue("@Sigla", data.StrSigla);
                int OidInstancia = Convert.ToInt32(consult.ExecuteScalar());

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $"Se crea el comité {data.StrNomReunion}",
                    strEntidad = "AReunionC"
                });

                conexion.CloseConnection();
                return true;
                
            }
            catch (Exception ex)
            {
                conexion.CloseConnection();
                return false;
            }
            
        }

        public bool set(List<AReunionC> datas)
        {
            throw new NotImplementedException();
        }

        public AReunionC setUltiomo()
        {
            throw new NotImplementedException();
        }

        public bool update(AReunionC data)
        {
            SqlCommand consult;

            try
            {
                consult = new SqlCommand("UPDATE [dbo].[AReunionC]"+
                                           "SET[CodReunion] = @CodReunion"+
                                              ",[NomReunion] = @NomReunion"+
                                              ",[EstadoReu] = @EstadoReu"+
                                              ",[GNCodUsu] =  @GNCodUsu"+
                                              ",[Periodicidad] = @Periodicidad"+
                                              ",[GnCdArea] = @GnCdArea"+
                                              ",[Tipo] =  @Tipo"+
                                              ",[Sigla] = @Sigla"+
                                        " WHERE  OidAReunionC = @OidAReunionC", OpenConnection());

                consult.Parameters.AddWithValue("@CodReunion", data.StrCodReunion);
                consult.Parameters.AddWithValue("@NomReunion", data.StrNomReunion);
                consult.Parameters.AddWithValue("@EstadoReu", data.IntEstadoReu);
                consult.Parameters.AddWithValue("@GNCodUsu", data.IntGNCodUsu);
                consult.Parameters.AddWithValue("@Periodicidad", data.IntPeriodicidad);
                consult.Parameters.AddWithValue("@GnCdArea", data.IntGnCdArea);
                consult.Parameters.AddWithValue("@Tipo", data.StrTipo);
                consult.Parameters.AddWithValue("@Sigla", data.StrSigla);
                consult.Parameters.AddWithValue("@OidAReunionC", data.IntOidAReunionC);


                consult.ExecuteNonQuery();

                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = data.IntOidAReunionC,
                    strAccion = "Modificar",
                    strDetalle = $"Se actualiza la información general del cometé {data.StrNomReunion}",
                    strEntidad = "AReunionC"
                });

                CloseConnection();
                return true;

            }
            catch (Exception ex)
            {
                CloseConnection();
                return false;
            }

        }

        

        public bool update(List<AReunionC> data)
        {
            throw new NotImplementedException();
        }

        bool DAOInterfaz<AReunionC>.update(int id)
        {
            throw new NotImplementedException();
        }

        
    }
}