using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly CategoriaService _categoriaService;

    public CategoriasController(CategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpPost]
    public async Task<IActionResult> CadastrarCategoria(Categoria categoria)
    {
        try
        {
            await _categoriaService.CadastrarCategoria(categoria);
            return Created();
        }
        catch(Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> RetornarCategorias()
    {
        try
        {
            var categorias = await _categoriaService.RetornarCategorias();
            return Ok(categorias);
        }
        catch( Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}