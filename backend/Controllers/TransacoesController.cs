using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly TransacaoService _transacaoService;

    public TransacoesController(TransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarTransacao(Transacao transacao)
    {
        try
        {
            var resultado = _transacaoService.CadastrarTransacao(transacao);
            return Ok(resultado);
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> RetornarTransacoes()
    {
        try
        {
            var transacoes = await _transacaoService.RetornarTransacoes();
            return Ok(transacoes);
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}