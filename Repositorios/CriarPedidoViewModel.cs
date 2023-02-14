namespace TesteWebApi.Repositorios
{
    public class CriarPedidoViewModel
    {
        public int UsuarioId { get; set; }
        public List<CriarPedidoItemViewModel> Produtos { get; set; }
    }

    public class CriarPedidoItemViewModel
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}
