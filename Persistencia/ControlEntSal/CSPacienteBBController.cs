﻿using Entidades.ControlEntSal;
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

        public static List<SPacienteBBModel> SalidaBBget(string ingreso)
        {
            List<SPacienteBBModel> spacienteBBModels = new List<SPacienteBBModel>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                //VALIDACION INGRESO EXIXTENTE
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
        public static List<SPacienteBBModel> SalidaPorDocBBGet(string doc)
        {
            List<SPacienteBBModel> spacienteBBModels = new List<SPacienteBBModel>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                //VALIDACION INGRESO EXIXTENTE
                command = new SqlCommand("SELECT * FROM SPacienteBB WHERE DocResponsable = @DocResponsable AND Estado2SC IS NULL AND Eliminado = 'NO' ", conexion.OpenConnection());
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
                command = new SqlCommand("SELECT count(*) AS NumBB FROM SPacienteBB where ADNINGRES1 = @ADNINGRES1", conexion.OpenConnection());
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

        public static void AcudienteBBSet(int CSingresoBB, string CSInomsBB, string CSIidenBB, string CSAidenBB, string CSAtipoBB, string CSAnomsBB, string CSBnumeroRBB)
        {
            int CountNumBebe = NumBebe(CSingresoBB) + 1;
            string NomBB = "HIJO " + CountNumBebe + " DE " + CSInomsBB;
            string Estado1SS = "SalServicio";
            string Eliminado = "NO";
            SqlCommand command;
            Conexion conexion = new Conexion(); 
            var DateAndTime = DateTime.Now;
            var Responsable = Convert.ToInt32(HttpContext.Current.Session["Admin"]);

            try
            {
                command = new SqlCommand("INSERT INTO SPacienteBB (ADNINGRES1 ,DocPaiente ,NomPaciente ,DocResponsable ,NomResponsable ,TpResponsable ,NomBB ,RegistroBB ,GnIdUsuSS ,Estado1SS ,FECHASS ,NumBebe, Eliminado ) " +
                                        "VALUES(@ADNINGRES1, @DocPaiente, @NomPaciente, @DocResponsable, @NomResponsable, @TpResponsable, @NomBB, @RegistroBB, @GnIdUsu, @Estado1SS, @FECHASS, @NumBebe, @Eliminado )", conexion.OpenConnection());

                command.Parameters.AddWithValue("ADNINGRES1", CSingresoBB);
                command.Parameters.AddWithValue("DocPaiente", CSIidenBB);
                command.Parameters.AddWithValue("NomPaciente", CSInomsBB);
                command.Parameters.AddWithValue("DocResponsable", CSAidenBB);
                command.Parameters.AddWithValue("NomResponsable", CSAnomsBB);
                command.Parameters.AddWithValue("TpResponsable", CSAtipoBB);
                command.Parameters.AddWithValue("NomBB", NomBB);
                command.Parameters.AddWithValue("RegistroBB", CSBnumeroRBB);
                command.Parameters.AddWithValue("GnIdUsu", Responsable);
                command.Parameters.AddWithValue("Estado1SS", Estado1SS);
                command.Parameters.AddWithValue("FECHASS", DateAndTime);
                command.Parameters.AddWithValue("NumBebe", CountNumBebe);
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
    }
}