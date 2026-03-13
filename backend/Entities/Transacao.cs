using System.Text.Json.Serialization;

public class Transacao
{
    public int Id {get; set;}
    public required string Descricao {get; set;}
    public decimal Valor {get; set;}
    public required string Tipo {get; set;}
    public int CategoriaId {get; set;}

    [JsonIgnore]
    public Categoria? Categoria {get; set;}

    public int PessoaId {get; set;}

    [JsonIgnore]
    public Pessoa? Pessoa {get; set;}
}