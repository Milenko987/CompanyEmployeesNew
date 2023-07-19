using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;

namespace Service.Contracts
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUser(UserForRegistrationDTO userForRegistrationDTO);

        Task<bool> ValidateUser(UserForAuthDTO userForAuthDTO);

        Task<string> CreateToken();
    }
}
