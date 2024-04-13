using System.Data.SqlClient;

namespace ExpenseManagerAPI.Settings
{
    public sealed class DBHelper
    {
        private IConfiguration _configuration;
        private readonly string _connectionString;
        public DBHelper(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._connectionString = configuration.GetValue<string>("Database:ConnectionString");
        }

        public SqlConnection GetSqlConnection()
        {
            return new SqlConnection(this._connectionString);
        }
    }
}
