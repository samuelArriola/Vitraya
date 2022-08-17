using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia
{
    public class CloseAllConnections
    {
        public void closeallconnections()
        {
            Conexion con = new Conexion();
            SqlCommand borrar;

            try
            {
                borrar = new SqlCommand("use eliminar select @@spid DECLARE @nameBD VARCHAR(100) SET @nameBD = 'eliminar' DECLARE @sql VARCHAR(500) SET @sql = '' select @sql = @sql + ' KILL ' + CAST(spid AS VARCHAR(4)) + '' FROM DBO.sysprocesses WHERE DB_NAME(dbid) = @nameBD AND spid > 50 select @sql EXEC(@sql)", con.OpenConnection());
                borrar.ExecuteNonQuery();

            }
            catch (Exception)
            {
            }

            con.CloseConnection();
        }
    }
}