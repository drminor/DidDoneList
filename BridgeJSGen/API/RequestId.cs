using System;

namespace BridgeReactTutorial.API
{
    public class RequestId
    {
        private static DateTime _timeOfLastId = DateTime.MinValue;
        private static int _offsetOfLastId = 0;

        private readonly DateTime _requestTime;
        private readonly int _requestOffset;
        public RequestId()
        {
            _requestTime = DateTime.Now;
            if (_timeOfLastId < _requestTime)
            {
                _offsetOfLastId = 0;
                _timeOfLastId = _requestTime;
            }
            else
                _offsetOfLastId++;
            _requestOffset = _offsetOfLastId;
        }

        public bool ComesAfter(RequestId other)
        {
            if (other == null)
                throw new ArgumentNullException("other");

            if (_requestTime == other._requestTime)
                return _requestOffset > other._requestOffset;
            return (_requestTime > other._requestTime);
        }
    }
}