using System.Collections.Generic;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector;
#endif
namespace CyberBeat
{
	[System.Serializable]
	public class Preset
	{
		public string Id;
		[ListDrawerSettings()]
		// [PreviewField]
		public List<Material> Objects;
	}/* 
#if UNITY_EDITOR	
	[CustomPropertyDrawer (typeof (Preset))]
	public class PresetDrawer : PropertyDrawer
	{
		int controlID = Animator.StringToHash ("Material");
		int selectedIndexItem = -1;
		public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
		{
			return EditorGUIUtility.singleLineHeight + 5f + 75f;
		}
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
		{
			var Id = property.FindPropertyRelative ("Id");
			var IDRect = new Rect (position) { height = EditorGUIUtility.singleLineHeight };
			EditorGUI.PropertyField (IDRect, Id);
			var Objects = property.FindPropertyRelative ("Objects");
			EditorGUI.BeginProperty (position, GUIContent.none, Objects);
			// EditorGUI.PropertyField (position, Objects);

			if (Objects.isArray)
			{
				Rect pos = position;
				pos.y += EditorGUIUtility.singleLineHeight + 2.5f;
				pos.height = 75f;
				pos.width = 75f;
				for (int i = 0; i < Objects.arraySize; i++)
				{
					var obj = Objects.GetArrayElementAtIndex (i);
					// EditorGUI.ObjectField (pos, GUIContent.none, obj.objectReferenceValue, typeof (Material), false);
					// EditorGUI.PropertyField (pos, obj, GUIContent.none, false);
					var guiContent = EditorGUIUtility.ObjectContent (obj.objectReferenceValue, typeof (Object));
					var style = GUI.skin.box;
					style.fixedHeight = 75;
					style.imagePosition = ImagePosition.ImageAbove;

					if (GUI.Button (pos, guiContent, style))
					{						
						EditorGUIUtility.ShowObjectPicker<Material> (obj.objectReferenceValue, false, "", controlID);
					}
					pos.x += 80;
				}
			}
			EditorGUI.EndProperty ();

		}
	}
#endif */
}
