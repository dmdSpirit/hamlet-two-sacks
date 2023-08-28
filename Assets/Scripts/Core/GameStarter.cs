#nullable enable

using UnityEngine;
using Zenject;

namespace dmdspirit.Core
{
    public sealed class GameStarter : MonoBehaviour
    {
        private IGameLifeCycle _gameLifeCycle = null!;

        [Inject]
        private void Construct(IGameLifeCycle gameLifeCycle)
            => _gameLifeCycle = gameLifeCycle;

        private void Start()
            => _gameLifeCycle.Start();
    }
}