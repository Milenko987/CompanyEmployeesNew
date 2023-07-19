using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

namespace Presentation.Controllers
{
    [Route("api/companies")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CompaniesV2Controller : ControllerBase
    {
        private readonly IServiceManager _service;

        public CompaniesV2Controller(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {

            var comapanies = await _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(comapanies);


        }
    }
}
