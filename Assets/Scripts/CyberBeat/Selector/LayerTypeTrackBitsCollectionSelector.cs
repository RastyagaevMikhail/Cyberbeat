using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
namespace CyberBeat
{
    [CreateAssetMenu (menuName = "CyberBeat/Selectors/LayerTypeTrackBitsCollection")]
    public class LayerTypeTrackBitsCollectionSelector : AEnumDataSelectorScriptableObject<LayerType, TrackBitsCollection>
    {
        public List<LayerTypeTrackBitsCollectionTypeData> datas;
        public override List<TypeData<LayerType, TrackBitsCollection>> Datas
        {
            get
            {
                return datas.Cast<TypeData<LayerType, TrackBitsCollection>> ().ToList ();
            }
        }

        [System.Serializable] public class LayerTypeTrackBitsCollectionTypeData : TypeData<LayerType, TrackBitsCollection> { }
    }
#if UNITY_EDITOR
    [CustomPropertyDrawer (typeof (LayerTypeTrackBitsCollectionSelector.LayerTypeTrackBitsCollectionTypeData))]
    public class LayerTypeTrackBitsCollectionTypeDataDrawer : PropertyDrawer
    {
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
        {
            var key = property.FindPropertyRelative ("type");
            var value = property.FindPropertyRelative ("data");
            var labelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 65f;
            var rect = EditorGUI.PrefixLabel (position, new GUIContent (((LayerType) key.intValue).ToString ()));
            EditorGUIUtility.labelWidth = labelWidth;
            EditorGUI.PropertyField (rect, value, GUIContent.none);

        }
    }
#endif
}
