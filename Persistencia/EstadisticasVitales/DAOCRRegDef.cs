using Entidades.EstadisticasVitales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Persistencia.EstadisticasVitales
{
    public class DAOCRRegDef
    {
        // guardar un registro 
        public static void setRegDef(CRRegDef regDef)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("iNSERT INTO [dbo].[CRDefuncion] ([TipDef],[FecDef],[NomPac],[IdPaciente],[OIdCRCodRuaf], [GNCodUsu], [NomDoc], [Servicio], EstdoPaciente) " +
                                         "VALUES (@TipDef ,@FecDef ,@NomPac ,@IdPaciente ,@OIdCRCodRuaf ,@GNCodUsu ,@NomDoc ,@Servicio, @EstdoPaciente ) select scope_identity()", conexion.OpenConnection());
                command.Parameters.AddWithValue("@TipDef", regDef.StrTipDef);
                command.Parameters.AddWithValue("@FecDef", regDef.DateFecDef);
                command.Parameters.AddWithValue("@NomPac", regDef.StrNomPac);
                command.Parameters.AddWithValue("@IdPaciente", regDef.DoubleIdPaciente);
                command.Parameters.AddWithValue("@OIdCRCodRuaf", regDef.IntOIdCRCodRuaf);
                command.Parameters.AddWithValue("@GNCodUsu", regDef.DoubleGNCodUsu);
                command.Parameters.AddWithValue("@NomDoc", regDef.StrNomDoc);
                command.Parameters.AddWithValue("@Servicio", regDef.StrServicio);
                command.Parameters.AddWithValue("@EstdoPaciente", regDef.BlnEstdoPaciente);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());

                Persistencia.Generales.DAOGNHistorico.SetHistorico(new Entidades.Generales.GNHistorico { 
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(System.Web.HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $@"Se raliza registro de defunción para el paciente {regDef.StrNomPac}",
                    strEntidad = "CRDefuncion"
                });

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        //obtener un registro
        public static List<CRRegDef> getRegDef(string dato, string opc)
        {
            List<CRRegDef> RegDefs = new List<CRRegDef>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            string tabla = "";

            switch (opc)
            {
                case "Documento Paciente": tabla = "IdPaciente"; break;
                case "Nombre Paciente": tabla = "NomPac"; break;
                case "Código RUAF": tabla = "OIdCRCodRuaf"; break;
                case "Documento Médico": tabla = "GNCodUsu"; break;
            }

            try
            {
                command = new SqlCommand("select  * from CRDefuncion WHERE " + tabla + "='" + dato + "'  ", conexion.OpenConnection());
                //command.Parameters.AddWithValue("@tabla", tabla);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CRRegDef regDef = new CRRegDef();
                    regDef.StrTipDef = reader["TipDef"].ToString();
                    regDef.DateFecDef = Convert.ToDateTime(reader["FecDef"].ToString());
                    regDef.StrNomPac = reader["NomPac"].ToString();
                    regDef.DoubleIdPaciente = Convert.ToDouble(reader["IdPaciente"].ToString());
                    regDef.IntOIdCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"].ToString());
                    regDef.DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"].ToString());
                    regDef.StrNomDoc = reader["NomDoc"].ToString();
                    regDef.StrServicio = reader["Servicio"].ToString();
                    regDef.BlnEstdoPaciente = reader["EstdoPaciente"].ToString() == "" ? false : Convert.ToBoolean(reader["EstdoPaciente"].ToString());   
                    RegDefs.Add(regDef);
                }
                return RegDefs;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        //obtener una lista de registros usando el comando *like*
        public static List<CRRegDef> getRegDefs(string dato, string opc)
        {
            List<CRRegDef> RegDefs = new List<CRRegDef>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            string tabla = "";

            switch (opc)
            {
                case "Documento Paciente": tabla = "IdPaciente"; break;
                case "Nombre Paciente": tabla = "NomPac"; break;
                case "Código RUAF": tabla = "OIdCRCodRuaf"; break;
                case "Documento Médico": tabla = "GNCodUsu"; break;
                case "Tipo defunción": tabla = "TipDef"; break;
                case "Todos": tabla = "NomPac"; break;
            }

            try
            {
                command = new SqlCommand("select * from CRDefuncion WHERE " + tabla + " like '%" + dato + "%'  ", conexion.OpenConnection());
                //command.Parameters.AddWithValue("@tabla", tabla);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CRRegDef regDef = new CRRegDef();
                    regDef.StrTipDef = reader["TipDef"].ToString();
                    regDef.DateFecDef = Convert.ToDateTime(reader["FecDef"].ToString());
                    regDef.StrNomPac = reader["NomPac"].ToString();
                    regDef.DoubleIdPaciente = Convert.ToDouble(reader["IdPaciente"].ToString());
                    regDef.IntOIdCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"].ToString());
                    regDef.DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"].ToString());
                    regDef.StrNomDoc = reader["NomDoc"].ToString();
                    regDef.StrServicio = reader["Servicio"].ToString();
                    regDef.BlnEstdoPaciente = reader["EstdoPaciente"].ToString() == "" ? false : Convert.ToBoolean(reader["EstdoPaciente"].ToString());
                    RegDefs.Add(regDef);
                }
                return RegDefs;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        //obtener una lista de registros a partir de una intervalo de fecha
        public static List<CRRegDef> getRegDefsFec(String dato, string opc, string fechaMin, string fechaMax)
        {
            List<CRRegDef> RegDefs = new List<CRRegDef>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            DateTime FechaMin = Convert.ToDateTime(fechaMin);
            DateTime FechaMax = Convert.ToDateTime(fechaMax);

            try
            {
                command = new SqlCommand("select  * from CRDefuncion WHERE FecDef between @fechaMin  and @fechaMax"
                                          , conexion.OpenConnection());
                command.Parameters.AddWithValue("@fechaMin", FechaMin);
                command.Parameters.AddWithValue("@fechaMax", FechaMax);

                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CRRegDef regDef = new CRRegDef();
                    regDef.StrTipDef = reader["TipDef"].ToString();
                    regDef.DateFecDef = Convert.ToDateTime(reader["FecDef"].ToString());
                    regDef.StrNomPac = reader["NomPac"].ToString();
                    regDef.DoubleIdPaciente = Convert.ToDouble(reader["IdPaciente"].ToString());
                    regDef.IntOIdCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"].ToString());
                    regDef.DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"].ToString());
                    regDef.StrNomDoc = reader["NomDoc"].ToString();
                    regDef.StrServicio = reader["Servicio"].ToString();
                    regDef.BlnEstdoPaciente = reader["EstdoPaciente"].ToString() == "" ? false : Convert.ToBoolean(reader["EstdoPaciente"].ToString());
                    RegDefs.Add(regDef);
                }
                return RegDefs;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        //actualizar un registro.
        public static void setUpdate(string dato, string opc, string condicion)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            string tabla = "";

            switch (opc)
            {
                case "Id Paciente": tabla = "IdPaciente"; break;
                case "Nombre Paciente": tabla = "NomPac"; break;
                case "Código ruaf": tabla = "OIdCRCodRuaf"; break;
                case "Id Doctor": tabla = "GNCodUsu"; break;
                case "Tipo Defunción": tabla = "TipDef"; break;
                case "Fecha": tabla = "FecDef"; break;
                case "Servicio": tabla = "Servicio"; break;
            }

            if (opc == "Fecha")
            {
                DateTime Fecha = Convert.ToDateTime(dato);
                dato = Fecha.ToString("dd/MM/yyyy HH:mm");
            }

            try
            {
                command = new SqlCommand($"update CRDefuncion set {tabla} = '{dato}' where OIdCRCodRuaf = '{condicion}'", conexion.OpenConnection());
                command.ExecuteNonQuery();
                Persistencia.Generales.DAOGNHistorico.SetHistorico(new Entidades.Generales.GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(System.Web.HttpContext.Current.Session["Admin"]),
                    intInstancia = Convert.ToInt32(condicion),
                    strAccion = "Modificar",
                    strDetalle = $@"Se actualiza la información de la defunción con código {condicion} ",
                    strEntidad = "CRDefuncion"
                });

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

        public static void updateRegDef(CRRegDef regDef, string IntOIdCRCodRuaf)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("UPDATE [dbo].[CRDefuncion]" +
                                           "SET [TipDef] = @TipDef " +
                                              ",[FecDef] = @FecDef" +
                                              ",[NomPac] = @NomPac " +
                                              ",[IdPaciente] = @IdPaciente" +
                                              ",[GNCodUsu] = @GNCodUsu" +
                                              ",[NomDoc] =  @NomDoc" +
                                              ",[Servicio] = @Servicio" +
                                              ",[EstdoPaciente] = @EstdoPaciente" +
                                        " WHERE OIdCRCodRuaf = @OIdCRCodRuaf ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@TipDef", regDef.StrTipDef);
                command.Parameters.AddWithValue("@FecDef", regDef.DateFecDef);
                command.Parameters.AddWithValue("@NomPac", regDef.StrNomPac);
                command.Parameters.AddWithValue("@IdPaciente", regDef.DoubleIdPaciente);
                command.Parameters.AddWithValue("@OIdCRCodRuaf", IntOIdCRCodRuaf);
                command.Parameters.AddWithValue("@GNCodUsu", regDef.DoubleGNCodUsu);
                command.Parameters.AddWithValue("@NomDoc", regDef.StrNomDoc);
                command.Parameters.AddWithValue("@Servicio", regDef.StrServicio);
                command.Parameters.AddWithValue("@EstdoPaciente", regDef.BlnEstdoPaciente);
                command.ExecuteNonQuery();
                Persistencia.Generales.DAOGNHistorico.SetHistorico(new Entidades.Generales.GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(System.Web.HttpContext.Current.Session["Admin"]),
                    intInstancia = Convert.ToInt32(IntOIdCRCodRuaf),
                    strAccion = "Modificar",
                    strDetalle = $@"Se actualiza la infromación para el registro de defunción con código {regDef.IntOIdCRCodRuaf}",
                    strEntidad = "CRDefuncion"

                });
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

       
        public static List<CRRegDef> getHistoryDef(string OIPaciente)
        {
            List<CRRegDef> RegDefs = new List<CRRegDef>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            
            try
            {
                command = new SqlCommand("select * from CRDefuncion_History as H " +
                                         " left  join CRDefuncion as D on D.OIdCRDef = H.OIdCRDef " +
                                         " where D.IdPaciente = @OIPaciente  ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OIPaciente", OIPaciente);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CRRegDef regDef = new CRRegDef();
                    regDef.StrTipDef = reader["TipDef"].ToString();
                    regDef.DateFecDef = Convert.ToDateTime(reader["FecDef"].ToString());
                    regDef.StrNomPac = reader["NomPac"].ToString();
                    regDef.DoubleIdPaciente = Convert.ToDouble(reader["IdPaciente"].ToString());
                    regDef.IntOIdCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"].ToString());
                    regDef.DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"].ToString());
                    regDef.StrNomDoc = reader["NomDoc"].ToString();
                    regDef.StrServicio = reader["Servicio"].ToString();
                    regDef.DateFecTimeStart = Convert.ToDateTime(reader["TimeStart"].ToString());
                    regDef.BlnEstdoPaciente = reader["EstdoPaciente"].ToString() == "" ? false : Convert.ToBoolean(reader["EstdoPaciente"].ToString());

                    RegDefs.Add(regDef);
                }

                return RegDefs;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexion.CloseConnection();
            }
        }

    }
}