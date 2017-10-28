using System;
using System.Collections.Generic;
using Egharpay.Business.EmailServiceReference;

namespace Egharpay.Business.Interfaces
{
    public interface IEmailBusinessService
    {
        void SendEmail(EmailData data);
        void SendEmail(EmailData data, List<Guid> docGuids);
        void SendEmail(EmailData data, Dictionary<string, byte[]> attachments);
    }
}
