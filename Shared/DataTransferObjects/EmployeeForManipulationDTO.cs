using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public abstract record EmployeeForManipulationDTO
    {
        [Required(ErrorMessage = "Employee name is required field")]
        [MaxLength(30, ErrorMessage = "Max length for the name is 30 charachters")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "Age is required field")]
        [Range(18, int.MaxValue, ErrorMessage = "Age is required and it can not be lower than 18")]
        public int Age { get; init; }

        [Required(ErrorMessage = "Position is required field")]
        [MaxLength(20, ErrorMessage = "Max length for the position is 20 charachters")]
        public string? Position { get; init; }
    }
}
