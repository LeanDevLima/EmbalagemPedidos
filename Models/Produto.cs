namespace EmbalagemPedidos.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string ProdutoId { get; set; } = string.Empty;
        public int DimensoesId { get; set; }
        public Dimensoes Dimensoes { get; set; } = new Dimensoes();
        public int PedidoId { get; set; }
    }
}