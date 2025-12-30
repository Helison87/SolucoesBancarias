using SolucoesBancarias.Domain;
using SolucoesBancarias.Repositories;

namespace SolucoesBancarias.Service
{
    public class ServicosBancarios : IServicosBancarios
    {
        private readonly IRepositorioDeContas _repositorio;

        public ServicosBancarios(IRepositorioDeContas repositorio)
        {
            _repositorio = repositorio;
        }

        public Guid CriarConta(string proprietario)
        {
            var conta = new Conta(proprietario);
            _repositorio.Adicionar(conta);
            return conta.Id;
        }

        public decimal Saldo(Guid contaId) => _repositorio.ObterPeloId(contaId).Saldo;

        public decimal Depositar(Guid contaId, decimal valor)
        {
            var conta = _repositorio.ObterPeloId(contaId);
            conta.Depositar(valor);
            _repositorio.Atualizar(conta);

            return conta.Saldo;
        }

        public decimal Sacar(Guid contaId, decimal valor)
        {
            var conta = _repositorio.ObterPeloId(contaId);
            conta.Sacar(valor);
            _repositorio.Atualizar(conta);

            return conta.Saldo;
        }

        public void Transferir(Guid contaId, Guid contaDestinoId, decimal valor)
        {
            var contaSaque = _repositorio.ObterPeloId(contaId);
            var contaDestino = _repositorio.ObterPeloId(contaDestinoId);

            contaSaque.Sacar(valor);
            contaDestino.Depositar(valor);

            _repositorio.Atualizar(contaSaque);
            _repositorio.Atualizar(contaDestino);
        }
    }
}
