using AutoMapper;
using Contracts;
using Entities;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<EmployeeDTO> _shaper;

        public EmployeeService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper, IDataShaper<EmployeeDTO> dataShaper)
        {
            _repository = repositoryManager;
            _logger = loggerManager;
            _mapper = mapper;
            _shaper = dataShaper;
        }

        public async Task<EmployeeDTO> CreateEmployeeForCompany(Guid companyID, EmployeeForCreationDTO employeeForCreationDTO, bool trackChanges)
        {
            await GetCompanyAndCheckIfExist(companyID, trackChanges);
            var employeeEntity = _mapper.Map<Employee>(employeeForCreationDTO);
            _repository.Employee.CreateEmployeeForCompany(companyID, employeeEntity);
            await _repository.SaveAsync();
            var employeeToReturn = _mapper.Map<EmployeeDTO>(employeeEntity);
            return employeeToReturn;
        }

        public async Task DeleteEmployeeForCompany(Guid companyID, Guid employeeID, bool trackChanges)
        {
            await GetCompanyAndCheckIfExist(companyID, trackChanges);
            Employee? employeeForCompany = await GetEmployeeAndCheckIfExists(companyID, employeeID, trackChanges);
            _repository.Employee.DeleteEmployee(employeeForCompany);
            await _repository.SaveAsync();

        }

        public async Task<EmployeeDTO> GetEmployee(Guid companyId, Guid employeeID, bool trackChanges)
        {
            await GetCompanyAndCheckIfExist(companyId, trackChanges);
            Employee? employeeEntity = await GetEmployeeAndCheckIfExists(companyId, employeeID, trackChanges);
            var employeeDTO = _mapper.Map<EmployeeDTO>(employeeEntity);
            return employeeDTO;
        }

        public async Task<(IEnumerable<ShapedEntity>, MetaData metaData)> GetEmployees(Guid companyID, EmployeeParameters employeeParameters, bool trackChanges)
        {
            if (!employeeParameters.ValidAgeRange)
            {
                throw new MaxAgeRangeBadRequestExcepetion();
            }
            await GetCompanyAndCheckIfExist(companyID, trackChanges);

            var employeesWithMetaData = await _repository.Employee.GetEmployees(companyID, employeeParameters, trackChanges);
            var employeesDTO = _mapper.Map<IEnumerable<EmployeeDTO>>(employeesWithMetaData);
            var shapedData = _shaper.ShapeData(employeesDTO, employeeParameters.Fields);
            return (shapedData, employeesWithMetaData.MetaData);
        }

        public async Task<(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity)> getEmployeeToPatch(Guid companyId, Guid id, bool companyTrackChanges, bool employeeTrackChanges)
        {
            await GetCompanyAndCheckIfExist(companyId, companyTrackChanges);
            Employee? employeeEntity = await GetEmployeeAndCheckIfExists(companyId, id, employeeTrackChanges);
            var employeeToPatch = _mapper.Map<EmployeeForUpdateDTO>(employeeEntity);
            return (employeeToPatch, employeeEntity);
        }

        public async Task SaveChangesForPatch(EmployeeForUpdateDTO employeeToPatch, Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch, employeeEntity);
            await _repository.SaveAsync();
        }

        public async Task UpdateEmployeeForCompany(Guid companyID, Guid employeeId, EmployeeForUpdateDTO employeeForUpdateDTO, bool companyTrackChanges, bool employeeTrackChanges)
        {
            await GetCompanyAndCheckIfExist(companyID, companyTrackChanges);
            Employee? employee = await GetEmployeeAndCheckIfExists(companyID, employeeId, employeeTrackChanges);
            _mapper.Map(employeeForUpdateDTO, employee);
            await _repository.SaveAsync();
        }

        private async Task<Employee> GetEmployeeAndCheckIfExists(Guid companyID, Guid employeeId, bool employeeTrackChanges)
        {
            var employee = await _repository.Employee.GetEmployee(companyID, employeeId, employeeTrackChanges);
            if (employee is null)
            {
                throw new EmployeeNotFoundException(employeeId);
            }

            return employee;
        }

        private async Task GetCompanyAndCheckIfExist(Guid companyID, bool companyTrackChanges)
        {
            var company = await _repository.Company.GetCompany(companyID, companyTrackChanges);
            if (company is null)
            {
                throw new CompanyNotFoundException(companyID);
            }
        }
    }
}
