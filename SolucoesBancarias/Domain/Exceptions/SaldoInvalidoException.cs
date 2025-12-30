namespace SolucoesBancarias.Domain.Exceptions
{
    public class SaldoInvalidoException : DomainException
    {
        public SaldoInvalidoException() : base("Saldo insuficiente para realizar a operação.") { }
    }
}
