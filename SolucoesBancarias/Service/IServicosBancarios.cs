namespace SolucoesBancarias.Service
{
    public interface IServicosBancarios
    {
        Guid CriarConta(string proprietario);
        decimal Saldo(Guid contaId);
        decimal Depositar(Guid contaId, decimal valor);
        decimal Sacar(Guid contaId, decimal valor);
        void Transferir(Guid contaId, Guid contaDestinoId, decimal valor);
    }
}
