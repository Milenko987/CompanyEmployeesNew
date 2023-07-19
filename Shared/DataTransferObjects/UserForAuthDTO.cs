using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record UserForAuthDTO
    {
        [Required(ErrorMessage = "UserName is requied")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is requied")]
        public string? Password { get; init; }
    }
}
