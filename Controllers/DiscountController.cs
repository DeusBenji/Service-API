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
public class DiscountController : ControllerBase
{
    private readonly IDiscountData _discountData;
    private readonly IMapper _mapper;

    public DiscountController(IDiscountData discountData, IMapper mapper)
    {
        _discountData = discountData;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetDiscounts()
    {
        var discounts = await _discountData.GetAllDiscounts();
        var discountDtos = _mapper.Map<List<DiscountDto>>(discounts);
        return Ok(discountDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscount(DiscountDto discountDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _discountData.CreateDiscount(discountDto);
                return Ok("Discount created successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error creating a discount: " + ex.Message);
                return BadRequest("Error creating a discount");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDiscountById(int id)
    {
        try
        {
            var discount = await _discountData.GetDiscountById(id);
            if (discount == null)
            {
                return NotFound();
            }
            var discountDto = _mapper.Map<DiscountDto>(discount);
            return Ok(discountDto);
        }
        catch(Exception ex)
        {
            LogError("Error retrieving descount with Id" + ex.Message);
            return BadRequest("Error finding discount");
        }
       
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDiscount(int id, DiscountDto discountDto)
    {
        if (id != discountDto.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _discountData.UpdateDiscountById(id, discountDto);
                return Ok("Discount updated successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error updating a discount: " + ex.Message);
                return BadRequest("Error updating a discount");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiscount(int id)
    {
        try
        {
            await _discountData.DeleteDiscountById(id);
            return Ok("Discount deleted successfully");
        }
        catch (Exception ex)
        {
            // Handle the exception or log it
            LogError("Error deleting a discount: " + ex.Message);
            return BadRequest("Error deleting a discount");
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


