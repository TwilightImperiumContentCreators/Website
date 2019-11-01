using System;

namespace TwilightImperiumContentCreatorsCommunity.Services
{
    public class ServiceResult
    {
        public Exception Exception { get; protected set; }

        public ServiceResult()
        {
            this.Exception = null;
        }

        public ServiceResult(Exception exception)
        {
            this.Exception = exception;
        }
    }
}