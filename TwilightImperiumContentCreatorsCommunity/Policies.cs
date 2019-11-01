using Polly;
using System;

namespace TwilightImperiumContentCreatorsCommunity
{
    public static class Policies
    {
        public static AsyncPolicy Default = Policy
            .Handle<Exception>()
            .RetryAsync(3);
    }
}
