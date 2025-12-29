using SolucoesBancarias.Domain;

namespace SolucoesBancarias.Repositories
{
    public class RepositorioDeContas
    {
        // ❌ Ainda InMemory e acoplado
        private static readonly Dictionary<Guid, Conta> contas = new();

        public void Adicionar(Conta conta)
        {
            contas.Add(conta.Id, conta);
        }

        public Conta ObterPeloId(Guid id)
        {
            if (!contas.ContainsKey(id))
                throw new Exception("conta não encontrada");

            return contas[id];
        }

        public void Atualizar(Conta conta)
        {
            contas[conta.Id] = conta;
        }
    }
}
