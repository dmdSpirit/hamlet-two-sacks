#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine.Localization;

namespace dmdspirit.Core.Localization
{
    public sealed class LocalizedStringData
    {
        public readonly string? TableID;
        public readonly string? EntryID;
        public readonly string? PureText;
        public readonly LocalizedString? LocalizedString;
        public readonly Dictionary<string, Func<string>>? Tokens;

        public bool IsPureText => PureText != null;

        public LocalizedStringData(string tableID, string entryID, Dictionary<string, Func<string>>? tokens)
        {
            LocalizedString = null;
            TableID = tableID;
            EntryID = entryID;
            PureText = null;
            Tokens = tokens;
        }

        public LocalizedStringData(string pureText, Dictionary<string, Func<string>>? tokens)
        {
            LocalizedString = null;
            TableID = null;
            EntryID = null;
            PureText = pureText;
            Tokens = tokens;
        }

        public LocalizedStringData(LocalizedString localizedString, Dictionary<string, Func<string>>? tokens)
        {
            LocalizedString = localizedString;
            TableID = null;
            EntryID = null;
            PureText = null;
            Tokens = tokens;
        }
    }
}