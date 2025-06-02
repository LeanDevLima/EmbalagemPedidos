using System.Collections.Generic;


namespace EmbalagemPedidos.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public List<Produto> Produtos { get; set; } = new List<Produto>();
    }
}