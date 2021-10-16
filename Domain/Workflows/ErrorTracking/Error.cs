using System;

namespace UStart.Domain.Workflows.ErrorTracking
{
    public class Error
    {
        public string Property { get; private set; }
        public string Message { get; private set; }
        public Object Value { get; private set; }

        public Error(string property, string message, Object value = null)
        {
            Property = property;
            Message = message;
            Value = value;
        }
    }

}