namespace ShortStuff.Domain.Entities
{
    public abstract class EntityBase<TId> : ValidatableBase
    {
        public TId Id { get; set; }
    }
}