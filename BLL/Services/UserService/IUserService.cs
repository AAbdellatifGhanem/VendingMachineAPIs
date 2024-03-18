using Microsoft.AspNetCore.Identity;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using VendingMachineAPIs.BLL.DTOs;
using VendingMachineAPIs.BLL.DTOs.UserDTOs;
using VendingMachineAPIs.DAL.Models;

namespace VendingMachineAPIs.BLL.Services.UserService
{
    public interface IUserService
    {
        Task<Response> Register(RegisterUserDTO model);
        Task<Response> UpdateUser(UpdateUserDTO model, string userId);
        Task<Response> DeleteUser(string userId);
        Task<Response> ChangePassword(ChangePasswordDTO model, string userId);

    }
}
