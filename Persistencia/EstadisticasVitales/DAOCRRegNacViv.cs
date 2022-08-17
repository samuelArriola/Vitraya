using Entidades.EstadisticasVitales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Persistencia.EstadisticasVitales
{
    public class DAOCRRegNacViv
    {

        //guardar un registro nacido vivo en BD
        public static void setRegNacViv(CRRegNacViv regNacViv)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("iNSERT INTO [dbo].[CRNacidoVivo] ([IdMadre],[NomMadre],[TipNac],[FecNac],[OIdCRCodRuaf], [EdadGes], [GNCodUsu], [NomDoc], [PesoRN], [TallaRN], [Sexo]) " +
                                         "VALUES (@IdMadre ,@NomMadre ,@TipNac ,@FecNac ,@OIdCRCodRuaf,@EdadGes ,@GNCodUsu ,@NomDoc ,@PesoRN ,@TallaRN, @Sexo ) select scope_identity()", conexion.OpenConnection());
                command.Parameters.AddWithValue("@IdMadre", regNacViv.DoubleIdMadre);
                command.Parameters.AddWithValue("@NomMadre", regNacViv.StrNomMadre);
                command.Parameters.AddWithValue("@TipNac", regNacViv.StrTipNac);
                command.Parameters.AddWithValue("@FecNac", regNacViv.DateFecNac);
                command.Parameters.AddWithValue("@OIdCRCodRuaf", regNacViv.IntCRCodRuaf);
                command.Parameters.AddWithValue("@EdadGes", regNacViv.IntEdGesNac);
                command.Parameters.AddWithValue("@GNCodUsu", regNacViv.DoubleGNCodUsu);
                command.Parameters.AddWithValue("@NomDoc", regNacViv.StrNomDoc);
                command.Parameters.AddWithValue("@PesoRN", regNacViv.IntPesoRn);
                command.Parameters.AddWithValue("@TallaRN", regNacViv.FloatTallaRN);
                command.Parameters.AddWithValue("@Sexo", regNacViv.StrSexo);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());

                Persistencia.Generales.DAOGNHistorico.SetHistorico(new Entidades.Generales.GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(System.Web.HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Crear",
                    strDetalle = $@"Se raliza registro de nacido vivo para la madre {regNacViv.StrNomMadre}",
                    strEntidad = "CRNacidoVivo"
                });
            }
            catch (Exception wi)
            {
                System.Windows.Forms.MessageBox.Show(wi.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

        }

        //optener un registro de la BD
        public static List<CRRegNacViv> getRegNacViv(string dato, string opc)
        {
            List<CRRegNacViv> RegNacVivs = new List<CRRegNacViv>();
            //RegNacVivs = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            string tabla = "";

            switch (opc)
            {
                case "Documento Madre": tabla = "IdMadre"; break;
                case "Nombre Madre": tabla = "NomMadre"; break;
                case "Tipo Parto": tabla = "TipNac"; break;
                case "Código RUAF": tabla = "OidCRCodRuaf"; break;
                case "Documento Médico": tabla = "GNCodUsu"; break;
            }

            try
            {
                command = new SqlCommand("select * from CRNacidoVivo WHERE " + tabla + "= '" + dato + "'  ", conexion.OpenConnection());
                //command.Parameters.AddWithValue("@tabla", tabla);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    CRRegNacViv regNacViv = new CRRegNacViv();
                    regNacViv.DoubleIdMadre = Convert.ToDouble(reader["IdMadre"].ToString());
                    regNacViv.StrNomMadre = reader["NomMadre"].ToString();
                    regNacViv.StrTipNac = reader["TipNac"].ToString();
                    regNacViv.DateFecNac = Convert.ToDateTime(reader["FecNac"].ToString());
                    regNacViv.IntCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"].ToString());
                    regNacViv.IntEdGesNac = Convert.ToInt32(reader["EdadGes"].ToString());
                    regNacViv.DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"].ToString());
                    regNacViv.StrNomDoc = reader["NomDoc"].ToString();
                    regNacViv.IntPesoRn = Convert.ToInt32(reader["PesoRN"].ToString());
                    regNacViv.FloatTallaRN = float.Parse(reader["TallaRN"].ToString());
                    regNacViv.StrSexo = reader["Sexo"].ToString();
                    RegNacVivs.Add(regNacViv);
                }
                 
                return RegNacVivs;

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
        public static List<CRRegNacViv> getRegNacVivs(string dato, string opc)
        {
            List<CRRegNacViv> RegNacVivs = new List<CRRegNacViv>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            string tabla = "";

            switch (opc)
            {
                case "Documento Madre":  tabla = "IdMadre"; break;
                case "Nombre Madre": tabla = "NomMadre"; break;
                case "Tipo Parto": tabla = "TipNac"; break;
                case "Código RUAF": tabla = "OidCRCodRuaf"; break;
                case "Documento Médico": tabla = "GNCodUsu"; break;
                case "Todos": tabla = "NomMadre"; break;
            }

            try
            {
                command = new SqlCommand("select * from CRNacidoVivo WHERE "+tabla+" like '%" +dato+ "%'  ", conexion.OpenConnection());
                //command.Parameters.AddWithValue("@tabla", tabla);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CRRegNacViv regNacViv = new CRRegNacViv();
                    regNacViv.DoubleIdMadre = Convert.ToDouble(reader["IdMadre"].ToString());
                    regNacViv.StrNomMadre = reader["NomMadre"].ToString();
                    regNacViv.StrTipNac = reader["TipNac"].ToString();
                    regNacViv.DateFecNac = Convert.ToDateTime(reader["FecNac"].ToString());
                    regNacViv.IntCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"].ToString());
                    regNacViv.IntEdGesNac = Convert.ToInt32(reader["EdadGes"].ToString());
                    regNacViv.DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"].ToString());
                    regNacViv.StrNomDoc = reader["NomDoc"].ToString();
                    regNacViv.IntPesoRn = Convert.ToInt32(reader["PesoRN"].ToString());
                    regNacViv.FloatTallaRN = float.Parse(reader["TallaRN"].ToString());
                    regNacViv.StrSexo = reader["Sexo"].ToString();
                    RegNacVivs.Add(regNacViv);
                }
                return RegNacVivs;

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

        //obtener una lista de registros a partir de un intervalo de fechas.
        public static List<CRRegNacViv> getRegNacVivsFec(String dato, string opc, string fechaMin, string fechaMax)
        {
            List<CRRegNacViv> RegNacVivs = new List<CRRegNacViv>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            DateTime FechaMin = Convert.ToDateTime(fechaMin);
            string sqlFormattedDateMin = FechaMin.ToString("yyyy-MM-dd HH:mm:ss.fff");
            DateTime FechaMax = Convert.ToDateTime(fechaMax);
            string sqlFormattedDateMax = FechaMax.ToString("yyyy-MM-dd HH:mm:ss.fff");
            try
            {
                command = new SqlCommand("select * from CRNacidoVivo WHERE FecNac between @FechaMin  and @FechaMax ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@FechaMin", FechaMin);
                command.Parameters.AddWithValue("@FechaMax", FechaMax);
                
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CRRegNacViv regNacViv = new CRRegNacViv();
                    regNacViv.DoubleIdMadre = Convert.ToDouble(reader["IdMadre"].ToString());
                    regNacViv.StrNomMadre = reader["NomMadre"].ToString();
                    regNacViv.StrTipNac = reader["TipNac"].ToString();
                    regNacViv.DateFecNac = Convert.ToDateTime(reader["FecNac"].ToString());
                    regNacViv.IntCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"].ToString());
                    regNacViv.IntEdGesNac = Convert.ToInt32(reader["EdadGes"].ToString());
                    regNacViv.DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"].ToString());
                    regNacViv.StrNomDoc = reader["NomDoc"].ToString();
                    regNacViv.IntPesoRn = Convert.ToInt32(reader["PesoRN"].ToString());
                    regNacViv.FloatTallaRN = float.Parse(reader["TallaRN"].ToString());
                    regNacViv.StrSexo = reader["Sexo"].ToString();
                    RegNacVivs.Add(regNacViv);
                }
                return RegNacVivs;

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
                case "Id Madre": tabla = "IdMadre"; break;
                case "Nombre Madre": tabla = "NomMadre"; break;
                case "Tipo Parto": tabla = "TipNac"; break;
                case "Código ruaf": tabla = "OidCRCodRuaf"; break;
                case "Edad gestacional": tabla = "EdadGes"; break;
                case "Id Doctor": tabla = "GNCodUsu"; break;
                case "Nombre doctor": tabla = "NomDoc"; break;
                case "Peso RN": tabla = "PesoRN"; break;
                case "Talla RN": tabla = "TallaRN"; break;
                case "Fecha nacimiento": tabla = "FecNac"; break;
            }

            if(opc == "Fecha nacimiento")
            {
                DateTime Fecha = Convert.ToDateTime(dato);
                dato = Fecha.ToString("dd/MM/yyyy HH:mm");
            }

            try
            {
                command = new SqlCommand("update CRNacidoVivo set "+tabla+" = '"+dato+ "' where OidCRCodRuaf = '"+condicion+"'", conexion.OpenConnection());
                command.ExecuteNonQuery();
                Persistencia.Generales.DAOGNHistorico.SetHistorico(new Entidades.Generales.GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(System.Web.HttpContext.Current.Session["Admin"]),
                    intInstancia = Convert.ToInt32(condicion),
                    strAccion = "Modificar",
                    strDetalle = $@"Se actualiza la información para el registro de nacido vivo con el código {condicion}",
                    strEntidad = "CRNacidoVivo"
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

        public static void updateRegNacViv(CRRegNacViv regNacViv, string IntCRCodRuaf)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();


            try
            {
                command = new SqlCommand("UPDATE [dbo].[CRNacidoVivo]" +
                                           "SET [IdMadre] = @IdMadre " +
                                              ",[NomMadre] = @NomMadre" +
                                              ",[TipNac] = @TipNac " +
                                              ",[FecNac] = @FecNac" +
                                              ",[EdadGes] = @EdadGes" +
                                              ",[GNCodUsu] = @GNCodUsu " +
                                              ",[NomDoc] = @NomDoc" +
                                              ",[PesoRN] = @PesoRN" +
                                              ",[TallaRN] = @TallaRN" +
                                              ",[Sexo] = @Sexo" +
                                        " WHERE OidCRCodRuaf = @OIdCRCodRuaf ", conexion.OpenConnection());

                command.Parameters.AddWithValue("@IdMadre", regNacViv.DoubleIdMadre);
                command.Parameters.AddWithValue("@NomMadre", regNacViv.StrNomMadre);
                command.Parameters.AddWithValue("@TipNac", regNacViv.StrTipNac);
                command.Parameters.AddWithValue("@FecNac", regNacViv.DateFecNac);
                command.Parameters.AddWithValue("@OIdCRCodRuaf", IntCRCodRuaf);
                command.Parameters.AddWithValue("@EdadGes", regNacViv.IntEdGesNac);
                command.Parameters.AddWithValue("@GNCodUsu", regNacViv.DoubleGNCodUsu);
                command.Parameters.AddWithValue("@NomDoc", regNacViv.StrNomDoc);
                command.Parameters.AddWithValue("@PesoRN", regNacViv.IntPesoRn);
                command.Parameters.AddWithValue("@TallaRN", regNacViv.FloatTallaRN);
                command.Parameters.AddWithValue("@Sexo", regNacViv.StrSexo);
                command.ExecuteNonQuery();

                Persistencia.Generales.DAOGNHistorico.SetHistorico(new Entidades.Generales.GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(System.Web.HttpContext.Current.Session["Admin"]),
                    intInstancia = Convert.ToInt32(IntCRCodRuaf),
                    strAccion = "Modificar",
                    strDetalle = $@"Se actualiza la información para el registro de nacido vivo con el código {IntCRCodRuaf}",
                    strEntidad = "CRNacidoVivo"
                });
            }
            catch (Exception wi)
            {
                System.Windows.Forms.MessageBox.Show(wi.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

        }

        

        public static List<CRRegNacViv> getHistoryNV(string IdMadre)
        {
            List<CRRegNacViv> RegNacVivs = new List<CRRegNacViv>();
            //RegNacVivs = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("select * from CRNacidoVivo_History  as H " +
                                          " left  join CRNacidoVivo as NV on NV.OIdCRNacViv = H.OIdCRNacViv " +
                                          " where NV.IdMadre = @IdMadre ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@IdMadre", IdMadre);
                command.ExecuteNonQuery();
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    CRRegNacViv regNacViv = new CRRegNacViv();
                    regNacViv.DoubleIdMadre = Convert.ToDouble(reader["IdMadre"].ToString());
                    regNacViv.StrNomMadre = reader["NomMadre"].ToString();
                    regNacViv.StrTipNac = reader["TipNac"].ToString();
                    regNacViv.DateFecNac = Convert.ToDateTime(reader["FecNac"].ToString());
                    regNacViv.IntCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"].ToString());
                    regNacViv.IntEdGesNac = Convert.ToInt32(reader["EdadGes"].ToString());
                    regNacViv.DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"].ToString());
                    regNacViv.StrNomDoc = reader["NomDoc"].ToString();
                    regNacViv.IntPesoRn = Convert.ToInt32(reader["PesoRN"].ToString());
                    regNacViv.FloatTallaRN = float.Parse(reader["TallaRN"].ToString());
                    regNacViv.StrSexo = reader["Sexo"].ToString();
                    regNacViv.DateFecTimeStart = Convert.ToDateTime(reader["TimeStart"].ToString());
                    RegNacVivs.Add(regNacViv);
                }

                return RegNacVivs;

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