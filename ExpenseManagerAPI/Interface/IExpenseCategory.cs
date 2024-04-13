using ExpenseManagerAPI.Models;

namespace ExpenseManagerAPI.Interface
{
    public interface IExpenseCategory
    {
        string AddExpenseCategory(ExpenseCategory expenseCategory);

        List<ExpenseCategory> GetExpenseCategories();
    }
}
