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
public class IngredientOrderlineController : ControllerBase
{
    private readonly IIngredientOrderlineData _ingredientOrderlineData;
    private readonly IMapper _mapper;

    public IngredientOrderlineController(IIngredientOrderlineData ingredientOrderlineData, IMapper mapper)
    {
        _ingredientOrderlineData = ingredientOrderlineData;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetIngredientOrderlines()
    {
        var ingredientOrderlines = await _ingredientOrderlineData.GetAllIngredientOrderlines();
        var ingredientOrderlineDtos = _mapper.Map<List<IngredientOrderlineDto>>(ingredientOrderlines);
        return Ok(ingredientOrderlineDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateIngredientOrderline(IngredientOrderlineDto ingredientOrderlineDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _ingredientOrderlineData.CreateIngredientOrderline(ingredientOrderlineDto);
                return Ok("IngredientOrderline created successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error creating an IngredientOrderline: " + ex.Message);
                return BadRequest("Error creating an IngredientOrderline");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpGet("{ingredientId}/{orderlineId}")]
    public async Task<IActionResult> GetIngredientOrderlineByIds(int ingredientId, int orderlineId)
    {
        try
        {
            var ingredientOrderline = await _ingredientOrderlineData.GetIngredientOrderlineById(ingredientId, orderlineId);
            if (ingredientOrderline == null)
            {
                return NotFound();
            }
            var ingredientOrderlineDto = _mapper.Map<IngredientOrderlineDto>(ingredientOrderline);
            return Ok(ingredientOrderlineDto);
        }
        catch (Exception ex)
        {
            LogError("Error retrieving IngredientOrderline with Ids" + ex.Message);
            return BadRequest("Error finding IngredientOrderline");
        }
    }

    [HttpPut("{ingredientId}/{orderlineId}")]
    public async Task<IActionResult> UpdateIngredientOrderline(int ingredientId, int orderlineId, IngredientOrderlineDto ingredientOrderlineDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _ingredientOrderlineData.UpdateIngredientOrderlineById(ingredientOrderlineDto);
                return Ok("IngredientOrderline updated successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error updating an IngredientOrderline: " + ex.Message);
                return BadRequest("Error updating an IngredientOrderline");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpDelete("{ingredientId}/{orderlineId}")]
    public async Task<IActionResult> DeleteIngredientOrderline(int ingredientId, int orderlineId)
    {
        try
        {
            await _ingredientOrderlineData.DeleteIngredientOrderlineById(ingredientId, orderlineId);
            return Ok("IngredientOrderline deleted successfully");
        }
        catch (Exception ex)
        {
            // Handle the exception or log it
            LogError("Error deleting an IngredientOrderline: " + ex.Message);
            return BadRequest("Error deleting an IngredientOrderline");
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
