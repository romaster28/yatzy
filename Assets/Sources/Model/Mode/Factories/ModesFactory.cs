using System;
using System.Linq;

namespace Sources.Model.Mode.Factories
{
    public class ModesFactory : IModesFactory
    {
        private readonly IMode[] _modes;

        public ModesFactory(IMode[] modes)
        {
            _modes = modes ?? throw new ArgumentNullException(nameof(modes));
        }

        public IMode Create<T>() where T : IMode
        {
            return _modes.First(x => x is T);
        }

        public IMode Create(int index)
        {
            return _modes[index];
        }
    }
}