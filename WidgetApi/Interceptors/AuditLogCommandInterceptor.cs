using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data;
using System.Data.Common;

namespace WidgetApi.Interceptors
{
    public class AuditLogCommandInterceptor : DbCommandInterceptor
    {
        const string objectName = "WidgetApi EFCore AuditLogCommandInterceptor";

        /// <summary>
        /// Write to audit log for every command executed
        /// </summary>
        /// <param name="command"></param>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
        {
            // ignore stored procedure calls
            if (command.CommandType == CommandType.StoredProcedure)
            {
                return result;
            }

            const string procedureName = "Logging.LogAudit";
            if (command.Connection != null)
            {
                using var cn = new SqlConnection(command.Connection.ConnectionString);
                var cm = new SqlCommand(procedureName, cn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                cm.Parameters.Add("@ObjectName", SqlDbType.NVarChar, 500).Value = objectName;
                cm.Parameters.Add("@ParametersPassed", SqlDbType.NVarChar, -1).Value = command.CommandText;
                cm.Parameters.Add("@UserId", SqlDbType.BigInt).Value = 999;
                cm.Parameters.Add("SQLAuditLogID", SqlDbType.BigInt).Direction = ParameterDirection.Output;

                try
                {
                    cn.Open();
                    cm.ExecuteNonQuery();
                }
                finally
                {
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
            }

            return result;
        }
    }
}
