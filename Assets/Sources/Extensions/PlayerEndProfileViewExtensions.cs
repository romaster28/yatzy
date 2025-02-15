using Sources.Model.Players;
using Sources.View.UserInterface.Default.Elements;

namespace Sources.Extensions
{
    public static class PlayerEndProfileViewExtensions
    {
        public static void ConnectWithPlayer(this PlayerEndProfileView view, Player player)
        {
            view.Name.Update(player.Profile.Name);
            
            view.Score.Update(player.Score.Value);
            
            view.SetAvatar(player.Profile.Avatar);
        }
    }
}