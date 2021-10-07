namespace EventBus.Message.Events
{
    public abstract class IntegrationBaseEvent
    {
        public Guid Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public IntegrationBaseEvent()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public IntegrationBaseEvent(Guid id, DateTime createdDate)
        {
            Id = id;
            CreatedDate = createdDate;
        }
    }
}
