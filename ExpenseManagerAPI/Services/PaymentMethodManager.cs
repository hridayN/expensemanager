using ExpenseManagerAPI.Database;
using ExpenseManagerAPI.Interface;
using ExpenseManagerAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseManagerAPI.Services
{
    public class PaymentMethodManager : IPaymentMethod
    {
        private readonly IDBService _dbService;

        public PaymentMethodManager(IDBService dBService)
        {
            this._dbService = dBService;
        }
        public SavePaymentMethodResponse AddPaymentMethod(SavePaymentMethodRequest savePaymentMethodRequest)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>()
            {
                new SqlParameter() { ParameterName = "@Name", Value = savePaymentMethodRequest.PaymentMethod.Name },
                new SqlParameter() { ParameterName = "@BillingDate", Value = savePaymentMethodRequest.PaymentMethod.BillingDate },
                new SqlParameter() { ParameterName = "@GradePeriodDays", Value = savePaymentMethodRequest.PaymentMethod.GracePeriod },
                new SqlParameter() { ParameterName = "@ActiveIndicator", Value = (savePaymentMethodRequest.PaymentMethod.ActiveIndicator) ? 1 : 0 }
            };
            this._dbService.ExecuteNonQueryStoredProcedure(DBProcedures.SP_AddUpdatePaymentMethod, parameters);
            SavePaymentMethodResponse savePaymentMethodResponse = new SavePaymentMethodResponse
            {
                StatusMessage = "Payment method added"
            };
            return savePaymentMethodResponse;
        }

        public GetPaymentMethodResponse GetPaymentMethods(GetPaymentMethodRequest getPaymentMethodRequest)
        {
            GetPaymentMethodResponse getPaymentMethodResponse = new GetPaymentMethodResponse();
            List<IDbDataParameter> parameters = new List<IDbDataParameter>()
            {
                new SqlParameter() { ParameterName = "@ActiveIndicator", Value = getPaymentMethodRequest.ActiveIndicator }
            };
            DataSet dataSet = this._dbService.LoadDataSet(DBProcedures.SP_GetAllPaymentMethods, parameters);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                List<PaymentMethod> paymentMethods = new List<PaymentMethod>();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    PaymentMethod paymentMethod = new PaymentMethod
                    {
                        Name = (string)dataRow["Name"],
                        BillingDate = Convert.ToInt32(dataRow["BillingDate"]),
                        GracePeriod = Convert.ToInt32(dataRow["GradePeriodDays"]),
                        ActiveIndicator = (dataRow["ActiveIndicator"] == DBNull.Value) ? false : (bool)dataRow["ActiveIndicator"]
                    };
                    paymentMethods.Add(paymentMethod);
                }
                getPaymentMethodResponse.PaymentMethods = paymentMethods;
            }
            return getPaymentMethodResponse;
        }
    }
}
