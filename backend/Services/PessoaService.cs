using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

public class PessoaService
{
    private readonly AppDbContext _context;

    public PessoaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pessoa> CadastrarPessoa(Pessoa pessoa)
    {
        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();
        return pessoa;
    }

    public async Task<List<Pessoa>> RetornarPessoas()
    {
        return await _context.Pessoas.ToListAsync();
    }

    public async Task<Pessoa?> RetornarPessoa(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if(pessoa == null)
            return null;

        return pessoa;
    }

    public async Task<Pessoa?> EditarPessoa(int id, Pessoa pessoaAtualizada)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if(pessoa == null)
            return null;
        
        pessoa.Nome = pessoaAtualizada.Nome;
        pessoa.Idade = pessoaAtualizada.Idade;

        await _context.SaveChangesAsync();

        return pessoa;
    }

    public async Task<bool> ExcluirPessoa(int id)
    {
        var pessoa = await _context.Pessoas.FindAsync(id);
        if(pessoa == null)
            return false;
        
        _context.Pessoas.Remove(pessoa);

        await _context.SaveChangesAsync();

        return true;
    }

}