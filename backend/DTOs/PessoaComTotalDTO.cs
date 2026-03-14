public class PessoaComTotalDTO
{
    public required string Nome { get; set; }
    public int Idade { get; set; }
    public decimal TotalReceitas { get; set; }
    public decimal TotalDespesas { get; set; }
    public decimal Saldo { get; set; }
}