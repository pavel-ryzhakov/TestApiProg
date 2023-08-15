
using Microsoft.AspNetCore.Mvc;
using TestApiProg.Data;
using TestApiProg.Dtos;
using TestApiProg.Models;
using TestApiProg.Services;


namespace TestApiProg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TestAPIContext _context;
        private readonly IProductService _productService;

        public UsersController(TestAPIContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _productService.GetProductById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddProductDto newProduct)
        {
            return Ok(await _productService.AddProduct(newProduct));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateProductDto updatedProduct)
        {
            ServiceResponse<GetProductDto> response = await _productService.UpdateProduct(updatedProduct);
            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<GetProductDto>> response = await _productService.DeleteProduct(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }


        private bool UserExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
