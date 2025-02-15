namespace Sources.Model.Mode
{
    public interface IModesFactory
    {
        IMode Create<T>() where T : IMode;

        IMode Create(int index);
    }
}