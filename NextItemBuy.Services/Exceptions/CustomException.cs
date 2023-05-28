using System;
using System.Collections.Generic;

namespace NextItemBuy.Services.Exceptions
{
    public class CustomException : Exception
    {
        public List<ValidationRecord> Errors { get; set; }
        public CustomException()
        {
        }

        public CustomException(string message, params object[] args)
            : base(string.Format(message ?? string.Empty, args))
        {
        }
        public CustomException(List<ValidationRecord> errors)
        {
            Errors = errors;
        }

    }

    public class CustomValidationException : Exception
    {
        public CustomValidationException()
        {
        }

        public CustomValidationException(string message, params object[] args)
            : base(string.Format(message ?? string.Empty, args))
        {
        }


    }
    public class ValidationRecord
    {
        public ValidationRecord() { }
        public ValidationRecord(string key, string message)
        {
            Key = key;
            Message = message;
        }
        public string Key { get; set; }
        public string Message { get; set; }
    }
}
