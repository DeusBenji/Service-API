using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;
using ServiceData.ModelLayer;

namespace Service_Api.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientData _ingredientData;

        public IngredientController(IIngredientData ingredientData)
        {
            _ingredientData = ingredientData;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _ingredientData.GetAllIngredients();
            return Ok(ingredients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngredientById(int id)
        {
            var ingredient = await _ingredientData.GetIngredientById(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return Ok(ingredient);
        }

        [HttpPost]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientDto ingredientDto)
        {
            if (ingredientDto == null)
            {
                return BadRequest("Invalid data");
            }

            var insertedId = await _ingredientData.CreateIngredient(ingredientDto);
            return CreatedAtAction(nameof(GetIngredientById), new { id = insertedId }, ingredientDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIngredient(int id, [FromBody] IngredientDto ingredientDto)
        {
            if (ingredientDto == null)
            {
                return BadRequest("Invalid data");
            }

            var success = await _ingredientData.UpdateIngredientById(id, ingredientDto);
            if (success)
            {
                return Ok("Combo updated successfully");
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            var success = await _ingredientData.DeleteIngredientById(id);
            if (success)
            {
                return Ok("Combo deleted successfully");
            }
            return NotFound();
        }
        [HttpGet("product/{id}")]
        public async Task<ActionResult<List<Ingredient>>> GetIngredientsByProductId(int id)
        {
            try
            {
                var ingredients = await _ingredientData.GetIngredientsByProductId(id);

                if (ingredients == null || ingredients.Count == 0)
                {
                    return NotFound("No ingredients found for the specified product.");
                }

                return Ok(ingredients);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "Internal server error");
            }

        }
    }
}
