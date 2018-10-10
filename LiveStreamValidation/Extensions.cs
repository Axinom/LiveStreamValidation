using System;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;

namespace Axinom.LiveStreamValidation
{
    public static class Extensions
    {
        public static string ToTimeStringAccurate(this DateTimeOffset timestamp)
        {
            // We do not really care about dates, it is not like live stream playback windows often span many days.
            return timestamp.ToString("HH':'mm':'ss.fff'Z'", CultureInfo.InvariantCulture);
        }

        public static string ToStringAccurate(this DateTimeOffset timestamp)
        {
            return timestamp.ToString("yyyy'-'MM'-'dd HH':'mm':'ss.fff'Z'", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns the value in hours:minutes:seconds.milliseconds format.
        /// </summary>
        public static string ToStringAccurate(this TimeSpan ts) => $"{ts.Days * 24 + ts.Hours:D2}:{ts.Minutes:D2}:{ts.Seconds:D2}.{ts.Milliseconds:D3}";

        public static string GetAttributeAsString(this XElement element, XName attributeName)
        {
            var a = element.Attribute(attributeName);

            if (a == null)
                throw new NotSupportedException($"Expected {element.Name} to have attribute {attributeName}.");

            return a.Value;
        }

        public static TimeSpan GetAttributeAsTimeSpan(this XElement element, XName attributeName)
        {
            var a = element.Attribute(attributeName);

            if (a == null)
                throw new NotSupportedException($"Expected {element.Name} to have attribute {attributeName}.");

            try
            {
                return XmlConvert.ToTimeSpan(a.Value);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException($"Expected {element.Name} to have attribute {attributeName} of type xs:duration. The attribute was found but the format was not a valid xs:duration.", ex);
            }
        }

        public static long GetAttributeAsInt64(this XElement element, XName attributeName)
        {
            var a = element.Attribute(attributeName);

            if (a == null)
                throw new NotSupportedException($"Expected {element.Name} to have attribute {attributeName}.");

            try
            {
                return long.Parse(a.Value, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException($"Expected {element.Name} to have attribute {attributeName} of type int64. The attribute was found but the format was not a valid int64.", ex);
            }
        }

        public static Uri GetAttributeAsAbsoluteUri(this XElement element, XName attributeName)
        {
            var a = element.Attribute(attributeName);

            if (a == null)
                throw new NotSupportedException($"Expected {element.Name} to have attribute {attributeName}.");

            try
            {
                return new Uri(a.Value, UriKind.Absolute);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException($"Expected {element.Name} to have attribute {attributeName} of type absolute URI. The attribute was found but the format was not a valid absolute URI.", ex);
            }
        }

        public static DateTimeOffset GetAttributeAsDateTimeOffset(this XElement element, XName attributeName)
        {
            var a = element.Attribute(attributeName);

            if (a == null)
                throw new NotSupportedException($"Expected {element.Name} to have attribute {attributeName}.");

            try
            {
                return DateTimeOffset.Parse(a.Value, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException($"Expected {element.Name} to have attribute {attributeName} of type DateTimeOffset. The attribute was found but the format was not a valid DateTimeOffset.", ex);
            }
        }

    }
}
