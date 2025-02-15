using UnityEngine;

namespace Sources.Model.Registration
{
    public class Profile
    {
        public string Name { get; set; }
        
        public Sprite Avatar { get; set; }
        
        public Profile(string name, Sprite avatar)
        {
            Name = name;
            Avatar = avatar;
        }
    }
}