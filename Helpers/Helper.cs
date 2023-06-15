using System.Transactions;

namespace LightCVTest.Helpers;

static public class Helper
{
    public static TransactionScope CreatTransactionScope(int seconds = 10)
    {
        return new TransactionScope(TransactionScopeOption.Required,
            new TimeSpan(0, 0, seconds),
            TransactionScopeAsyncFlowOption.Enabled);
    }
}