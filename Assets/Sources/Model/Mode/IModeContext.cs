namespace Sources.Model.Mode
{
    public interface IModeContext
    {
        IMode Selected { get; }
        
        void Select(IMode mode);
    }
}