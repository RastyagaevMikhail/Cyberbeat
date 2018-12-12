using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector.Editor.Drawers;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace GameCore
{
    [OdinDrawer]
    public class VariableReferenceDrawer<TReference, TVariable, TValue> : OdinValueDrawer<TReference>
        where TReference : VariableReference<TVariable, TValue>
        where TVariable : SavableVariable<TValue>
        {
            protected override void DrawPropertyLayout (IPropertyValueEntry<TReference> entry, GUIContent label)
            {
                var value = entry.SmartValue;

                GUILayout.BeginVertical ();
                {
                    var btnRect = GUIHelper.GetCurrentLayoutRect ();
                    btnRect.width = EditorGUIUtility.labelWidth;
                    btnRect = btnRect.AlignRight (18);
                    btnRect.y += 4;

                    if (GUI.Button (btnRect, GUIContent.none, "PaneOptions"))
                    {
                        var menu = new GenericMenu ();
                        menu.AddItem (new GUIContent ("Constant"), value.UseConstant, () => value.UseConstant = true);
                        menu.AddItem (new GUIContent ("Variable"), !value.UseConstant, () => value.UseConstant = false);
                        menu.ShowAsContext ();
                    }

                    EditorGUIUtility.AddCursorRect (btnRect, MouseCursor.Arrow);

                    if (value.UseConstant)
                    {
                        entry.Property.Children["ConstantValue"].Draw (label);
                    }
                    else
                    {
                        entry.Property.Children["Variable"].Draw (label);
                    }
                }
                GUILayout.EndVertical ();
            }
        }
    public class ColorReferenceDrawer : VariableReferenceDrawer<ColorReference, ColorVariable, Color>
    {

    }
    public class IntReferenceDrawer : VariableReferenceDrawer<IntReference, IntVariable, int>
    {
        
    }

}
