#nullable enable

using System;
using System.Linq;
using dmdspirit.Core.Attributes;
using UnityEngine;

namespace HamletTwoSacks.Infrastructure.StaticData
{
    public abstract class GameConfig : ScriptableObject
    {
        
#if UNITY_EDITOR
        private ConfigList? _configList;

        [SerializeField, Button(nameof(AddToList))]
        private bool _addToList;

        [SerializeField, Button(nameof(RemoveFromList))]
        private bool _removeFromList;

        [SerializeField, ReadOnly]
        private bool _isAdded;

        private void Awake()
        {
            if(_configList==null)
                FetchConfigList();
            _isAdded = _configList!.Configs.Contains(this);
        }

        protected void AddToList()
        {
            if (_configList == null)
                FetchConfigList();
            _configList!.AddConfig(this);
            _isAdded = _configList!.Configs.Contains(this);
        }

        protected void RemoveFromList()
        {
            if (_configList == null)
                FetchConfigList();
            
            _configList!.RemoveConfig(this);
            _isAdded = _configList!.Configs.Contains(this);
        }

        private void FetchConfigList()
        {
            _configList = Resources.Load<ConfigList>(StaticDataProvider.CONFIG_LIST_PATH);
            if (_configList == null)
                Debug.LogError($"Could not load {typeof(ConfigList)} at path {StaticDataProvider.CONFIG_LIST_PATH}.");
        }
#endif

    }
}