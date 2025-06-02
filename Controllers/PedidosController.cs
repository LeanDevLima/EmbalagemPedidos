using EmbalagemPedidos.Models;
using EmbalagemPedidos.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace EmbalagemPedidos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly EmbalagemService _embalagemService;

        public PedidosController(EmbalagemService embalagemService)
        {
            _embalagemService = embalagemService;
        }

        [HttpPost("processar")]
        public IActionResult ProcessarPedidos([FromBody] List<Pedido> pedidos)
        {
            var resposta = _embalagemService.ProcessarPedidos(pedidos);
            return Ok(new { Pedidos = resposta });
        }

        [HttpPost("processar-json")]
        public IActionResult ProcessarPedidosJson([FromBody] PedidosWrapper input)
        {
            if (input?.pedidos == null)
                return BadRequest("Dados de entrada inválidos");

            var pedidos = input.pedidos.Select(p => new Pedido
            {
                PedidoId = p.pedido_id,
                Produtos = p.produtos?.Select(prod => new Produto
                {
                    ProdutoId = prod.produto_id,
                    Dimensoes = new Dimensoes
                    {
                        Altura = prod.dimensoes.altura,
                        Largura = prod.dimensoes.largura,
                        Comprimento = prod.dimensoes.comprimento
                    }
                }).ToList() ?? new List<Produto>()
            }).ToList();

            var resposta = _embalagemService.ProcessarPedidos(pedidos);
            
            var resultado = new
            {
                pedidos = resposta.Select(r => new
                {
                    pedido_id = r.PedidoId,
                    caixas = r.Caixas.Select(c => new
                    {
                        caixa_id = c.CaixaId,
                        produtos = c.Produtos,
                        observacao = c.Observacao ?? ""
                    })
                })
            };

            return Ok(resultado);
        }

        [HttpGet]
        public IActionResult ListarPedidosProcessados()
        {
            return Ok(_embalagemService.ObterUltimosPedidosProcessados());
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPedidoProcessado(int id)
        {
            var pedido = _embalagemService.ObterPedidoProcessado(id);
            if (pedido == null)
                return NotFound();
            
            return Ok(pedido);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarPedido(int id, [FromBody] Pedido pedidoAtualizado)
        {
            return StatusCode(501, "Funcionalidade não implementada - este serviço não mantém estado persistente dos pedidos");
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarParcialmentePedido(int id, [FromBody] JsonPatchDocument<Pedido> patchDoc)
        {
            return StatusCode(501, "Funcionalidade não implementada - este serviço não mantém estado persistente dos pedidos");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarPedido(int id)
        {
            return StatusCode(501, "Funcionalidade não implementada - este serviço não mantém estado persistente dos pedidos");
        }
    }

    public class PedidosWrapper
    {
        [JsonPropertyName("pedidos")]
        public List<PedidoInput> pedidos { get; set; }
    }

    public class PedidoInput
    {
        [JsonPropertyName("pedido_id")]
        public int pedido_id { get; set; }

        [JsonPropertyName("produtos")]
        public List<ProdutoInput> produtos { get; set; }
    }

    public class ProdutoInput
    {
        [JsonPropertyName("produto_id")]
        public string produto_id { get; set; }

        [JsonPropertyName("dimensoes")]
        public DimensoesInput dimensoes { get; set; }
    }

    public class DimensoesInput
    {
        [JsonPropertyName("altura")]
        public int altura { get; set; }

        [JsonPropertyName("largura")]
        public int largura { get; set; }

        [JsonPropertyName("comprimento")]
        public int comprimento { get; set; }
    }
}