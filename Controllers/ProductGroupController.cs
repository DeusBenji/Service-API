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
public class ProductGroupController : ControllerBase
{
    private readonly IProductGroupData _productGroupData;
    private readonly IMapper _mapper;

    public ProductGroupController(IProductGroupData productGroupData, IMapper mapper)
    {
        _productGroupData = productGroupData;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductGroups()
    {
        var productGroups = await _productGroupData.GetAllProductGroups();
        var productGroupDtos = _mapper.Map<List<ProductGroupDto>>(productGroups);
        return Ok(productGroupDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductGroup(ProductGroupDto productGroupDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _productGroupData.CreateProductGroup(productGroupDto);
                return Ok("Product Group created successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error creating a Product Group: " + ex.Message);
                return BadRequest("Error creating a Product Group");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductGroupById(int id)
    {
        try
        {
            var productGroup = await _productGroupData.GetProductGroupById(id);
            if (productGroup == null)
            {
                return NotFound();
            }
            var productGroupDto = _mapper.Map<ProductGroupDto>(productGroup);
            return Ok(productGroupDto);
        }
        catch (Exception ex)
        {
            LogError("Error retrieving Product Group with Id: " + ex.Message);
            return BadRequest("Error finding Product Group");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProductGroup(int id, ProductGroupDto productGroupDto)
    {
        if (id != productGroupDto.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _productGroupData.UpdateProductGroupById(id, productGroupDto);
                return Ok("Product Group updated successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error updating a Product Group: " + ex.Message);
                return BadRequest("Error updating a Product Group");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductGroup(int id)
    {
        try
        {
            await _productGroupData.DeleteProductGroupById(id);
            return Ok("Product Group deleted successfully");
        }
        catch (Exception ex)
        {
            // Handle the exception or log it
            LogError("Error deleting a Product Group: " + ex.Message);
            return BadRequest("Error deleting a Product Group");
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
