using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly PessoaService _pessoaService;

    public PessoasController( PessoaService pessoaService )
    {
        _pessoaService = pessoaService;
    }

    [HttpPost]
    public async Task<IActionResult> CadastroPessoa(Pessoa pessoa)
    {
        try
        {
            await _pessoaService.CadastrarPessoa(pessoa);
            return Created();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> RetornarPessoas()
    {
        try
        {
            var pessoas = await _pessoaService.RetornarPessoas();
            return Ok(pessoas);
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> RetornarPessoa(int id)
    {
        try
        {
            var pessoa = await _pessoaService.RetornarPessoa(id);
            if(pessoa == null)
                return NotFound();

            return Ok(pessoa);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditarPessoa(int id, Pessoa pessoa)
    {
        try
        {
            var pessoaAtualizada = await _pessoaService.EditarPessoa(id, pessoa);
            if(pessoaAtualizada == null)
                return NotFound();
            
            return Ok(pessoaAtualizada);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ExcluirPessoas(int id)
    {
        try
        {
            var deletado = await _pessoaService.ExcluirPessoa(id);
            if(!deletado)
                return NotFound();

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}