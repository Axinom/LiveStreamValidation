using System;

namespace Axinom.LiveStreamValidation
{
    static class Constants
    {
        // Everything should be nice and fast for proper live stream validation.
        public static readonly TimeSpan HttpRequestTimeout = TimeSpan.FromSeconds(2);

        public static readonly TimeSpan ReasonablePublishTimeDistance = TimeSpan.FromSeconds(25);

        // Some calculations are rounded to seconds, so sometimes we give some flexibility.
        public static readonly TimeSpan TimingTolerance = TimeSpan.FromSeconds(1);
    }
}
