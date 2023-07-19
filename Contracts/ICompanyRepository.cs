using Entities;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges);

        Task<Company> GetCompany(Guid CompanyId, bool trackChanges);

        Task<IEnumerable<Company>> GetCompanyList(IEnumerable<Guid> ids, bool trackChanges);

        void CreateCompany(Company company);

        void DeleteCompany(Company company);


    }
}
