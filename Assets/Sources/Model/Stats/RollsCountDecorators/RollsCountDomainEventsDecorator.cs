using System;
using Sources.DomainEvent;
using Sources.Model.Game.GameContextDecorators;
using Sources.Model.Stats.DomainEvents;
using UnityEngine;

namespace Sources.Model.Stats.RollsCountDecorators
{
    public class RollsCountDomainEventsDecorator : IRollsCount
    {
        private readonly IHandler<RollsChanged> _handler;

        private readonly IRollsCount _rollsCount;

        public RollsCountDomainEventsDecorator(IRollsCount rollsCount, IHandler<RollsChanged> handler)
        {
            _rollsCount = rollsCount ?? throw new ArgumentNullException(nameof(rollsCount));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public int Value => _rollsCount.Value;
        
        public void Add()
        {
            _rollsCount.Add();
            
            _handler.Handle(new RollsChanged(Value));
        }
    }
}