#nullable enable

using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.Commands
{
    [UsedImplicitly]
    public sealed class CommandsFactory
    {
        private readonly DiContainer _container;

        public CommandsFactory(DiContainer container)
            => _container = container;

        public T GetCommand<T>(List<object> parameters) where T : ICommand
            => _container.Instantiate<T>(parameters);

        public FlyObjectToCommand GetFlyObjectToCommand(Transform target, Transform destination, float speed,
            float completionRadius)
            => GetCommand<FlyObjectToCommand>(new List<object> { target, destination, speed, completionRadius });
    }
}