namespace SolucoesBancarias.Service
{
    public class ServicosBancarios
    {
        // ❌ Ainda ruim, mas agora isolado
        private static Dictionary<Guid, decimal> contas = new();

        public Guid CriarConta(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("nome invalido");

            var id = Guid.NewGuid();
            contas.Add(id, 0);

            return id;
        }

        public decimal Saldo(Guid contaId)
        {
            if (!contas.ContainsKey(contaId))
                throw new Exception("Conta não existe");

            return contas[contaId];
        }

        public decimal Depositar(Guid contaId, decimal valor)
        {
            if (!contas.ContainsKey(contaId))
                throw new Exception("Conta não existe");

            if (valor <= 0)
                throw new Exception("valor invalido");

            contas[contaId] += valor;
            return contas[contaId];
        }

        public decimal Sacar(Guid contaId, decimal valor)
        {
            if (!contas.ContainsKey(contaId))
                throw new Exception("Conta não existe");

            if (valor <= 0)
                throw new Exception("valor invalido");

            if (contas[contaId] < valor)
                throw new Exception("sem saldo");

            contas[contaId] -= valor;
            return contas[contaId];
        }

        public void Transferir(Guid contaSaqueId, Guid contaDestinoId, decimal valor)
        {
            if (!contas.ContainsKey(contaSaqueId))
                throw new Exception("conta do saque não existe");

            if (!contas.ContainsKey(contaDestinoId))
                throw new Exception("conta de destino não existe");

            if (valor <= 0)
                throw new Exception("valor invalido");

            if (contas[contaSaqueId] < valor)
                throw new Exception("sem saldo");

            contas[contaSaqueId] -= valor;
            contas[contaDestinoId] += valor;
        }
    }
}
