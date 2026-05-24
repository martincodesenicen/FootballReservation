using FootballReservation.Application.DTOs;
using FootballReservation.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballReservation.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FieldsController : ControllerBase
{
    private readonly IFieldService _fieldService;

    public FieldsController(IFieldService fieldService)
    {
        _fieldService = fieldService;
    }

    [HttpGet] // Público: Cualquiera puede listar las canchas disponibles
    public async Task<IActionResult> GetAll()
    {
        var fields = await _fieldService.GetAllFieldsAsync();
        return Ok(fields);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var field = await _fieldService.GetFieldByIdAsync(id);
        if (field == null) return NotFound(new { Message = "La cancha no existe." });
        
        return Ok(field);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")] // Restringido: Solo administradores pueden agregar canchas.
    public async Task<IActionResult> Create([FromBody] CreateFieldDto createFieldDto)
    {
        var newField = await _fieldService.CreateFieldAsync(createFieldDto);
        return CreatedAtAction(nameof(GetById), new { id = newField.Id }, newField);
    }
}