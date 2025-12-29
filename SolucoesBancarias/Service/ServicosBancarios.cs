using SolucoesBancarias.Domain;
using SolucoesBancarias.Repositories;

namespace SolucoesBancarias.Service
{
    public class ServicosBancarios
    {
        private readonly RepositorioDeContas _repositorio;

        public ServicosBancarios()
        {
            _repositorio = new RepositorioDeContas();
        }

        public Guid CriarConta(string proprietario)
        {
            var conta = new Conta(proprietario);
            _repositorio.Adicionar(conta);
            return conta.Id;
        }

        public decimal Saldo(Guid contaId)
        {
            var conta = _repositorio.ObterPeloId(contaId);
            return conta.Saldo;
        }

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

        public void Transferir(Guid contaSaqueId, Guid contaDestinoId, decimal valor)
        {
            var contaSaque = _repositorio.ObterPeloId(contaSaqueId);
            var contaDestino = _repositorio.ObterPeloId(contaDestinoId);

            contaSaque.Sacar(valor);
            contaDestino.Depositar(valor);

            _repositorio.Atualizar(contaSaque);
            _repositorio.Atualizar(contaDestino);
        }
    }
}
