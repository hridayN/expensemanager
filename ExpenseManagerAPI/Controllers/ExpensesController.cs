using ExpenseManagerAPI.Interface;
using ExpenseManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseManagerAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpenseManager _expenseManager;
        public ExpensesController(IExpenseManager expenseManager)
        {
            _expenseManager = expenseManager;
        }

        [Route("AddUpdateExpense")]
        [HttpPost]
        public SaveExpenseResponse AddUpdateExpense(SaveExpenseRequest saveExpenseRequest)
        {
            return _expenseManager.AddExpense(saveExpenseRequest);
        }
    }
}
