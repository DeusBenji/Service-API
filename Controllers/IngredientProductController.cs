using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using log4net.Config;
using log4net.Core;
using log4net;
using System.Reflection;

[ApiController]
[Route("api/[controller]")]
public class IngredientProductController : ControllerBase
{
    private readonly IIngredientProductData _ingredientProductData;
    private readonly IMapper _mapper;

    public IngredientProductController(IIngredientProductData ingredientProductData, IMapper mapper)
    {
        _ingredientProductData = ingredientProductData;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetIngredientProducts()
    {
        var ingredientProducts = await _ingredientProductData.GetAllIngredientProducts();
        var ingredientProductDtos = _mapper.Map<List<IngredientProductDto>>(ingredientProducts);
        return Ok(ingredientProductDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateIngredientProduct(IngredientProductDto ingredientProductDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _ingredientProductData.CreateIngredientProduct(ingredientProductDto);
                return Ok("IngredientProduct created successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error creating an IngredientProduct: " + ex.Message);
                return BadRequest("Error creating an IngredientProduct");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpGet("{productId}/{ingredientId}")]
    public async Task<IActionResult> GetIngredientProductByIds(int productId, int ingredientId)
    {
        try
        {
            var ingredientProduct = await _ingredientProductData.GetIngredientProductByIds(productId, ingredientId);
            if (ingredientProduct == null)
            {
                return NotFound();
            }
            var ingredientProductDto = _mapper.Map<IngredientProductDto>(ingredientProduct);
            return Ok(ingredientProductDto);
        }
        catch (Exception ex)
        {
            LogError("Error retrieving IngredientProduct with Ids" + ex.Message);
            return BadRequest("Error finding IngredientProduct");
        }
    }

    [HttpPut("{productId}/{ingredientId}")]
    public async Task<IActionResult> UpdateIngredientProduct(int productId, int ingredientId, IngredientProductDto ingredientProductDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _ingredientProductData.UpdateIngredientProduct(ingredientProductDto);
                return Ok("IngredientProduct updated successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error updating an IngredientProduct: " + ex.Message);
                return BadRequest("Error updating an IngredientProduct");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpDelete("{productId}/{ingredientId}")]
    public async Task<IActionResult> DeleteIngredientProduct(int productId, int ingredientId)
    {
        try
        {
            await _ingredientProductData.DeleteIngredientProduct(productId, ingredientId);
            return Ok("IngredientProduct deleted successfully");
        }
        catch (Exception ex)
        {
            // Handle the exception or log it
            LogError("Error deleting an IngredientProduct: " + ex.Message);
            return BadRequest("Error deleting an IngredientProduct");
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
