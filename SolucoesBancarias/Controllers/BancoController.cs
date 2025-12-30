using Microsoft.AspNetCore.Mvc;
using SolucoesBancarias.Service;
using static SolucoesBancarias.DTOs.DTOs;

namespace SolucoesBancarias.Controllers
{
    [ApiController]
    [Route("api/contas")]
    public class BancoController : ControllerBase
    {
        private readonly IServicosBancarios _servico;

        public BancoController(IServicosBancarios servico)
        {
            _servico = servico;
        }

        [HttpPost("criar")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public IActionResult Criar(CriarContaDto dto)
        {
            return Ok(_servico.CriarConta(dto.Proprietario));
        }

        [HttpPost("{id}/depositar")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        public IActionResult Depositar(Guid id, ValorDto dto)
        {
            return Ok(_servico.Depositar(id, dto.Valor));
        }

        [HttpPost("{id}/sacar")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        public IActionResult Sacar(Guid id, ValorDto dto)
        {
            return Ok(_servico.Sacar(id, dto.Valor));
        }

        [HttpPost("{id}/transferir")]
        public IActionResult Transferir(Guid id, TransferenciaDto dto)
        {
            _servico.Transferir(id, dto.ContaDestino, dto.Valor);
            return NoContent();
        }

        [HttpGet("{id}/saldo")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        public IActionResult ObterSaldo(Guid id)
        {
            return Ok(_servico.Saldo(id));
        }
    }
}
