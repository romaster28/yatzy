namespace Sources.DomainEvent
{
    public interface IHandler<T>
    {
        void Handle(T handle);
    }
}