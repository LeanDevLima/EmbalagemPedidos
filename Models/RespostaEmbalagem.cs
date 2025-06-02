using System.Collections.Generic;

namespace EmbalagemPedidos.Models
{
    public class RespostaEmbalagem
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido? Pedido { get; set; }
        public List<Caixa> Caixas { get; set; } = new List<Caixa>();
    }
}