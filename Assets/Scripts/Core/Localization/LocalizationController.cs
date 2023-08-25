#nullable enable

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace dmdspirit.Core.Localization
{
    [UsedImplicitly]
    public sealed class LocalizationController : ILocalizationController
    {
        private readonly Dictionary<Language, Locale> _availableLocales = new();
        private readonly IReactiveProperty<bool> _isInitialized = new ReactiveProperty<bool>();

        public Language? SelectedLanguage { get; private set; }

        public IReadOnlyList<Language> AvailableLanguages => _availableLocales.Keys.ToList();
        public IReadOnlyReactiveProperty<bool> IsInitialized => _isInitialized;

        public LocalizationController()
            => InitializeLocalization();

        public void SelectLanguage(Language language)
        {
            LocalizationSettings.SelectedLocale = GetLocale(language);
            SelectedLanguage = language;
        }

        public LocalizedString GetLocalizedString(string table, string entryID)
        {
            var result = new LocalizedString(table, entryID);
            if (result.IsEmpty)
                Debug.LogError($"Could not get {nameof(StringLocalized)} from table {table} and entry {entryID}");

            return result;
        }

        private async void InitializeLocalization()
        {
            await LocalizationSettings.Instance.GetInitializationOperation().Task;
            List<Locale>? locales = LocalizationSettings.AvailableLocales.Locales;

            foreach (Locale locale in locales)
            {
                var language = new Language(locale.LocaleName);
                _availableLocales.Add(language, locale);
                if (locale == LocalizationSettings.SelectedLocale)
                    SelectedLanguage = language;
            }

            _isInitialized.Value = true;
        }

        private Locale GetLocale(Language language)
            => _availableLocales[language];
    }
}