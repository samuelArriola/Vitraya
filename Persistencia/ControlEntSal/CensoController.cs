using Entidades.ControlEntSal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.ControlEntSal
{
    public class CensoController
    {
        public static List<Censo> CensoGet(string buscar, string grupo, string subgrupo)
        {

            List<Censo> censos = new List<Censo>();
            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();


            try
            {
                command.Connection = conexion2;
                command.CommandText = @"Select
                                      ADNINGRESO.AINCONSEC as Ingreso, HPNGRUPOS.HGRCODIGO as 'Cod_Grupo', HPNGRUPOS.HGRNOMBRE as 'Nom_Grupo',
                                      HPNSUBGRU.HSUCODIGO as 'Cod_Subgrupo', HPNSUBGRU.HSUNOMBRE as 'Nom_Subgrupo', GENPACIEN.PACNUMDOC as Documento,
                                      (GENPACIEN.PACPRINOM + ' ' + GENPACIEN.PACSEGNOM + ' ' + GENPACIEN.PACPRIAPE +
                                      ' ' + GENPACIEN.PACSEGAPE) NOM_PAC, HPNDEFCAM.HCACODIGO as 'Cod_Cama', HPNDEFCAM.HCANOMBRE as 'Nom_Cama'
                                    From SLNSERPRO Inner Join
                                      ADNINGRESO On ADNINGRESO.OID = SLNSERPRO.ADNINGRES1 Inner Join
                                      HPNESTANC On ADNINGRESO.OID = HPNESTANC.ADNINGRES Inner Join
                                      HPNDEFCAM On HPNDEFCAM.OID = HPNESTANC.HPNDEFCAM Inner Join
                                      GENPACIEN On GENPACIEN.OID = ADNINGRESO.GENPACIEN Inner Join
                                      HPNGRUPOS On HPNGRUPOS.OID = HPNDEFCAM.HPNGRUPOS Inner Join
                                      HPNSUBGRU On HPNSUBGRU.OID = HPNDEFCAM.HPNSUBGRU Full Join
                                      SLNORDSER On SLNORDSER.OID = SLNSERPRO.SLNORDSER1
                                    Where HPNESTANC.HESFECSAL Is Null And HPNDEFCAM.HCAESTADO = 2 And
                                      ADNINGRESO.AINESTADO = 0 
                                       AND  (  ( GENPACIEN.PACPRINOM LIKE '%' + @buscar + '%' OR  GENPACIEN.PACPRIAPE LIKE '%' + @buscar + '%' OR  GENPACIEN.PACSEGAPE LIKE '%' + @buscar + '%'  OR GENPACIEN.PACNUMDOC LIKE '%' + @buscar + '%' ) 
                                       AND (HPNGRUPOS.HGRCODIGO LIKE '%' + @grupo + '%' AND HPNSUBGRU.HSUCODIGO LIKE '%'+ @subgrupo + '%') )
                                    Group By ADNINGRESO.AINCONSEC, HPNGRUPOS.HGRCODIGO, HPNGRUPOS.HGRNOMBRE,
                                      HPNSUBGRU.HSUCODIGO, HPNSUBGRU.HSUNOMBRE, GENPACIEN.PACNUMDOC,
                                      HPNDEFCAM.HCACODIGO, HPNDEFCAM.HCANOMBRE, GENPACIEN.PACPRINOM,
                                      GENPACIEN.PACSEGNOM, GENPACIEN.PACPRIAPE, GENPACIEN.PACSEGAPE
                                    Order By HPNGRUPOS.HGRCODIGO, HPNSUBGRU.HSUCODIGO, HPNDEFCAM.HCACODIGO";
                command.Parameters.AddWithValue("@buscar", buscar);
                command.Parameters.AddWithValue("@grupo", grupo);
                command.Parameters.AddWithValue("@subgrupo", subgrupo);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var visto = VerVisita(reader["Ingreso"].ToString());
                    Censo censo = new Censo()
                    {
                        Ingreso = Convert.ToInt32(reader["Ingreso"].ToString()),
                        Cod_Grupo = reader["Cod_Grupo"].ToString(),
                        Nom_Grupo = reader["Nom_Grupo"].ToString(),
                        Cod_Subgrupo = reader["Cod_Subgrupo"].ToString(),
                        Nom_Subgrupo = reader["Nom_Subgrupo"].ToString(),
                        Documento = reader["Documento"].ToString(),
                        NOM_PAC = reader["NOM_PAC"].ToString(),
                        Cod_Cama = reader["Cod_Cama"].ToString(),
                        Nom_Cama = reader["Nom_Cama"].ToString(),
                        VISITA = visto
                    };
                    censos.Add(censo);
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
            return censos;

        } 

         public static List<Censo> CensoSubGruposGet(string Cod_grupo)
        {

            List<Censo> censos = new List<Censo>();
            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();


            try
            {
                command.Connection = conexion2;
                command.CommandText = @"Select
                                      DISTINCT (HPNSUBGRU.HSUCODIGO) as 'Cod_Subgrupo', HPNSUBGRU.HSUNOMBRE as 'Nom_Subgrupo'
                                    From SLNSERPRO Inner Join
                                      ADNINGRESO On ADNINGRESO.OID = SLNSERPRO.ADNINGRES1 Inner Join
                                      HPNESTANC On ADNINGRESO.OID = HPNESTANC.ADNINGRES Inner Join
                                      HPNDEFCAM On HPNDEFCAM.OID = HPNESTANC.HPNDEFCAM Inner Join
                                      GENPACIEN On GENPACIEN.OID = ADNINGRESO.GENPACIEN Inner Join
                                      HPNGRUPOS On HPNGRUPOS.OID = HPNDEFCAM.HPNGRUPOS Inner Join
                                      HPNSUBGRU On HPNSUBGRU.OID = HPNDEFCAM.HPNSUBGRU Full Join
                                      SLNORDSER On SLNORDSER.OID = SLNSERPRO.SLNORDSER1
                                    Where HPNESTANC.HESFECSAL Is Null And HPNDEFCAM.HCAESTADO = 2 And
                                      ADNINGRESO.AINESTADO = 0 And HPNGRUPOS.HGRCODIGO LIKE '%' + @Cod_grupo + '%'
                                    Group By ADNINGRESO.AINCONSEC, HPNGRUPOS.HGRCODIGO, HPNGRUPOS.HGRNOMBRE,
                                      HPNSUBGRU.HSUCODIGO, HPNSUBGRU.HSUNOMBRE, GENPACIEN.PACNUMDOC,
                                      HPNDEFCAM.HCACODIGO, HPNDEFCAM.HCANOMBRE, GENPACIEN.PACPRINOM,
                                      GENPACIEN.PACSEGNOM, GENPACIEN.PACPRIAPE, GENPACIEN.PACSEGAPE";
                command.Parameters.AddWithValue("@Cod_grupo", Cod_grupo);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Censo censo = new Censo()
                    {
                        Cod_Subgrupo = reader["Cod_Subgrupo"].ToString(),
                        Nom_Subgrupo = reader["Nom_Subgrupo"].ToString(),
                    };
                    censos.Add(censo);
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
            return censos;

        }

        public static string VerVisita(string ADNINGRES1)
        {

            SqlCommand command;
            Conexion conexion = new Conexion();
            string resvisto = "";

            try
            {
                command = new SqlCommand("SELECT  VISITA FROM SCvisita WHERE ADNINGRES1 = @ADNINGRES1 ", conexion.OpenConnection());
                command.Parameters.AddWithValue("@ADNINGRES1", ADNINGRES1);
                string visto = Convert.ToString(command.ExecuteScalar());
                if (String.IsNullOrEmpty(visto))
                {
                    resvisto = "NO";
                }
                else
                {
                    resvisto = "SI";
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

            return resvisto;

        }


    }
}