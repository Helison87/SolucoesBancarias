namespace SolucoesBancarias.Domain
{
    public class Conta
    {
        public Guid Id { get; private set; }
        public string Proprietario { get; private set; }
        public decimal Saldo { get; private set; }

        public Conta(string proprietario)
        {
            Id = Guid.NewGuid();
            Proprietario = proprietario;
            Saldo = 0;
        }

        public void Depositar(decimal valor)
        {
            if (valor <= 0)
                throw new Exception("valor invalido");

            Saldo += valor;
        }

        public void Sacar(decimal valor)
        {
            if (valor <= 0)
                throw new Exception("valor invalido");

            if (Saldo < valor)
                throw new Exception("saldo insuficiente");

            Saldo -= valor;
        }
    }
}
