using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EmbalagemPedidos.Models
{
    public class PedidosWrapper
    {
        [JsonPropertyName("pedidos")]
        public List<PedidoInput> Pedidos { get; set; } = new();
    }

    public class PedidoInput
    {
        [JsonPropertyName("pedido_id")]
        public int PedidoId { get; set; }

        [JsonPropertyName("produtos")]
        public List<ProdutoInput> Produtos { get; set; } = new();
    }

    public class ProdutoInput
    {
        [JsonPropertyName("produto_id")]
        public string ProdutoId { get; set; } = string.Empty;

        [JsonPropertyName("dimensoes")]
        public DimensoesInput Dimensoes { get; set; } = new();
    }

    public class DimensoesInput
    {
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Comprimento { get; set; }
    }
}