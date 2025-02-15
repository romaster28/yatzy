using System;
using Sources.DomainEvent;
using Sources.Misc;
using Zenject;

namespace Sources.Model
{
    public class App : IInitializable
    {
        private readonly IHandler<AppLaunched> _launchHandler;

        private readonly IHandler<AppClosed> _closeHandler;

        private readonly MonoEvents _monoEvents;

        public App(IHandler<AppLaunched> launchHandler, IHandler<AppClosed> closeHandler, MonoEvents monoEvents)
        {
            _launchHandler = launchHandler ?? throw new ArgumentNullException(nameof(launchHandler));
            _closeHandler = closeHandler ?? throw new ArgumentNullException(nameof(closeHandler));
            _monoEvents = monoEvents ? monoEvents : throw new ArgumentNullException(nameof(monoEvents));
        }

        public void Initialize()
        {
            _launchHandler.Handle(new AppLaunched());
            
            _monoEvents.ApplicationQuit += delegate
            {
                _closeHandler.Handle(new AppClosed());
            };
        }
    }

    public class AppLaunched
    {
        
    }

    public class AppClosed
    {
        
    }
}