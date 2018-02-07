using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Egharpay.Business.Models;
using Egharpay.Entity;

namespace Egharpay.Business.Interfaces
{
    public interface IPersonnelEmailBusinessService
    {
        Task SendConfirmationMail(PersonnelCreatedEmail personnelCreatedEmail);
        Task SendOrderCreatedMail(OrderCreatedEmail orderCreatedEmail);
        //Please move below function to ISellerEmailBusinessService, create new.
        Task SendOrderCreatedMailToSeller(OrderCreatedEmail orderCreatedEmail);
        Task SendForgotMail(PersonnelCreatedEmail forgotEmail);
        }
}
