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
    public class PersonnelEmailBusinessService : IPersonnelEmailBusinessService
    {
        private readonly IEmailBusinessService _emailBusinessService;
        private readonly ITemplateBusinessService _templateBusinessService;

        public PersonnelEmailBusinessService(IEmailBusinessService emailBusinessService, ITemplateBusinessService templateBusinessService)
        {
            _emailBusinessService = emailBusinessService;
            _templateBusinessService = templateBusinessService;
        }

        public async Task SendConfirmationMail(PersonnelCreatedEmail personnelCreatedEmail)
        {
            var templateJson = personnelCreatedEmail.ToJson();
            var body = _templateBusinessService.CreateText(templateJson, personnelCreatedEmail.TemplateName);
            if (body == null)
                return;

            _emailBusinessService.SendEmail(new EmailData
            {
                Subject = personnelCreatedEmail.Subject, //ToDo
                ToAddressList = personnelCreatedEmail.ToAddress,
                IsHtml = true,
                Body = body
            });
        }

        public async Task SendOrderCreatedMail(OrderCreatedEmail orderCreatedEmail)
        {
            var templateJson = orderCreatedEmail.ToJson();
            var body = _templateBusinessService.CreateText(templateJson, orderCreatedEmail.TemplateName);
            if (body == null)
                return;

            _emailBusinessService.SendEmail(new EmailData
            {
                Subject = orderCreatedEmail.Subject, //ToDo
                ToAddressList = orderCreatedEmail.ToAddress,
                IsHtml = true,
                Body = body
            });
        }

        public async Task SendOrderCreatedMailToSeller(OrderCreatedEmail orderCreatedEmail)
        {
            var templateJson = orderCreatedEmail.ToJson();
            var body = _templateBusinessService.CreateText(templateJson, orderCreatedEmail.TemplateName);
            if (body == null)
                return;

            _emailBusinessService.SendEmail(new EmailData
            {
                Subject = orderCreatedEmail.Subject, //ToDo
                ToAddressList = orderCreatedEmail.ToAddress,
                IsHtml = true,
                Body = body
            });
        }

        public async Task SendForgotMail(PersonnelCreatedEmail forgotEmail)
        {
            var templateJson = forgotEmail.ToJson();
            var body = _templateBusinessService.CreateText(templateJson, forgotEmail.TemplateName);
            if (body == null)
                return;

            _emailBusinessService.SendEmail(new EmailData
            {
                Subject = forgotEmail.Subject, //ToDo
                ToAddressList = forgotEmail.ToAddress,
                IsHtml = true,
                Body = body
            });
        }
    }
}
