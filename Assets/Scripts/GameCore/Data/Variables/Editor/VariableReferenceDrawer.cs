using System;
using System.Collections.Generic;

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Drawers;
using Sirenix.Utilities.Editor;

using UnityEditor;

using UnityEngine;

namespace GameCore
{
    [OdinDrawer]
    public class VariableReferenceDrawer<U, T, K> : OdinValueDrawer<U> where U : VariableReference<T, K> where T : SavableVariable<K>
    {
        /// <summary>
        /// Options to display in the popup to select constant or variable.
        /// </summary>
        private readonly string[] popupOptions = { "Constant", "Variable" };
        protected override GUICallType GUICallType { get { return GUICallType.Rect; } }
        /// <summary> Cached style to use to draw the popup button. </summary>
        private GUIStyle popupStyle;

        protected override void DrawPropertyRect (Rect position, IPropertyValueEntry<U> entry, GUIContent label)
        {
            var variableReference = entry.SmartValue;
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle (GUI.skin.GetStyle ("PaneOptions"));
                popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            if (label != null)
            {
                position = EditorGUI.PrefixLabel (position, label);
            }

            EditorGUI.BeginChangeCheck ();

            // Calculate rect for configuration button
            Rect buttonRect = new Rect (position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            variableReference.UseConstant = EditorGUI.Popup (buttonRect, variableReference.UseConstant ? 0 : 1, popupOptions, popupStyle) == 0;
            
            variableReference.DrawMe (position);

            entry.SmartValue = variableReference;
        }
    }
}
