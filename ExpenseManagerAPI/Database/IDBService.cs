using System.Data;

namespace ExpenseManagerAPI.Database
{
    public interface IDBService
    {
        void ExecuteNonQueryStoredProcedure(string sqlQuery, List<IDbDataParameter> sqlParams);

        DataSet LoadDataSet(string sqlQuery, List<IDbDataParameter> sqlParams);
        /*
        void OpenConnection();

        void CloseConnection();

        void BeginTransaction(bool throwError);

        void BeginTransaction();

        void CommitTransaction(bool throwError);

        void CommitTransaction();

        void RollbackTransaction(bool throwError);

        void RollbackTransaction();

        object ExecuteScalar(string sqlQuery, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams);

        object ExecuteScalar(string sqlQuery);

        object ExecuteScalar(string sqlQuery, QueryCommandType queryCommandType);

        object ExecuteScalar(string sqlQuery, List<IDbDataParameter> sqlParams);

        int ExecuteNonQuery(string sqlQuery, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams);

        int ExecuteNonQuery(string sqlQuery);

        int ExecuteNonQuery(string sqlQuery, QueryCommandType queryCommandType);

        int ExecuteNonQuery(string sqlQuery, List<IDbDataParameter> sqlParams);

        IDataReader ExecuteReader(string sqlQuery, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams);

        IDataReader ExecuteReader(string sqlQuery);

        IDataReader ExecuteReader(string sqlQuery, QueryCommandType queryCommandType);

        IDataReader ExecuteReader(string sqlQuery, List<IDbDataParameter> sqlParams);

        DataSet LoadDataSet(string sqlQuery, List<string> tableNames, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams);

        DataSet LoadDataSet(string sqlQuery, List<string> tableNames);

        DataSet LoadDataSet(string sqlQuery, List<string> tableNames, QueryCommandType queryCommandType);

        DataSet LoadDataSet(string sqlQuery, List<string> tableNames, List<IDbDataParameter> sqlParams);

        DataSet LoadDataSet(string sqlQuery);

        DataSet LoadDataSet(string sqlQuery, string tableName);

        DataSet LoadDataSet(string sqlQuery, string tableName, QueryCommandType queryCommandType);

        DataSet LoadDataSet(string sqlQuery, QueryCommandType queryCommandType);

        DataSet LoadDataSet(string sqlQuery, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams);

        DataSet LoadDataSet(string sqlQuery, List<IDbDataParameter> sqlParams);

        DataSet LoadDataSet(string sqlQuery, string tableName, QueryCommandType queryCommandType, List<IDbDataParameter> sqlParams);

        int UpdateDataSet(DataSet dataSet, string tableName, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand);

        int UpdateDataSet(DataSet dataSet, string tableName, string insertSqlQuery, string updateSqlQuery, string deleteSqlQuery, QueryCommandType insertQueryCommandType, QueryCommandType updateQueryCommandType, QueryCommandType deleteQueryCommandType, List<IDbDataParameter> insertSqlParams, List<IDbDataParameter> updateSqlParams, List<IDbDataParameter> deleteSqlParams);

        IDbDataParameter GetSingleParameter(string parameterName, ParameterType parameterType, int size, ParameterDirection parameterDirection, bool isNullAble, byte precision, byte scale, string sourceColumn, DataRowVersion dataRowVersion, object parameterValue);

        IDbDataParameter GetSingleParameter(string parameterName, object parameterValue);

        IDbDataParameter GetSingleParameter(string parameterName, ParameterType parameterType, object parameterValue);

        IDbDataParameter GetSingleParameter(string parameterName, ParameterType parameterType, int size, object parameterValue);

        IDbDataParameter GetSingleParameter(string parameterName, ParameterType parameterType, ParameterDirection parameterDirection, object parameterValue);

        IDbDataParameter GetSingleParameter(string parameterName, ParameterType parameterType, int size, string sourceColumn);

        IDbDataParameter GetSingleParameter(string parameterName, ParameterType parameterType, int size, ParameterDirection parameterDirection, object parameterValue);

        void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, ParameterType parameterType, int size, ParameterDirection parameterDirection, bool isNullAble, byte precision, byte scale, string sourceColumn, DataRowVersion dataRowVersion, object parameterValue);

        void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, object parameterValue);

        void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, ParameterType parameterType, object parameterValue);

        void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, ParameterType parameterType, int size, object parameterValue);

        void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, ParameterType parameterType, ParameterDirection parameterDirection, object parameterValue);

        void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, ParameterType parameterType, int size, string sourceColumn);

        void AddSingleParameter(List<IDbDataParameter> sqlParams, string parameterName, ParameterType parameterType, int size, ParameterDirection parameterDirection, object parameterValue);

        */
    }
}
