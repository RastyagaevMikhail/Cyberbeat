using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class EditorStates : MonoBehaviour
{
	public List<State> States;
	[Button ("AddState",ButtonSizes.Medium)]
	void AddState ()
	{
#if UNITY_EDITOR
		var objects = UnityEditor.Selection.gameObjects.Select (go => new ObjectState () { gameObject = go, active = go.activeSelf }).ToList ();
		States.Add (new State () { ObjectStates = objects });
#endif
	}
}

[System.Serializable]
public class ObjectState
{
	public GameObject gameObject;
	public bool active;
}

[System.Serializable]
public class State
{
	[InlineButton ("ActivateState", "Activate")]
	public string name;
	public List<ObjectState> ObjectStates;

	public void ActivateState ()
	{
		foreach (var os in ObjectStates)
			os.gameObject.SetActive (os.active);
	}

}

#if UNITY_EDITOR

// [CustomPropertyDrawer (typeof (State))]
// public class StateDrawer : PropertyDrawer
// {
// 	SerializedProperty nameProp;
// 	SerializedProperty ObjectStatesProp;

// 	public override float GetPropertyHeight (SerializedProperty property, GUIContent label)
// 	{
// 		return EditorGUIUtility.singleLineHeight * (3 * ObjectStatesProp.arraySize + 1);
// 	}
// 	public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
// 	{
// 		nameProp = property.FindPropertyRelative ("name");
// 		nameProp.stringValue = EditorGUI.TextField (position, nameProp.stringValue);

// 		ObjectStatesProp = property.FindPropertyRelative ("ObjectStates");
// 		EditorGUI.PropertyField (position, ObjectStatesProp, true);
// 	}
// }

#endif
