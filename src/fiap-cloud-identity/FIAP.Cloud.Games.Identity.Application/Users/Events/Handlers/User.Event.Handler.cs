using FIAP.Cloud.Games.Core.Contracts.Users;
using MassTransit;

namespace FIAP.Cloud.Games.Identity.Application.Users.Events.Handlers
{
    public class UserEventHandler(IBus bus)
    {
        public async Task Handle(UserCreatedEvent @event)
        {
            await bus.Publish(new UserCreated
            {
                Id = @event.UserId,
            });
        }
    }
}
