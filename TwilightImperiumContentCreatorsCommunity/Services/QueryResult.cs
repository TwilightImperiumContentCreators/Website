using System;
using Polly;

namespace TwilightImperiumContentCreatorsCommunity.Services
{
    public class QueryResult<TResult> : ServiceResult
    {
        public QueryResult(Exception exception)
            : base (exception)
        {
        }

        public QueryResult(TResult result)
            : base()
        {
            this.result = result;
        }

        private readonly TResult result;

        public virtual TResult Result => this.Exception != null ? throw this.Exception : this.result;
    }
}