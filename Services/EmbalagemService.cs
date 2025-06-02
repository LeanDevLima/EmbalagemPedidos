using EmbalagemPedidos.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EmbalagemPedidos.Services
{
    public class EmbalagemService
    {
        private readonly List<Caixa> _caixasDisponiveis = new List<Caixa>
        {
            new Caixa { CaixaId = "Caixa 1", Dimensoes = new Dimensoes(30, 40, 80) },
            new Caixa { CaixaId = "Caixa 2", Dimensoes = new Dimensoes(80, 50, 40) },
            new Caixa { CaixaId = "Caixa 3", Dimensoes = new Dimensoes(50, 80, 60) }
        };

        private readonly ConcurrentDictionary<int, RespostaEmbalagem> _pedidosProcessados = new();

        public List<RespostaEmbalagem> ProcessarPedidos(List<Pedido> pedidos)
        {
            var respostas = new List<RespostaEmbalagem>();

            foreach (var pedido in pedidos)
            {
                var resposta = new RespostaEmbalagem
                {
                    PedidoId = pedido.PedidoId,
                    Caixas = new List<Caixa>()
                };

                var produtosRestantes = new List<Produto>(pedido.Produtos);

                produtosRestantes.Sort((a, b) =>
                    (b.Dimensoes.Altura * b.Dimensoes.Largura * b.Dimensoes.Comprimento)
                    .CompareTo(a.Dimensoes.Altura * a.Dimensoes.Largura * a.Dimensoes.Comprimento));

                while (produtosRestantes.Any())
                {
                    var melhorCaixa = EncontrarMelhorCaixa(produtosRestantes);

                    if (melhorCaixa == null)
                    {
                        resposta.Caixas.Add(new Caixa
                        {
                            CaixaId = null,
                            Produtos = produtosRestantes.Select(p => p.ProdutoId).ToList(),
                            Observacao = "Produto não cabe em nenhuma caixa disponível."
                        });
                        break;
                    }

                    resposta.Caixas.Add(melhorCaixa);
                    produtosRestantes.RemoveAll(p => melhorCaixa.Produtos.Contains(p.ProdutoId));
                }

                _pedidosProcessados.AddOrUpdate(pedido.PedidoId, resposta, (id, old) => resposta);
                respostas.Add(resposta);
            }

            return respostas;
        }

        

        private Caixa EncontrarMelhorCaixa(List<Produto> produtos)
        {
            foreach (var caixa in _caixasDisponiveis
                .OrderBy(c => c.Dimensoes.Altura * c.Dimensoes.Largura * c.Dimensoes.Comprimento))
            {
                var produtosNaCaixa = new List<string>();
                var produtosEmpacotados = new List<Produto>();

                var empacotador = new Empacotador3D(
                    caixa.Dimensoes.Altura,
                    caixa.Dimensoes.Largura,
                    caixa.Dimensoes.Comprimento);

                foreach (var produto in produtos)
                {
                    if (empacotador.AdicionarProduto(
                        produto.Dimensoes.Altura,
                        produto.Dimensoes.Largura,
                        produto.Dimensoes.Comprimento))
                    {
                        produtosNaCaixa.Add(produto.ProdutoId);
                        produtosEmpacotados.Add(produto);
                    }
                }

                if (produtosNaCaixa.Any())
                {
                    return new Caixa
                    {
                        CaixaId = caixa.CaixaId,
                        Produtos = produtosNaCaixa
                    };
                }
            }

            return null;
        }

        public RespostaEmbalagem ObterPedidoProcessado(int id)
        {
            _pedidosProcessados.TryGetValue(id, out var pedido);
            return pedido;
        }

        public List<RespostaEmbalagem> ObterUltimosPedidosProcessados()
        {
            return _pedidosProcessados.Values.OrderByDescending(p => p.PedidoId).ToList();
        }
    }


    public class Empacotador3D
    {
        private readonly int _altura;
        private readonly int _largura;
        private readonly int _comprimento;
        private readonly List<EspacoLivre> _espacosLivres;

        public Empacotador3D(int altura, int largura, int comprimento)
        {
            _altura = altura;
            _largura = largura;
            _comprimento = comprimento;
            _espacosLivres = new List<EspacoLivre>
            {
                new EspacoLivre(0, 0, 0, altura, largura, comprimento)
            };
        }

        public bool AdicionarProduto(int altura, int largura, int comprimento)
        {
            var dimensoes = new[]
            {
                new[] { altura, largura, comprimento },
                new[] { altura, comprimento, largura },
                new[] { largura, altura, comprimento },
                new[] { largura, comprimento, altura },
                new[] { comprimento, altura, largura },
                new[] { comprimento, largura, altura }
            };

            foreach (var dim in dimensoes)
            {
                if (TentarAdicionar(dim[0], dim[1], dim[2]))
                    return true;
            }

            return false;
        }

        private bool TentarAdicionar(int altura, int largura, int comprimento)
        {
            for (int i = 0; i < _espacosLivres.Count; i++)
            {
                var espaco = _espacosLivres[i];

                if (altura <= espaco.Altura &&
                    largura <= espaco.Largura &&
                    comprimento <= espaco.Comprimento)
                {
                    _espacosLivres.RemoveAt(i);

                    if (espaco.Altura > altura)
                    {
                        _espacosLivres.Add(new EspacoLivre(
                            espaco.X,
                            espaco.Y,
                            espaco.Z + altura,
                            espaco.Altura - altura,
                            espaco.Largura,
                            espaco.Comprimento));
                    }

                    if (espaco.Largura > largura)
                    {
                        _espacosLivres.Add(new EspacoLivre(
                            espaco.X,
                            espaco.Y + largura,
                            espaco.Z,
                            espaco.Altura,
                            espaco.Largura - largura,
                            espaco.Comprimento));
                    }

                    if (espaco.Comprimento > comprimento)
                    {
                        _espacosLivres.Add(new EspacoLivre(
                            espaco.X + comprimento,
                            espaco.Y,
                            espaco.Z,
                            espaco.Altura,
                            espaco.Largura,
                            espaco.Comprimento - comprimento));
                    }

                    return true;
                }
            }

            return false;
        }
    }

    public class EspacoLivre
    {
        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public int Altura { get; }
        public int Largura { get; }
        public int Comprimento { get; }

        public EspacoLivre(int x, int y, int z, int altura, int largura, int comprimento)
        {
            X = x;
            Y = y;
            Z = z;
            Altura = altura;
            Largura = largura;
            Comprimento = comprimento;
        }
    }
}
