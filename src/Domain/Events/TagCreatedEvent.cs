namespace AlgoCode.Domain.Events;

public class TagCreatedEvent : BaseEvent
{
    public TagCreatedEvent(Problem tag)
    {
        Tag = tag;
    }
    public Problem Tag { get; set; }
}
