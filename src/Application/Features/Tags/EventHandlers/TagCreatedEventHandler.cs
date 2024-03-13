using Microsoft.Extensions.Logging;

namespace Application.Features.Tags.EventHandlers
{
    public class TagCreatedEventHandler : INotificationHandler<TagCreatedEvent>
    {
        private readonly ILogger<TagCreatedEventHandler> _logger;
        public TagCreatedEventHandler(ILogger<TagCreatedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(TagCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CleanArchitecture Application Event: {Name} {@UserId} {@UserName} {@Request}",
                               notification.GetType().Name, notification.Tag.CreatedBy, notification.Tag.CreatedBy, notification);
            return Task.CompletedTask;
        }
    }
}
