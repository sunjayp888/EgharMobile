using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Configuration.Interface;
using Egharpay.Business.Interfaces;
using Egharpay.Entity;
using Egharpay.Entity.Dto;
using Egharpay.Extensions;
using Egharpay.Models;
using Microsoft.Owin.Security.Authorization;

namespace Egharpay.Controllers
{
    public class BrandController : BaseController
    {
        private readonly IBrandBusinessService _brandBusinessService;
        private readonly IMobileBusinessService _mobileBusinessService;
        public BrandController(IBrandBusinessService brandBusinessService, IConfigurationManager configurationManager, IAuthorizationService authorizationService, IMobileBusinessService mobileBusinessService) : base(configurationManager, authorizationService)
        {
            _brandBusinessService = brandBusinessService;
            _mobileBusinessService = mobileBusinessService;
        }

        // GET: Brand
        public ActionResult Index()
        {
            return View(new BaseViewModel());
        }
        
        [HttpPost]
        public async Task<ActionResult> List(List<OrderBy> orderBy)
        {
            var data = await _brandBusinessService.RetrieveBrands(orderBy);
            return this.JsonNet(data);
        }
    }
}