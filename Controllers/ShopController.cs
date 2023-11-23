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

[ApiController]
[Route("api/[controller]")]
public class ShopController : ControllerBase
{
    private readonly IShopData _shopData;
    private readonly IMapper _mapper;

    public ShopController(IShopData shopData, IMapper mapper)
    {
        _shopData = shopData;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetShops()
    {
        var shops = await _shopData.GetAllShops();
        var shopDtos = _mapper.Map<List<ShopDto>>(shops);
        return Ok(shopDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateShop(ShopDto shopDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _shopData.CreateShop(shopDto);
                return Ok("Shop created successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error creating a shop: " + ex.Message);
                return BadRequest("Error creating a shop");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShopById(int id)
    {
        try
        {
            var shop = await _shopData.GetShopById(id);
            if (shop == null)
            {
                return NotFound();
            }
            var shopDto = _mapper.Map<ShopDto>(shop);
            return Ok(shopDto);
        }
        catch (Exception ex)
        {
            LogError("Error retrieving shop with Id" + ex.Message);
            return BadRequest("Error finding shop");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateShop(int id, ShopDto shopDto)
    {
        if (id != shopDto.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _shopData.UpdateShopById(id, shopDto);
                return Ok("Shop updated successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error updating a shop: " + ex.Message);
                return BadRequest("Error updating a shop");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteShop(int id)
    {
        try
        {
            await _shopData.DeleteShopById(id);
            return Ok("Shop deleted successfully");
        }
        catch (Exception ex)
        {
            // Handle the exception or log it
            LogError("Error deleting a shop: " + ex.Message);
            return BadRequest("Error deleting a shop");
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
