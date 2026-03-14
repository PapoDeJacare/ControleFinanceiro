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
            var resultado = await _transacaoService.CadastrarTransacao(transacao);
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

    [HttpGet("totais-por-pessoa")]
    public async Task<IActionResult> RetornarTotaisPorPessoa()
    {
        var resultado = await _transacaoService.RetornarRelatorioPorPessoa();
        return Ok(resultado);
    }

    [HttpGet("totais-por-categoria")]
    public async Task<IActionResult> RetornarTotaisPorCategoria()
    {
        var resultado = await _transacaoService.RetornarRelatorioPorCategoria();
        return Ok(resultado);
    }
}