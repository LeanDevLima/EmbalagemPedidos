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

O projeto está configurado para rodar em containers Docker:

```bash
docker-compose build
docker-compose up -d
```

### Aplique as migrações do banco de dados

```bash
docker-compose exec app dotnet ef database update
```