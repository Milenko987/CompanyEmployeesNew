using AutoMapper;
using Contracts;
using Entities;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CompanyService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
        {
            _repository = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;
        }

        public async Task<CompanyDTO> CreateCompany(CompanyForCreattionDTO company)
        {
            var companyEntity = _mapper.Map<Company>(company);

            _repository.Company.CreateCompany(companyEntity);
            await _repository.SaveAsync();

            var companyToReturn = _mapper.Map<CompanyDTO>(companyEntity);

            return companyToReturn;
        }

        public async Task<(IEnumerable<CompanyDTO> companies, string ids)> CreateCompanyCollection(IEnumerable<CompanyForCreattionDTO> companyCollection)
        {
            if (companyCollection == null)
            {
                throw new CompanyCollectionBadRequest();
            }
            var companyEntityList = _mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach (var company in companyEntityList)
            {
                _repository.Company.CreateCompany(company);
            }
            await _repository.SaveAsync();
            var companyCollectionToReturn = _mapper.Map<IEnumerable<CompanyDTO>>(companyEntityList);
            var ids = string.Join(",", companyCollectionToReturn.Select(company => company.Id));
            return (companies: companyCollectionToReturn, ids);
        }

        public async Task DeleteCompany(Guid companyId, bool trackChanges)
        {
            Company company = await GetCompanyAndCheckIfExists(companyId, trackChanges);

            _repository.Company.DeleteCompany(company);
            await _repository.SaveAsync();
        }

        private async Task<Company> GetCompanyAndCheckIfExists(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompany(companyId, trackChanges);
            if (company == null)
            {
                throw new CompanyNotFoundException(companyId);
            }

            return company;
        }

        public async Task UpdateCompany(Guid companyId, CompanyForUpdateDTO companyForUpdateDTO, bool trackChanges)
        {
            Company company = await GetCompanyAndCheckIfExists(companyId, trackChanges);
            _mapper.Map(companyForUpdateDTO, company);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<CompanyDTO>> GetAllCompanies(bool trackChanges)
        {

            var companies = await _repository.Company.GetAllCompanies(trackChanges);
            var companiesDTO = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
            return companiesDTO;



        }

        public async Task<CompanyDTO> GetCompany(Guid companyId, bool trackChanges)
        {
            Company company = await GetCompanyAndCheckIfExists(companyId, trackChanges);

            var companyDTO = _mapper.Map<CompanyDTO>(company);
            return companyDTO;
        }

        public async Task<IEnumerable<CompanyDTO>> GetCompanyList(IEnumerable<Guid> ids, bool trackChanges)
        {
            if (ids is null)
            {
                throw new IdParametersBadRequestException();
            }
            var companyEntityList = await _repository.Company.GetCompanyList(ids, trackChanges);
            if (ids.Count() != companyEntityList.Count())
            {
                throw new CollectionByIdsBadRequestException();
            }

            var companyListToReturn = _mapper.Map<IEnumerable<CompanyDTO>>(companyEntityList);

            return companyListToReturn;
        }
    }
}
