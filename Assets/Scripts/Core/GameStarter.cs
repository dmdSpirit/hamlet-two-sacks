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
        {
            Application.targetFrameRate = -1;
            QualitySettings.vSyncCount = 0;
            _gameLifeCycle = gameLifeCycle;
        }

        private void Start()
            => _gameLifeCycle.Start();
    }
}