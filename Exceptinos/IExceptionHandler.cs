using System;

namespace MailMarkup.Exceptinos
{
    public interface IExceptionHandler
    {
        void Handle(Exception ex);
    }
}