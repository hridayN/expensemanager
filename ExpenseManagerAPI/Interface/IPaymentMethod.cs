using ExpenseManagerAPI.Models;

namespace ExpenseManagerAPI.Interface
{
    public interface IPaymentMethod
    {
        SavePaymentMethodResponse AddPaymentMethod(SavePaymentMethodRequest savePaymentMethodRequest);

        GetPaymentMethodResponse GetPaymentMethods(GetPaymentMethodRequest getPaymentMethodRequest);
    }
}
