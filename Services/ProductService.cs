using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestApiProg.Data;
using TestApiProg.Dtos;
using TestApiProg.Models;

namespace TestApiProg.Services
{
    public class ProductService : IProductService
    {

        private readonly IMapper _mapper;
        private readonly TestAPIContext _context;

        public ProductService(IMapper mapper, TestAPIContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResponse<List<GetProductDto>>> AddProduct(AddProductDto newProduct)
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            Product product = _mapper.Map<Product>(newProduct);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            serviceResponse.Data = _context.Products.Select(c => _mapper.Map<GetProductDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProducts()
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            List<Product> dbValues = await _context.Products.ToListAsync();
            serviceResponse.Data = dbValues.Select(c => _mapper.Map<GetProductDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> GetProductById(int id)
        {
            ServiceResponse<GetProductDto> serviceResponse = new ServiceResponse<GetProductDto>();
            Product dbCharacter = await _context.Products.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetProductDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetProductDto>> UpdateProduct(UpdateProductDto updatedProduct)
        {
            ServiceResponse<GetProductDto> serviceResponse = new ServiceResponse<GetProductDto>();
            try
            {
                Product product = await _context.Products.FirstOrDefaultAsync(c => c.Id == updatedProduct.Id);
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetProductDto>(product);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetProductDto>>> DeleteProduct(int id)
        {
            ServiceResponse<List<GetProductDto>> serviceResponse = new ServiceResponse<List<GetProductDto>>();
            try
            {
                Product product = await _context.Products.FirstAsync(c => c.Id == id);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _context.Products.Select(c => _mapper.Map<GetProductDto>(c)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
