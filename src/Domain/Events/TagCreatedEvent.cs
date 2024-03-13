namespace Domain.Events
{
    public class TagCreatedEvent : BaseEvent
    {
        public TagCreatedEvent(Tag tag)
        {
            Tag = tag;
        }
        public Tag Tag { get; set; }
    }
}
