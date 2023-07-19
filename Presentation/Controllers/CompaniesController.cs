using CompanyEmployeesNew.ActionFilters;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ModelBinders;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Presentation.Controllers
{
    [Route("api/companies")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;
        public CompaniesController(IServiceManager service)
        {
            _service = service;
        }
        [HttpGet]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
        [HttpCacheValidation(MustRevalidate = false)]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetCompanies()
        {

            var comapanies = await _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(comapanies);


        }

        [HttpGet("collection/({ids})", Name = "CompanyCollection")]
        public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            var comapanies = await _service.CompanyService.GetCompanyList(ids, trackChanges: false);
            return Ok(comapanies);
        }

        [HttpGet("{id:guid}", Name = "CompanyById")]
        [ResponseCache(CacheProfileName = "120secondsDuration")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var companyDTO = await _service.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(companyDTO);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribite))]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreattionDTO companyForCreattionDTO)
        {

            var createdCompany = await _service.CompanyService.CreateCompany(companyForCreattionDTO);

            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreattionDTO> companyCollection)
        {
            var result = await _service.CompanyService.CreateCompanyCollection(companyCollection);
            return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            await _service.CompanyService.DeleteCompany(id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        [ServiceFilter(typeof(ValidationFilterAttribite))]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDTO companyForUpdateDTO)
        {

            await _service.CompanyService.UpdateCompany(id, companyForUpdateDTO, trackChanges: true);
            return NoContent();

        }
    }
}
