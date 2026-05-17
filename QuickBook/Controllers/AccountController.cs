using Microsoft.AspNetCore.Mvc;
using QuickBook.Application.Dto.AccountDto;
using QuickBook.Application.Interface;
using QuickBook.Domain.Enums;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _services;

    public AccountController(IAccountService service)
    {
        _services = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateAccountDto dto)
    {
        var result = await _services.CreateAccountAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _services.GetAllAccountAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _services.GetAccountByIdAsync(id);
        if (result == null)
            return NotFound();
        return Ok(result);
    }

    [HttpGet("type/{type}")]
    public async Task<IActionResult> GetByTypeAsync(AccountType type)
    {
        try
        {
            var result = await _services.GetAccountByTypeAsync(type);
            return Ok(result);
        } catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateAccountDto dto)
    {
        try
        {
            var result = await _services.UpdateAccountAsync(id, dto);
            return Ok(result);
        } catch(KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _services.DeleteAccountAsync(id);
        return NoContent();
    }
}