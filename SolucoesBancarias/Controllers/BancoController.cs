using Microsoft.AspNetCore.Mvc;
using SolucoesBancarias.Service;

namespace SolucoesBancarias.Controllers
{
    [ApiController]
    [Route("api/banco")]
    public class BancoController : ControllerBase
    {
        private readonly IServicosBancarios _servico;

        public BancoController(IServicosBancarios servico)
        {
            _servico = servico;
        }

        [HttpPost("criar")]
        public IActionResult Criar(string proprietario)
        {
            return Ok(_servico.CriarConta(proprietario));
        }

        [HttpPost("depositar")]
        public IActionResult Depositar(Guid id, decimal valor)
        {
            return Ok(_servico.Depositar(id, valor));
        }

        [HttpPost("sacar")]
        public IActionResult Sacar(Guid id, decimal valor)
        {
            return Ok(_servico.Sacar(id, valor));
        }

        [HttpPost("transferir")]
        public IActionResult Transferir(Guid contaSaque, Guid contaDestino, decimal valor)
        {
            _servico.Transferir(contaSaque, contaDestino, valor);
            return Ok("Transferência concluída");
        }

        [HttpGet("saldo")]
        public IActionResult ObterSaldo(Guid id)
        {
            return Ok(_servico.Saldo(id));
        }
    }
}
