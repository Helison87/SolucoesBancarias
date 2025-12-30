namespace SolucoesBancarias.DTOs
{
    public class DTOs
    {
        public record CriarContaDto(string Proprietario);
        public record ValorDto(decimal Valor);
        public record TransferenciaDto(Guid ContaDestino, decimal Valor);
    }
}
