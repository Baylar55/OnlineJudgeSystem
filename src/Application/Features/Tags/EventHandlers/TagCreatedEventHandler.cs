using AlgoCode.Domain.Events;
using Microsoft.Extensions.Logging;

namespace AlgoCode.Application.Features.Tags.EventHandlers;

public class TagCreatedEventHandler(ILogger<TagCreatedEventHandler> logger) : INotificationHandler<TagCreatedEvent>
{
    public Task Handle(TagCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("CleanArchitecture Application Event: {Name} {@UserId} {@UserName} {@Request}",
                           notification.GetType().Name, notification.Tag.CreatedBy, notification.Tag.CreatedBy, notification);
        return Task.CompletedTask;
    }
}
