using FIAP.Cloud.Games.Core.Responses;
using FIAP.Cloud.Games.Core.Exceptions;
using FIAP.Cloud.Games.Application.Games.Responses;
using FIAP.Cloud.Games.Application.Games.Requests;
using FIAP.Cloud.Games.Domain.Games.Rules;
using FIAP.Cloud.Games.Domain.Games.Entities;
using FIAP.Cloud.Games.Application.Games.Abstractions;
using FIAP.Cloud.Games.Application.Games.Filters;
using FIAP.Cloud.Games.Application.Games.Mappers;
using FIAP.Cloud.Games.Application.Games.Responses.Core;
using FIAP.Cloud.Games.Domain.Games.Exceptions;

namespace FIAP.Cloud.Games.Application.Games.Services
{
    public class GameService(IGameRepository repository)
    {
        public async Task<InternalResponse<IEnumerable<GameResponse>>> FindAllAsync(GameFindRequest request)
        {
            if (request is { Name: "" or null, Pricing: 0 } && !Enum.IsDefined(typeof(GameCategoryEnum), request.Category))
                return DefaultErrorFactory.GetInvalidResource<IEnumerable<GameResponse>>();

            var filter = new GameFilterBuilder()
                    .WithName(request.Name)
                    .WithPricing(request.Pricing)
                    .WithCategory(request.Category)
                    .Build();

            var listOfGames = await repository.FindAllAsync(filter);

            if (!listOfGames.Any())
                return DefaultErrorFactory.GetErrors<IEnumerable<GameResponse>>(GameErrorConst.GAME_NOT_FOUND);

            return new InternalResponse<IEnumerable<GameResponse>>(GameMapper.SetGame(listOfGames));
        }

        public async Task<InternalResponse<GameCreateResponse>> CreateAsync(GameCreateRequest request)
        {
            if (request is { Name: "" or null, Pricing: 0 } && !Enum.IsDefined(typeof(GameCategoryEnum), request.Category))
                return DefaultErrorFactory.GetInvalidResource<GameCreateResponse>();

            var newGame = new Game(request.Name, request.Pricing, request.Category);

            await repository.CreateAsync(newGame);

            var resultOfCreation = await repository.UnitOfWork.CommitAsync(CancellationToken.None);

            if (!resultOfCreation)
                return DefaultErrorFactory.GetErrors<GameCreateResponse>(GameErrorConst.GAME_COULD_NOT_BE_CREATED);

            return new InternalResponse<GameCreateResponse>(new GameCreateResponse(newGame.Name, newGame.Pricing, newGame.Category, newGame.CreatedAt));
        }

        public async Task<InternalResponse<GameResponse>> ChangeAsync(Guid gameId, GameChangeRequest request)
        {
            if (gameId == Guid.Empty && (string.IsNullOrEmpty(request.Name) || request.Pricing is not 0 || !Enum.IsDefined(typeof(GameCategoryEnum), request.Category)))
                return DefaultErrorFactory.GetInvalidResource<GameResponse>();

            var currentGame = await repository.FindByIdAsync(gameId);

            if (currentGame is null)
                return DefaultErrorFactory.GetErrors<GameResponse>(GameErrorConst.GAME_NOT_FOUND);

            if (!string.IsNullOrEmpty(request.Name))
                currentGame.ChangeName(request.Name);

            if (request.Pricing is not null)
                currentGame.ChangePrice(request.Pricing.Value);

            if (request.Category is not null)
                currentGame.ChangeCategory(request.Category.Value);

            var resultOfChange = await repository.ChangeGame(currentGame);

            if (!resultOfChange)
                return DefaultErrorFactory.GetErrors<GameResponse>(GameErrorConst.GAME_COULD_NOT_BE_CHANGED);

            return new InternalResponse<GameResponse>(GameMapper.SetGame(currentGame));
        }
    }
}