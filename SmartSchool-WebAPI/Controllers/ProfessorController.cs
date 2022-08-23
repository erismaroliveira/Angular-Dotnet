using Microsoft.AspNetCore.Mvc;
using SmartSchool_WebAPI.Data;
using SmartSchool_WebAPI.Models;

namespace SmartSchool_WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfessorController : ControllerBase
{
    public readonly IRepository repo;

    public ProfessorController(IRepository repo)
    {
        this.repo = repo; 
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await repo.GetAllProfessoresAsync(true);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro: {ex.Message}");
        }
    }

    [HttpGet("{professorId}")]
    public async Task<IActionResult> GetByProfessorId(int professorId)
    {
        try
        {
            var result = await repo.GetProfessorAsyncById(professorId, false);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro: {ex.Message}");
        }
    }

    [HttpGet("ByAluno/{alunoId}")]
    public async Task<IActionResult> ByAluno(int alunoId)
    {
        try
        {
            var result = await repo.GetProfessoresAsyncByAlunoId(alunoId, true);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(Professor model)
    {
        try
        {
            if (model != null)
                repo.Add(model);
            
            await repo.SaveChangesAsync();
            return Ok(model);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro: {ex.Message}");
        }
    }

    [HttpPut("{professorId}")]
    public async Task<IActionResult> Put(int professorId, Professor model)
    {
        try
        {
            var professor = await repo.GetProfessorAsyncById(professorId, false);

            if (professor == null) return NotFound();

            if (model != null)
                repo.Update(model);
            
            await repo.SaveChangesAsync();
            return Ok(model);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro: {ex.Message}");
        }
    }

    [HttpDelete("{professorId}")]
    public async Task<IActionResult> Delete(int professorId)
    {
        try
        {
            var professor = await repo.GetProfessorAsyncById(professorId, false);

            if (professor == null) return NotFound();

            repo.Delete(professor);
            
            await repo.SaveChangesAsync();
            return Ok("Excluído com sucesso!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro: {ex.Message}");
        }
    }
}