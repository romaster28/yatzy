using System;

namespace Sources.Model.Mode
{
    public interface IModeAsker
    {
        void Ask(Action<IMode> selected, Action canceled);
    }
}