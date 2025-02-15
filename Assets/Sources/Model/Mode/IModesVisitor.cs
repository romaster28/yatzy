using Sources.Model.Mode.ConcreteModes;

namespace Sources.Model.Mode
{
    public interface IModesVisitor
    {
        void Visit(SoloMode solo);

        void Visit(VersusBotMode versusBot);

        void Visit(VersusFriendMode versusFriend);
    }
}