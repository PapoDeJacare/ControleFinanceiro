using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class TransacaoService
{
    private readonly AppDbContext _context;

    public TransacaoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Transacao> CadastrarTransacao(Transacao transacao)
    {
        var pessoa = await _context.Pessoas.FindAsync(transacao.PessoaId);
        if(pessoa == null)
            throw new Exception("Pessoa não encontrada");

        var categoria = await _context.Categorias.FindAsync(transacao.CategoriaId);
        if(categoria == null)
            throw new Exception("Categoria não encontrada");

        if(pessoa.Idade < 18 && categoria.Finalidade == "receita")
            throw new Exception("Menores de idade não podem ter receita");

        if(transacao.Tipo != categoria.Finalidade)
            throw new Exception("Tipo de transação não condiz com categoria selecionada");

        if(transacao.Valor < 0)
            throw new Exception("Transação deve conter um valor positivo");

        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();
        return transacao;
            
    }

    public async Task<List<Transacao>> RetornarTransacoes()
    {
        return await _context.Transacoes.ToListAsync();
    }
}