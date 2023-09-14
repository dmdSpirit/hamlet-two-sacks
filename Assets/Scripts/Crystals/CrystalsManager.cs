#nullable enable

using System.Collections.Generic;
using HamletTwoSacks.Infrastructure;
using JetBrains.Annotations;
using UnityEngine;

namespace HamletTwoSacks.Crystals
{
    [UsedImplicitly]
    public sealed class CrystalsManager
    {
        private IPrefabFactory? _prefabFactory;

        private readonly List<Crystal> _crystals = new();

        public CrystalsManager(IPrefabFactory prefabFactory)
            => _prefabFactory = prefabFactory;

        // HACK (Stas): Yeah... Got confused trying to create structure of scene-context factory and game-context managers. The problem is still with the fact that i don't know how to inject scene-context dicontainer into game-context factory for it to be able to resolve scene-level dependencies.
        // - Stas 13 September 2023
        public void RegisterPrefabFactory(IPrefabFactory prefabFactory)
            => _prefabFactory = prefabFactory;

        public void UnregisterPrefabFactory()
            => _prefabFactory = null;

        public Crystal CreateCrystal()
        {
            var crystal = _prefabFactory.CreateObject<Crystal>();
            _crystals.Add(crystal);
            return crystal;
        }

        public void DestroyCrystal(Crystal crystal)
        {
            _crystals.Remove(crystal);
            if (crystal == null!)
                return;
            Object.Destroy(crystal.gameObject);
        }

        public void Reset()
        {
            var crystals = _crystals.ToArray();
            for (var i = 0; i < crystals.Length; i++)
            {
                if (crystals[i] == null!)
                    return;
                Object.Destroy(crystals[i].gameObject);
            }

            _crystals.Clear();
        }
    }
}