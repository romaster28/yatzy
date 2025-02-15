using Sources.Model.Players;
using Sources.View.UserInterface.Default.Elements;

namespace Sources.Extensions
{
    public static class PlayerViewExtensions
    {
        public static void Connect(this PlayerView view, Player player)
        {
            view.Name.Update(player.Profile.Name);
            
            view.Score.Update(player.Score.Value);
            
            view.UpdateAvatar(player.Profile.Avatar);
        }
    }
}