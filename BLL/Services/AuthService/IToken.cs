using VendingMachineAPIs.BLL.DTOs.UserDTOs;
using VendingMachineAPIs.BLL.DTOs;

namespace VendingMachineAPIs.BLL.Services.AuthService
{
    public interface IToken
    {
        Task<GetUserResponseDTO?> GenerateLoginResponseWithToken(LoginDTOModel model);
        string GetUserIdFromToken(string token);
        public string ExtractToken(HttpRequest _request);
    }
}
