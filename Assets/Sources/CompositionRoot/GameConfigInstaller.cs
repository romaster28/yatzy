using Sources.Config;
using UnityEngine;
using Zenject;

namespace Sources.CompositionRoot
{
    [CreateAssetMenu(fileName = "GameConfigInstaller", menuName = "Installers/GameConfigInstaller")]
    public class GameConfigInstaller : ScriptableObjectInstaller<GameConfigInstaller>
    {
        [SerializeField] private RollConfig _roll;

        [SerializeField] private AvatarsConfig _avatars;

        [SerializeField] private RulesConfig _rules;

        [SerializeField] private BotConfig _bot;

        [SerializeField] private LeadersConfig _leaders;
        
        public override void InstallBindings()
        {
            Container.BindInstances(_roll, _avatars, _rules, _bot, _leaders);
        }
    }
}