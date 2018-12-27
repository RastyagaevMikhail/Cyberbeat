using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class EditorStates : MonoBehaviour
{
	public List<State> States;
	[ContextMenu ("AddState")]
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
	public string name;
	public List<ObjectState> ObjectStates;
	[ContextMenu ("ActivateState")]
	public void ActivateState ()
	{
		foreach (var os in ObjectStates)
			os.gameObject.SetActive (os.active);
	}
}
