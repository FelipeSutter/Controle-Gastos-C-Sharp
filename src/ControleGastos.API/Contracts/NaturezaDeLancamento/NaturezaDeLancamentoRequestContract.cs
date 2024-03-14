namespace ControleGastos.API.Contracts.NaturezaDeLancamento;

public class NaturezaDeLancamentoRequestContract {

    public string Descricao  { get; set; } = string.Empty;
    public string? Observacao { get; set; } = string.Empty;

}
