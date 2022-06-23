using System;
using System.Collections.Generic;

namespace MailMarkup.Models
{
    public class ExceptionRecord
    {
        public ExceptionRecord()
        {

        }

        public ExceptionRecord(string message, string additinal)
        {
            Message = message;
            AdditionalInfo = additinal;
        }

        public string Message { get; set; }

        public string AdditionalInfo { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public List<ExceptionRecord> InnerExceptions { get; set; } = new List<ExceptionRecord>();
    }
}