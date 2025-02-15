namespace Sources.Model.Stats.DomainEvents
{
    public class RollsChanged
    {
        public int Value { get; }

        public RollsChanged(int value)
        {
            Value = value;
        }
    }
}