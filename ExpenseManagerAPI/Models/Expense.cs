namespace ExpenseManagerAPI.Models
{
    public class Expense
    {
        public string Name { get; set; }
        public string PaymentMethod { get; set; }
        public string ExpenseCategory { get; set; }
        public DateOnly ExpenseDate { get; set; }
        public decimal Amount { get; set; }
    }
}
