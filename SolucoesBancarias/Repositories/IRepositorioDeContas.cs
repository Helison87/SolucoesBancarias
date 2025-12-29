using SolucoesBancarias.Domain;

namespace SolucoesBancarias.Repositories
{
    public interface IRepositorioDeContas
    {
        void Adicionar(Conta conta);
        Conta ObterPeloId(Guid id);
        void Atualizar(Conta conta);
    }
}
