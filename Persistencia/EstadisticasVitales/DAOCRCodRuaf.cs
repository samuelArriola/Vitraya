using Entidades.EstadisticasVitales;
using Entidades.Generales;
using Persistencia.Generales;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;

namespace Persistencia.EstadisticasVitales
{
    public class DAOCRCodRuaf
    {
        //guardar un codigo en BD
        public static void SetCodigoRuaf(CRCodRuaf codRuaf) {
            SqlCommand command;
            Conexion conexion = new Conexion();

            
                try
                {
                    command = new SqlCommand("iNSERT INTO [dbo].[CRCodRuaf] ([FecCod],[Incidencia],[Estado],[TipCodigo],[GNCodUsu], [CRcodRuaf], Eliminado) " +
                                             "VALUES (@FecCod ,@Incidencia ,@Estado ,@TipCodigo ,@GNCodUsu ,@CRcodRuaf, 0 ) " +
                                             "SELECT SCOPE_IDENTITY()", conexion.OpenConnection());

                    command.Parameters.AddWithValue("@FecCod", codRuaf.DateFecCod);
                    command.Parameters.AddWithValue("@Incidencia", codRuaf.StrIncidencia);
                    command.Parameters.AddWithValue("@Estado", codRuaf.IsEstado);
                    command.Parameters.AddWithValue("@TipCodigo", codRuaf.StrTipCodigo);
                    command.Parameters.AddWithValue("@GNCodUsu", codRuaf.DoubleGNCodUsu);
                    command.Parameters.AddWithValue("@CRcodRuaf", codRuaf.DoubleCRcodRuaf);

                    int idInstancia =  Convert.ToInt32(command.ExecuteScalar());

                    DAOGNHistorico.SetHistorico(new GNHistorico {
                        intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                        intInstancia = idInstancia,
                        intOidGNHistorico = 0,
                        strAccion = "Crear",
                        strDetalle = $"Se ingresa el código {codRuaf.DoubleCRcodRuaf}",
                        dtmFecha = DateTime.Now,
                        strEntidad = "CRCodRuaf"
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

        //buscar un código por su id
        public static CRCodRuaf getCodRuafId(string OIdCRCodRuaf)
        {
            CRCodRuaf codRuaf = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from CRCodRuaf where OIdCRCodRuaf = @CRcodRuaf and  isnull(Eliminado,0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@CRcodRuaf", OIdCRCodRuaf);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    codRuaf = new CRCodRuaf
                    {
                        IntOIdCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"]),
                        DateFecCod = Convert.ToDateTime(reader["FecCod"]),
                        StrIncidencia = reader["Incidencia"].ToString(),
                        IsEstado = Convert.ToBoolean(reader["Estado"]),
                        StrTipCodigo = reader["TipCodigo"].ToString(),
                        DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"]),
                        DoubleCRcodRuaf = Convert.ToDouble(reader["CRcodRuaf"])
                    };
                }
            }
            catch (Exception wi)
            {

                System.Windows.Forms.MessageBox.Show(wi.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }



            return codRuaf;
        }

        //eliminar un código usando el mismo código como parametro
        public static bool eliminarCodRuaf(string eliCodRuaf) 
        {
            bool isDeleted = true;
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand(@" update CRCodRuaf set isnull(Eliminado,0) = 1 where CRcodRuaf = @CRcodRuaf
                                            select OIdCRCodRuaf from CRCodRuaf where CRcodRuaf = @CRcodRuaf", conexion.OpenConnection());
                command.Parameters.AddWithValue("@CRcodRuaf", eliCodRuaf);
                int OidInstancia = Convert.ToInt32(command.ExecuteScalar());
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    dtmFecha = DateTime.Now,
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = OidInstancia,
                    strAccion = "Eliminar",
                    strDetalle = $"Se elimina el código {eliCodRuaf} ",
                    strEntidad = "CRcodRuaf"
                });
            }
            catch (Exception wi)
            {
                isDeleted = false;
                //System.Windows.Forms.MessageBox.Show(wi.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return isDeleted;
        }

        //obtener 1 código usando como parametro el mismo código
        public static CRCodRuaf getCodRuaf(string CodRuafBus)
        {
            CRCodRuaf codRuaf = new CRCodRuaf();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from CRCodRuaf where CRcodRuaf = @CRcodRuaf and  isnull(Eliminado,0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@CRcodRuaf", CodRuafBus);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    codRuaf = new CRCodRuaf
                    {
                        IntOIdCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"]),
                        DateFecCod = Convert.ToDateTime(reader["FecCod"]),
                        StrIncidencia = reader["Incidencia"].ToString(),
                        IsEstado = Convert.ToBoolean(reader["Estado"]),
                        StrTipCodigo = reader["TipCodigo"].ToString(),
                        DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"]),
                        DoubleCRcodRuaf = Convert.ToDouble(reader["CRcodRuaf"]),

                    };
                }
            }
            catch (Exception wi)
            {

                System.Windows.Forms.MessageBox.Show(wi.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }



            return codRuaf;
        }

        //obtener 1 código con el estado true que indica que puede ser asignado.
        public static CRCodRuaf getCodRuafUltValido(string tipoCodRuaf)
        {
            CRCodRuaf codRuafUlt = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select top(1) * from CRCodRuaf where  CRCodRuaf.TipCodigo = @TipCodigo and CRCodRuaf.Estado = 1 and  isnull(Eliminado,0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@TipCodigo", tipoCodRuaf );
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    codRuafUlt = new CRCodRuaf
                    {
                        IntOIdCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"]),
                        DateFecCod = Convert.ToDateTime(reader["FecCod"]),
                        StrIncidencia = reader["Incidencia"].ToString(),
                        IsEstado = Convert.ToBoolean(reader["Estado"]),
                        StrTipCodigo = reader["TipCodigo"].ToString(),
                        DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"]),
                        DoubleCRcodRuaf = Convert.ToDouble(reader["CRcodRuaf"]),
                        
                    };
                }
            }
            catch (Exception wi)
            {

                System.Windows.Forms.MessageBox.Show(wi.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }



            return codRuafUlt;
        }

        //cambiar estado de un código. 
        public static void cambEstCodRuaf(int IntOIdCRCodRuaf)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("UPDATE [dbo].[CRCodRuaf] SET CRCodRuaf.Estado = 0 WHERE CRCodRuaf.OIdCRCodRuaf = @OIdCRCodRuaf and  isnull(Eliminado,0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OIdCRCodRuaf", IntOIdCRCodRuaf);  
                command.ExecuteNonQuery();
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

        //guardar incidendia en un codigo
        public static void SetIncidencia(int IntOIdCRCodRuaf, string incidencia)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("UPDATE [dbo].[CRCodRuaf] SET CRCodRuaf.Incidencia = '"+ incidencia + "' WHERE CRCodRuaf.OIdCRCodRuaf = @OIdCRCodRuaf and  isnull(Eliminado,0) = 0", conexion.OpenConnection());
                command.Parameters.AddWithValue("@OIdCRCodRuaf", IntOIdCRCodRuaf);
                command.ExecuteNonQuery();
                DAOGNHistorico.SetHistorico(new GNHistorico
                {
                    intGNCodUsu = Convert.ToInt32(HttpContext.Current.Session["Admin"]),
                    intInstancia = IntOIdCRCodRuaf,
                    intOidGNHistorico = 0,
                    strAccion = "Crear",
                    strDetalle = $"Se ingresa insidencia: {incidencia}",
                    dtmFecha = DateTime.Now,
                    strEntidad  = "CRCodRuaf"
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

        //obtener la lista de códigos existentes
        public static List<CRCodRuaf> GetCodRuafTot()
        {
            List<CRCodRuaf> CodRuafTol = new List<CRCodRuaf>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from CRCodRuaf where  isnull(Eliminado,0) = 0", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CRCodRuaf CodRuaf = new CRCodRuaf
                    {
                        IntOIdCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"]),
                        DateFecCod = Convert.ToDateTime(reader["FecCod"]),
                        StrIncidencia = reader["Incidencia"].ToString(),
                        IsEstado = Convert.ToBoolean(reader["Estado"]),
                        StrTipCodigo = reader["TipCodigo"].ToString(),
                        DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"]),
                        DoubleCRcodRuaf = Convert.ToDouble(reader["CRcodRuaf"]),    
                    };
                    CodRuafTol.Add(CodRuaf);
                }
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return CodRuafTol;
        }

        //obtener todos los codigos RUAF Validos.
        public static List<CRCodRuaf> GetCodRuafTotVal()
        {
            List<CRCodRuaf> CodRuafTol = new List<CRCodRuaf>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from CRCodRuaf where CRCodRuaf.Estado = 1 and  isnull(Eliminado,0) = 0", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CRCodRuaf CodRuaf = new CRCodRuaf
                    {
                        IntOIdCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"]),
                        DateFecCod = Convert.ToDateTime(reader["FecCod"]),
                        StrIncidencia = reader["Incidencia"].ToString(),
                        IsEstado = Convert.ToBoolean(reader["Estado"]),
                        StrTipCodigo = reader["TipCodigo"].ToString(),
                        DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"]),
                        DoubleCRcodRuaf = Convert.ToDouble(reader["CRcodRuaf"]),
                    };
                    CodRuafTol.Add(CodRuaf);
                }
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return CodRuafTol;
        }

        public static void sentEmail(List<CRCodRuaf> codRuafVal)
        {
            int contDef = 0;
            int contNV = 0;
            foreach (CRCodRuaf cod in codRuafVal)
            {

                if (cod.StrTipCodigo == "Defunción")
                    contDef++;

                if (cod.StrTipCodigo == "NacViv")
                    contNV++;
            }



        }

        //obtener todos los códigos RUAF de tipo nacidos vivos, validos para asignar. 
        public static List<CRCodRuaf> GetCodRuafNVVal()
        {
            List<CRCodRuaf> CodRuafTol = new List<CRCodRuaf>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from CRCodRuaf where CRCodRuaf.Estado = 1 and CRCodRuaf.TipCodigo = 'NacViv' and  isnull(Eliminado,0) = 0", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CRCodRuaf CodRuaf = new CRCodRuaf
                    {
                        IntOIdCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"]),
                        DateFecCod = Convert.ToDateTime(reader["FecCod"]),
                        StrIncidencia = reader["Incidencia"].ToString(),
                        IsEstado = Convert.ToBoolean(reader["Estado"]),
                        StrTipCodigo = reader["TipCodigo"].ToString(),
                        DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"]),
                        DoubleCRcodRuaf = Convert.ToDouble(reader["CRcodRuaf"]),
                    };
                    CodRuafTol.Add(CodRuaf);
                }
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return CodRuafTol;
        }

        //obtener todos los códigos RUAF de tipo Defunción, validos para asignar. 
        public static List<CRCodRuaf> GetCodRuafDefVal()
        {
            List<CRCodRuaf> CodRuafTol = new List<CRCodRuaf>();
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("select * from CRCodRuaf where CRCodRuaf.Estado = 1 and CRCodRuaf.TipCodigo = 'Defunción' and  isnull(Eliminado,0) = 0", conexion.OpenConnection());
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CRCodRuaf CodRuaf = new CRCodRuaf
                    {
                        IntOIdCRCodRuaf = Convert.ToInt32(reader["OIdCRCodRuaf"]),
                        DateFecCod = Convert.ToDateTime(reader["FecCod"]),
                        StrIncidencia = reader["Incidencia"].ToString(),
                        IsEstado = Convert.ToBoolean(reader["Estado"]),
                        StrTipCodigo = reader["TipCodigo"].ToString(),
                        DoubleGNCodUsu = Convert.ToDouble(reader["GNCodUsu"]),
                        DoubleCRcodRuaf = Convert.ToDouble(reader["CRcodRuaf"]),
                    };
                    CodRuafTol.Add(CodRuaf);
                }
            }
            catch (Exception ex)
            {

                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return CodRuafTol;
        }

        public static List<dynamic> GetDocsRepetidosNV(string codigo, string docMadre, string nomMadre, string nomDoctor)
        {
            List<dynamic> docsRepetidos = new List<dynamic>();

            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand(@" select C.CRcodRuaf Codigo,  NV.IdMadre DocumentoMadre, NV.NomMadre, NV.NomDoc,cast(NV.FecNac as Date) FecNacimiento 
                                            from CRNacidoVivo NV
	                                            inner join CRCodRuaf C on C.OIdCRCodRuaf = NV.OidCRCodRuaf
                                            where C.CRcodRuaf like '%'+ @Codigo +'%' and NV.IdMadre like '%'+ @DocumentoMadre +'%' 
                                            and NomMadre like '%'+ @NomMadre +'%' and NomDoc like '%'+ @NomDoc +'%'
                                            and  (select count(*) from CRNacidoVivo where IdMadre = NV.IdMadre) > 1 and  isnull(Eliminado,0) = 0", conexion.OpenConnection());

                command.Parameters.AddWithValue("Codigo", codigo);
                command.Parameters.AddWithValue("DocumentoMadre", docMadre);
                command.Parameters.AddWithValue("NomMadre", nomMadre);
                command.Parameters.AddWithValue("NomDoc", nomDoctor);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        docsRepetidos.Add(new
                        {
                            Codigo = reader["Codigo"].ToString(),
                            DocumentoMadre = reader["DocumentoMadre"].ToString(),
                            NombreMadre = reader["NomMadre"].ToString(),
                            NombreDoctor = reader["NomDoc"].ToString(),
                            Fecha = reader["FecNacimiento"].ToString()
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return docsRepetidos;
        }
    }
}