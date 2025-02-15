using System;

namespace Sources.Model.Registration
{
    public interface IRegistrar
    {
        // TODO: Params? maybe?
        void Register(Action<Profile[]> registered, Action canceled, int count);
    }
}