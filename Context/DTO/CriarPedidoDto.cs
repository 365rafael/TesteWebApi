namespace TesteWebApi.Context.DTO
{
    public class CriarPedidoDto
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public List<PedidoItemDto> ProdutosExtras { get; set; }
    }
}
