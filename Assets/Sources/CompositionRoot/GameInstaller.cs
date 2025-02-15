using Sources.Config;
using Sources.DomainEvent;
using Sources.Misc;
using Sources.Model;
using Sources.Model.Combination;
using Sources.Model.Combination.Combinations;
using Sources.Model.Game;
using Sources.Model.Game.GameContextDecorators;
using Sources.Model.Leaders;
using Sources.Model.Mode;
using Sources.Model.Mode.ConcreteModes;
using Sources.Model.Mode.DomainEvents;
using Sources.Model.Mode.Factories;
using Sources.Model.Mode.InitializeModesChainHandlers;
using Sources.Model.Players;
using Sources.Model.Registration;
using Sources.Model.Registration.Avatar;
using Sources.Model.Roller;
using Sources.Model.Stats;
using Sources.Model.Stats.DomainEvents;
using Sources.Model.Stats.RollsCountDecorators;
using Sources.View.UserInterface;
using Sources.View.UserInterface.Default;
using Sources.View.UserInterface.ToolKit;
using Sources.ViewModel;
using UnityEngine;
using Zenject;

namespace Sources.CompositionRoot
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ToolKitScreensFacade _toolKitScreens;

        [SerializeField] private DefaultScreensFacade _defaultScreens;

        public override void InstallBindings()
        {
            Container.Bind(typeof(IInitializable), typeof(App)).To<App>().AsSingle();

            Container.Bind<IScreensFacade>().FromInstance(_defaultScreens).AsSingle();

            Container.Bind<IGameContext>().To<GameContext>().AsSingle();

            Container.Decorate<IGameContext>().With<GameContextDomainEventDecorator>();

            // Container.Decorate<IGameContext>().With<GameDebugContextDecorator>();

            Container.Decorate<IGameContext>().With<GameContextWithModeSelectDecorator>();

            Container.Bind<IModeAsker>().To<ModeAskerViewModel>().AsSingle();

            Container.Bind<IModeContext>().To<ModeContext>().AsSingle();

            Container.Bind<IModesFactory>().To<ModesFactory>().AsSingle();

            Container.Bind(typeof(IInitializable), typeof(ILeadersWriter)).To<PlayerPrefsLeadersWriter>().AsSingle();

            Container.Bind<IMode[]>().FromMethod(context => new IMode[]
            {
                new SoloMode(context.Container.Resolve<IPlayersContainer>(),
                    context.Container.Resolve<IHandler<StateUpdated>>(), context.Container.Resolve<RulesConfig>()),
                new VersusBotMode(context.Container.Resolve<BotConfig>(), context.Container.Resolve<RulesConfig>(),
                    context.Container.Resolve<IPlayersContainer>(),
                    context.Container.Resolve<IHandler<StateUpdated>>(), context.Container.Resolve<AsyncProcessor>()),
                new VersusFriendMode(context.Container.Resolve<IPlayersContainer>(),
                    context.Container.Resolve<IHandler<StateUpdated>>(), context.Container.Resolve<RulesConfig>())
            }).AsSingle().WhenInjectedInto<ModesFactory>();

            Container.Bind<InitializeModeChainHandler>().FromMethod(CreateInitializeModeChain).AsSingle();

            Container.Bind<IRegistrar>().To<RegistrarViewModel>().AsSingle();

            Container.Bind<IPlayersContainer>().To<PlayersContainer>().AsSingle();

            Container.Bind<IAvatarAsker>().To<AvatarAskerViewModel>().AsSingle();

            Container.Bind<IRegistrarApproval>().To<RegisterApprovalViewModel>().AsSingle();

            Container.BindFactory<Profile, IRoller, IScore, ICombinator, Player, Player.Factory>();

            Container.BindFactory<IScore, ScoreFactory>().To<Score>();

            Container.BindFactory<IRoller, RollerFactory>().To<Roller>();

            Container.BindFactory<ICombinator, CombinatorFactory>().To<Combinator>();

            Container.Bind<IRollsCount>().To<RollsCountPlayerPrefs>().AsSingle();

            Container.Decorate<IRollsCount>().With<RollsCountDomainEventsDecorator>();

            Container.Bind<ICombination[]>().FromMethod(GenerateCombinations).AsTransient();

            InstallViewModel();

            InstallHandlers();

            InstallMisc();
        }

        private ICombination[] GenerateCombinations()
        {
            return new ICombination[]
            {
                new OnesCombination(),
                new TwosCombination(),
                new ThreesCombination(),
                new FoursCombination(),
                new FivesCombination(),
                new SixesCombination(),
                new ThreeIdenticalCombination(),
                new FourIdenticalCombination(),
                new FullHouseCombination(),
                new SmallStraightCombination(),
                new LargeStraightCombination(),
                new YatzyCombination(),
                new ChanceCombination()
            };
        }

        private InitializeModeChainHandler CreateInitializeModeChain(InjectContext context)
        {
            var registrar = context.Container.Resolve<IRegistrar>();

            var players = context.Container.Resolve<IPlayersContainer>();

            var versusFriend = new VersusFriendInitializeHandler(null, registrar, players);

            var botInitialize =
                new VersusBotInitializeHandler(versusFriend, registrar, players,
                    context.Container.Resolve<AvatarsConfig>());

            return new SoloModeInitializeHandler(botInitialize, registrar, players);
        }

        private void InstallMisc()
        {
            Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();

            Container.Bind<MonoEvents>().FromNewComponentOnNewGameObject().AsSingle();
        }

        private void InstallHandlers()
        {
            Container.Bind<IHandler<AppLaunched>>().FromMethod(context =>
            {
                return new CompositeHandler<AppLaunched>(new IHandler<AppLaunched>[]
                {
                    context.Container.Resolve<AppViewModel>(),
                    context.Container.Resolve<LeadersViewModel>(),
                    context.Container.Resolve<MenuViewModel>(),
                    context.Container.Resolve<GameScreenViewModel>(),
                    context.Container.Resolve<GameEndViewModel>()
                });
            }).AsSingle();

            Container.Bind<IHandler<AppClosed>>().FromMethod(context =>
            {
                return new CompositeHandler<AppClosed>(new IHandler<AppClosed>[]
                {
                    context.Container.Resolve<LeadersViewModel>(),
                    context.Container.Resolve<MenuViewModel>(),
                    context.Container.Resolve<GameScreenViewModel>(),
                    context.Container.Resolve<GameEndViewModel>()
                });
            }).AsSingle();

            Container.Bind<IHandler<GameLaunched>>().FromMethod(context =>
            {
                return new CompositeHandler<GameLaunched>(new IHandler<GameLaunched>[]
                {
                    context.Container.Resolve<GameContextViewModel>(),
                });
            }).AsSingle();

            Container.Bind<IHandler<StateUpdated>>().FromMethod(context =>
            {
                return new CompositeHandler<StateUpdated>(new IHandler<StateUpdated>[]
                {
                    context.Container.Resolve<SoloStateViewModel>(),
                    context.Container.Resolve<VersusBotStateViewModel>(),
                    context.Container.Resolve<VersusFriendStateViewModel>()
                });
            }).AsSingle();

            Container.Bind<IHandler<RollsChanged>>().FromMethod(context =>
            {
                return new CompositeHandler<RollsChanged>(new IHandler<RollsChanged>[]
                {
                    context.Container.Resolve<RollsCountViewModel>()
                });
            }).AsSingle();

            Container.Bind<IHandler<GameEnded>>().FromMethod(context =>
            {
                return new CompositeHandler<GameEnded>(new IHandler<GameEnded>[]
                {
                    context.Container.Resolve<GameEndViewModel>(),
                    context.Container.Resolve<LeadersViewModel>()
                });
            }).AsSingle();
        }

        private void InstallViewModel()
        {
            Container.Bind<AppViewModel>().To<AppViewModel>().AsSingle();

            Container.Bind<LeadersViewModel>().To<LeadersViewModel>().AsSingle();

            Container.Bind<MenuViewModel>().To<MenuViewModel>().AsSingle();

            Container.Bind<GameContextViewModel>().To<GameContextViewModel>().AsSingle();

            Container.Bind<GameScreenViewModel>().To<GameScreenViewModel>().AsSingle();

            Container.Bind<SoloStateViewModel>().To<SoloStateViewModel>().AsSingle();

            Container.Bind<RollsCountViewModel>().To<RollsCountViewModel>().AsSingle();

            Container.Bind<GameEndViewModel>().To<GameEndViewModel>().AsSingle();

            Container.Bind<VersusBotStateViewModel>().To<VersusBotStateViewModel>().AsSingle();

            Container.Bind<VersusFriendStateViewModel>().To<VersusFriendStateViewModel>().AsSingle();
        }
    }
}