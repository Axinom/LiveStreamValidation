using System;

namespace Axinom.LiveStreamValidation
{
    static class Constants
    {
        // Everything should be nice and fast for proper live stream validation.
        public static readonly TimeSpan HttpRequestTimeout = TimeSpan.FromSeconds(2);
    }
}
