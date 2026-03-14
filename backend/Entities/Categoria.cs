using System.Text.Json.Serialization;

public class Categoria
{
    public int Id {get; set;}
    public required string Descricao {get; set;}
    public required string Finalidade {get; set;}
    [JsonIgnore]
    public List<Transacao> Transacoes {get; set;} = new();
}