using AutoMapper;
using VendingMachineAPIs.BLL.DTOs;
using VendingMachineAPIs.BLL.DTOs.ProductDTOs;
using VendingMachineAPIs.BLL.DTOs.UserDTOs;
using VendingMachineAPIs.DAL.Models;

namespace VendingMachineAPIs.BLL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddProductDTO,Product>();
            CreateMap<DeleteProductDTO,Product>();
            CreateMap<Product,GetAllProductsDTO>(); 
            CreateMap<UpdateProductDTO,Product>();  
        }
    }
}
