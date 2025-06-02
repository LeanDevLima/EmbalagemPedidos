# Embalagem de Pedidos - API de Otimização de Caixas

## Visão Geral

Este projeto oferece uma solução API para otimizar o empacotamento de produtos em caixas pré-definidas, desenvolvido em **.NET 6** com **SQL Server**. A aplicação calcula a combinação ideal de caixas para um conjunto de pedidos, considerando as dimensões dos produtos.

---

## Pré-requisitos

Para executar este projeto localmente, você precisará dos seguintes componentes instalados em sua máquina:

- Docker Desktop (versão 4.0 ou superior)
- .NET SDK 6.0
- Git (para controle de versão)
- SQL Server Management Studio *(opcional, para visualização do banco de dados)*

---

## Pacotes NuGet Necessários

O projeto utiliza os seguintes pacotes NuGet:

- `Microsoft.EntityFrameworkCore.SqlServer` (6.0.0)
- `Microsoft.EntityFrameworkCore.Design` (6.0.0)
- `Microsoft.EntityFrameworkCore.Tools` (6.0.0)
- `Swashbuckle.AspNetCore` (6.2.3)
- `Microsoft.AspNetCore.JsonPatch` (6.0.0)

---

## Configuração do Ambiente

### Clone o repositório

```bash
git clone https://github.com/LeanDevLima/EmbalagemPedidos.git
cd EmbalagemPedidos
```

### Execute com Docker

O projeto está configurado para rodar em containers Docker, dê esses comandos nessa sequência:

```bash
docker-compose down -v
docker-compose up --build
```
#### Com esses comandos você estará:
 - Limpando containers antigos;
 - Construindo as imagens e já iniciando os containers.


### Testar a API

Este projeto inclui um arquivo de exemplo (`entrada.json`) que pode ser utilizado para testar a API. Basta executar o comando abaixo no terminal (com o terminal posicionado na raiz do projeto):

```bash
curl -X POST "http://localhost:5000/api/Pedidos/processar-json" \
-H "Content-Type: application/json" \
-d "@entrada.json"
```

A resposta será exibida diretamente no terminal, no formato JSON.


### Conecte-se comigo

Se quiser saber mais sobre este projeto ou trocar ideias sobre desenvolvimento de software, fique à vontade para entrar em contato pelo LinkedIn:

👨🏽‍💻 [Leanderson Lima](https://www.linkedin.com/in/leanderson-dias-de-lima/) 

