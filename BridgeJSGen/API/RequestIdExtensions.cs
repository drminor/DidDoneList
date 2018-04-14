using System;

namespace BridgeReactTutorial.API
{
    public static class RequestIdExtensions
    {
        public static bool IsEqualToOrComesAfter(this RequestId source, RequestId other)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            // If the "other" reference is no-RequestId then the "source" may be considered to
            // come after it
            if (other == null)
                return true;

            return (source == other) || source.ComesAfter(other);
        }
    }
}