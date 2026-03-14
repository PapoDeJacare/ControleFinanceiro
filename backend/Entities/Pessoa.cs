using System.Text.Json.Serialization;

public class Pessoa
{
    public int Id {get; set;}
    public required string Nome {get; set;}
    public int Idade{get; set;}
    [JsonIgnore]
    public List<Transacao> Transacoes {get; set;} = new();
}