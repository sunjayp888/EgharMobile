using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Egharpay.Business.EmailServiceReference;

namespace Egharpay.Business.Interfaces
{
    public interface IEmailBusinessService
    {
        Task<bool> SendEmail(EmailData data);
        void SendEmail(EmailData data, List<Guid> docGuids);
        Task<bool> SendEmail(EmailData data, Dictionary<string, byte[]> attachments);
    }
}
