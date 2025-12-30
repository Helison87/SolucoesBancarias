namespace SolucoesBancarias.Domain.Exceptions
{
    public class ContaNaoEncontradaException : DomainException
    {
        public ContaNaoEncontradaException() : base("Conta não encontrada.") { }
    }
}
