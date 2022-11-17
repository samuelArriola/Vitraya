using Entidades.ControlEntSal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.ControlEntSal
{
    public class CSPacienteBBController
    {
        public static List<ControlEntSalModel> AcudienteBBGet(string CSingresoBB)
        {

            List<ControlEntSalModel> controlEntSalModels = new List<ControlEntSalModel>();
            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();


            try
            {
                command.Connection = conexion2;
                command.CommandText = "SELECT OID, PACNUMDOC, PACPRINOM,PACSEGNOM, PACPRIAPE, PACSEGAPE FROM GENPACIEN " +
                                       "WHERE PACNUMDOC = @PACNUMDOC";
                command.Parameters.AddWithValue("@PACNUMDOC", CSingresoBB);
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

        public static List<ControlEntSalModel> GetPacienteIngreso(long Codigo)
        {

            List<ControlEntSalModel> controlEntSalModels = new List<ControlEntSalModel>();
            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();


            try
            {
                command.Connection = conexion2;
                command.CommandText = @"SELECT GENPACIEN.PACNUMDOC,(GENPACIEN.PACPRINOM + ' ' + GENPACIEN.PACSEGNOM + ' ' + GENPACIEN.PACPRIAPE +' ' + GENPACIEN.PACSEGAPE) AS NOMBRE,ADNINGRESO.OID AS OIDINGRESO, ADNINGRESO.AINCONSEC AS ADMISION, GENPACIEN.OID AS OIDPACIENTE, DATEDIFF( YEAR, GPAFECNAC ,SYSDATETIME() ) AS EDAD, SRACONSEC 
                                        FROM ADNINGRESO 
                                            INNER JOIN GENPACIEN ON GENPACIEN.OID = ADNINGRESO.GENPACIEN 
                                            LEFT JOIN SLNORDSAL ON ADNINGRESO.OID = SLNORDSAL.ADNINGRES1
                                            WHERE AINCONSEC = @AINCONSEC";
                command.Parameters.AddWithValue("@AINCONSEC", Codigo);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ControlEntSalModel controlEntSalModel = new ControlEntSalModel()
                    {
                        PACNUMDOC = reader["PACNUMDOC"].ToString(),
                        PACPRINOM = reader["NOMBRE"].ToString(),
                        ORDENSALIDA = reader["SRACONSEC"].ToString(),
                        EDAD = reader["EDAD"].ToString()
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

        public static List<SPacienteBBModel> SalidaBBget(string ingreso)
        {
            List<SPacienteBBModel> spacienteBBModels = new List<SPacienteBBModel>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                //VALIDACION INGRESO EXISTENTE
                command = new SqlCommand("SELECT * FROM SPacienteBB WHERE ADNINGRES1 = @ADNINGRES1 AND Estado2SC IS NULL AND Eliminado = 'NO'", conexion.OpenConnection());
                command.Parameters.AddWithValue("@ADNINGRES1", ingreso);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SPacienteBBModel sPacienteBBModel = new SPacienteBBModel()
                    {
                        OID = Convert.ToInt32( reader["OID"].ToString() ),
                        DOCPACIENTE = reader["DocPaiente"].ToString(),
                        NOMPACIENTE = reader["NomPaciente"].ToString(),
                        DOCRESPONSABLE = reader["DocResponsable"].ToString(),
                        NOMRESPONSABLE = reader["NomResponsable"].ToString(),
                        TPRESPONSABLE = reader["TpResponsable"].ToString(),
                        NOMBB = reader["NomBB"].ToString(),
                        FECHASS = Convert.ToDateTime(reader["FECHASS"].ToString()),
                       
                    };
                    spacienteBBModels.Add(sPacienteBBModel);

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

            return spacienteBBModels; 

        }

        public static List<SPacienteBBModel> GetBuscarEgreso(string buscar)
        {

            List<SPacienteBBModel> sPacientelist = new List<SPacienteBBModel>();
            Conexion conexion = new Conexion();
            SqlCommand command;
            SqlDataReader reader;

            try
            {

                command = new SqlCommand(@"SELECT * FROM SPacienteBB 
                                            WHERE  Estado2SC IS not NULL AND Eliminado = 'NO' AND
	                                            FECHASC > DateAdd(DAY, -2,  SYSDATETIME()) AND
                                                (DocPaiente LIKE '%' + @buscar + '%' 
                                                OR NomPaciente LIKE '%' + @buscar + '%' 
	                                            OR NomResponsable LIKE '%' + @buscar + '%'
                                                OR NomBB LIKE '%' + @buscar + '%'    ) 
                                            ORDER BY FECHASC desc", conexion.OpenConnection());
              
                command.Parameters.AddWithValue("@buscar", buscar);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SPacienteBBModel spacienteBB = new SPacienteBBModel()
                    {
                        OID = Convert.ToInt32(reader["OID"].ToString()),
                        ORDENSALIDA = reader["ORDENSALIDA"].ToString(),
                        DOCPACIENTE = reader["DocPaiente"].ToString(),
                        NOMPACIENTE = reader["NomBB"].ToString(),
                        DOCRESPONSABLE = reader["DocResponsable"].ToString(),
                        NOMRESPONSABLE = reader["NomResponsable"].ToString(),
                        TPRESPONSABLE = reader["TpResponsable"].ToString(),
                        NOMBB = reader["NomBB"].ToString(),
                        FECHASC = Convert.ToDateTime(reader["FECHASC"].ToString()),
                    };
                    sPacientelist.Add(spacienteBB);
                }

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return sPacientelist;

        }

        public static List<SPacienteBBModel> AcuBBGetUpdate(string oid)
        {
            List<SPacienteBBModel> spacienteBBModels = new List<SPacienteBBModel>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                //VALIDACION INGRESO EXIXTENTE
                command = new SqlCommand("SELECT * FROM SPacienteBB WHERE OID = @OID AND Estado2SC IS NULL AND Eliminado = 'NO'", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OID", oid);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SPacienteBBModel sPacienteBBModel = new SPacienteBBModel()
                    {
                        OID = Convert.ToInt32( reader["OID"].ToString() ),
                        DOCRESPONSABLE = reader["DocResponsable"].ToString(),
                        NOMRESPONSABLE = reader["NomResponsable"].ToString(),
                        TPRESPONSABLE = reader["TpResponsable"].ToString(),
                       
                    };
                    spacienteBBModels.Add(sPacienteBBModel);

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

            return spacienteBBModels; 

        }
        public static List<SPacienteBBModel> SalidaPorDocBBGet(string doc)
        {
            List<SPacienteBBModel> spacienteBBModels = new List<SPacienteBBModel>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                //VALIDACION INGRESO EXISTENTE
                command = new SqlCommand("SELECT * FROM SPacienteBB WHERE DocResponsable = @DocResponsable AND Estado2SC IS NULL  AND Eliminado = 'NO' ORDER BY FECHASS DESC ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@DocResponsable", doc);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SPacienteBBModel sPacienteBBModel = new SPacienteBBModel()
                    {
                        OID = Convert.ToInt32( reader["OID"].ToString() ),
                        DOCPACIENTE = reader["DocPaiente"].ToString(),
                        NOMPACIENTE = reader["NomPaciente"].ToString(),
                        DOCRESPONSABLE = reader["DocResponsable"].ToString(),
                        NOMRESPONSABLE = reader["NomResponsable"].ToString(),
                        TPRESPONSABLE = reader["TpResponsable"].ToString(),
                        NOMBB = reader["NomBB"].ToString(),
                        FECHASS = Convert.ToDateTime(reader["FECHASS"].ToString()),
                       
                    };
                    spacienteBBModels.Add(sPacienteBBModel);

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

            return spacienteBBModels; 

        }

        public static int NumBebe(int ingreso) {
            int num = 0;
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT count(*) AS NumBB FROM SPacienteBB where ADNINGRES1 = @ADNINGRES1 And Estado2SC is null and Eliminado = 'NO'", conexion.OpenConnection());
                command.Parameters.AddWithValue("@ADNINGRES1", ingreso);
                num = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return num;
        }

        public static int NumDeAcuBB(int ingreso)
        {
            int num = 0;
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT COUNT(*) FROM SPacienteBB WHERE ADNINGRES1 = @ADNINGRES1 AND Estado2SC IS NULL AND Edad < 19 AND Eliminado = 'NO'", conexion.OpenConnection());
                command.Parameters.AddWithValue("@ADNINGRES1", ingreso);
                num = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return num;
        }
        public static int AcudienteBBMenorSet(int CSingresoBB, string CSInomsBB, string CSIidenBB, string CSAidenBB, string CSAtipoBB, string CSAnomsBB, int CSedadBB, string ORDENSALIDA)
        {
            int NumDeAcu =  NumDeAcuBB(CSingresoBB);
            string Estado1SS = "SalServicio";
            string Eliminado = "NO";
            SqlCommand command;
            Conexion conexion = new Conexion(); 
            var DateAndTime = DateTime.Now;
            var Responsable = Convert.ToInt32(HttpContext.Current.Session["Admin"]);
            string NomBB = CSInomsBB;
            CSInomsBB = "";

            if (NumDeAcu == 0) { 
        
                try
                {
                    command = new SqlCommand("INSERT INTO SPacienteBB (ADNINGRES1 ,DocPaiente ,NomPaciente ,DocResponsable ,NomResponsable ,TpResponsable ,NomBB  ,GnIdUsuSS ,Estado1SS ,FECHASS , Edad, Eliminado, ORDENSALIDA ) " +
                                            "VALUES(@ADNINGRES1, @DocPaiente, @NomPaciente, @DocResponsable, @NomResponsable, @TpResponsable, @NomBB, @GnIdUsu, @Estado1SS, @FECHASS, @Edad, @Eliminado, @ORDENSALIDA )", conexion.OpenConnection());

                    command.Parameters.AddWithValue("ADNINGRES1", CSingresoBB);
                    command.Parameters.AddWithValue("ORDENSALIDA", CSingresoBB);
                    command.Parameters.AddWithValue("DocPaiente", CSIidenBB);
                    command.Parameters.AddWithValue("NomPaciente", CSInomsBB);
                    command.Parameters.AddWithValue("DocResponsable", CSAidenBB);
                    command.Parameters.AddWithValue("NomResponsable", CSAnomsBB);
                    command.Parameters.AddWithValue("TpResponsable", CSAtipoBB);
                    command.Parameters.AddWithValue("NomBB", NomBB);
                    command.Parameters.AddWithValue("GnIdUsu", Responsable);
                    command.Parameters.AddWithValue("Estado1SS", Estado1SS);
                    command.Parameters.AddWithValue("FECHASS", DateAndTime);
                    command.Parameters.AddWithValue("Edad", CSedadBB);
                    command.Parameters.AddWithValue("Eliminado ", Eliminado);


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

            return NumDeAcu;

        }
        public static void AcudienteBBSet(int CSingresoBB, string CSInomsBB, string CSIidenBB, string CSAidenBB, string CSAtipoBB, string CSAnomsBB, int CSedadBB, string ORDENSALIDA)
        {
            int CountNumBebe = NumBebe(CSingresoBB) + 1;
            string Estado1SS = "SalServicio";
            string Eliminado = "NO";
            SqlCommand command;
            Conexion conexion = new Conexion(); 
            var DateAndTime = DateTime.Now;
            var Responsable = Convert.ToInt32(HttpContext.Current.Session["Admin"]);
            string NomBB = "HIJO " + CountNumBebe + " DE " + CSInomsBB;
          

            try
            {
                command = new SqlCommand("INSERT INTO SPacienteBB (ADNINGRES1 ,DocPaiente ,NomPaciente ,DocResponsable ,NomResponsable ,TpResponsable ,NomBB  ,GnIdUsuSS ,Estado1SS ,FECHASS ,NumBebe, Edad, Eliminado, ORDENSALIDA ) " +
                                        "VALUES(@ADNINGRES1, @DocPaiente, @NomPaciente, @DocResponsable, @NomResponsable, @TpResponsable, @NomBB, @GnIdUsu, @Estado1SS, @FECHASS, @NumBebe, @Edad, @Eliminado, @ORDENSALIDA )", conexion.OpenConnection());

                command.Parameters.AddWithValue("ADNINGRES1", CSingresoBB);
                command.Parameters.AddWithValue("ORDENSALIDA", ORDENSALIDA);
                command.Parameters.AddWithValue("DocPaiente", CSIidenBB);
                command.Parameters.AddWithValue("NomPaciente", CSInomsBB);
                command.Parameters.AddWithValue("DocResponsable", CSAidenBB);
                command.Parameters.AddWithValue("NomResponsable", CSAnomsBB);
                command.Parameters.AddWithValue("TpResponsable", CSAtipoBB);
                command.Parameters.AddWithValue("NomBB", NomBB);
                command.Parameters.AddWithValue("GnIdUsu", Responsable);
                command.Parameters.AddWithValue("Estado1SS", Estado1SS);
                command.Parameters.AddWithValue("FECHASS", DateAndTime);
                command.Parameters.AddWithValue("NumBebe", CountNumBebe);
                command.Parameters.AddWithValue("Edad", CSedadBB);
                command.Parameters.AddWithValue("Eliminado ", Eliminado);


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

        public static void AcuBBDelete(string oid)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE SPacienteBB SET Eliminado = 'SI' WHERE OID = @OID", conexion.OpenConnection());
                command.Parameters.AddWithValue("OID", oid);
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

         public static void AcuBBUpdate(int CSAoidEdiBB,  string CSAidenIdiBB, string CSATpResEdiCCBB, string CSACCNombreEdiBB)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE SPacienteBB SET DocResponsable  = @DocResponsable, NomResponsable = @NomResponsable, TpResponsable = @TpResponsable  WHERE OID = @OID", conexion.OpenConnection());
                command.Parameters.AddWithValue("OID ", CSAoidEdiBB);
                command.Parameters.AddWithValue("DocResponsable ", CSAidenIdiBB);
                command.Parameters.AddWithValue("TpResponsable ", CSATpResEdiCCBB);
                command.Parameters.AddWithValue("NomResponsable ", CSACCNombreEdiBB);
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

        public static int CountPacienteSalida(int ingreso)
        {
            int num = 0;
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT count(*) AS NumBB FROM SPacienteBB where ADNINGRES1 = @ADNINGRES1 And Edad < 18 And Estado2SC is not null and Eliminado = 'NO'", conexion.OpenConnection());
                command.Parameters.AddWithValue("@ADNINGRES1", ingreso);
                num = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }

            return num;
        }

    }
}