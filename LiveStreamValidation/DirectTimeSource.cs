using System;
using System.Threading.Tasks;

namespace Axinom.LiveStreamValidation
{
    internal sealed class DirectTimeSource : ITimeSource
    {
        public string Name => "direct";
        public bool IsStaticValue => true;

        public DirectTimeSource(string value)
        {
            _value = DateTimeOffset.Parse(value);
        }

        private DateTimeOffset _value;

        public Task<DateTimeOffset> GetTimeAsync()
        {
            return Task.FromResult(_value);
        }
    }
}
