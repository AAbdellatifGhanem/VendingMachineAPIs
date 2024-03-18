using AutoMapper;
using Microsoft.AspNetCore.Identity;
using VendingMachineAPIs.BLL.DTOs;
using VendingMachineAPIs.BLL.DTOs.UserDTOs;
using VendingMachineAPIs.DAL.Models;
using VendingMachineAPIs.DAL.Repo;

namespace VendingMachineAPIs.BLL.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        public UserService(UserManager<User> userManger, RoleManager<IdentityRole> roleManager,IMapper mapper,
                           IUserRepo userRepo)
        {
            _userManager = userManger;
            _roleManager = roleManager;
            _mapper = mapper;
            _userRepo = userRepo;
        }
        public async Task<Response> Register(RegisterUserDTO model)
        {
            //check if user exists
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return new Response {Status = 400, Message = "User already exists"} ;

            User user = new User()
            {
                UserName = model.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                deposit = model.deposite
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new Response { Status = 500, Message = "User Creation failed" };
            
                //check and add roles
            if (!await _roleManager.RoleExistsAsync(VendingMachineRoles.Buyer))
                await _roleManager.CreateAsync(new IdentityRole(VendingMachineRoles.Buyer));

            if (!await _roleManager.RoleExistsAsync(VendingMachineRoles.Seller))
                await _roleManager.CreateAsync(new IdentityRole(VendingMachineRoles.Seller));

            if (await _roleManager.RoleExistsAsync(VendingMachineRoles.Seller) &&
                model.Role?.ToLower() == "seller")
            {
                await _userManager.AddToRoleAsync(user, VendingMachineRoles.Seller);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, VendingMachineRoles.Buyer);
            }
            return new Response { Status = 200, Message = "User Created Succeffully"};
        }
        public async Task<Response> UpdateUser (UpdateUserDTO model,string userId)
        {
            var userTobeUpdated = await _userManager.FindByIdAsync(userId);
            if(userTobeUpdated == null)
                return new Response { Status = 404, Message = "User Not found" };

            userTobeUpdated.UserName = model.UserName;
            userTobeUpdated.deposit = model.deposit;

            var result =await _userManager.UpdateAsync(userTobeUpdated);
            if (!result.Succeeded)
                return new Response { Status = 500, Message = "User update failed" };

            return new Response { Status = 200, Message = "User updated Succeffully" };
        }
        public async Task<Response> DeleteUser (string userId)
        {
           var deletedUser = await _userManager.FindByIdAsync(userId);

            if (deletedUser == null)
                return new Response { Status = 404, Message = "User Not found" };

            var result = await _userManager.DeleteAsync(deletedUser);
            if(!result.Succeeded)
                return new Response { Status = 500, Message = "User deletion failed" };

            return new Response { Status = 200, Message = "User deleted Succeffully" };
        }
        public async Task<Response> ChangePassword(ChangePasswordDTO model, string userId)
        {
            if (model == null || string.IsNullOrEmpty(model.OldPassword) || string.IsNullOrEmpty(model.Password))
                 return new Response { Status = 400, Message = "Please enter old and new password." };

            var changePwdUser = await _userManager.FindByIdAsync(userId);
            if (changePwdUser == null)
                return new Response { Status = 404, Message = "User Not found" };

      
            var oldPwdCheck = await _userManager.CheckPasswordAsync(changePwdUser, model.OldPassword);
            if (!oldPwdCheck)
                return new Response { Status = 401, Message = "Wrong old password" };
            
                var changePasswordResult = await _userManager.ChangePasswordAsync(changePwdUser,
                                                              model.OldPassword, model.Password);

                if (!changePasswordResult.Succeeded)
                {
                return new Response { Status = 500, Message = "Change Password failed" };
                }

            return new Response { Status = 200, Message = "Password Changed successfully" };
        }
    }
}

