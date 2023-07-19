using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                new Employee
                {
                    Id = new Guid("b70a7067-a271-41c8-b996-2722a3410278"),
                    Name = "Sam Raiden",
                    Age = 26,
                    Position = "Software developer",
                    CompanyId = new Guid("aa4af657-4ac2-4a5a-99a3-68ac1a5a6fc2")
                },
                new Employee
                {
                    Id = new Guid("c9977417-2d8d-4b00-8c7a-134d8dcecf8f"),
                    Name = "Jana McLeaf",
                    Age = 30,
                    Position = "Software developer",
                    CompanyId = new Guid("aa4af657-4ac2-4a5a-99a3-68ac1a5a6fc2")
                },
                new Employee
                {
                    Id = new Guid("8b831a47-e957-4beb-ba2b-18b5a1ffc610"),
                    Name = "Kane Miller",
                    Age = 35,
                    Position = "Administrator",
                    CompanyId = new Guid("a23c10e8-affc-4cb3-a6eb-f25f57b84be9")
                });
        }
    }
}
