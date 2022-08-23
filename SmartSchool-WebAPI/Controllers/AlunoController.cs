using Microsoft.AspNetCore.Mvc;
using SmartSchool_WebAPI.Data;
using SmartSchool_WebAPI.Models;

namespace SmartSchool_WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlunoController : ControllerBase
{
    private readonly IRepository repo;

    public AlunoController(IRepository repo)
    {
        this.repo = repo;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var result = await repo.GetAllAlunosAsync(true);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro: {ex.Message}");
        }
    }

    [HttpGet("{alunoId}")]
    public async Task<IActionResult> GetByAlunoId(int alunoId)
    {
        try
        {
            var result = await repo.GetAlunoAsyncById(alunoId, true);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro: {ex.Message}");
        }
    }

    [HttpGet("ByDisciplina/{disciplinaId}")]
    public async Task<IActionResult> GetByDisciplinaId(int disciplinaId)
    {
        try
        {
            var result = await repo.GetAlunosAsyncByDisciplinaId(disciplinaId, false);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(Aluno model)
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

    [HttpPut("{alunoId}")]
    public async Task<IActionResult> Put(int alunoId, Aluno model)
    {
        try
        {
            var aluno = await repo.GetAlunoAsyncById(alunoId, false);

            if (aluno == null) return NotFound();

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

    [HttpDelete("{alunoId}")]
    public async Task<IActionResult> Delete(int alunoId)
    {
        try
        {
            var aluno = await repo.GetAlunoAsyncById(alunoId, false);

            if (aluno == null) return NotFound();

            repo.Delete(aluno);

            await repo.SaveChangesAsync();
            return Ok(new { message = "Excluído com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro: {ex.Message}");
        }
    }
}