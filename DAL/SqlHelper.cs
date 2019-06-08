using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DAL
{
    /// <summary>
    /// Class SqlHelper.
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <returns>System.String.</returns>
        /// <exception cref="System.Exception">You must specify connectionString name in the Web.Config</exception>
        public string GetConnectionString()
        {
            return GetConnectionString(DbConstants.ConnectionString);
        }
        public string GetConnectionString(string connectionStringName)
        {
            string conString = ConfigurationManager.AppSettings[connectionStringName];
            if (string.IsNullOrEmpty(conString))
            {
                var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName];
                conString = connectionString.ConnectionString;
                if (string.IsNullOrEmpty(conString))
                {
                    throw new Exception("You must specify connection string name in the Web.Config");
                }
            }
            return conString;
        }
        #region Gets an instance of SQL Command object
        /// <summary>
        /// Gets an instance of sql command object.
        /// </summary>
        /// <param name="commandText">The sql command text.</param>
        /// <param name="sqlConnection">The sql connection.</param>
        /// <param name="isStoredProcedure">if set to true is stored procedure.</param>
        /// <returns>Return object of sql command class.</returns>
        public SqlCommand GetSqlCommand(string commandText, SqlConnection sqlConnection, bool isStoredProcedure)
        {
            var cmdText = !string.IsNullOrEmpty(DbConstants.DatabaseObjectOwner)
                                 ? (isStoredProcedure
                                        ? string.Format("{0}.{1}", DbConstants.DatabaseObjectOwner, commandText)
                                        : commandText)
                                 : commandText;

            var sqlCommand = new SqlCommand(cmdText, sqlConnection)
            {
                CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text
            };
            return sqlCommand;
        }

        #endregion
    }//end class

}//end namespace