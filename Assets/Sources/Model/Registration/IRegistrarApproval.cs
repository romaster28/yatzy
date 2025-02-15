using System;
using System.Collections.Generic;

namespace Sources.Model.Registration
{
    public interface IRegistrarApproval
    {
        void Approve(IEnumerable<Profile> profiles, Action approved, Action canceled);
    }
}