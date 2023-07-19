using Entities;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<ShapedEntity>, MetaData metaData)> GetEmployees(Guid companyID, EmployeeParameters employeeParameters, bool trackChanges);

        Task<EmployeeDTO> GetEmployee(Guid companyId, Guid employeeID, bool trackChanges);

        Task<EmployeeDTO> CreateEmployeeForCompany(Guid companyID, EmployeeForCreationDTO employeeForCreationDTO, bool trackChanges);

        Task DeleteEmployeeForCompany(Guid companyID, Guid employeeID, bool trackChanges);

        Task UpdateEmployeeForCompany(Guid companyID, Guid employeeId, EmployeeForUpdateDTO employeeForUpdateDTO, bool companyTrackChanges, bool employeeTrackChanges);

        Task<(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity)> getEmployeeToPatch(Guid companyId, Guid id, bool companyTrackChanges, bool employeeTrackChanges);

        Task SaveChangesForPatch(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity);
    }
}
