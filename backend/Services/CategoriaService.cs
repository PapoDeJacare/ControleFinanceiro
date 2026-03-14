using Microsoft.EntityFrameworkCore;

/*
    Service responsável pela lógica de negócio das categorias.
    Aqui é realizado o cadastro e retorno de categorias
*/

public class CategoriaService
{
    private readonly AppDbContext _context;

    public CategoriaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria> CadastrarCategoria(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        await _context.SaveChangesAsync();
        return categoria;
    }

    public async Task<List<Categoria>> RetornarCategorias()
    {
        return await _context.Categorias.Include(c => c.Transacoes).ToListAsync();
    }
}