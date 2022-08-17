using Entidades.Vacunacion;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Persistencia.Vacunacion
{
    public class DAORegistroDiarioVac
    {
        public static bool SetRegistroDiario(RegistroDiarioVac registro)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            bool isRegistroCreated = false;
            try
            {
                command = new SqlCommand(@"IF (EXISTS(SELECT * FROM RegistroDiarioVac R WHERE R.FechaVacunacion = Cast(@FechaVacunacion as Date) and Documento = @Documento ))
	                                            SELECT 0
                                            ELSE 
	                                            BEGIN
		                                            SELECT 1
		                                            INSERT INTO [dbo].[RegistroDiarioVac]
			                                            ([FechaVacunacion]
			                                            ,[TipoDocumento]
			                                            ,[Documento]
			                                            ,[FechaNacimiento]
			                                            ,[Sexo]
			                                            ,[PrimerApellido]
			                                            ,[SegundoApellido]
			                                            ,[Nombres]
			                                            ,[Regimen]
			                                            ,[Eps]
			                                            ,[DeptoResidencia]
			                                            ,[MunicipioResidencia]
			                                            ,[AreaResidencia]
			                                            ,[Barrio]
			                                            ,[Direccion]
			                                            ,[Telefono]
			                                            ,[GrupoEtnico]
			                                            ,[Desplazamiento]
			                                            ,[Discapacidad]
			                                            ,[Email]
			                                            ,[Gestacion]
			                                            ,[FechaProbableParto]
			                                            ,[EtapaVacunacion]
			                                            ,[TipoPoblacion]
			                                            ,[Dosis]
			                                            ,[Biologico]
			                                            ,[Lote]
			                                            ,[Jeringa]
			                                            ,[NombreVacunador]
			                                            ,[NombreIps]
			                                            ,[TipoDocumentoAC]
			                                            ,[DocumentoAC]
			                                            ,[ParentescoAC]
			                                            ,[NombresAC]
			                                            ,[RegimenAC]
			                                            ,[EpsAC]
			                                            ,[GrupoEtnicoAC]
			                                            ,[DesplazamientoAC]
			                                            ,[DiscapacidadAC]
			                                            ,[TelefonoAC]
			                                            ,[LoteJeringa]
			                                            ,[Edad]
			                                            ,[FechaRetgistro]
			                                            ,[LugarRegistro]
			                                            ,[OidRegistrador]
			                                            ,[OidVCLoteJeringa]
			                                            ,[OidVCLoteBiologico]
			                                            ,[Estado]
			                                            ,[EmailAC]
                                                        ,[SemanasMenor]
                                                        ,[PesoMenor]
                                                        ,[LugarParto]
                                                        ,[NombreLugarParto]
                                                        ,[DeptoNacimiento]
                                                        ,[MunicipioNacimiento])
		                                            VALUES
			                                            (@FechaVacunacion
			                                            ,@TipoDocumento
			                                            ,@Documento
			                                            ,@FechaNacimiento
			                                            ,@Sexo
			                                            ,@PrimerApellido
			                                            ,@SegundoApellido
			                                            ,@Nombres
			                                            ,@Regimen
			                                            ,@Eps
			                                            ,@DeptoResidencia
			                                            ,@MunicipioResidencia
			                                            ,@AreaResidencia
			                                            ,@Barrio
			                                            ,@Direccion
			                                            ,@Telefono
			                                            ,@GrupoEtnico
			                                            ,@Desplazamiento
			                                            ,@Discapacidad
			                                            ,@Email
			                                            ,@Gestacion
			                                            ,@FechaProbableParto
			                                            ,@EtapaVacunacion
			                                            ,@TipoPoblacion
			                                            ,@Dosis
			                                            ,@Biologico
			                                            ,@Lote
			                                            ,@Jeringa
			                                            ,@NombreVacunador
			                                            ,@NombreIps
			                                            ,@TipoDocumentoAC
			                                            ,@DocumentoAC
			                                            ,@ParentescoAC
			                                            ,@NombresAC
			                                            ,@RegimenAC
			                                            ,@EpsAC
			                                            ,@GrupoEtnicoAC
			                                            ,@DesplazamientoAC
			                                            ,@DiscapacidadAC
			                                            ,@TelefonoAC
			                                            ,@LoteJeringa
			                                            ,@Edad
			                                            ,@FechaRetgistro
			                                            ,@LugarRegistro
			                                            ,@OidRegistrador
			                                            ,@OidVCLoteJeringa
			                                            ,@OidVCLoteBiologico
			                                            ,@Estado
			                                            ,@EmailAC
                                                        ,@SemanasGestacionMenor
			                                            ,@PesoAlNacerMenor
			                                            ,@LugarParto
			                                            ,@NombreLugarParto
                                                        ,@DepartamentoNacimiento
			                                            ,@MunicipioNacimiento)
	                                            END", conexion.OpenConnection());

                command.Parameters.AddWithValue("FechaVacunacion", registro.DtmFechaVacunacion);
                command.Parameters.AddWithValue("TipoDocumento", registro.StrTipoDocumento);
                command.Parameters.AddWithValue("Documento", registro.StrDocumento);
                command.Parameters.AddWithValue("FechaNacimiento", registro.DtmFechaNacimiento);
                command.Parameters.AddWithValue("Sexo", registro.StrSexo);
                command.Parameters.AddWithValue("PrimerApellido", registro.StrPrimerApellido);
                command.Parameters.AddWithValue("SegundoApellido", registro.StrSegundoApellido);
                command.Parameters.AddWithValue("Nombres", registro.StrNombres);
                command.Parameters.AddWithValue("Regimen", registro.StrRegimen);
                command.Parameters.AddWithValue("Eps", registro.StrEps);
                command.Parameters.AddWithValue("DeptoResidencia", registro.StrDeptoResidencia);
                command.Parameters.AddWithValue("MunicipioResidencia", registro.StrMunicipioResidencia);
                command.Parameters.AddWithValue("AreaResidencia", registro.StrAreaResidencia);
                command.Parameters.AddWithValue("Barrio", registro.StrBarrio);
                command.Parameters.AddWithValue("Direccion", registro.StrDireccion);
                command.Parameters.AddWithValue("Telefono", registro.StrTelefono);
                command.Parameters.AddWithValue("GrupoEtnico", registro.StrGrupoEtnico);
                command.Parameters.AddWithValue("Desplazamiento", registro.StrDesplazamiento);
                command.Parameters.AddWithValue("Discapacidad", registro.StrDiscapacidad);
                command.Parameters.AddWithValue("Email", registro.StrEmail);
                command.Parameters.AddWithValue("Gestacion", registro.StrGestacion);
                command.Parameters.AddWithValue("Edad", registro.IntEdad);
                command.Parameters.AddWithValue("FechaRetgistro", registro.DtmFechaRetgistro);
                command.Parameters.AddWithValue("LugarRegistro", registro.StrLugarRegistro);
                command.Parameters.AddWithValue("OidRegistrador", registro.intOidRegistrador);

                if(registro.StrFechaProbableParto == null)
                    command.Parameters.AddWithValue("FechaProbableParto", DBNull.Value);
                else
                    command.Parameters.AddWithValue("FechaProbableParto", registro.StrFechaProbableParto);

                command.Parameters.AddWithValue("EtapaVacunacion", registro.StrEtapaVacunacion);
                command.Parameters.AddWithValue("TipoPoblacion", registro.StrTipoPoblacion);
                command.Parameters.AddWithValue("Dosis", registro.StrDosis);
                command.Parameters.AddWithValue("Biologico", registro.StrBiologico);
                command.Parameters.AddWithValue("Lote", registro.StrLote);
                command.Parameters.AddWithValue("Jeringa", registro.StrJeringa);
                command.Parameters.AddWithValue("NombreVacunador", registro.StrNombreVacunador);
                command.Parameters.AddWithValue("NombreIps", registro.StrNombreIps);
                command.Parameters.AddWithValue("TipoDocumentoAC", registro.StrTipoDocumentoAC);
                command.Parameters.AddWithValue("DocumentoAC", registro.StrDocumentoAC);
                command.Parameters.AddWithValue("ParentescoAC", registro.StrParentescoAC);
                command.Parameters.AddWithValue("NombresAC", registro.StrNombresAC);
                command.Parameters.AddWithValue("RegimenAC", registro.StrRegimenAC);
                command.Parameters.AddWithValue("EpsAC", registro.StrEpsAC);
                command.Parameters.AddWithValue("GrupoEtnicoAC", registro.StrGrupoEtnicoAC);
                command.Parameters.AddWithValue("DesplazamientoAC", registro.StrDesplazamientoAC);
                command.Parameters.AddWithValue("DiscapacidadAC", registro.StrDiscapacidadAC);
                command.Parameters.AddWithValue("EmailAC", registro.StrEmailAC);
                command.Parameters.AddWithValue("TelefonoAC", registro.StrTelefonoAC);
                command.Parameters.AddWithValue("LoteJeringa", registro.StrLoteJeringa);
                command.Parameters.AddWithValue("OidVCLoteJeringa", registro.IntOidVCLoteJeringa);
                command.Parameters.AddWithValue("Estado", true);

                command.Parameters.AddWithValue("DepartamentoNacimiento", registro.StrDeptoNacimiento);
                command.Parameters.AddWithValue("MunicipioNacimiento", registro.StrMunicipioNacimiento);
                command.Parameters.AddWithValue("SemanasGestacionMenor", registro.StrSemanasMenor);
                command.Parameters.AddWithValue("PesoAlNacerMenor", registro.StrPesoMenor);
                command.Parameters.AddWithValue("LugarParto", registro.StrLugarParto);
                command.Parameters.AddWithValue("NombreLugarParto", registro.StrNombreLugarParto);

                command.Parameters.AddWithValue("OidVCLoteBiologico", registro.IntOidVCLoteBiologico);

                isRegistroCreated = Convert.ToInt32(command.ExecuteScalar()) == 1;

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return isRegistroCreated;
        }

        public static RegistroDiarioVac GetRegistroDiarioVac(int idRegistro)
        {
            RegistroDiarioVac registro = null;
            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("SELECT * FROM RegistroDiarioVac where OidRegistroDiarioVac = @OidRegistroDiarioVac", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidRegistroDiarioVac", idRegistro);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    registro = new RegistroDiarioVac
                    {
                        DtmFechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        StrFechaProbableParto = reader["FechaProbableParto"].ToString(),
                        DtmFechaVacunacion = Convert.ToDateTime(reader["FechaVacunacion"]),
                        IntOidRegistroDiarioVac = Convert.ToInt32(reader["OidRegistroDiarioVac"]),
                        IntEdad = Convert.ToInt32(reader["Edad"]),
                        StrAreaResidencia = reader["AreaResidencia"].ToString(),
                        StrBarrio = reader["Barrio"].ToString(),
                        StrBiologico = reader["Biologico"].ToString(),
                        StrDeptoResidencia = reader["DeptoResidencia"].ToString(),
                        StrDesplazamiento = reader["Desplazamiento"].ToString(),
                        StrDesplazamientoAC = reader["DesplazamientoAC"].ToString(),
                        StrDireccion = reader["Direccion"].ToString(),
                        StrDiscapacidad = reader["Discapacidad"].ToString(),
                        StrDiscapacidadAC = reader["DiscapacidadAC"].ToString(),
                        StrDocumento = reader["Documento"].ToString(),
                        StrDocumentoAC = reader["DocumentoAC"].ToString(),
                        StrDosis = reader["Dosis"].ToString(),
                        StrEmail = reader["Email"].ToString(),
                        StrEmailAC = reader["EmailAC"].ToString(),
                        StrEps = reader["Eps"].ToString(),
                        StrEpsAC = reader["EpsAC"].ToString(),
                        StrEtapaVacunacion = reader["EtapaVacunacion"].ToString(),
                        StrGestacion = reader["Gestacion"].ToString(),
                        StrGrupoEtnico = reader["GrupoEtnico"].ToString(),
                        StrGrupoEtnicoAC = reader["GrupoEtnicoAC"].ToString(),
                        StrJeringa = reader["Jeringa"].ToString(),
                        StrLote = reader["Lote"].ToString(),
                        StrMunicipioResidencia = reader["MunicipioResidencia"].ToString(),
                        StrNombreIps = reader["NombreIps"].ToString(),
                        StrNombres = reader["Nombres"].ToString(),
                        StrNombresAC = reader["NombresAC"].ToString(),
                        StrNombreVacunador = reader["NombreVacunador"].ToString(),
                        StrParentescoAC = reader["ParentescoAC"].ToString(),
                        StrPrimerApellido = reader["PrimerApellido"].ToString(),
                        StrRegimen = reader["Regimen"].ToString(),
                        StrRegimenAC = reader["RegimenAC"].ToString(),
                        StrSegundoApellido = reader["SegundoApellido"].ToString(),
                        StrSexo = reader["Sexo"].ToString(),
                        StrTelefono = reader["Telefono"].ToString(),
                        StrTelefonoAC = reader["TelefonoAC"].ToString(),
                        StrTipoDocumento = reader["TipoDocumento"].ToString(),
                        StrTipoDocumentoAC = reader["TipoDocumentoAC"].ToString(),
                        StrTipoPoblacion = reader["TipoPoblacion"].ToString(),
                        StrLoteJeringa = reader["LoteJeringa"].ToString(),
                        DtmFechaRetgistro = reader["FechaRetgistro"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["FechaRetgistro"]),
                        StrLugarRegistro = reader["LugarRegistro"].ToString(),
                        intOidRegistrador = reader["OidRegistrador"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["OidRegistrador"]),
                        IntOidVCLoteBiologico = reader["OidVCLoteBiologico"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["OidVCLoteBiologico"]),
                        IntOidVCLoteJeringa = reader["OidVCLoteJeringa"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["OidVCLoteJeringa"]),
                        BlnEstado = Convert.ToBoolean(reader["Estado"]),
                        StrSemanasMenor = reader["SemanasMenor"].ToString(),
                        StrPesoMenor = reader["PesoMenor"].ToString(),
                        StrLugarParto = reader["LugarParto"].ToString(),
                        StrNombreLugarParto = reader["NombreLugarParto"].ToString(),
                        StrDeptoNacimiento = reader["DeptoNacimiento"].ToString(),
                        StrMunicipioNacimiento = reader["MunicipioNacimiento"].ToString()

                    };
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
            return registro;
        }

        public static bool updateRegistroDiarioVac(RegistroDiarioVac registro)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();
            bool estado = false;
            try
            {
                command = new SqlCommand(@"UPDATE [dbo].[RegistroDiarioVac]
                                               SET [FechaVacunacion] = @FechaVacunacion
                                                  ,[TipoDocumento] = @TipoDocumento
                                                  ,[Documento] = @Documento
                                                  ,[FechaNacimiento] = @FechaNacimiento
                                                  ,[Sexo] = @Sexo
                                                  ,[PrimerApellido] = @PrimerApellido
                                                  ,[SegundoApellido] = @SegundoApellido
                                                  ,[Nombres] = @Nombres
                                                  ,[Regimen] = @Regimen
                                                  ,[Eps] = @Eps
                                                  ,[DeptoResidencia] = @DeptoResidencia
                                                  ,[MunicipioResidencia] = @MunicipioResidencia
                                                  ,[AreaResidencia] = @AreaResidencia
                                                  ,[Barrio] = @Barrio
                                                  ,[Direccion] = @Direccion
                                                  ,[Telefono] = @Telefono
                                                  ,[GrupoEtnico] = @GrupoEtnico
                                                  ,[Desplazamiento] = @Desplazamiento
                                                  ,[Discapacidad] = @Discapacidad
                                                  ,[Email] = @Email
                                                  ,[Gestacion] = @Gestacion
                                                  ,[FechaProbableParto] = @FechaProbableParto
                                                  ,[EtapaVacunacion] = @EtapaVacunacion
                                                  ,[TipoPoblacion] = @TipoPoblacion
                                                  ,[Dosis] = @Dosis
                                                  ,[Biologico] = @Biologico
                                                  ,[Lote] = @Lote
                                                  ,[Jeringa] = @Jeringa
                                                  ,[NombreVacunador] = @NombreVacunador
                                                  ,[NombreIps] = @NombreIps
                                                  ,[TipoDocumentoAC] = @TipoDocumentoAC
                                                  ,[DocumentoAC] = @DocumentoAC
                                                  ,[ParentescoAC] = @ParentescoAC
                                                  ,[NombresAC] = @NombresAC
                                                  ,[RegimenAC] = @RegimenAC
                                                  ,[EpsAC] = @EpsAC
                                                  ,[GrupoEtnicoAC] = @GrupoEtnicoAC
                                                  ,[DesplazamientoAC] = @DesplazamientoAC
                                                  ,[DiscapacidadAC] = @DiscapacidadAC
                                                  ,[EmailAC] = @EmailAC
                                                  ,[TelefonoAC] = @TelefonoAC
                                                  ,[LoteJeringa] = @LoteJeringa
                                                  ,[Edad] = @Edad
                                                  ,[FechaRetgistro] = @FechaRetgistro
                                                  ,[LugarRegistro] = @LugarRegistro
                                                  ,[OidVCLoteJeringa] = @OidVCLoteJeringa
                                                  ,[OidVCLoteBiologico] = @OidVCLoteBiologico

                                                  ,[DeptoNacimiento] = @DepartamentoNacimiento
                                                  ,[MunicipioNacimiento] = @MunicipioNacimiento
                                                  ,[SemanasMenor] = @SemanasGestacionMenor
                                                  ,[PesoMenor] = @PesoAlNacerMenor
                                                  ,[LugarParto] = @LugarParto
                                                  ,[NombreLugarParto] = @NombreLugarParto
                                                
                                             WHERE OidRegistroDiarioVac = @OidRegistroDiarioVac", conexion.OpenConnection());

                command.Parameters.AddWithValue("FechaVacunacion", registro.DtmFechaVacunacion);
                command.Parameters.AddWithValue("TipoDocumento", registro.StrTipoDocumento);
                command.Parameters.AddWithValue("Documento", registro.StrDocumento);
                command.Parameters.AddWithValue("FechaNacimiento", registro.DtmFechaNacimiento);
                command.Parameters.AddWithValue("Sexo", registro.StrSexo);
                command.Parameters.AddWithValue("PrimerApellido", registro.StrPrimerApellido);
                command.Parameters.AddWithValue("SegundoApellido", registro.StrSegundoApellido);
                command.Parameters.AddWithValue("Nombres", registro.StrNombres);
                command.Parameters.AddWithValue("Regimen", registro.StrRegimen);
                command.Parameters.AddWithValue("Eps", registro.StrEps);
                command.Parameters.AddWithValue("DeptoResidencia", registro.StrDeptoResidencia);
                command.Parameters.AddWithValue("MunicipioResidencia", registro.StrMunicipioResidencia);
                command.Parameters.AddWithValue("AreaResidencia", registro.StrAreaResidencia);
                command.Parameters.AddWithValue("Barrio", registro.StrBarrio);
                command.Parameters.AddWithValue("Direccion", registro.StrDireccion);
                command.Parameters.AddWithValue("Telefono", registro.StrTelefono);
                command.Parameters.AddWithValue("GrupoEtnico", registro.StrGrupoEtnico);
                command.Parameters.AddWithValue("Desplazamiento", registro.StrDesplazamiento);
                command.Parameters.AddWithValue("Discapacidad", registro.StrDiscapacidad);
                command.Parameters.AddWithValue("Email", registro.StrEmail);
                command.Parameters.AddWithValue("Gestacion", registro.StrGestacion);
                command.Parameters.AddWithValue("FechaRetgistro", registro.DtmFechaRetgistro);
                command.Parameters.AddWithValue("LugarRegistro", registro.StrLugarRegistro);

                if (registro.StrFechaProbableParto == null)
                    command.Parameters.AddWithValue("FechaProbableParto", DBNull.Value);
                else
                    command.Parameters.AddWithValue("FechaProbableParto", registro.StrFechaProbableParto);

                command.Parameters.AddWithValue("EtapaVacunacion", registro.StrEtapaVacunacion);
                command.Parameters.AddWithValue("TipoPoblacion", registro.StrTipoPoblacion);
                command.Parameters.AddWithValue("Dosis", registro.StrDosis);
                command.Parameters.AddWithValue("Biologico", registro.StrBiologico);
                command.Parameters.AddWithValue("Lote", registro.StrLote);
                command.Parameters.AddWithValue("Jeringa", registro.StrJeringa);
                command.Parameters.AddWithValue("NombreVacunador", registro.StrNombreVacunador);
                command.Parameters.AddWithValue("NombreIps", registro.StrNombreIps);
                command.Parameters.AddWithValue("TipoDocumentoAC", registro.StrTipoDocumentoAC);
                command.Parameters.AddWithValue("DocumentoAC", registro.StrDocumentoAC);
                command.Parameters.AddWithValue("ParentescoAC", registro.StrParentescoAC);
                command.Parameters.AddWithValue("NombresAC", registro.StrNombresAC);
                command.Parameters.AddWithValue("RegimenAC", registro.StrRegimenAC);
                command.Parameters.AddWithValue("EpsAC", registro.StrEpsAC);
                command.Parameters.AddWithValue("GrupoEtnicoAC", registro.StrGrupoEtnicoAC);
                command.Parameters.AddWithValue("DesplazamientoAC", registro.StrDesplazamientoAC);
                command.Parameters.AddWithValue("DiscapacidadAC", registro.StrDiscapacidadAC);
                command.Parameters.AddWithValue("EmailAC", registro.StrEmailAC);
                command.Parameters.AddWithValue("TelefonoAC", registro.StrTelefonoAC);
                command.Parameters.AddWithValue("LoteJeringa", registro.StrLoteJeringa);
                command.Parameters.AddWithValue("OidRegistroDiarioVac", registro.IntOidRegistroDiarioVac);
                command.Parameters.AddWithValue("Edad", registro.IntEdad);
                command.Parameters.AddWithValue("OidVCLoteJeringa", registro.IntOidVCLoteJeringa);
                command.Parameters.AddWithValue("OidVCLoteBiologico", registro.IntOidVCLoteBiologico);

                command.Parameters.AddWithValue("DepartamentoNacimiento", registro.StrDeptoNacimiento);
                command.Parameters.AddWithValue("MunicipioNacimiento", registro.StrMunicipioNacimiento);
                command.Parameters.AddWithValue("SemanasGestacionMenor", registro.StrSemanasMenor);
                command.Parameters.AddWithValue("PesoAlNacerMenor", registro.StrPesoMenor);
                command.Parameters.AddWithValue("LugarParto", registro.StrLugarParto);
                command.Parameters.AddWithValue("NombreLugarParto", registro.StrNombreLugarParto);

                command.ExecuteNonQuery();

                estado = true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            finally
            {
                conexion.CloseConnection();
            }
            return estado;
        }

        public static List<RegistroDiarioVac> GetRegistrosVacunacion(DateTime fecha1, DateTime fecha2, string documento, string nombre, string biologico, string etapa, string dosis, string cantidad, string lugar)
        {
            List<RegistroDiarioVac> registros = new List<RegistroDiarioVac>();

            SqlCommand command;
            SqlDataReader reader;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand($@" SELECT {cantidad}  * FROM RegistroDiarioVac R
                                            WHERE R.FechaVacunacion >= @fecha1 AND R.FechaVacunacion <= @fecha2 AND R.Documento LIKE '%'+ @Documento +'%'
	                                            AND CONCAT(R.Nombres, ' ', R.PrimerApellido, ' ', R.SegundoApellido) LIKE '%'+ @Nombres +'%' 
                                                AND R.Biologico LIKE '%'+@Biologico+'%' AND R.EtapaVacunacion LIKE '%'+@EtapaVacunacion+'%'
                                                AND R.Dosis LIKE '%'+@Dosis+'%' AND isnull(LugarRegistro, '') like '%' + @LugarRegistro + '%'
                                                AND Estado = 1
                                            ORDER BY FechaVacunacion", conexion.OpenConnection());

                command.Parameters.AddWithValue("fecha1", fecha1);
                command.Parameters.AddWithValue("fecha2", fecha2);
                command.Parameters.AddWithValue("Documento", documento);
                command.Parameters.AddWithValue("Nombres", nombre);
                command.Parameters.AddWithValue("Biologico", biologico);
                command.Parameters.AddWithValue("EtapaVacunacion", etapa);
                command.Parameters.AddWithValue("Dosis", dosis);
                command.Parameters.AddWithValue("LugarRegistro", lugar);

                reader = command.ExecuteReader();
                while(reader.Read())
                {
                    registros.Add(new RegistroDiarioVac {
                        DtmFechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                        StrFechaProbableParto = reader["FechaProbableParto"].ToString(),
                        DtmFechaVacunacion = Convert.ToDateTime(reader["FechaVacunacion"]),
                        IntOidRegistroDiarioVac = Convert.ToInt32(reader["OidRegistroDiarioVac"]),
                        IntEdad = Convert.ToInt32(reader["Edad"]),
                        StrAreaResidencia = reader["AreaResidencia"].ToString(),
                        StrBarrio = reader["Barrio"].ToString(),
                        StrBiologico = reader["Biologico"].ToString(),
                        StrDeptoResidencia = reader["DeptoResidencia"].ToString(),
                        StrDesplazamiento = reader["Desplazamiento"].ToString(),
                        StrDesplazamientoAC = reader["DesplazamientoAC"].ToString(),
                        StrDireccion = reader["Direccion"].ToString(),
                        StrDiscapacidad = reader["Discapacidad"].ToString(),
                        StrDiscapacidadAC = reader["DiscapacidadAC"].ToString(),
                        StrDocumento = reader["Documento"].ToString(),
                        StrDocumentoAC = reader["DocumentoAC"].ToString(),
                        StrDosis = reader["Dosis"].ToString(),
                        StrEmail = reader["Email"].ToString(),
                        StrEmailAC = reader["EmailAC"].ToString(),
                        StrEps = reader["Eps"].ToString(),
                        StrEpsAC = reader["EpsAC"].ToString(),
                        StrEtapaVacunacion = reader["EtapaVacunacion"].ToString(),
                        StrGestacion = reader["Gestacion"].ToString(),
                        StrGrupoEtnico = reader["GrupoEtnico"].ToString(),
                        StrGrupoEtnicoAC = reader["GrupoEtnicoAC"].ToString(),
                        StrJeringa = reader["Jeringa"].ToString(),
                        StrLote = reader["Lote"].ToString(),
                        StrMunicipioResidencia = reader["MunicipioResidencia"].ToString(),
                        StrNombreIps = reader["NombreIps"].ToString(),
                        StrNombres = reader["Nombres"].ToString(),
                        StrNombresAC = reader["NombresAC"].ToString(),
                        StrNombreVacunador = reader["NombreVacunador"].ToString(),
                        StrParentescoAC = reader["ParentescoAC"].ToString(),
                        StrPrimerApellido = reader["PrimerApellido"].ToString(),
                        StrRegimen = reader["Regimen"].ToString(),
                        StrRegimenAC = reader["RegimenAC"].ToString(),
                        StrSegundoApellido = reader["SegundoApellido"].ToString(),
                        StrSexo = reader["Sexo"].ToString(),
                        StrTelefono = reader["Telefono"].ToString(),
                        StrTelefonoAC = reader["TelefonoAC"].ToString(),
                        StrTipoDocumento = reader["TipoDocumento"].ToString(),
                        StrTipoDocumentoAC = reader["TipoDocumentoAC"].ToString(),
                        StrTipoPoblacion = reader["TipoPoblacion"].ToString(),
                        StrLoteJeringa = reader["LoteJeringa"].ToString(),
                        DtmFechaRetgistro = reader["FechaRetgistro"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["FechaRetgistro"]),
                        StrLugarRegistro = reader["LugarRegistro"].ToString(),
                        intOidRegistrador = reader["OidRegistrador"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["OidRegistrador"]),
                        IntOidVCLoteBiologico = reader["OidVCLoteBiologico"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["OidVCLoteBiologico"]),
                        IntOidVCLoteJeringa = reader["OidVCLoteJeringa"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["OidVCLoteJeringa"]),
                        StrSemanasMenor = reader["SemanasMenor"].ToString(),
                        StrPesoMenor = reader["PesoMenor"].ToString(),
                        StrLugarParto = reader["LugarParto"].ToString(),
                        StrNombreLugarParto = reader["NombreLugarParto"].ToString(),
                        StrDeptoNacimiento = reader["DeptoNacimiento"].ToString(),
                        StrMunicipioNacimiento = reader["MunicipioNacimiento"].ToString(),

                        BlnEstado = Convert.ToBoolean(reader["Estado"]),
                        
                    });
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
            return registros;
        }
        public static List<RegistroDiarioVac> GetRegistrosByDosis(string documento, string dosis)
        {
            List<RegistroDiarioVac> registros = new List<RegistroDiarioVac>();
            SqlCommand command;
            Conexion conexion = new Conexion();
            try
            {
                command = new SqlCommand("SELECT * FROM RegistroDiarioVac WHERE Documento = @Documento AND Dosis = @Dosis AND Estado = 1", conexion.OpenConnection());
                command.Parameters.AddWithValue("Documento",documento);
                command.Parameters.AddWithValue("Dosis",dosis);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        registros.Add(new RegistroDiarioVac
                        {
                            DtmFechaNacimiento = Convert.ToDateTime(reader["FechaNacimiento"]),
                            StrFechaProbableParto = reader["FechaProbableParto"].ToString(),
                            DtmFechaVacunacion = Convert.ToDateTime(reader["FechaVacunacion"]),
                            IntOidRegistroDiarioVac = Convert.ToInt32(reader["OidRegistroDiarioVac"]),
                            IntEdad = Convert.ToInt32(reader["Edad"]),
                            StrAreaResidencia = reader["AreaResidencia"].ToString(),
                            StrBarrio = reader["Barrio"].ToString(),
                            StrBiologico = reader["Biologico"].ToString(),
                            StrDeptoResidencia = reader["DeptoResidencia"].ToString(),
                            StrDesplazamiento = reader["Desplazamiento"].ToString(),
                            StrDesplazamientoAC = reader["DesplazamientoAC"].ToString(),
                            StrDireccion = reader["Direccion"].ToString(),
                            StrDiscapacidad = reader["Discapacidad"].ToString(),
                            StrDiscapacidadAC = reader["DiscapacidadAC"].ToString(),
                            StrDocumento = reader["Documento"].ToString(),
                            StrDocumentoAC = reader["DocumentoAC"].ToString(),
                            StrDosis = reader["Dosis"].ToString(),
                            StrEmail = reader["Email"].ToString(),
                            StrEmailAC = reader["EmailAC"].ToString(),
                            StrEps = reader["Eps"].ToString(),
                            StrEpsAC = reader["EpsAC"].ToString(),
                            StrEtapaVacunacion = reader["EtapaVacunacion"].ToString(),
                            StrGestacion = reader["Gestacion"].ToString(),
                            StrGrupoEtnico = reader["GrupoEtnico"].ToString(),
                            StrGrupoEtnicoAC = reader["GrupoEtnicoAC"].ToString(),
                            StrJeringa = reader["Jeringa"].ToString(),
                            StrLote = reader["Lote"].ToString(),
                            StrMunicipioResidencia = reader["MunicipioResidencia"].ToString(),
                            StrNombreIps = reader["NombreIps"].ToString(),
                            StrNombres = reader["Nombres"].ToString(),
                            StrNombresAC = reader["NombresAC"].ToString(),
                            StrNombreVacunador = reader["NombreVacunador"].ToString(),
                            StrParentescoAC = reader["ParentescoAC"].ToString(),
                            StrPrimerApellido = reader["PrimerApellido"].ToString(),
                            StrRegimen = reader["Regimen"].ToString(),
                            StrRegimenAC = reader["RegimenAC"].ToString(),
                            StrSegundoApellido = reader["SegundoApellido"].ToString(),
                            StrSexo = reader["Sexo"].ToString(),
                            StrTelefono = reader["Telefono"].ToString(),
                            StrTelefonoAC = reader["TelefonoAC"].ToString(),
                            StrTipoDocumento = reader["TipoDocumento"].ToString(),
                            StrTipoDocumentoAC = reader["TipoDocumentoAC"].ToString(),
                            StrTipoPoblacion = reader["TipoPoblacion"].ToString(),
                            StrLoteJeringa = reader["LoteJeringa"].ToString(),
                            DtmFechaRetgistro = reader["FechaRetgistro"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["FechaRetgistro"]),
                            StrLugarRegistro = reader["LugarRegistro"].ToString(),
                            intOidRegistrador = reader["OidRegistrador"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["OidRegistrador"]),
                            IntOidVCLoteBiologico = reader["OidVCLoteBiologico"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["OidVCLoteBiologico"]),
                            IntOidVCLoteJeringa = reader["OidVCLoteJeringa"] == DBNull.Value ? (int?)null : Convert.ToInt32(reader["OidVCLoteJeringa"]),
                            StrSemanasMenor = reader["SemanasMenor"].ToString(),
                            StrPesoMenor = reader["PesoMenor"].ToString(),
                            StrLugarParto = reader["LugarParto"].ToString(),
                            StrNombreLugarParto = reader["NombreLugarParto"].ToString(),
                            StrDeptoNacimiento = reader["DeptoNacimiento"].ToString(),
                            StrMunicipioNacimiento = reader["MunicipioNacimiento"].ToString(),
                            BlnEstado = Convert.ToBoolean(reader["Estado"]),
                            
                        });
                    }
                }
            }
            catch (Exception)
            {
               
            }
            finally
            {
                conexion.CloseConnection();
            }
            return registros;
        }
        public static bool DeleteRegistro(int idRegistro, string motivo)
        {
            SqlCommand command;
            Conexion conexion = new Conexion();

            try
            {
                command = new SqlCommand("UPDATE RegistroDiarioVac SET Estado = 0, MotivoEliminacion = @motivo WHERE OidRegistroDiarioVac = @OidRegistroDiarioVac", conexion.OpenConnection());
                command.Parameters.AddWithValue("OidRegistroDiarioVac", idRegistro);
                command.Parameters.AddWithValue("motivo", motivo); ;
                command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conexion.CloseConnection();
            }
        }
    }
}