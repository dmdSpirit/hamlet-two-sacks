#nullable enable

using System;
using UnityEngine;

namespace dmdspirit.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class
                    | AttributeTargets.Struct)]
    public class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionBoolean;
        public bool Hidden;
        public bool Negative;

        public ShowIfAttribute(string conditionBoolean)
        {
            ConditionBoolean = conditionBoolean;
            Hidden = false;
            Negative = false;
        }

        public ShowIfAttribute(string conditionBoolean, bool hideInInspector)
        {
            ConditionBoolean = conditionBoolean;
            Hidden = hideInInspector;
            Negative = false;
        }

        public ShowIfAttribute(string conditionBoolean, bool hideInInspector, bool negative)
        {
            ConditionBoolean = conditionBoolean;
            Hidden = hideInInspector;
            Negative = negative;
        }
    }
}