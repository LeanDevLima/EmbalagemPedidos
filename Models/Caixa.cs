using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmbalagemPedidos.Models
{
    public class Caixa
    {
        public int Id { get; set; }
        public string CaixaId { get; set; } = string.Empty;
        public int DimensoesId { get; set; }
        public Dimensoes Dimensoes { get; set; } = new Dimensoes();
        public string ProdutosSerializados { get; set; } = "[]";
        public string Observacao { get; set; } = string.Empty;
        public int RespostaEmbalagemId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public List<string> Produtos 
        { 
            get => JsonSerializer.Deserialize<List<string>>(ProdutosSerializados) ?? new List<string>();
            set => ProdutosSerializados = JsonSerializer.Serialize(value);
        }
    }
}