#nullable enable

using dmdspirit.Core.Attributes;
using HamletTwoSacks.Characters;
using HamletTwoSacks.Level;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace HamletTwoSacks.Infrastructure
{
    public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour
    {
        private LevelTransforms _levelTransforms = null!;
        private EntityManager _manager = null!;

        [SerializeField]
        private bool _spawnAtStart;

        [SerializeField]
        private bool _useSpread = true;

        [SerializeField, ShowIf(nameof(_useSpread), true)]
        private float _spread = 0.5f;

        [SerializeField, Button(nameof(Spawn))]
        private bool _spawnObject;

        [Inject]
        private void Construct(EntityManager manager, LevelTransforms levelTransforms)
        {
            _manager = manager;
            _manager.RegisterSpawner(this);
            _levelTransforms = levelTransforms;
        }

        private void Start()
        {
            if (_spawnAtStart)
                Spawn();
        }

        private void OnDestroy()
            => _manager.UnregisterSpawner(this);

        public T Spawn()
        {
            var obj = _manager.CreateObject<T>();
            var parentTransform = _levelTransforms.GetParent<T>();
            obj.transform.SetParent(parentTransform);
            Vector3 position = transform.position;
            if (_useSpread)
                position.x += Random.Range(-_spread, _spread);
            obj.transform.position = position;
            return obj;
        }
    }
}