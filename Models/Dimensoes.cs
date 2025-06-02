using System.ComponentModel.DataAnnotations.Schema;

namespace EmbalagemPedidos.Models
{
    public class Dimensoes
        {
            public int Id { get; set; }
            public int Altura { get; set; }
            public int Largura { get; set; }
            public int Comprimento { get; set; }
            public int ProdutoId { get; set; } 

        public Dimensoes(int altura, int largura, int comprimento)
        {
            Altura = altura;
            Largura = largura;
            Comprimento = comprimento;
        }
            
            public Dimensoes() {}
        }  
}