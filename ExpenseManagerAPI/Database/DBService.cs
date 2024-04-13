using ExpenseManagerAPI.Settings;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagerAPI.Database
{
    public class DBService : IDBService
    {
        private readonly DBHelper _dbHelper;
        public DBService(DBHelper dbHelper)
        {
            this._dbHelper = dbHelper;
        }

        public void ExecuteNonQueryStoredProcedure(string sqlQuery, List<IDbDataParameter> sqlParams)
        {
            using (SqlConnection sqlConnection = this._dbHelper.GetSqlConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                
                foreach(IDbDataParameter param in sqlParams)
                {
                    sqlCommand.Parameters.Add(param);
                }
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public DataSet LoadDataSet(string sqlQuery, List<IDbDataParameter> sqlParams)
        {
            DataSet dataSet = new DataSet();
            using (SqlConnection sqlConnection = this._dbHelper.GetSqlConnection())
            {
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                foreach (IDbDataParameter param in sqlParams)
                {
                    sqlCommand.Parameters.Add(param);
                }
                sqlConnection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataSet);
                sqlConnection.Close();
            }
            return dataSet;
        }
        /*
private Dictionary<string, DateTime> processList = new Dictionary<string, DateTime>();

private Guid currentSession;

private List<string> processToBeLoggedList = new List<string>();

private static readonly object locker = new object();

private IDbConnection connection;

private IDbTransaction transaction;

private DbProviderFactory factory;

private Providers provider;

private string dbName = "DBName";

private bool isOutputParameterExist;

public IDbConnection DBConnection
{
   get
   {
       return connection;
   }
   set
   {
       connection = value;
   }
}

public string DBName
{
   get
   {
       return dbName;
   }
   set
   {
       dbName = value;
   }
}

public void CreateDBObjects(Providers providerHandle)
{
   // StartTimer("CreateDBObjects");
   try
   {
       if (factory == null)
       {
           provider = Providers.SqlServer;
           factory = SqlClientFactory.Instance;
       }
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer("CreateDBObjects");
   }
}

public IDbConnection CreateConnection()
{
   // StartTimer("CreateConnection");
   try
   {
       CreateDBObjects(provider);
       connection = factory.CreateConnection();
       connection.ConnectionString = DBConnectionString;
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer("CreateConnection");
   }

   return connection;
}

public void OpenConnection()
{
   // StartTimer("OpenConnection");
   try
   {
       if (connection == null)
       {
           if (string.IsNullOrEmpty(DBConnectionString) || string.IsNullOrWhiteSpace(DBConnectionString))
           {
               GetConnectionString(DBName);
           }

           connection = CreateConnection();
       }

       if (connection.State != ConnectionState.Open)
       {
           connection.Open();
       }
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer("OpenConnection");
   }
}

public void CloseConnection()
{
   // StartTimer("CloseConnection");
   try
   {
       if (connection == null)
       {
           return;
       }

       if (connection.State != 0)
       {
           if (transaction != null)
           {
               RollbackTransaction(throwError: false);
           }

           connection.Close();
       }

       connection.Dispose();
       connection = null;
       GC.Collect(GC.MaxGeneration);
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer("CloseConnection");
   }
}

public void BeginTransaction(bool throwError)
{
   string text = "";
   // StartTimer("BeginTransaction");
   try
   {
       if (connection != null)
       {
           if (connection.State != 0)
           {
               if (transaction == null)
               {
                   transaction = connection.BeginTransaction();
               }
           }
           else
           {
               text = DBConstants.DatabaseErrors.OPEN_CONNECTION;
           }
       }
       else
       {
           text = DBConstants.DatabaseErrors.OPEN_CONNECTION;
       }
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer("BeginTransaction");
   }

   if (throwError && !string.IsNullOrEmpty(text))
   {
       throw new Exception(text);
   }
}

public void BeginTransaction()
{
   BeginTransaction(throwError: true);
}

public void CommitTransaction(bool throwError)
{
   string text = "";
   // StartTimer("CommitTransaction");
   try
   {
       if (connection != null)
       {
           if (connection.State != 0)
           {
               if (transaction != null)
               {
                   transaction.Commit();
                   transaction = null;
               }
               else
               {
                   text = DBConstants.DatabaseErrors.START_TRANSACTION_COMMIT;
               }
           }
           else
           {
               text = DBConstants.DatabaseErrors.OPEN_CONNECTION;
           }
       }
       else
       {
           text = DBConstants.DatabaseErrors.OPEN_CONNECTION;
       }
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer("CommitTransaction");
   }

   if (throwError && !string.IsNullOrEmpty(text))
   {
       throw new Exception(text);
   }
}

public void CommitTransaction()
{
   CommitTransaction(throwError: true);
}

public void RollbackTransaction(bool throwError)
{
   string text = "";
   // StartTimer("RollbackTransaction");
   try
   {
       if (connection != null)
       {
           if (connection.State != 0)
           {
               if (transaction != null)
               {
                   transaction.Rollback();
                   transaction.Dispose();
                   transaction = null;
               }
               else
               {
                   text = DBConstants.DatabaseErrors.START_TRANSACTION_REVERT;
               }
           }
           else
           {
               text = DBConstants.DatabaseErrors.OPEN_CONNECTION;
           }
       }
       else
       {
           text = DBConstants.DatabaseErrors.OPEN_CONNECTION;
       }
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer("RollbackTransaction");
   }

   if (throwError && !string.IsNullOrEmpty(text))
   {
       throw new Exception(text);
   }
}

public void RollbackTransaction()
{
   RollbackTransaction(throwError: true);
}

public object ExecuteScalar(string sqlQuery, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams)
{
   DbCommand dbCommand = null;
   // StartTimer("Execute Scaler");
   try
   {
       using (dbCommand = GetDBCommand(sqlQuery, queryCommandType, sqlParams))
       {
           // StartTimer($"\"Executing Query : {sqlQuery}\"");
           object result = dbCommand.ExecuteScalar();
           // EndTimer($"\"Executing Query : {sqlQuery}\"");
           GetParametersOutputValues(dbCommand, sqlParams);
           return result;
       }
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer($"\"Executing Query : {sqlQuery}\"");
       // EndTimer("Execute Scaler");
   }
}

public object ExecuteScalar(string sqlQuery)
{
   return ExecuteScalar(sqlQuery, QueryCommandType.Text, null);
}

public object ExecuteScalar(string sqlQuery, QueryCommandType queryCommandType)
{
   return ExecuteScalar(sqlQuery, queryCommandType, null);
}

public object ExecuteScalar(string sqlQuery, List<IDbDataParameter> sqlParams)
{
   return ExecuteScalar(sqlQuery, QueryCommandType.Text, sqlParams);
}

public int ExecuteNonQuery(string sqlQuery, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams)
{
   DbCommand dbCommand = null;
   // StartTimer("Execute Non Query");
   try
   {
       using (dbCommand = GetDBCommand($"{AppSettings.SchemaName}{sqlQuery}", queryCommandType, sqlParams))
       {
           // StartTimer($"\"Executing Query : {sqlQuery}\"");
           int result = dbCommand.ExecuteNonQuery();
           // EndTimer($"\"Executing Query : {sqlQuery}\"");
           GetParametersOutputValues(dbCommand, sqlParams);
           return result;
       }
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer($"\"Executing Query : {sqlQuery}\"");
       // EndTimer("Execute Non Query");
   }
}

public int ExecuteNonQuery(string sqlQuery)
{
   return ExecuteNonQuery(sqlQuery, QueryCommandType.Text, null);
}

public int ExecuteNonQuery(string sqlQuery, QueryCommandType queryCommandType)
{
   return ExecuteNonQuery(sqlQuery, queryCommandType, null);
}

public int ExecuteNonQuery(string sqlQuery, List<IDbDataParameter> sqlParams)
{
   return ExecuteNonQuery(sqlQuery, QueryCommandType.Text, sqlParams);
}

public IDataReader ExecuteReader(string sqlQuery, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams)
{
   DbCommand dbCommand = null;
   // StartTimer("Execute Reader");
   try
   {
       using (dbCommand = GetDBCommand($"{AppSettings.SchemaName}{sqlQuery}", queryCommandType, sqlParams))
       {
           // StartTimer($"\"Executing Query : {sqlQuery}\"");
           IDataReader result = dbCommand.ExecuteReader();
           // EndTimer($"\"Executing Query : {sqlQuery}\"");
           GetParametersOutputValues(dbCommand, sqlParams);
           return result;
       }
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer($"\"Executing Query : {sqlQuery}\"");
       // // EndTimer("Execute Reader");
   }
}

public IDataReader ExecuteReader(string sqlQuery)
{
   return ExecuteReader(sqlQuery, QueryCommandType.Text, null);
}

public IDataReader ExecuteReader(string sqlQuery, QueryCommandType queryCommandType)
{
   return ExecuteReader(sqlQuery, queryCommandType, null);
}

public IDataReader ExecuteReader(string sqlQuery, List<IDbDataParameter> sqlParams)
{
   return ExecuteReader(sqlQuery, QueryCommandType.Text, sqlParams);
}

public DataSet LoadDataSet(string sqlQuery, List<string> tableNames, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams)
{
   DataSet dataSet = new DataSet();
   DbCommand dbCommand = null;
   // StartTimer("Load DataSet");
   try
   {
       using (dbCommand = GetDBCommand($"{AppSettings.SchemaName}{sqlQuery}", queryCommandType, sqlParams))
       {
           DbDataAdapter dataAdapter = GetDataAdapter(dbCommand);
           // StartTimer($"\"Executing Query : {sqlQuery}\"");
           dataAdapter.Fill(dataSet);
           // EndTimer($"\"Executing Query : {sqlQuery}\"");
           for (int i = 0; i < tableNames.Count(); i++)
           {
               if (!string.IsNullOrEmpty(tableNames[i]) && !string.IsNullOrWhiteSpace(tableNames[i]) && i < dataSet.Tables.Count)
               {
                   dataSet.Tables[i].TableName = tableNames[i];
               }
           }

           GetParametersOutputValues(dbCommand, sqlParams);
           return dataSet;
       }
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer($"\"Executing Query : {sqlQuery}\"");
       // EndTimer("Load DataSet");
   }
}

public DataSet LoadDataSet(string sqlQuery, List<string> tableNames)
{
   return LoadDataSet(sqlQuery, tableNames, QueryCommandType.Text, null);
}

public DataSet LoadDataSet(string sqlQuery, List<string> tableNames, QueryCommandType queryCommandType)
{
   return LoadDataSet(sqlQuery, tableNames, queryCommandType, null);
}

public DataSet LoadDataSet(string sqlQuery, List<string> tableNames, List<IDbDataParameter> sqlParams)
{
   return LoadDataSet(sqlQuery, tableNames, QueryCommandType.Text, sqlParams);
}

public DataSet LoadDataSet(string sqlQuery)
{
   return LoadDataSet(sqlQuery, "", QueryCommandType.Text, null);
}

public DataSet LoadDataSet(string sqlQuery, string tableName)
{
   return LoadDataSet(sqlQuery, tableName, QueryCommandType.Text, null);
}

public DataSet LoadDataSet(string sqlQuery, string tableName, QueryCommandType queryCommandType)
{
   return LoadDataSet(sqlQuery, tableName, queryCommandType, null);
}

public DataSet LoadDataSet(string sqlQuery, QueryCommandType queryCommandType)
{
   return LoadDataSet(sqlQuery, "", queryCommandType, null);
}

public DataSet LoadDataSet(string sqlQuery, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams)
{
   return LoadDataSet(sqlQuery, "", queryCommandType, sqlParams);
}

public DataSet LoadDataSet(string sqlQuery, List<IDbDataParameter> sqlParams)
{
   return LoadDataSet(sqlQuery, "", QueryCommandType.Text, sqlParams);
}

public DataSet LoadDataSet(string sqlQuery, string tableName, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams)
{
   if (string.IsNullOrEmpty(tableName) || string.IsNullOrWhiteSpace(tableName))
   {
       tableName = "DataTable1";
   }

   List<string> list = new List<string>();
   list.Add(tableName);
   return LoadDataSet(sqlQuery, list, queryCommandType, sqlParams);
}

public int UpdateDataSet(DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand)
{
   CreateDBObjects(provider);
   DbDataAdapter dbDataAdapter = factory.CreateDataAdapter();
   dbDataAdapter.InsertCommand = insertCommand;
   dbDataAdapter.UpdateCommand = updateCommand;
   dbDataAdapter.DeleteCommand = deleteCommand;
   return dbDataAdapter.Update(dataSet);
}

public int UpdateDataSet(DataSet dataSet, string tableName, string insertSqlQuery, string updateSqlQuery, string deleteSqlQuery, QueryCommandType insertQueryCommandType, QueryCommandType updateQueryCommandType, QueryCommandType deleteQueryCommandType, List<IDbDataParameter> insertSqlParams, List<IDbDataParameter> updateSqlParams, List<IDbDataParameter> deleteSqlParams)
{
   DbCommand dBCommand = GetDBCommand(insertSqlQuery, insertQueryCommandType, insertSqlParams);
   DbCommand dBCommand2 = GetDBCommand(updateSqlQuery, updateQueryCommandType, updateSqlParams);
   DbCommand dBCommand3 = GetDBCommand(deleteSqlQuery, deleteQueryCommandType, deleteSqlParams);
   return UpdateDataSet(dataSet, tableName, dBCommand, dBCommand2, dBCommand3);
}

public IDbDataParameter GetSingleParameter(string parameterName, ParameterType parameterType, int size, ParameterDirection parameterDirection, bool isNullAble, byte precision, byte scale, string sourceColumn, DataRowVersion dataRowVersion, object parameterValue)
{
   CreateDBObjects(provider);
   DbParameter dbParameter = factory.CreateParameter();
   dbParameter.ParameterName = parameterName;
   dbParameter.Size = size;
   dbParameter.Direction = parameterDirection;
   dbParameter.SourceColumnNullMapping = isNullAble;
   dbParameter.Value = parameterValue;
   dbParameter.SourceVersion = dataRowVersion;
   dbParameter.SourceColumn = sourceColumn;
   dbParameter.DbType = (DbType)parameterType;
   return dbParameter;
}

public IDbDataParameter GetSingleParameter(string parameterName, object parameterValue)
{
   return GetSingleParameter(parameterName, ParameterType.NVarchar2, 0, ParameterDirection.Input, isNullAble: true, 0, 0, "", DataRowVersion.Default, parameterValue);
}

public IDbDataParameter GetSingleParameter(string parameterName, ParameterType parameterType, object parameterValue)
{
   return GetSingleParameter(parameterName, parameterType, 0, ParameterDirection.Input, isNullAble: true, 0, 0, "", DataRowVersion.Default, parameterValue);
}

public IDbDataParameter GetSingleParameter(string parameterName, ParameterType parameterType, int size, object parameterValue)
{
   return GetSingleParameter(parameterName, parameterType, size, ParameterDirection.Input, isNullAble: true, 0, 0, "", DataRowVersion.Default, parameterValue);
}

public IDbDataParameter GetSingleParameter(string parameterName, ParameterType parameterType, ParameterDirection parameterDirection, object parameterValue)
{
   return GetSingleParameter(parameterName, parameterType, 0, parameterDirection, isNullAble: true, 0, 0, "", DataRowVersion.Default, parameterValue);
}

public IDbDataParameter GetSingleParameter(string parameterName, ParameterType parameterType, int size, string sourceColumn)
{
   return GetSingleParameter(parameterName, parameterType, size, ParameterDirection.Input, isNullAble: true, 0, 0, sourceColumn, DataRowVersion.Default, null);
}

public IDbDataParameter GetSingleParameter(string parameterName, ParameterType parameterType, int size, ParameterDirection parameterDirection, object parameterValue)
{
   return GetSingleParameter(parameterName, parameterType, size, ParameterDirection.Input, isNullAble: true, 0, 0, "", DataRowVersion.Default, parameterValue);
}

public void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, ParameterType parameterType, int size, ParameterDirection parameterDirection, bool isNullAble, byte precision, byte scale, string sourceColumn, DataRowVersion dataRowVersion, object parameterValue)
{
   sqlParams.Add(GetSingleParameter(parameterName, parameterType, size, parameterDirection, isNullAble, precision, scale, sourceColumn, dataRowVersion, parameterValue));
}

public void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, object parameterValue)
{
   AddSingleParameter(sqlParams, parameterName, ParameterType.NVarchar2, 0, ParameterDirection.Input, isNullAble: true, 0, 0, "", DataRowVersion.Default, parameterValue);
}

public void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, ParameterType parameterType, object parameterValue)
{
   AddSingleParameter(sqlParams, parameterName, parameterType, 0, ParameterDirection.Input, isNullAble: true, 0, 0, "", DataRowVersion.Default, parameterValue);
}

public void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, ParameterType parameterType, int size, object parameterValue)
{
   AddSingleParameter(sqlParams, parameterName, parameterType, size, ParameterDirection.Input, isNullAble: true, 0, 0, "", DataRowVersion.Default, parameterValue);
}

public void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, ParameterType parameterType, ParameterDirection parameterDirection, object parameterValue)
{
   AddSingleParameter(sqlParams, parameterName, parameterType, 0, parameterDirection, isNullAble: true, 0, 0, "", DataRowVersion.Default, parameterValue);
}

public void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, ParameterType parameterType, int size, string sourceColumn)
{
   AddSingleParameter(sqlParams, parameterName, parameterType, size, ParameterDirection.Input, isNullAble: true, 0, 0, sourceColumn, DataRowVersion.Default, null);
}

public void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, ParameterType parameterType, int size, ParameterDirection parameterDirection, object parameterValue)
{
   AddSingleParameter(sqlParams, parameterName, parameterType, size, parameterDirection, isNullAble: true, 0, 0, "", DataRowVersion.Default, parameterValue);
}

public void Dispose()
{
   CloseConnection();
   GC.SuppressFinalize(this);
}

private DbParameter GetXmlParameter(string xml)
{
   CreateDBObjects(provider);
   DbParameter dbParameter = factory.CreateParameter();
   dbParameter.DbType = DbType.Xml;
   dbParameter.Value = xml;
   return dbParameter;
}

private DbCommand CreateCommand(string query, IDbConnection connection)
{
   return new SqlCommand(query, (SqlConnection)connection);
}

private DbCommand GetDBCommand(string sqlQuery, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams)
{
   DbCommand dbCommand = null;
   // StartTimer($"\"Get DB Command : {sqlQuery}\"");
   try
   {
       CreateDBObjects(provider);
       OpenConnection();
       dbCommand = CreateCommand(sqlQuery, connection);
       switch (queryCommandType)
       {
           case QueryCommandType.Text:
               dbCommand.CommandType = CommandType.Text;
               break;
           case QueryCommandType.StoredProcedure:
               dbCommand.CommandType = CommandType.StoredProcedure;
               break;
       }

       // dbCommand.CommandTimeout = AppSettings.DBCommandTimeOut;
       if (transaction != null)
       {
           dbCommand.Transaction = (DbTransaction)transaction;
       }

       isOutputParameterExist = false;
       if (sqlParams != null)
       {
           int num = 0;
           {
               foreach (IDbDataParameter sqlParam in sqlParams)
               {
                   _ = sqlParam;
                   DbParameter dbParameter = (DbParameter)((ICloneable)sqlParams[num]).Clone();
                   if (!isOutputParameterExist && (dbParameter.Direction == ParameterDirection.Output || dbParameter.Direction == ParameterDirection.InputOutput || dbParameter.Direction == ParameterDirection.ReturnValue))
                   {
                       isOutputParameterExist = true;
                   }

                   if (dbParameter.DbType == DbType.Xml && dbParameter.Direction != ParameterDirection.Output && dbParameter.Direction != ParameterDirection.ReturnValue)
                   {
                       string text = dbParameter.Value.ToString();
                       if (!string.IsNullOrEmpty(text))
                       {
                           dbParameter.Value = GetXmlParameter(text);
                       }
                   }

                   dbCommand.Parameters.Add(dbParameter);
                   num++;
               }

               return dbCommand;
           }
       }

       return dbCommand;
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer($"\"Get DB Command : {sqlQuery}\"");
   }
}

private void GetParametersOutputValues(DbCommand cmd, List<IDbDataParameter> sqlParams)
{
   if (!isOutputParameterExist)
   {
       return;
   }

   int num = 0;
   foreach (IDbDataParameter parameter in cmd.Parameters)
   {
       if (parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Output || parameter.Direction == ParameterDirection.ReturnValue)
       {
           if (parameter.DbType == DbType.String)
           {
               sqlParams[num].Value = Convert.ToString(parameter.Value);
           }
           else
           {
               sqlParams[num].Value = parameter.Value;
           }
       }

       num++;
   }
}

private void GetConnectionString(string dbAppSettingName, string defaultValue = "")
{
   // StartTimer("GetConnectionString");
   try
   {
       if (string.IsNullOrWhiteSpace(DBConnectionString))
       {
           if (string.IsNullOrWhiteSpace(dbAppSettingName) && string.IsNullOrWhiteSpace(defaultValue))
           {
               throw new ArgumentNullException("Please provide detail for Database that store application data");
           }

           if (!string.IsNullOrWhiteSpace(dbAppSettingName) && !string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[dbAppSettingName]))
           {
               defaultValue = ConfigurationManager.AppSettings[dbAppSettingName];
           }

           if (CacheHelper.Instance.IsExists(defaultValue))
           {
               DBConnectionString = CacheHelper.Instance.Get<string>(defaultValue);
               return;
           }

           // StartTimer("GettingDBConnectionString");
           DBConnectionString = (AppSettings.IsDBConnEncrypted ? XCUEncryption.DecryptText(defaultValue, (string)null) : defaultValue);
           CacheHelper.Instance.Add(defaultValue, DBConnectionString, AppSettings.DBConnectionStringCacheDuration);
           // EndTimer("GettingDBConnectionString");
       }
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer("GettingDBConnectionString");
       // EndTimer("GetConnectionString");
   }
}

private DbDataAdapter GetDataAdapter(DbCommand command)
{
   return new SqlDataAdapter((SqlCommand)command);
}
/*
private void // StartTimer(string processName)
{
   if (!string.IsNullOrWhiteSpace(AppSettings.DBRecordProcessTimingLocation) && CheckProcessToBeLogged(processName) && !processList.ContainsKey(processName))
   {
       processList.Add(processName, DateTime.Now);
   }
}

private void EndTimer(string processName)
{
   if (!string.IsNullOrWhiteSpace(AppSettings.DBRecordProcessTimingLocation) && CheckProcessToBeLogged(processName) && processList.ContainsKey(processName))
   {
       _ = currentSession;
       if (!string.IsNullOrWhiteSpace(processName))
       {
           DateTime now = DateTime.Now;
           DateTime dateTime = processList[processName];
           processList.Remove(processName);
           WriteAsyncTextAsync(AppSettings.DBRecordProcessTimingLocation, $"{currentSession},{processName},{dateTime},{now},{(now - dateTime).TotalMilliseconds}{Environment.NewLine}");
       }
   }
}

private void WriteAsyncTextAsync(string path, string content)
{
   try
   {
       if (!Directory.Exists(path))
       {
           Directory.CreateDirectory(path);
       }

       string path2 = string.Format("{0}{1}{2}{3}{4}", path, path.EndsWith("\\") ? string.Empty : "\\", "dbExecutionLog", DateTime.Now.ToString("yyyyMMdd"), ".CSV");
       if (!File.Exists(path2))
       {
           content = "Session ID, Process Name, Start Time, End Time, Total Time Taken (ms)" + Environment.NewLine + content;
       }

       byte[] bytes = Encoding.Unicode.GetBytes(content);
       lock (locker)
       {
           using FileStream fileStream = new FileStream(path2, FileMode.Append, FileAccess.Write, FileShare.Read, 4096, useAsync: true);
           fileStream.WriteAsync(bytes, 0, bytes.Length);
       }
   }
   catch (Exception exception)
   {
       LoggingService.Instance.Error(exception, "C:\\SourceFolder\\ARTool\\XCU.Common.DBHelper\\XCUDBBase.cs", "WriteAsyncTextAsync", 1340);
   }
}

private void GetProcessToBeLogged(string processToBeLogged)
{
   if (!processToBeLogged.Equals("ALL", StringComparison.OrdinalIgnoreCase))
   {
       if (string.IsNullOrWhiteSpace(processToBeLogged))
       {
           processToBeLoggedList.Add("Executing Query");
           return;
       }

       processToBeLoggedList = (from res in processToBeLogged.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                select res.Trim()).ToList();
   }
   else
   {
       processToBeLoggedList = new List<string>();
   }
}

private bool CheckProcessToBeLogged(string processName)
{
   if (processToBeLoggedList.Any())
   {
       string procName = ((processName.IndexOf(":") < 0) ? processName : (processName.StartsWith("\"Executing Query", StringComparison.OrdinalIgnoreCase) ? "Executing Query" : "Get DB Command")).Trim();
       return processToBeLoggedList.Any((string res) => res.Equals(procName, StringComparison.OrdinalIgnoreCase));
   }

   return true;
}

public async Task<object> ExecuteScalarAsync(string sqlQuery, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams)
{
   // StartTimer("Execute Scaler");
   try
   {
       DbCommand dBCommand;
       DbCommand cmd = (dBCommand = GetDBCommand(sqlQuery, queryCommandType, sqlParams));
       object result;
       using (dBCommand)
       {
           // StartTimer($"Executing Query : {sqlQuery}");
           result = await cmd.ExecuteScalarAsync();
           // EndTimer($"Executing Query : {sqlQuery}");
           GetParametersOutputValues(cmd, sqlParams);
       }

       return result;
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer($"Executing Query : {sqlQuery}");
       // EndTimer("Execute Scaler");
   }
}

public async Task<object> ExecuteScalarAsync(string sqlQuery)
{
   return await ExecuteScalarAsync(sqlQuery, QueryCommandType.Text, null);
}

public async Task<object> ExecuteScalarAsync(string sqlQuery, QueryCommandType queryCommandType)
{
   return await ExecuteScalarAsync(sqlQuery, queryCommandType, null);
}

public async Task<object> ExecuteScalarAsync(string sqlQuery, List<IDbDataParameter> sqlParams)
{
   return await ExecuteScalarAsync(sqlQuery, QueryCommandType.Text, sqlParams);
}

public async Task<int> ExecuteNonQueryAsync(string sqlQuery, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams)
{
   // StartTimer("Execute Non Query");
   try
   {
       DbCommand dBCommand;
       DbCommand cmd = (dBCommand = GetDBCommand($"{AppSettings.SchemaName}{sqlQuery}", queryCommandType, sqlParams));
       int result;
       using (dBCommand)
       {
           // StartTimer($"Executing Query : {sqlQuery}");
           result = await cmd.ExecuteNonQueryAsync();
           // EndTimer($"Executing Query : {sqlQuery}");
           GetParametersOutputValues(cmd, sqlParams);
       }

       return result;
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer($"Executing Query : {sqlQuery}");
       // EndTimer("Execute Non Query");
   }
}

public async Task<int> ExecuteNonQueryAsync(string sqlQuery)
{
   return await ExecuteNonQueryAsync(sqlQuery, QueryCommandType.Text, null);
}

public async Task<int> ExecuteNonQueryAsync(string sqlQuery, QueryCommandType queryCommandType)
{
   return await ExecuteNonQueryAsync(sqlQuery, queryCommandType, null);
}

public async Task<int> ExecuteNonQueryAsync(string sqlQuery, List<IDbDataParameter> sqlParams)
{
   return await ExecuteNonQueryAsync(sqlQuery, QueryCommandType.Text, sqlParams);
}

public async Task<IDataReader> ExecuteReaderAsync(string sqlQuery, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams)
{
   // StartTimer("Execute Reader");
   try
   {
       DbCommand dBCommand;
       DbCommand cmd = (dBCommand = GetDBCommand($"{AppSettings.SchemaName}{sqlQuery}", queryCommandType, sqlParams));
       IDataReader result;
       using (dBCommand)
       {
           // StartTimer($"Executing Query : {sqlQuery}");
           result = await cmd.ExecuteReaderAsync();
           // EndTimer($"Executing Query : {sqlQuery}");
           GetParametersOutputValues(cmd, sqlParams);
       }

       return result;
   }
   catch
   {
       throw;
   }
   finally
   {
       // EndTimer($"Executing Query : {sqlQuery}");
       // EndTimer("Execute Reader");
   }
}

public async Task<IDataReader> ExecuteReaderAsync(string sqlQuery)
{
   return await ExecuteReaderAsync(sqlQuery, QueryCommandType.Text, null);
}

public async Task<IDataReader> ExecuteReaderAsync(string sqlQuery, QueryCommandType queryCommandType)
{
   return await ExecuteReaderAsync(sqlQuery, queryCommandType, null);
}

public async Task<IDataReader> ExecuteReaderAsync(string sqlQuery, List<IDbDataParameter> sqlParams)
{
   return await ExecuteReaderAsync(sqlQuery, QueryCommandType.Text, sqlParams);
}
*/
    }
}
