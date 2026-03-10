public class Transacao
{
    public int Id {get; set;}
    public required string Descricao {get; set;}
    public decimal Valor {get; set;}
    public required string Tipo {get; set;}
    public int CategoriaId {get; set;}
    public Categoria Categoria {get; set;} = null!;
    public int PessoaId {get; set;}
    public Pessoa Pessoa {get; set;} = null!;
}