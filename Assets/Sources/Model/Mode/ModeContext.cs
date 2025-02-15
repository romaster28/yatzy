using System;

namespace Sources.Model.Mode
{
    public class ModeContext : IModeContext
    {
        public IMode Selected { get; private set; }
        
        public void Select(IMode mode)
        {
            if (mode == null)
                throw new ArgumentNullException(nameof(mode));
            
            Selected = mode;
        }
    }
}