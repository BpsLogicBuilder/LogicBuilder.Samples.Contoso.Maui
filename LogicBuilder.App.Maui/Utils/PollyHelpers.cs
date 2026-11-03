using Polly;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LogicBuilder.App.Maui.Utils
{
    public static class PollyHelpers
    {
        public static Task<TResult> ExecutePolicyAsync<TResult>(Func<Task<TResult>> action)
        {
            return Policy.Handle<HttpRequestException>
            (
                ex =>
                {
                    return true;
                })
            .WaitAndRetryAsync
            (
                2,
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
            )
            .ExecuteAsync(action);
        }
    }
}
