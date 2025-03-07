using System;
using System.Collections.Generic;

namespace Sources.Model.Leaders
{
    public interface ILeaderBoards
    {
        void Write(LeaderInfo info);

        void Read(Action<IEnumerable<LeaderInfo>> received);
    }
}