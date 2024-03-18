using VendingMachineAPIs.BLL.DTOs.ProductDTOs;
using VendingMachineAPIs.DAL.Models;

namespace VendingMachineAPIs.BLL.Services.ProductService
{
    public interface IProductService
    {
        List<GetAllProductsDTO> GetAllProducts();
        Product AddProduct(AddProductDTO productDTO, string sellerId);
        Product? UpdateProduct(UpdateProductDTO updatedProduct, string sellerId);
        Product? DeleteProduct(DeleteProductDTO deletedProduct, string sellerId);
        Product? GetProductById(string productId);
    }
}
