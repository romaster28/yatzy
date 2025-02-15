namespace Sources.Model.Game
{
    public interface IGameContext
    {
        void Launch();

        void End(GameResult result);
    }
}