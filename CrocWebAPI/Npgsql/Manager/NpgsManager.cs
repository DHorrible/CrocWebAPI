using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocWebAPI.Npgsql.Manager
{
    public static class NpgsManager
    {
        #region Properties

        public static string Host { get; } = "localhost";
        public static string Database { get; } = "croc";
        public static string Username { get; } = "postgres";
        public static string Password { get; } = "02042000";
        public static string ConnectionString { get; } = $"Host={Host};Database={Database};Username={Username};Password={Password};";

        #endregion

        #region Fields

        private static NpgsqlConnection npgsqlConnection;

        #endregion

        #region Public methods

        public static bool Connection()
        {
            npgsqlConnection = new NpgsqlConnection(ConnectionString);
            
            return npgsqlConnection != null;
        }

        public static bool Open()
        {
            if (npgsqlConnection == null)
            {
                return false;
            }

            try
            {
                npgsqlConnection.Open();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool Close()
        {
            if (npgsqlConnection == null)
            {
                return false;
            }

            try
            {
                npgsqlConnection.Close();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static NpgsqlCommand SetCommand(string query)
        {
            return new NpgsqlCommand(query, npgsqlConnection);
        }

        #endregion
    }
}
