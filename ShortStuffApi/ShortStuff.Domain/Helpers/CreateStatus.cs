using ShortStuff.Domain.Enums;

namespace ShortStuff.Domain.Helpers
{
    public class CreateStatus<TId>
    {
        public CreateStatusEnum Status { get; set; }
        public TId Id { get; set; }
    }
}
