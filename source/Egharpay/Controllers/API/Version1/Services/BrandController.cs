using System;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Egharpay.Business.Interfaces;

namespace Egharpay.Controllers.API.Version1.Services
{
    [RoutePrefix("api/v1/brands")]
    public class BrandController : ApiController
    {
        private readonly IBrandBusinessService _brandBusinessService;
        private readonly IMapper _mapper;

        public BrandController(IBrandBusinessService brandBusinessService, IMapper mapper)
        {
            _brandBusinessService = brandBusinessService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(Name = "RetrieveBrands")]
        public async Task<IHttpActionResult> Retrieve()
        {
            try
            {
                var brands = await _brandBusinessService.RetrieveBrands(e => e.IsTopSellingBrand, orderBy);
                if (brands == null)
                    return NotFound();
                return Ok(brands);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
