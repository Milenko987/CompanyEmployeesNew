using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDTO>> GetAllCompanies(bool trackChanges);

        Task<CompanyDTO> GetCompany(Guid companyId, bool trackChanges);

        Task<IEnumerable<CompanyDTO>> GetCompanyList(IEnumerable<Guid> ids, bool trackChanges);

        Task<CompanyDTO> CreateCompany(CompanyForCreattionDTO company);

        Task DeleteCompany(Guid companyId, bool trackChanges);

        Task<(IEnumerable<CompanyDTO> companies, string ids)> CreateCompanyCollection(IEnumerable<CompanyForCreattionDTO> companyCollection);

        Task UpdateCompany(Guid companyId, CompanyForUpdateDTO companyForUpdateDTO, bool trackChanges);

    }
}
