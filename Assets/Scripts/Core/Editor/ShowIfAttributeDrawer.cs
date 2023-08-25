#nullable enable

using dmdspirit.Core.Attributes;
using UnityEditor;
using UnityEngine;

namespace dmdspirit.Editor.Core.Editor
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var showIf = (ShowIfAttribute)attribute;
            bool enabled = GetConditionAttributeResult(showIf, property);
            bool previouslyEnabled = GUI.enabled;
            GUI.enabled = showIf.Negative ? !enabled : enabled;
            if (ShouldDisplay(showIf, enabled))
                EditorGUI.PropertyField(position, property, label, true);

            GUI.enabled = previouslyEnabled;
        }

        private bool GetConditionAttributeResult(ShowIfAttribute conditionAttribute, SerializedProperty property)
        {
            var enabled = true;
            string propertyPath = property.propertyPath;
            string conditionPath = propertyPath.Replace(property.name, conditionAttribute.ConditionBoolean);
            SerializedProperty sourcePropertyValue = property.serializedObject.FindProperty(conditionPath);

            if (sourcePropertyValue != null)
                enabled = sourcePropertyValue.boolValue;
            else
                Debug.LogWarning("No matching boolean found for ConditionAttribute in object: {conditionAttribute.ConditionBoolean}");

            return enabled;
        }

        private bool ShouldDisplay(ShowIfAttribute conditionAttribute, bool result)
        {
            bool shouldDisplay = !conditionAttribute.Hidden || result;
            if (conditionAttribute.Negative)
                shouldDisplay = !shouldDisplay;

            return shouldDisplay;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var conditionAttribute = (ShowIfAttribute)attribute;
            bool enabled = GetConditionAttributeResult(conditionAttribute, property);

            if (ShouldDisplay(conditionAttribute, enabled))
                return EditorGUI.GetPropertyHeight(property, label);
            return -EditorGUIUtility.standardVerticalSpacing;
        }
    }
}