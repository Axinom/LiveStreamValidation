using System;
using System.Threading.Tasks;

namespace Axinom.LiveStreamValidation
{
    interface ITimeSource
    {
        Task<DateTimeOffset> GetTimeAsync();

        string Name { get; }

        /// <summary>
        /// If true, the time source embeds the time value in the manifest directly.
        /// If false, the time source reports a new current value each time it is polled.
        /// </summary>
        bool IsStaticValue { get; }
    }
}
