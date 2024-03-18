using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendingMachineAPIs.BLL.DTOs.ProductDTOs;
using VendingMachineAPIs.BLL.Services.AuthService;
using VendingMachineAPIs.BLL.Services.ProductService;
using VendingMachineAPIs.BLL.Services.UserService;
using VendingMachineAPIs.DAL.Models;

namespace VendingMachineAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IToken _authService;
        private readonly IProductService _productService;
        public ProductController(IToken authservice,IProductService productService)
        {
            _authService = authservice;
            _productService = productService;
        }


        [Authorize(Roles = VendingMachineRoles.Seller)]
        [HttpPost]
        [Route("AddProduct")]
        public ActionResult<Product> AddProduct(AddProductDTO newProd)
        {
            var token = _authService.ExtractToken(Request);
            return _productService.AddProduct(newProd, _authService.GetUserIdFromToken(token));
        }



        [Authorize(Roles = VendingMachineRoles.Seller)]
        [HttpDelete]
        [Route("DeleteProduct")]
        public ActionResult DeleteProduct(DeleteProductDTO deleteProduct) 
        {
            var token = _authService.ExtractToken(Request);
            var result = _productService.DeleteProduct(deleteProduct,_authService.GetUserIdFromToken(token));
            if (result == null)
                return NotFound("Product not found");
            return Ok(result);
        }



        [Authorize(Roles = VendingMachineRoles.Seller)]
        [HttpPut]
        [Route("EditProduct")]
        public ActionResult EditProduct(UpdateProductDTO updatedProduct)
        {
            var token = _authService.ExtractToken(Request);
            var result = _productService.UpdateProduct(updatedProduct, _authService.GetUserIdFromToken(token));
            if (result == null)
                return NotFound("Product not found");
            return Ok(result);
        }


        [HttpGet]
        [Route("GetProducts")]
        public ActionResult<List<GetAllProductsDTO>> GetProducts()
        {
            return _productService.GetAllProducts();
        }
    }
}
