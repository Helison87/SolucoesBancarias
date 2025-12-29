using Microsoft.AspNetCore.Mvc;

namespace SolucoesBancarias.Controllers
{
    [ApiController]
    [Route("api/banco")]
    public class BancoController : ControllerBase
    {
        // ❌ Estado compartilhado no controller
        private static Dictionary<Guid, decimal> contas = new();

        // ❌ Criação de conta misturada com lógica
        [HttpPost("criar")]
        public IActionResult Criar(string name)
        {
            if (name == null || name == "")
            {
                throw new Exception("nome invalido");
            }

            var id = Guid.NewGuid();

            // ❌ Lógica de inicialização espalhada
            contas.Add(id, 0);

            return Ok(id);
        }

        // ❌ Regra de negócio no controller
        [HttpPost("depositar")]
        public IActionResult Deposit(Guid id, decimal value)
        {
            if (!contas.ContainsKey(id))
            {
                throw new Exception("Conta não existe");
            }

            if (value <= 0)
            {
                throw new Exception("valor invalido");
            }

            // ❌ Manipulação direta de dados
            contas[id] = contas[id] + value;

            return Ok(contas[id]);
        }

        // ❌ Lógica duplicada
        [HttpPost("sacar")]
        public IActionResult Withdraw(Guid id, decimal value)
        {
            if (!contas.ContainsKey(id))
            {
                throw new Exception("Conta não existe");
            }

            if (value <= 0)
            {
                throw new Exception("valor invalido");
            }

            if (contas[id] < value)
            {
                throw new Exception("sem saldo");
            }

            contas[id] = contas[id] - value;

            return Ok(contas[id]);
        }

        // ❌ Método gigante fazendo tudo
        [HttpPost("transferir")]
        public IActionResult Transfer(Guid from, Guid to, decimal value)
        {
            if (!contas.ContainsKey(from))
                throw new Exception("conta do saque não existe");

            if (!contas.ContainsKey(to))
                throw new Exception("conta de destino não existe");

            if (value <= 0)
                throw new Exception("valor invalido");

            if (contas[from] < value)
                throw new Exception("Sem saldo");

            contas[from] -= value;
            contas[to] += value;

            return Ok("Transferência concluída");
        }

        // ❌ Endpoint com múltiplas responsabilidades
        [HttpGet("saldo")]
        public IActionResult Balance(Guid id)
        {
            if (!contas.ContainsKey(id))
            {
                return BadRequest("Conta não encontrada");
            }

            return Ok(contas[id]);
        }
    }
}
