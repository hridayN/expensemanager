using ExpenseManagerAPI.Database;
using ExpenseManagerAPI.Interface;
using ExpenseManagerAPI.Models;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace ExpenseManagerAPI.Services
{
    public class ExpenseManager : IExpenseManager
    {
        private readonly IDBService _dbService;

        public ExpenseManager(IDBService dBService)
        {
            _dbService = dBService;
        }
        public SaveExpenseResponse AddExpense(SaveExpenseRequest saveExpenseRequest)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>()
            {
                new SqlParameter() { ParameterName = "@Description", Value = saveExpenseRequest.Expense.Name },
                new SqlParameter() { ParameterName = "@PaymentMethod", Value = saveExpenseRequest.Expense.PaymentMethod },
                new SqlParameter() { ParameterName = "@ExpenseCategory", Value = saveExpenseRequest.Expense.ExpenseCategory },
                new SqlParameter() { ParameterName = "@ExpenseDate", Value = DateOnly.Parse(saveExpenseRequest.Expense.ExpenseDate.ToString()).ToString("yyyy-MM-dd") },
                new SqlParameter() { ParameterName = "@Amount", Value = saveExpenseRequest.Expense.Amount }
            };
            _dbService.ExecuteNonQueryStoredProcedure(DBProcedures.SP_AddUpdateExpenses, parameters);
            SaveExpenseResponse saveExpenseResponse = new SaveExpenseResponse
            {
                StatusMessage = "Expense added/Updated"
            };
            return saveExpenseResponse;
        }

        GetExpenseResponse IExpenseManager.GetExpenses(GetExpenseRequest getExpenseRequest)
        {
            throw new NotImplementedException();
        }
    }
}
