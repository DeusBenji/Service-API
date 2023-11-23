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
public class ComboController : ControllerBase
{
    private readonly IComboData _comboData;
    private readonly IMapper _mapper;

    public ComboController(IComboData comboData, IMapper mapper)
    {
        _comboData = comboData;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetCombos()
    {
        var combos = await _comboData.GetAllCombos();
        var comboDtos = _mapper.Map<List<ComboDto>>(combos);
        return Ok(comboDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCombo(ComboDto comboDto)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _comboData.CreateCombo(comboDto);
                return Ok("Combo created successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error creating a combo: " + ex.Message);
                return BadRequest("Error creating a combo");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetComboById(int id)
    {
        try
        {
            var combo = await _comboData.GetComboById(id);
            if (combo == null)
            {
                return NotFound();
            }
            var comboDto = _mapper.Map<ComboDto>(combo);
            return Ok(comboDto);
        }
        catch (Exception ex)
        {
            LogError("Error retrieving combo with Id" + ex.Message);
            return BadRequest("Error finding combo");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCombo(int id, ComboDto comboDto)
    {
        if (id != comboDto.Id)
        {
            return BadRequest();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _comboData.UpdateComboById(id, comboDto);
                return Ok("Combo updated successfully");
            }
            catch (Exception ex)
            {
                // Handle the exception or log it
                LogError("Error updating a combo: " + ex.Message);
                return BadRequest("Error updating a combo");
            }
        }
        return BadRequest("Invalid model state");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCombo(int id)
    {
        try
        {
            await _comboData.DeleteComboById(id);
            return Ok("Combo deleted successfully");
        }
        catch (Exception ex)
        {
            // Handle the exception or log it
            LogError("Error deleting a combo: " + ex.Message);
            return BadRequest("Error deleting a combo");
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
