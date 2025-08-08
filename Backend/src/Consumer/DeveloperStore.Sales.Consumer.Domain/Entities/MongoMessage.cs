namespace DeveloperStore.Sales.Consumer.Domain.Entities
{
    public class MongoMessage
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        public MongoMessage()
        {
                
        }
    }    
}