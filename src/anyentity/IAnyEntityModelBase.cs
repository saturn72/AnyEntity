namespace AnyEntity
{
    public interface IAnyEntityModelBase<TId>
    {
        TId Id { get; set; }
    }
}
