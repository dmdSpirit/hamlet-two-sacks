#nullable enable

using System;
using Aether.UI;
using HamletTwoSacks.Characters.PlayerControl;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace HamletTwoSacks.UI
{
    public sealed class ResourcesPanel : UIScreen
    {
        private Player _player = null!;

        private IDisposable? _sub;

        [SerializeField]
        private TMP_Text _value = null!;

        [Inject]
        private void Construct(Player player)
            => _player = player;

        protected override void OnInitialize() { }

        protected override void OnShow()
        {
            _sub?.Dispose();
            _sub = _player.Crystals.Subscribe(UpdateValue);
            UpdateValue(_player.Crystals.Value);
        }

        protected override void OnHide()
            => _sub?.Dispose();

        private void UpdateValue(int value)
            => _value.text = value.ToString();
    }
}