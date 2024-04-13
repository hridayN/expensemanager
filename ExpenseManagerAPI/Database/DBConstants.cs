namespace ExpenseManagerAPI.Database
{
    public static class DBConstants
    {
        public static class DatabaseErrors
        {
            public static string OPEN_CONNECTION = "Please open connection before performing any Transaction Activity";

            public static string START_TRANSACTION_COMMIT = "Please Start a Transaction before submitting any changes";

            public static string START_TRANSACTION_REVERT = "Please Start a Transaction before reverting any changes";
        }
    }
}
