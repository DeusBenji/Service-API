using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using System;
using System.Threading.Tasks;
using log4net.Config;
using log4net.Core;
using log4net;
using System.Reflection;
using ServiceData.ModelLayer;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly IProductData _productData;
    private readonly IMapper _mapper;

    public ProductController(IProductData productData, IMapper mapper)
    {
        _productData = productData;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productData.GetAllProducts();
        var productDtos = _mapper.Map<List<ProductDto>>(products);
        return Ok(productDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(ProductDto productDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _productData.CreateProduct(productDto);
                return Ok("Product created successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error creating a product: " + ex.Message);
                return BadRequest("Error creating a product");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        try
        {
            var product = await _productData.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }
        catch (Exception ex)
        {
            LogError("Error retrieving product with Id" + ex.Message);
            return BadRequest("Error finding product");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto)
    {
        if (id != productDto.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _productData.UpdateProductById(id, productDto);
                return Ok("Product updated successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error updating a product: " + ex.Message);
                return BadRequest("Error updating a product");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            await _productData.DeleteProductById(id);
            return Ok("Product deleted successfully");
        }
        catch (Exception ex)
        {
            // Handle the exception or log it
            LogError("Error deleting a product: " + ex.Message);
            return BadRequest("Error deleting a product");
        }
    }

    [HttpGet("shops/{id}")]
    public async Task<ActionResult<List<Product>>> GetProductsByShopId(int id)
    {
        try
        {
            var products = await _productData.GetProductsByShopId(id);

            if (products == null || products.Count == 0)
            {
                return NotFound("No products found for the specified shop.");
            }

            return Ok(products);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it accordingly
            return StatusCode(500, "Internal server error");
        }
    }

    private void LogError(string message)
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
        ILog _logger = LogManager.GetLogger(typeof(LoggerManager));
        _logger.Info(message);
    }
}
