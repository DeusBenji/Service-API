using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service_Api.BusinessLogicLayer.Interfaces;
using Service_Api.DTOs;

[ApiController]
[Route("api/customergroups")]
public class CustomerGroupController : ControllerBase
{
    private readonly ICustomerGroupData _customerGroupData;
    private readonly IMapper _mapper;

    public CustomerGroupController(ICustomerGroupData customerGroupData, IMapper mapper)
    {
        _customerGroupData = customerGroupData;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAllCustomerGroups()
    {
        var customerGroups = _customerGroupData.GetAllCustomerGroups();
        var customerGroupDtos = _mapper.Map<List<CustomerGroupDto>>(customerGroups);

        if (customerGroupDtos.Count > 0)
        {
            return Ok(customerGroupDtos);
        }
        else
        {
            return NotFound("No customer groups found.");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetCustomerGroupById(int id)
    {
        var customerGroup = _customerGroupData.GetCustomerGroupById(id);
        if (customerGroup != null)
        {
            var customerGroupDto = _mapper.Map<CustomerGroupDto>(customerGroup);
            return Ok(customerGroupDto);
        }
        else
        {
            return NotFound("Customer group not found.");
        }
    }

    [HttpPost]
    public IActionResult CreateCustomerGroup([FromBody] CustomerGroupDto customerGroupDto)
    {
        int id;
        var createdId = _customerGroupData.CreateCustomerGroup(customerGroupDto);
        if (createdId > 0)
        {
            var customerGrp = _customerGroupData.GetCustomerGroupById(createdId);

            return CreatedAtAction(nameof(GetCustomerGroupById), new { id = createdId }, customerGrp);
        }
        else
        {
            return BadRequest("Failed to create customer group.");
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCustomerGroup(int id, [FromBody] CustomerGroupDto customerGroupDto)
    {
        if (_customerGroupData.UpdateCustomerGroupById(id, customerGroupDto))
        {
            return (Ok("Customer updated" + " Id: "+ id + " Navn: " + customerGroupDto.Name ));
        }
        else
        {
            return NotFound("Customer group not found or failed to update.");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCustomerGroup(int id)
    {
        if (_customerGroupData.DeleteCustomerGroupById(id))
        {
            return (Ok("Customer Deleted"));
        }
        else
        {
            return NotFound("Customer group not found or failed to delete.");
        }
    }
}
