using Entities;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;
namespace Repository.Extensions
{
    public static class RepositoryEmployeeExtensions
    {
        public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees, uint minAge, uint maxAge)
        {
            return employees.Where(emp => (emp.Age >= minAge && emp.Age <= maxAge));
        }

        public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return employees;
            }
            var lowerCaseTerm = searchTerm.Trim().ToLower();
            return employees.Where(a => a.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByQueryString)
        {
            if (string.IsNullOrEmpty(orderByQueryString))
            {
                return employees.OrderBy(a => a.Name);
            }
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);
            if (string.IsNullOrWhiteSpace(orderQuery))
            {
                return employees.OrderBy(a => a.Name);
            }
            return employees.OrderBy(orderQuery);
        }
    }
}
