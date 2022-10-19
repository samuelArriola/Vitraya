using Entidades.ControlEntSal;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Persistencia.ControlEntSal
{
    public class InformesController
    {


        public static object GetSPacienteReal(DateTime fechaI, DateTime fechaF)
        {

            List<SPacienteReal> sPacienteReallist = new List<SPacienteReal>();
            Conexion conexion = new Conexion();
            SqlCommand command;
            SqlDataReader reader;
            var FECSALIDA = DateTime.Now;
            try
            {

                command = new SqlCommand(@"SELECT * FROM SPacienteReal  
                                            WHERE  FECSALIDA >= @fechaI AND  FECSALIDA <= @fechaF 
                                            ORDER BY FECSALIDA desc ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@FECSALIDA", FECSALIDA);
                command.Parameters.AddWithValue("@fechaI", fechaI);
                command.Parameters.AddWithValue("@fechaF", fechaF);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SPacienteReal spacienteReal = new SPacienteReal()
                    {
                        OID = Convert.ToInt32(reader["OID"].ToString()),
                        DOCUMENTO = reader["DOCUMENTO"].ToString(),
                        ORDENSALIDA = reader["ORDENSALIDA"].ToString(),
                        ADNINGRES1 = reader["ADNINGRES1"].ToString(),
                        //GnIdUsu = reader["GnIdUsu"].ToString(),
                        NOMBRE_COMPLETO = reader["NOMBRE_COMPLETO"].ToString(),
                        FECSALIDA = Convert.ToDateTime(reader["FECSALIDA"].ToString())
                    };
                    sPacienteReallist.Add(spacienteReal);
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


            //var json = JsonConvert.SerializeObject(sPacienteReallist, Formatting.None);
            //return json;
            //return json;
            //JavaScriptSerializer ser = new JavaScriptSerializer();
            //ser.Serialize(new sPacienteReallist );
            //return Json(new { data = sPacienteReallist }, JsonRequestBehavior.AllowGet);
            object json = new { data = sPacienteReallist };
            return json;
        }

        public static object SalidaBBget(DateTime fechaI, DateTime fechaF)
        {

            List<SPacienteBBModel> sPacienteReallist = new List<SPacienteBBModel>();
            Conexion conexion = new Conexion();
            SqlCommand command;
            SqlDataReader reader;
           
            try
            {
                command = new SqlCommand(@" SELECT OID, ADNINGRES1, DocPaiente, ORDENSALIDA, NOMPACIENTE, DocResponsable, NomResponsable, FECHASC
                                            FROM SPacienteBB 
                                            WHERE FECHASC >= @fechaI AND FECHASC <= @fechaF AND  Estado2SC IS NOT NULL AND Eliminado = 'NO'
                                            ORDER BY FECHASC desc ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@fechaI", fechaI);
                command.Parameters.AddWithValue("@fechaF", fechaF);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SPacienteBBModel spacienteReal = new SPacienteBBModel()
                    {
                        OID = Convert.ToInt32(reader["OID"].ToString()),
                        ADNINGRES1 = Convert.ToInt32(reader["ADNINGRES1"].ToString()),
                        ORDENSALIDA = reader["ORDENSALIDA"].ToString(),
                        DOCPACIENTE = reader["DocPaiente"].ToString(),
                        NOMPACIENTE = reader["NOMPACIENTE"].ToString(),
                        //NOMBRE_COMPLETO = reader["NOMBRE_COMPLETO"].ToString(),
                        FECHASC = Convert.ToDateTime(reader["FECHASC"].ToString())
                    };
                    sPacienteReallist.Add(spacienteReal);
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

            object json = new { data = sPacienteReallist };
            return json;
        }

        public static string OrdenSalidaGet(DateTime fechaI, DateTime fechaF) 
        {
            var data = $@"''";
            Conexion conexion = new Conexion();
            SqlCommand command;
            SqlDataReader reader;

            try
            {
                command = new SqlCommand(@"SELECT SP.ORDENSALIDA AS SPORDENSALIDA , SPB.ORDENSALIDA AS SPORDENSALIDABB, CONCAT(SP.ORDENSALIDA,  SPB.ORDENSALIDA) as ORDENSALIDACONC
	                                          FROM SPacienteReal SP
	                                          FULL OUTER JOIN SPacienteBB SPB ON SP.ORDENSALIDA = SPB.ORDENSALIDA 
                                        WHERE (SP.FECSALIDA >= @fechaI AND  SP.FECSALIDA <= @fechaF  
		                                        OR  SPB.FECHASC >= @fechaI AND SPB.FECHASC <= @fechaF AND  Estado2SC IS NOT NULL AND Eliminado = 'NO') 
		                                        AND (SP.ORDENSALIDA IS NOT NULL OR SPB.ORDENSALIDA  IS NOT NULL)
                                        ORDER BY ORDENSALIDACONC", conexion.OpenConnection());
                command.Parameters.AddWithValue("@fechaI", fechaI);
                command.Parameters.AddWithValue("@fechaF", fechaF);
                reader = command.ExecuteReader();

                while (reader.Read()) {
                    SPacienteBBModel spacienteReal = new SPacienteBBModel()
                    {
                        ORDENSALIDA = reader["ORDENSALIDACONC"].ToString(),
                    };

                    data += $@",'{spacienteReal.ORDENSALIDA}' ";
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


            return data;
        }

        public static object GetPacientesFuga(DateTime fechaI, DateTime fechaF)
        {

            string getOrden = OrdenSalidaGet(fechaI, fechaF);
            List<InfofugasModel> InfofugasModel = new List<InfofugasModel>();
            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();


            try
            {
                command.Connection = conexion2;
                command.CommandText = $@"SELECT SLNORDSAL.SRACONSEC AS 'SALIDA_ORDEN',
                                      ADNINGRESO.AINCONSEC AS INGRESO,
                                      ADNINGRESO.AINFECING AS 'FEC_INGRESO',
                                      ADNINGRESO.AINFECEGRE AS 'FEC_EGRESO',
                                      SLNORDSAL.SRAFECSAL AS 'FEC_ORDEN',
                                      HPNDEFCAM.HCACODIGO AS CAMA,
                                      HPNDEFCAM.HCANOMBRE AS 'NOMBRE_CAMA',
                                      ADNINGRESO.GENPACIEN,
                                      GENPACIEN.PACNUMDOC as 'Documento',
                                      (GENPACIEN.PACPRINOM + ' ' + GENPACIEN.PACSEGNOM + ' ' + GENPACIEN.PACPRIAPE + ' ' + GENPACIEN.PACSEGAPE) 'NOM_PAC',
                                      (DATEDIFF(MI, ADNINGRESO.AINFECING, ADNINGRESO.AINFECEGRE)) AS 'OPORT_MIN_INGRESO_ORDEN',
                                      (DATEDIFF(MI, ADNINGRESO.AINFECEGRE, SLNORDSAL.SRAFECSAL)) AS  'OPORT_MIN_ORDEN_EGRESO'
                                    FROM SLNORDSAL
                                      INNER JOIN ADNINGRESO ON ADNINGRESO.OID = SLNORDSAL.ADNINGRES1
                                      LEFT Join GENPACIEN On GENPACIEN.OID = ADNINGRESO.GENPACIEN
                                      LEFT JOIN HPNDEFCAM ON HPNDEFCAM.OID = ADNINGRESO.HPNDEFCAM
                                    WHERE 
                                          SLNORDSAL.SRAFECSAL >=  @fechaI AND  SLNORDSAL.SRAFECSAL <= @fechaF
                                      AND SLNORDSAL.SRACONSEC NOT IN ( {getOrden}) 
                                          
                                    ORDER BY 'SALIDA_ORDEN'
                                    ";
                command.Parameters.AddWithValue("@fechaI", fechaI);
                command.Parameters.AddWithValue("@fechaF", fechaF);
              
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    InfofugasModel infofugasModel = new InfofugasModel()
                    {
                        Ingreso = Convert.ToInt64( reader["INGRESO"].ToString() ),
                        OrdenSalida = reader["SALIDA_ORDEN"].ToString(),
                        Cama = reader["CAMA"].ToString(),
                        NombreCama = reader["NOMBRE_CAMA"].ToString(),
                        Doc = reader["Documento"].ToString(),
                        NombreCompleto = reader["NOM_PAC"].ToString(),
                        //OporMiIngreOrde = (reader["OPORT_MIN_INGRESO_ORDEN"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["OPORT_MIN_INGRESO_ORDEN"].ToString()),
                        //OporMiOrdEgre = (reader["OPORT_MIN_ORDEN_EGRESO"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["OPORT_MIN_ORDEN_EGRESO"].ToString()),
                        FechaIgre = Convert.ToDateTime(reader["FEC_INGRESO"].ToString()),
                        FechaEgre = (reader["FEC_EGRESO"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FEC_EGRESO"].ToString()),
                        FechaOrden = (reader["FEC_ORDEN"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FEC_ORDEN"].ToString()),

                    };
                    InfofugasModel.Add(infofugasModel);
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

            object json = new { data = InfofugasModel };
            return json;

        }

    }
}



