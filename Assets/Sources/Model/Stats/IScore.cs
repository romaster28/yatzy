namespace Sources.Model.Stats
{
    public interface IScore
    {
        int Value { get; }

        void Add(int amount);
    }
}