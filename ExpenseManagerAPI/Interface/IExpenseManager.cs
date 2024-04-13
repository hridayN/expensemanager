using ExpenseManagerAPI.Models;

namespace ExpenseManagerAPI.Interface
{
    public interface IExpenseManager
    {
        public SaveExpenseResponse AddExpense(SaveExpenseRequest saveExpenseRequest);

        public GetExpenseResponse GetExpenses(GetExpenseRequest getExpenseRequest);
    }
}
