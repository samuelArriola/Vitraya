using Entidades.ControlEntSal;
using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;

namespace Persistencia.ControlEntSal
{
    public class ControlEntSalController
    {
        public static List<ControlEntSalModel> GetPacientes(long Codigo)
        {

            List<ControlEntSalModel> controlEntSalModels = new List<ControlEntSalModel>();
            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();


            try
            {
                command.Connection = conexion2;
                command.CommandText = "SELECT OID, PACNUMDOC, PACPRINOM,PACSEGNOM, PACPRIAPE, PACSEGAPE FROM GENPACIEN " +
                                       "WHERE OID = (SELECT GENPACIEN FROM ADNINGRESO WHERE OID = (SELECT ADNINGRES1 FROM SLNORDSAL WHERE SRACONSEC = @SRACONSEC))";
                command.Parameters.AddWithValue("@SRACONSEC", Codigo);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ControlEntSalModel controlEntSalModel = new ControlEntSalModel()
                    {
                        PACNUMDOC = reader["PACNUMDOC"].ToString(),
                        PACPRINOM = reader["PACPRINOM"].ToString(),
                        PACSEGNOM = reader["PACSEGNOM"].ToString(),
                        PACPRIAPE = reader["PACPRIAPE"].ToString(),
                        PACSEGAPE = reader["PACSEGAPE"].ToString(),
                    };
                    controlEntSalModels.Add(controlEntSalModel);
                }

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion2.Close();
            }
            return controlEntSalModels;

        }

        public static int PacienteSalida(string CScodigoR)
        {

            int respos = 0;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                //VALIDACION INGRESO EXIXTENTE
                command = new SqlCommand("SELECT * FROM SPacienteReal WHERE ORDENSALIDA = @CScodigoR ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@CScodigoR", CScodigoR);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    respos++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return respos;
        }

        public static int InserPciente(string CScodigoR, string CSiden)
        {

            var count = PacienteSalida(CScodigoR);
            SqlCommand command;
            Conexion conexion = new Conexion();
            var DateAndTime = DateTime.Now;
            if (count < 1 ) { 
            
                try
                {
                    command = new SqlCommand("INSERT INTO SPacienteReal  (DOCUMENTO ,ORDENSALIDA ,FECSALIDA)" +
                                             "VALUES(@DOCUMENTO, @ORDENSALIDA, @FECSALIDA) select scope_identity()", conexion.OpenConnection());
                    command.Parameters.AddWithValue("@DOCUMENTO", CSiden);
                    command.Parameters.AddWithValue("@ORDENSALIDA", CScodigoR);
                    command.Parameters.AddWithValue("@FECSALIDA", DateAndTime);
                    int OidInstancia = Convert.ToInt32(command.ExecuteScalar());

                    DAOGNHistorico.SetHistorico(new GNHistorico
                    {
                        dtmFecha = DateTime.Now,
                        intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                        intInstancia = OidInstancia,
                        strAccion = "Crear",
                        strDetalle = $"se ha insertado la salida del paciente {CSiden} ",
                        strEntidad = "SPacienteReal"
                    });
             

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    conexion.CloseConnection();
                }
            }

            return count;

        }  
        
        public static void NoCoincideSP(string CScodigoR, string CSmanilla)
        {
   
            try
            {
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = 0,
                    strAccion = "Crear fallido",
                    strDetalle = $"Documento de orden {CScodigoR} no coincide con el No de manilla {CSmanilla} ",
                    strEntidad = "SPacienteReal"
                });
             
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public static void DarSalidaAcuBBSet(string oid)
        {
            string Estado2SC = "SalClinica";
            SqlCommand command;
            Conexion conexion = new Conexion();
            var DateAndTime = DateTime.Now;
            var Responsable = Convert.ToInt32(HttpContext.Current.Session["Admin"]);

            try
            {
                command = new SqlCommand("UPDATE SPacienteBB SET Estado2SC = @Estado2SC, FECHASC = @FECHASC, GnIdUsuSC = @GnIdUsuSC WHERE OID = @OID" , conexion.OpenConnection());
                command.Parameters.AddWithValue("FECHASC", DateAndTime);
                command.Parameters.AddWithValue("Estado2SC", Estado2SC);
                command.Parameters.AddWithValue("OID", oid);
                command.Parameters.AddWithValue("GnIdUsuSC", Responsable);
                command.ExecuteNonQuery();

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
   
  
    }
}
