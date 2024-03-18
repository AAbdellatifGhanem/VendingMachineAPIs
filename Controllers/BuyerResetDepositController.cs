using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VendingMachineAPIs.BLL.DTOs.BuyerResetDTOs;
using VendingMachineAPIs.BLL.Services.AuthService;
using VendingMachineAPIs.BLL.Services.ProductService;
using VendingMachineAPIs.BLL.Services.UserService;
using VendingMachineAPIs.DAL.Models;

namespace VendingMachineAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyerResetDepositController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IToken _authService;
        private readonly UserManager<User> _userManager;
        public BuyerResetDepositController(IUserService userService,IProductService productService
                                           ,IToken authService,UserManager<User> userManager)
        {
            _userService = userService;
            _productService = productService;
            _authService = authService;
            _userManager = userManager;
        }
        [Authorize(Roles = VendingMachineRoles.Buyer)]
        [HttpPost]
        [Route("deposite")]
        public async Task<ActionResult> BuyerDeposite(DepositeDTO addedDeposite)
        {

            var token = _authService.ExtractToken(Request);
            var user = await  _userManager.FindByIdAsync(_authService.GetUserIdFromToken(token));
            if (user == null)
                return NotFound("User not found");
            user.deposit += addedDeposite.added_deposit;
            await _userManager.UpdateAsync(user);
            return Ok($"deposite successfully added.  deposite now = {user.deposit}");
        }

        [Authorize(Roles = VendingMachineRoles.Buyer)]
        [HttpPost]
        [Route("reset")]
        public async Task<ActionResult> ResetDeposite()
        {
            var token = _authService.ExtractToken(Request);
            var user = await _userManager.FindByIdAsync(_authService.GetUserIdFromToken(token));
            if (user == null)
                return NotFound("User not found");
            user.deposit = 0;
            await _userManager.UpdateAsync(user);
            return Ok("User deposite reset to zero successfuly");
        }

        [Authorize(Roles = VendingMachineRoles.Buyer)]
        [HttpPost]
        [Route("buy")]
        public async Task<ActionResult> Buy(BuyDTO BuyTransaction)
        {
            //get user
            var token = _authService.ExtractToken(Request);
            var user = await _userManager.FindByIdAsync(_authService.GetUserIdFromToken(token));
            if (user == null)
                return NotFound("User not found");

            //get product
            var product = _productService.GetProductById(BuyTransaction.ProductId);
            if (product == null)
                return NotFound("Product not found");

            //check available amount for product and deposite for buyer
            if (product.Amount - BuyTransaction.amount < 0)
                return BadRequest("Stock Have not enough quantity");
            if (user.deposit < BuyTransaction.amount * product.Cost)
                return BadRequest("Not enough credit");

            //complete tranasction
            var total = BuyTransaction.amount * product.Cost;
            var change = user.deposit - total;
            user.deposit = change;

            return Ok(new BuyResponseDTO
            {
                Total = total,
                Change = change,
                product = product
            });


        }
       
    }
}
