namespace Sources.Model.Roller
{
    public interface IReadOnlyDice
    {
        int Value { get; }
        
        bool Frozen { get; }
    }
}