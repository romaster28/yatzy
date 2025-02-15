using System;
using UnityEngine;

namespace Sources.Model.Registration.Avatar
{
    public interface IAvatarAsker
    {
        void Ask(Action<Sprite> onSelected, Sprite selected);
    }
}