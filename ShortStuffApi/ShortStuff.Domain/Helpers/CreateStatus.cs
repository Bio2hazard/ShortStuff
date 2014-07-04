namespace ShortStuff.Domain.Enums
{
    public class CreateStatus<TId>
    {
        public CreateStatusEnum status { get; set; }
        public TId Id { get; set; }
    }
}
