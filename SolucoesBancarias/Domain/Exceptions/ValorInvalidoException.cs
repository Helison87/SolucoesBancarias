namespace SolucoesBancarias.Domain.Exceptions
{
    public class ValorInvalidoException : DomainException
    {
        public ValorInvalidoException() : base("O valor informado é inválido, deve ser diferente de zero.") { }
    
    }
}
