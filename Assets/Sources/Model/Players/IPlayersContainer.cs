using System;
using System.Collections.Generic;
using ModestTree.Util;
using Sources.Model.Registration;

namespace Sources.Model.Players
{
    public interface IPlayersContainer
    {
        void Add(Profile profile);

        void Add(params Profile[] profiles)
        {
            foreach (Profile profile in profiles) 
                Add(profile);
        }

        public IEnumerable<Player> Get();

        Player this[int index] { get; }
        
        int Count { get; }

        bool Any(Func<Player, bool> condition);
    }
}