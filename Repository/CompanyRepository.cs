using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateCompany(Company company)
        {
            Create(company);
        }

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }

        public async Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<Company> GetCompany(Guid CompanyId, bool trackChanges)
        {
            return await FindByCondition(c => c.Id.Equals(CompanyId), trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Company>> GetCompanyList(IEnumerable<Guid> ids, bool trackChanges)
        {
            return await FindByCondition(a => ids.Contains(a.Id), trackChanges).ToListAsync();
        }
    }
}
