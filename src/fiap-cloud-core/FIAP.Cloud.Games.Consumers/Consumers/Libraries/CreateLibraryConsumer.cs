using FIAP.Cloud.Games.Core.Contracts.Users;
using FIAP.Cloud.Games.SDK;
using FIAP.Cloud.Games.SDK.Libraries.Requests;
using MassTransit;

namespace FIAP.Cloud.Games.Consumers.Consumers.Libraries
{
    public class CreateLibraryConsumer(CloudGamesClient cloudGamesClient) : IConsumer<UserCreated>
    {
        public async Task Consume(ConsumeContext<UserCreated> context)
        {
            var response = await cloudGamesClient.LibraryClient.CreateAsync(new LibraryCreateRequest
            {
                    UserId = Guid.Parse(context.Message.Id)
            });

            if(response.Error)
                throw new Exception($"Error creating library for user {context.Message.Id}: {response.StatusCode}");

            return;
        }
    }
}
