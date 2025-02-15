using Sources.Model.Leaders;
using Sources.Model.Players;

namespace Sources.Extensions
{
    public static class LeadersWriterExtensions
    {
        public static void Write(this ILeadersWriter writer, Player player)
        {
            writer.Write(new LeaderInfo(player.Profile.Name, player.Score.Value));
        }
    }
}