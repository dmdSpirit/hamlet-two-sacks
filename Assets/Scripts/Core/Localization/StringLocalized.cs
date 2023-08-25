#nullable enable

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using Zenject;

namespace dmdspirit.Core.Localization
{
    // TODO (Stas): I think it would be better to use IVariable functionality of unity localization system
    // https://docs.unity3d.com/Packages/com.unity.localization@1.2/manual/Smart/Persistent-Variables-Source.html
    // - Stas 26 June 2023
    [RequireComponent(typeof(TMP_Text), typeof(LocalizeStringEvent))]
    public sealed class StringLocalized : MonoBehaviour
    {
        private readonly Dictionary<string, Func<string>> _tokens = new();

        private LocalizationController _localizationController = null!;

        private TMP_Text _text = null!;
        private LocalizeStringEvent _localizeStringEvent = null!;

        private bool _usePureText;
        private string _localizedString = null!;
        private bool _isInitialized;

        [Inject]
        private void Construct(LocalizationController localizationController)
        {
            _localizationController = localizationController;
        }

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _localizeStringEvent = GetComponent<LocalizeStringEvent>();
            _localizeStringEvent.OnUpdateString.AddListener(OnStringUpdate);
            _isInitialized = true;
        }

        public void SetLocalizedData(LocalizedStringData localizedStringData)
        {
            AddTokens(localizedStringData.Tokens);
            _usePureText = localizedStringData.IsPureText;
            if (localizedStringData.LocalizedString != null)
                SetStringReference(localizedStringData.LocalizedString);
            else if (_usePureText)
                SetText(localizedStringData.PureText!);
            else
                SetStringReference(localizedStringData.TableID!, localizedStringData.EntryID!);
        }

        public void SetStringReference(LocalizedString stringLocalized)
        {
            _usePureText = false;
            if (_localizeStringEvent == null)
                _localizeStringEvent = GetComponent<LocalizeStringEvent>();
            _localizeStringEvent.StringReference = stringLocalized;
        }

        public void SetStringReference(string tableID, string entryID)
        {
            _usePureText = false;
            _localizeStringEvent.StringReference = _localizationController.GetLocalizedString(tableID, entryID);
        }

        public void SetText(string text)
        {
            _usePureText = true;
            _localizedString = text;
            UpdateText();
        }

        public void AddToken(string token, Func<string> action)
        {
            _tokens.Add(token, action);
            UpdateText();
        }

        public void AddTokens(Dictionary<string, Func<string>>? tokens)
        {
            if (tokens == null)
                return;
            foreach (KeyValuePair<string, Func<string>> keyValuePair in tokens)
                _tokens.Add(keyValuePair.Key, keyValuePair.Value);
        }

        public void Clear()
        {
            if (!_isInitialized)
                return;
            _usePureText = true;
            _text.text = string.Empty;
            _localizedString = null!;
            _tokens.Clear();
        }

        private void UpdateText()
        {
            if (_localizedString == null!)
                return;
            _text.text = GetResultString(_localizedString);
        }

        private string GetResultString(string localizedString)
        {
            foreach (KeyValuePair<string, Func<string>> keyValuePair in _tokens)
            {
                if (!localizedString.Contains(keyValuePair.Key))
                    continue;
                string replacement = keyValuePair.Value.Invoke();
                localizedString = localizedString.Replace(keyValuePair.Key, replacement);
            }

            return localizedString;
        }

        private void OnStringUpdate(string localizedString)
        {
            if (!_usePureText)
                _localizedString = localizedString;
            UpdateText();
        }
    }
}