namespace Sources.Model.Leaders
{
    public struct LeaderInfo
    {
        public string Name { get; }
        
        public int Score { get; }

        public LeaderInfo(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}