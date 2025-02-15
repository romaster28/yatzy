namespace Sources.Model.Roller
{
    public class Dice : IReadOnlyDice
    {
        public int Value { get; set; }
        
        public bool Frozen { get; set; }
    }
}