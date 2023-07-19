namespace Shared.DataTransferObjects
{
    public record CompanyForCreattionDTO(string Name, string Address, string Country, IEnumerable<EmployeeForCreationDTO> Employees);
}
