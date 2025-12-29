using Microsoft.AspNetCore.Mvc;
using SolucoesBancarias.Service;

namespace SolucoesBancarias.Controllers
{
    [ApiController]
    [Route("api/banco")]
    public class BancoController : ControllerBase
    {

        private readonly ServicosBancarios _service;

        public BancoController()
        {
            // ❌ Acoplamento forte (vamos resolver depois)
            _service = new ServicosBancarios();
        }

        [HttpPost("criar")]
        public IActionResult Criar(string name)
        {
            var id = _service.CriarConta(name);
            return Ok(id);
        }

        [HttpPost("depositar")]
        public IActionResult Deposit(Guid id, decimal value)
        {
            var balance = _service.Depositar(id, value);
            return Ok(balance);
        }

        [HttpPost("sacar")]
        public IActionResult Withdraw(Guid id, decimal value)
        {
            var balance = _service.Sacar(id, value);
            return Ok(balance);
        }

        [HttpPost("transferir")]
        public IActionResult Transfer(Guid from, Guid to, decimal value)
        {
            _service.Transferir(from, to, value);
            return Ok("Transferência concluída");
        }

        [HttpGet("saldo")]
        public IActionResult Balance(Guid id)
        {
            var balance = _service.Saldo(id);
            return Ok(balance);
        }
    }
}
