using System.Collections.Generic;

namespace Sources.Model.Leaders
{
    public interface ILeadersWriter
    {
        void Write(LeaderInfo info);

        IEnumerable<LeaderInfo> Read();
    }
}