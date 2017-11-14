using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.EmailServiceReference;
using Egharpay.Business.Extensions;
using Egharpay.Business.Interfaces;
using Egharpay.Business.Models;
using Egharpay.Entity;

namespace Egharpay.Business.Services
{
    public class PersonnelEmailService : IPersonnelEmailService
    {
        private readonly IEmailService _emailService;
        private readonly ITemplateService _templateService;

        public PersonnelEmailService(IEmailService emailService, ITemplateService templateService)
        {
            _emailService = emailService;
            _templateService = templateService;
        }

        public async Task SendConfirmationMail(PersonnelCreatedEmail personnelCreatedEmail)
        {
            var templateJson = personnelCreatedEmail.ToJson();
            var body = _templateService.CreateText(templateJson, "PersonnelCreatedEmail");
            if (body == null)
                return;

            await _emailService.SendEmailAsync(new EmailData
            {
                Subject = "Welcome to mumbile", //ToDo
                FromAddress = personnelCreatedEmail.FromAddress,
                ToAddressList = personnelCreatedEmail.ToAddress,
                IsHtml = true,
                Body = body
            });

        }
    }
}
