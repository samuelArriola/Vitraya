using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using Entidades.InventarioFarmacia;

namespace Persistencia.InventarioFarmacia
{
    public class ControlDespControler
    {
        public static List<ControlDespModel> GetSuminustros(string oid_suministro) {
            
            List<ControlDespModel> listSuministro = new List<ControlDespModel>();
            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();

            try
            {
                command.Connection = conexion2;
                command.CommandText = @"SELECT INNCSUMPA.OID AS ID,
                                      INNDOCUME.IDCONSEC AS SUMINISTRO,
                                      INNDOCUME.IDFECDOC AS 'FEC_SUMINISTRO',
                                      GENARESER.GASNOMBRE AS 'AREA_SERVICIO',
                                      INNALMACE.IALCODIGO AS ALMACEN,
                                      INNALMACE.IALNOMBRE AS 'NOM_ALMACEN',
                                      HPNDEFCAM.HCANOMBRE AS CAMA,
                                      ADNINGRESO.AINCONSEC AS ADMISION,
                                      GENPACIEN.PACNUMDOC AS DOCUMENTO,
                                     (GENPACIEN.PACPRINOM + ' ' + GENPACIEN.PACSEGNOM + ' ' + GENPACIEN.PACPRIAPE + ' ' + GENPACIEN.PACSEGAPE) AS NOMBRE,
                                      HCNMEDPAC.HCSFECSOL AS 'FEC SOLICITA',
                                      INNPRODUC.IPRCODIGO AS 'COD_PRODUCTO',
                                      INNPRODUC.IPRCODALT AS 'COD ATC',
                                      INNPRODUC.IPRDESCOR AS 'NOM_PRODUCTO',
                                      HCNMEDPAC.HCSOBSERV AS POSOLOGIA,
                                      INNMSUMPA.IDDCANTID AS CANTIDAD,
                                      INNLOTSER.ILSCODIGO AS LOTE,
                                      HCNMEDPAC.HCSDOSIS As DOSIS,
                                      INNUNIDAD.IUNUNICOM As 'UNIDAD_DE_MEDIDA',
                                      HCNMEDPAC.HCSDOSTPER As FRECUENCIA,
                                      (Case HCNMEDPAC.HCSUNDTPER When '1' Then 'SEGUNDO' When '2' Then 'MINUTO'
                                        When '3' Then 'HORA' When '4' Then 'DIA' When '5' Then 'SEMANA'
                                        When '6' Then 'MES' When '7' Then 'AÑO' End) As 'TIEMPO FRECUENCIA',
                                      (CASE HCNMEDPAC.HCSVIAADM WHEN '0' THEN 'ORAL' WHEN '1' THEN 'ORAL SONDA'
                                        WHEN '2' THEN 'ORAL SUCCION' WHEN '3' THEN 'ENDOVENOSA'
                                        WHEN '4' THEN 'INTRAMUSCULAR' WHEN '5' THEN 'SUBCUTANEO'
                                        WHEN '6' THEN 'TÓPICO' WHEN '7' THEN 'INFUSIÓN'
                                        WHEN '8' THEN 'NUTRICION ENTERAL' WHEN '9' THEN 'NUTRICION PARENTERAL'
                                        WHEN '10' THEN 'INHALATORIA' WHEN '11' THEN 'INTRARRECTAL'
                                        WHEN '12' THEN 'TRANSDÉRMICA' WHEN '13' THEN 'SUBLINGUAL'
                                        WHEN '14' THEN 'VAGINAL' WHEN '15' THEN 'OFTÁLMICA' WHEN '16' THEN 'ÓTICA'
                                        WHEN '17' THEN 'INTRADÉRMICA' WHEN '18' THEN 'BUCAL'
                                        WHEN '19' THEN 'POR SONDA NASOGÁSTRICA' WHEN '20' THEN 'RECTAL'
                                        WHEN '21' THEN 'POR SONDA RECTAL' WHEN '22' THEN 'CUTÁNEA'
                                        WHEN '23' THEN 'NASAL' WHEN '24' THEN 'POR SONDA VESICAL'
                                        WHEN '25' THEN 'PARENTERAL' WHEN '26' THEN 'POR MICRONEBULIZACIÓN'
                                        WHEN '27' THEN 'INTRAVENOSA DIRECTA' WHEN '28' THEN 'INTRAVENOSA CONTINÚA'
                                        WHEN '29' THEN 'INTRAVENOSA DILUIDA' WHEN '30' THEN 'EPIDURAL'
                                        WHEN '31' THEN 'ENDOTRAQUEAL' WHEN '32' THEN 'INTRAARTERIAL'
                                        WHEN '33' THEN 'INTRACARDIACA' WHEN '34' THEN 'INTRACAVITARIA'
                                        WHEN '35' THEN 'INTRACORONARIA' WHEN '36' THEN 'INFILTRACIÓN LOCAL'
                                        WHEN '37' THEN 'INYECCIÓN LOCAL' WHEN '38' THEN 'INTRAPERITONEAL'
                                        WHEN '39' THEN 'INTRAPLEURAL' WHEN '40' THEN 'INTRARRAQUÍDEA'
                                        WHEN '41' THEN 'INTRASINOVIAL' WHEN '42' THEN 'INTRATECAL'
                                        WHEN '43' THEN 'INTRAOSEA' WHEN '98' THEN 'OTRAS'
                                        WHEN '99' THEN 'TODAS LAS VIAS' WHEN '100' THEN 'AURICULAR (ÓTICA)'
                                        WHEN '101' THEN 'DENTAL' WHEN '102' THEN 'ENDOCERVICAL'
                                        WHEN '103' THEN 'ENDOSINUSIAL' WHEN '104' THEN 'EXTRA-AMNIÓTICO'
                                        WHEN '105' THEN 'HEMODIALISIS' WHEN '106' THEN 'INTRA CORPUS CAVERNOSO'
                                        WHEN '107' THEN 'INTRAAMNIÓTICAP' WHEN '108' THEN 'INTRAARTICULAR'
                                        WHEN '109' THEN 'INTRAUTERINA' WHEN '110' THEN 'INTRACAVERNOSA'
                                        WHEN '111' THEN 'INTRACEREBRAL' WHEN '112' THEN 'INTRACERVICAL'
                                        WHEN '113' THEN 'INTRACISTERNAL (CEREBELOMEDULAR)'
                                        WHEN '114' THEN 'INTRACORNEAL' WHEN '115' THEN 'INTRADISCAL'
                                        WHEN '116' THEN 'INTRAHEPÁTICA' WHEN '117' THEN 'USO INTRALESIONAL'
                                        WHEN '118' THEN 'USO INTRALINFÁTICO' WHEN '119' THEN 'INTRAMEDULAR'
                                        WHEN '120' THEN 'INTRAMENÍNGEA' WHEN '121' THEN 'INTRAOCULAR'
                                        WHEN '122' THEN 'INTRAPERICARDIAL' WHEN '123' THEN 'INTRATORÁXICA'
                                        WHEN '124' THEN 'INTRATRAQUEAL' WHEN '125' THEN 'INTRATUMORAL'
                                        WHEN '126' THEN 'BOLO INTRAVENOSO' WHEN '127' THEN 'GOTEO INTRAVENOSO'
                                        WHEN '128' THEN 'INTRAVENOSA' WHEN '129' THEN 'INTRAVESICULAR'
                                        WHEN '130' THEN 'IONTOFORESIS' WHEN '131' THEN 'TÉCNICA DE VENDAJE OCLUSIVO'
                                        WHEN '132' THEN 'OROFARÍNGEA' WHEN '133' THEN 'PERIARTICULAR'
                                        WHEN '134' THEN 'PERINEURAL' WHEN '135' THEN 'RETROBULBAL'
                                        WHEN '136' THEN 'SUBCONJUNTIVAL' WHEN '137' THEN 'SUBCUTÁNEA'
                                        WHEN '138' THEN 'TÓPICA' WHEN '139' THEN 'TRANSMAMARIA'
                                        WHEN '140' THEN 'TRANSPLACENTARIA' WHEN '141' THEN 'URETRAL'
                                        WHEN '142' THEN 'CONJUNTIVAL' WHEN '143' THEN 'ELECTRO-OSMOSIS'
                                        WHEN '144' THEN 'ENTERAL' WHEN '145' THEN 'GASTROENTERAL'
                                        WHEN '146' THEN 'INTRAGINGIVAL' WHEN '147' THEN 'IN VITRO'
                                        WHEN '148' THEN 'INFILTRACIÓN' WHEN '149' THEN 'INTERSTICIAL'
                                        WHEN '150' THEN 'INTRABDOMINAL' WHEN '151' THEN 'INTRABILIAR'
                                        WHEN '152' THEN 'INTRABRONQUIAL' WHEN '153' THEN 'INTRABURSAL'
                                        WHEN '154' THEN 'INTRACARTILAGINOSO' WHEN '155' THEN 'INTRACAUDAL'
                                        WHEN '156' THEN 'INTRACORONARIO DENTAL' WHEN '157' THEN 'INTRADUCTAL'
                                        WHEN '158' THEN 'INTRADUODENAL' WHEN '159' THEN 'INTRADURAL'
                                        WHEN '160' THEN 'INTRAEPIDERMAL' WHEN '161' THEN 'INTRAESOFÁGICA'
                                        WHEN '162' THEN 'INTRAGÁSTRICA' WHEN '163' THEN 'INTRAILEAL'
                                        WHEN '164' THEN 'INTRAOVARICA' WHEN '165' THEN 'INTRAPROSTATICA'
                                        WHEN '166' THEN 'INTRAPULMONAR' WHEN '167' THEN 'INTRASINUSAL'
                                        WHEN '168' THEN 'INTRAESTERNAL' WHEN '169' THEN 'INTRATENDINOSA'
                                        WHEN '170' THEN 'INTRATESTICULAR' WHEN '171' THEN 'INTRATUBULAR'
                                        WHEN '172' THEN 'INTRATIMPANICA' WHEN '173' THEN 'INTRAVASCULAR'
                                        WHEN '174' THEN 'INTRAVENTRICULAR' WHEN '175' THEN 'INTRAVÍTREA'
                                        WHEN '176' THEN 'IRRIGACION' WHEN '177' THEN 'LARINGEO'
                                        WHEN '178' THEN 'LARINGOFARINGEAL' WHEN '179' THEN 'USO OROMUCOSA'
                                        WHEN '180' THEN 'PERCUTANEA' WHEN '181' THEN 'PERIDURAL'
                                        WHEN '182' THEN 'PERIODONTAL' WHEN '183' THEN 'TEJIDO BLANDO'
                                        WHEN '184' THEN 'SUBARACNOIDEA' WHEN '185' THEN 'SUBMUCOSA'
                                        WHEN '186' THEN 'TRANSMUCOSA' WHEN '187' THEN 'TRANSTRAQUEAL'
                                        WHEN '188' THEN 'TRANSTIMPANICA' WHEN '189' THEN 'URETERAL'
                                        WHEN '190' THEN 'INTRAVESICAL' WHEN '200' THEN 'INTRADETRUSOR'
                                        WHEN '201' THEN 'USO EPILESIONAL' WHEN '202' THEN 'INHALATORIA NASAL'
                                        WHEN '203' THEN 'INHALATORIA BUCAL' WHEN '204' THEN 'SONDA'
                                      END) AS 'VIA_ADMON',
                                      HCNMEDPAC.HCSDOSTDUR AS 'HORAS FORMULA'
                                    FROM INNCSUMPA
                                      INNER JOIN INNMSUMPA ON INNCSUMPA.OID = INNMSUMPA.INNCSUMPA
                                      INNER JOIN INNPRODUC ON INNPRODUC.OID = INNMSUMPA.INNPRODUC
                                      INNER JOIN INNDOCUME ON INNDOCUME.OID = INNCSUMPA.OID
                                      INNER JOIN ADNINGRESO ON ADNINGRESO.OID = INNCSUMPA.ADNINGRESO
                                      INNER JOIN GENPACIEN ON GENPACIEN.OID = ADNINGRESO.GENPACIEN
                                      INNER JOIN INNALMACE ON INNALMACE.OID = INNCSUMPA.INNALMACE
                                      INNER JOIN INNLOTSER ON INNLOTSER.OID = INNMSUMPA.INNLOTSER AND INNPRODUC.OID = INNLOTSER.INNPRODUC
                                      INNER JOIN HCNMEDPAC ON HCNMEDPAC.OID = INNMSUMPA.HCNMEDPAC
                                      LEFT JOIN HPNDEFCAM ON HPNDEFCAM.OID = ADNINGRESO.HPNDEFCAM AND ADNINGRESO.OID = HPNDEFCAM.ADNINGRESO
                                      INNER JOIN GENARESER ON GENARESER.OID = INNCSUMPA.GENARESER
                                      Left Join INNUNIDAD On INNUNIDAD.OID = HCNMEDPAC.INNUNIDADC
                                    WHERE INNDOCUME.IDCONSEC = @oid_suministro ";
                command.Parameters.AddWithValue("@oid_suministro", oid_suministro);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ControlDespModel suministro = new ControlDespModel()
                    {
                        CONSECUTIVO = Convert.ToInt64( reader["ID"].ToString() ),
                        OID_SUMINISTRO = reader["SUMINISTRO"].ToString(),
                        FEC_DOCUMENTO = (reader["FEC_SUMINISTRO"] == DBNull.Value) ? (DateTime?)null : Convert.ToDateTime(reader["FEC_SUMINISTRO"].ToString()),
                        DOCUMENTO_PAC = reader["DOCUMENTO"].ToString(),
                        NOMBRE_PAC = reader["NOMBRE"].ToString(),
                        ALMACEN = reader["NOM_ALMACEN"].ToString(),
                        CAMA = reader["CAMA"].ToString(),
                        POSOLOGIA = reader["POSOLOGIA"].ToString(),
                        AREA_SERVICIO = reader["AREA_SERVICIO"].ToString(),
                        UNIDAD_DE_MEDIDA = reader["UNIDAD_DE_MEDIDA"].ToString(),
                        CODIGO_MED = reader["COD_PRODUCTO"].ToString(),
                        DESCRIP_MED = reader["NOM_PRODUCTO"].ToString(),
                        DOSIS_MED = float.Parse( reader["DOSIS"].ToString(), CultureInfo.InvariantCulture.NumberFormat),
                        FECUENCIA_MED = reader["FRECUENCIA"].ToString(),
                        VIA_ADMIN_MED = reader["VIA_ADMON"].ToString(),
                        OID_LOTE = reader["LOTE"].ToString(),
                        CANTIDAD = float.Parse(reader["CANTIDAD"].ToString()),

                    };
                    listSuministro.Add(suministro);
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


            return listSuministro;
        } 
        
        public static void setSuministro(string OIDSUMINISTRO, long CONSECUTIVO, long DOCUMENTO_PAC, long USUARIOFIRMA, char CPAC, char CCANT, char CVIAADMIN, char CDOSIS, string OBSPRO = "NO APLICA") {
            
            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();
            var DateAndTime = DateTime.Now;
            var Responsable = Convert.ToInt32(HttpContext.Current.Session["Admin"]);

            try
            {
                command.Connection = conexion2;
                command.CommandText = @"INSERT INTO INNSUMENTREGADO
                                           ([OIDSUMINISTRO]
                                           ,[CONSECUTIVO]
                                           ,[DOC_PAC]
                                           ,[USUARIOLOG]
                                           ,[USUARIOFIRMA]
                                           ,[FECFIRMA]
                                           ,[CPAC]
                                           ,[CCANT]
                                           ,[CVIAADMIN]
                                           ,[CDOSIS]
                                           ,[OBSPRO])
                                     VALUES(
                                            @OIDSUMINISTRO
                                           ,@CONSECUTIVO
                                           ,@DOC_PAC
                                           ,@USUARIOLOG
                                           ,@USUARIOFIRMA
                                           ,@FECFIRMA
                                           ,@CPAC
                                           ,@CCANT
                                           ,@CVIAADMIN
                                           ,@CDOSIS
                                           ,@OBSPRO
                                            )
                ";

                command.Parameters.AddWithValue("@OIDSUMINISTRO", OIDSUMINISTRO );
                command.Parameters.AddWithValue("@CONSECUTIVO",  CONSECUTIVO);
                command.Parameters.AddWithValue("@DOC_PAC",  DOCUMENTO_PAC);
                command.Parameters.AddWithValue("@USUARIOLOG", Responsable);
                command.Parameters.AddWithValue("@USUARIOFIRMA",USUARIOFIRMA);
                command.Parameters.AddWithValue("@FECFIRMA", DateAndTime);
                command.Parameters.AddWithValue("@CPAC", CPAC);
                command.Parameters.AddWithValue("@CCANT", CCANT );
                command.Parameters.AddWithValue("@CVIAADMIN",  CVIAADMIN);
                command.Parameters.AddWithValue("@CDOSIS", CDOSIS);
                command.Parameters.AddWithValue("@OBSPRO", OBSPRO);
                var reader = command.ExecuteReader();
               

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                conexion2.Close();
            }


           
        }

        public static Boolean loginFirma(string user, string pass)
        {

            Conexion con = new Conexion();
            SqlCommand consulta;
            DataSet ds = new DataSet();

            try
            {

                consulta = new SqlCommand("select GNCodUsu,GNConUsu,GnEtUsu,codigoR from Usuario where GNCodUsu = @user and GNConUsu = @pass", con.OpenConnection());
                consulta.Parameters.AddWithValue("@user", Convert.ToInt64(user));
                consulta.Parameters.AddWithValue("@pass", pass);
                consulta.ExecuteNonQuery();

                SqlDataAdapter sd = new SqlDataAdapter(consulta);
                sd.Fill(ds, "user");
                DataRow dr;
                dr = ds.Tables["user"].Rows[0];

                if  ( user == dr["GNCodUsu"].ToString() & pass == dr["GNConUsu"].ToString() & dr["GnEtUsu"].ToString() == "Activo")
                {
                    return true;

                }

            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                con.CloseConnection();
            }


            return false;
        }

        public static int CountSuministro(string OIDSUMINISTRO)
        {
            int num = 0;
            SqlConnection conexion2 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conexion2"].ConnectionString);
            conexion2.Open();
            var command = new SqlCommand();
          

            try
            {
                command.Connection = conexion2;
                command.CommandText =" SELECT COUNT(*) FROM INNSUMENTREGADO WHERE  [OIDSUMINISTRO] = @OIDSUMINISTRO ";
                command.Parameters.AddWithValue("@OIDSUMINISTRO", OIDSUMINISTRO);
                num = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion2.Close();
            }

            return num;
        }
    }
}