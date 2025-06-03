
# FIAP Cloud Games (FCG)

É um client de jogos da FIAP para você poder gerenciar seus jogos na sua biblioteca e poder adicionar novos jogos a ela.


## Estrutura

Hoje o projeto está dentro da mesma solução, mas temos os contextos bem separados para cada aplicação.

Nosso contexto de Identity está dentro de um projeto chamado FIAP.Cloud.Games.Identity e o nosso projeto de dominio se encontra no projeto FIAP.Cloud.Games.

Seguimos a ideia de ter um projeto dentro da mesma solution com os modulos bem definidos, mas deixando abertura pra futuro separação em produtos apartados.
# Fiap.Cloud.Games

API de Dominio para a Fiap Cloud Games.

## Informações

- **Título:** Fiap.Cloud.Games.Identity
- **Descrição:** API de identidade para a Fiap Cloud Games
- **Versão:** v1
- **Contato:** Leonardo Goes - leonardo.goes@gmail.com
- **Licença:** [MIT](https://opensource.org/licenses/MIT)
- **Servidor:** https://localhost:7138

## Autenticação

Esta API usa autenticação via **JWT Bearer Token**.

```http
Authorization: Bearer {seu_token}
```

## Endpoints

### `/games`

#### POST `/games`
- **Descrição:** Cria um novo jogo.
- **Segurança:** Bearer Token
- **Body:**

| Campo    | Tipo    | Obrigatório | Descrição |
| -------- | ------- | ----------- | --------- |
| name     | string  | Sim         | Nome do jogo |
| pricing  | decimal | Sim         | Preço do jogo |
| category | inteiro | Sim         | Categoria do jogo (veja enum abaixo) |

- **Respostas:** 400, 401, 403, 404, 409

#### GET `/games`
- **Descrição:** Lista jogos filtrando por nome, preço ou categoria.
- **Segurança:** Bearer Token
- **Parâmetros de Query:**

| Parâmetro | Tipo    | Obrigatório | Descrição |
| --------- | ------- | ----------- | --------- |
| name      | string  | Não         | Nome do jogo |
| pricing   | decimal | Não         | Preço do jogo |
| category  | inteiro | Não         | Categoria do jogo (veja enum abaixo) |

- **Respostas:** 400, 401, 403, 404, 409

### `/games/{game_id}`

#### PUT `/games/{game_id}`
- **Descrição:** Atualiza os dados de um jogo existente.
- **Segurança:** Bearer Token
- **Parâmetros de Path:**

| Parâmetro | Tipo  | Obrigatório |
| --------- | ----- | ----------- |
| game_id   | GUID  | Sim         | ID do jogo |

- **Body:**

| Campo    | Tipo    | Obrigatório | Descrição |
| -------- | ------- | ----------- | --------- |
| name     | string  | Não         | Nome do jogo |
| pricing  | decimal | Não         | Preço do jogo |
| category | inteiro | Não         | Categoria do jogo (veja enum abaixo) |

- **Respostas:** 400, 401, 403, 404, 409

### `/library`

#### POST `/library`
- **Descrição:** Cria uma biblioteca para o usuário.
- **Segurança:** Bearer Token
- **Body:**

| Campo  | Tipo  | Obrigatório |
| ------ | ----- | ----------- |
| userId | GUID  | Não         | ID do usuário |

- **Respostas:** 400, 404, 409

#### GET `/library`
- **Descrição:** Lista a biblioteca de um usuário.
- **Segurança:** Bearer Token
- **Parâmetros de Query:**

| Parâmetro | Tipo | Obrigatório |
| --------- | ---- | ----------- |
| user_id   | GUID | Não         | ID do usuário |

- **Respostas:** 400, 404, 409

## Enumeração de Categorias de Jogo

| Valor | Categoria    |
| ----- | ------------- |
| 0     | Action        |
| 1     | Adventure     |
| 2     | RolePlaying   |
| 3     | Simulation    |
| 4     | Strategy      |
| 5     | Sports        |
| 6     | Puzzle        |
| 7     | Racing        |
| 8     | Fighting      |
| 9     | Horror        |
| 10    | Indie         |
| 11    | Other         |

## Respostas de Erro

Todas as respostas de erro seguem este formato:

```json
{
  "slug": "error-code",
  "message": "Descrição do erro",
  "details": [
    {
      "slug": "campo-error",
      "message": "Descrição detalhada",
      "location": "body/query/path",
      "field": "nomeDoCampo"
    }
  ]
}
```

# Fiap.Cloud.Games.Identity API

API de identidade para a Fiap Cloud Games.

## Informações

- **Título:** Fiap.Cloud.Games.Identity
- **Descrição:** API de identidade para a Fiap Cloud Games
- **Versão:** v1
- **Licença:** [MIT](https://opensource.org/licenses/MIT)
- **Contato:** Leonardo Goes (leonardo.goes@gmail.com)
- **Servidor:** https://localhost:7169

## Endpoints

### Criar Usuário

`POST /user`

**Request Body:**

```json
{
  "name": "string",
  "password": "string",
  "email": "string"
}
```

**Responses:**

- 400: Bad Request
- 404: Not Found
- 409: Conflict

### Atribuir Papel (Role) ao Usuário

`POST /role`

**Segurança:** Bearer Token

**Request Body:**

```json
{
  "email": "string",
  "role": 1 // 1 = Admin, 2 = User
}
```

**Responses:**

- 400: Bad Request
- 404: Not Found
- 409: Conflict

### Autenticar Usuário

`POST /user/token`

**Request Body:**

```json
{
  "email": "string",
  "password": "string"
}
```

**Responses:**

- 200: OK

```json
{
  "access_token": "string",
  "expires_in": 3600
}
```

- 400: Bad Request
- 404: Not Found
- 409: Conflict

## Modelos de Erro

### ResponseErrorDetail

```json
{
  "slug": "string",
  "message": "string",
  "details": [
    {
      "slug": "string",
      "message": "string",
      "location": "string",
      "field": "string"
    }
  ]
}
```

## Segurança

- **Bearer Token:** JWT Authorization header usando o esquema Bearer.

Exemplo:

```
Authorization: Bearer {seu_token}
```

## Enumerações

### UserRoleEnum

- **1** - Admin
- **2** - User

## Instalação

Para iniciar o projeto devera selecionar na solution para rodar multiplos projetos sendo eles:

- Core/FIAP.Cloud.Games.API
- Core/FIAP.Cloud.Games.Consumers
- Identity/FIAP.Cloud.Games.Identity.API

### Migrations

```bash
dotnet ef database update --verbose --project './' --startup-project '../FIAP.Cloud.Games.Identity.API'

dotnet ef database update --verbose --project './' --startup-project '../FIAP.Cloud.Games.API'
```
    
### Docker
```bash
cd ~\fiap-cloud-games\docker

docker compose up -d
```
## Autores

- [@leogoes](https://github.com/leogoes)

