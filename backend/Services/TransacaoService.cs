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
        return await _context.Transacoes
            .Include(t => t.Pessoa)
            .Include(t => t.Categoria)
            .ToListAsync();
    }

    public async Task<RelatorioPessoaDTO> RetornarRelatorioPorPessoa()
    {
        var pessoas = await RetornarTotaisPorPessoa();
        var totalGeral = new TotalGeralDto
        {
            TotalReceitas = pessoas.Sum(p => p.TotalReceitas),
            TotalDespesas = pessoas.Sum(p => p.TotalDespesas),
            Saldo = pessoas.Sum(p => p.Saldo)
        };

        var relatorio = new RelatorioPessoaDTO
        {
            Pessoas = pessoas,
            TotalGeral = totalGeral
        };

        return relatorio;

    }

    public async Task<List<PessoaComTotalDTO>> RetornarTotaisPorPessoa()
    {
        var resultado = await _context.Pessoas
            .Select(p => new PessoaComTotalDTO
            {
                Nome = p.Nome,
                Idade = p.Idade,
                TotalReceitas = _context.Transacoes
                    .Where(t => t.PessoaId == p.Id && t.Tipo == "receita")
                    .Sum(t => (decimal?)t.Valor) ?? 0,

                TotalDespesas = _context.Transacoes
                    .Where(t => t.PessoaId == p.Id && t.Tipo == "despesa")
                    .Sum(t => (decimal?)t.Valor) ?? 0
            })
            .ToListAsync();

        foreach(var item in resultado)
            item.Saldo = item.TotalReceitas - item.TotalDespesas;

        return resultado;
    }

    public async Task<RelatorioCategoriaDTO> RetornarRelatorioPorCategoria()
    {
        var categorias = await RetornarTotaisPorCategoria();
        var totalGeral = new CategoriaTotalGeralDTO
        {
            TotalReceitas = categorias.Sum(c => c.TotalReceitas),
            TotalDespesas = categorias.Sum(c => c.TotalDespesas),
            Saldo = categorias.Sum(c => c.Saldo)
        };

        var relatorio = new RelatorioCategoriaDTO
        {
            Categorias = categorias,
            TotalGeral = totalGeral
        };

        return relatorio;


    }

    public async Task<List<CategoriaComTotalDTO>> RetornarTotaisPorCategoria()
    {
        var resultado = await _context.Categorias
            .Select(c => new CategoriaComTotalDTO
            {
                Descricao = c.Descricao,
                Finalidade = c.Finalidade,
                TotalReceitas = _context.Transacoes
                    .Where(t => t.CategoriaId == c.Id && t.Tipo == "receita")
                    .Sum(t => (decimal?)t.Valor) ?? 0,
                TotalDespesas = _context.Transacoes
                    .Where(t => t.CategoriaId == c.Id && t.Tipo == "despesa")
                    .Sum(t => (decimal?)t.Valor) ?? 0
            }).ToListAsync();

        foreach(var item in resultado)
            item.Saldo = item.TotalReceitas - item.TotalDespesas;

        return resultado;
    }
}