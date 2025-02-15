namespace Sources.Model.Stats
{
    public interface IRollsCount
    {
        int Value { get; }

        void Add();
    }
}