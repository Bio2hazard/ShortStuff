namespace ShortStuff.Data.Entities
{
    public interface IDataEntity<TId>
    {
        TId Id { get; set; }

    }
}
