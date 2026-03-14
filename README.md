# Controle Financeiro

Projeto full stack para controle financeiro com cadastro de pessoas, categorias e transacoes, incluindo relatorios de totais por pessoa e por categoria.

## Tecnologias utilizadas

### Backend
- ASP.NET Core 9 (Web API)
- Entity Framework Core 9
- SQLite

### Frontend
- React 19
- TypeScript
- Vite
- React Router

## Como o projeto esta organizado

- `backend`: API REST responsavel pelas regras de negocio, persistencia de dados e relatorios.
- `frontend`: interface web para cadastro e visualizacao dos dados.

## Banco de dados

O projeto usa SQLite no backend, com a conexao configurada para:

`Data Source=controlefinanceiro.db`

Esse arquivo de banco e criado no backend.

## Portas da aplicacao

- Backend (HTTP): `http://localhost:5249`
- Frontend (Vite): `http://localhost:5173`

Observacao: o frontend esta configurado para consumir a API em `http://localhost:5249/api`.

## Funcionalidades

- Cadastro e listagem de pessoas
- Cadastro e listagem de categorias
- Cadastro e listagem de transacoes
- Relatorio de totais por pessoa (receitas, despesas e saldo)
- Relatorio de totais por categoria (receitas, despesas e saldo)
- Total geral consolidado nas telas de pessoas e categorias

## Pre-requisitos

- .NET SDK 9
- Node.js (recomendado: versao LTS)
- npm

## Instalacao de dependencias

### Backend

No diretorio `backend`, execute:

```bash
dotnet restore
```

### Frontend

No diretorio `frontend`, execute:

```bash
npm install
```

## Como executar

### 1. Iniciar o backend

No diretorio `backend`:

```bash
dotnet run
```

API disponivel em `http://localhost:5249`.

### 2. Iniciar o frontend

No diretorio `frontend`:

```bash
npm run dev
```

Aplicacao web disponivel em `http://localhost:5173`.

## Endpoints principais da API

- `GET /api/pessoas`
- `POST /api/pessoas`
- `GET /api/categorias`
- `POST /api/categorias`
- `GET /api/transacoes`
- `POST /api/transacoes`
- `GET /api/transacoes/totais-por-pessoa`
- `GET /api/transacoes/totais-por-categoria`

## Observacoes

- O CORS no backend esta liberado para facilitar o desenvolvimento local.
- Se o banco ainda nao existir no ambiente, rode as migracoes antes de usar a aplicacao:

```bash
dotnet ef database update
```
