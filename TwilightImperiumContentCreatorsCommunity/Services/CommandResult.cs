using System;

namespace TwilightImperiumContentCreatorsCommunity.Services
{
    public class CommandResult : ServiceResult
    {
        public CommandResult()
        {
        }

        public CommandResult(Exception exception) 
            : base(exception)
        {
        }
    }
}