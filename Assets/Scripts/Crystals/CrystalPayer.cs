#nullable enable

using HamletTwoSacks.Character;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace HamletTwoSacks.Crystals
{
    public sealed class CrystalPayer : MonoBehaviour
    {
        private Player _player = null!;

        private CrystalCostPanel? _costPanel; 

        [SerializeField]
        private CrystalSpawner _crystalSpawner = null!;

        [Inject]
        private void Construct(Player player)
            => _player = player;

        private void OnCollisionEnter2D(Collision2D target)
        {
            if (_costPanel != null)
                return;
            
            _costPanel = target.gameObject.GetComponent<CrystalCostPanel>();
            if (_costPanel == null)
                return;
            // show input prompt
            // read input
        }

        private void OnCollisionExit2D(Collision2D target)
        {
            if (_costPanel == null)
                return;

            _costPanel.HidePanel();

            // hide prompt
            // stop reading input
        }

        public Crystal GetCrystal()
        {
            Assert.IsTrue(_player.Crystals.Value > 0);
            Crystal crystal = _crystalSpawner.SpawnCrystal();
            _player.SpendCrystal();
            return crystal;
        }
    }
}