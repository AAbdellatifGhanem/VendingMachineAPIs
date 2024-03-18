using AutoMapper;
using VendingMachineAPIs.BLL.DTOs.ProductDTOs;
using VendingMachineAPIs.DAL.Models;
using VendingMachineAPIs.DAL.Repo;

namespace VendingMachineAPIs.BLL.Services.ProductService
{
    public class ProductService :IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepo _productRepo;
        public ProductService(IMapper mapper, IProductRepo productRepo)
        {
            _mapper = mapper;   
            _productRepo = productRepo;
        }
        public List<GetAllProductsDTO> GetAllProducts()
        {
            var products = _productRepo.GetAll();
            return _mapper.Map<List<GetAllProductsDTO>>(products);
        }
        public Product AddProduct(AddProductDTO productDTO,string sellerId)
        {
            var newProduct = _mapper.Map<Product>(productDTO);
            newProduct.UserId = sellerId;
            newProduct.ProductId = Guid.NewGuid().ToString();
            return _productRepo.Add(newProduct);
        }
        public Product? UpdateProduct(UpdateProductDTO updatedProduct,string sellerId)
        {
            updatedProduct.UserId = sellerId;
            var prodSellerId = _productRepo.GetById(updatedProduct.ProductId)?.UserId;
            if (prodSellerId != sellerId)
                return null;
            return _productRepo.Update(_mapper.Map<Product>(updatedProduct));
        }
        public Product? DeleteProduct(DeleteProductDTO deletedProduct,string sellerId)
        {
            var prodSellerId = _productRepo.GetById(deletedProduct.ProductId)?.UserId;
            if (prodSellerId != sellerId)
                return null;
            return _productRepo.Delete(_mapper.Map<Product>(deletedProduct));
        }
        public Product? GetProductById (string productId)
        {
           return _productRepo.GetById(productId);
        }

    }
}
