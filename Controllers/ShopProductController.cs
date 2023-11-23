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
public class ShopProductController : ControllerBase
{
    private readonly IShopProductData _shopProductData;
    private readonly IMapper _mapper;

    public ShopProductController(IShopProductData shopProductData, IMapper mapper)
    {
        _shopProductData = shopProductData;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetShopProducts()
    {
        var shopProducts = await _shopProductData.GetAllShopProducts();
        var shopProductDtos = _mapper.Map<List<ShopProductDto>>(shopProducts);
        return Ok(shopProductDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShopProduct(ShopProductDto shopProductDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _shopProductData.CreateShopProduct(shopProductDto);
                return Ok("ShopProduct created successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error creating a ShopProduct: " + ex.Message);
                return BadRequest("Error creating a ShopProduct");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpGet("{shopId}/{productId}")]
    public async Task<IActionResult> GetShopProductByIds(int shopId, int productId)
    {
        try
        {
            var shopProduct = await _shopProductData.GetShopProductByIds(shopId, productId);
            if (shopProduct == null)
            {
                return NotFound();
            }
            var shopProductDto = _mapper.Map<ShopProductDto>(shopProduct);
            return Ok(shopProductDto);
        }
        catch (Exception ex)
        {
            LogError("Error retrieving ShopProduct with Ids" + ex.Message);
            return BadRequest("Error finding ShopProduct");
        }
    }

    [HttpPut("{shopId}/{productId}")]
    public async Task<IActionResult> UpdateShopProduct(int shopId, int productId, ShopProductDto shopProductDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _shopProductData.UpdateShopProductByIds(shopId, productId, shopProductDto);
                return Ok("ShopProduct updated successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error updating a ShopProduct: " + ex.Message);
                return BadRequest("Error updating a ShopProduct");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpDelete("{shopId}/{productId}")]
    public async Task<IActionResult> DeleteShopProduct(int shopId, int productId)
    {
        try
        {
            await _shopProductData.DeleteShopProductByIds(shopId, productId);
            return Ok("ShopProduct deleted successfully");
        }
        catch (Exception ex)
        {
            // Handle the exception or log it
            LogError("Error deleting a ShopProduct: " + ex.Message);
            return BadRequest("Error deleting a ShopProduct");
        }
    }
    [HttpGet("ByShopId/{shopId}")]
    public async Task<IActionResult> GetShopProductsByShopId(int shopId)
    {
        try
        {
            var shopProducts = await _shopProductData.GetShopProductsByShopId(shopId);
            var shopProductDtos = _mapper.Map<List<ShopProductDto>>(shopProducts);
            return Ok(shopProductDtos);
        }
        catch (Exception ex)
        {
            LogError("Error retrieving ShopProducts by ShopId" + ex.Message);
            return BadRequest("Error finding ShopProducts");
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