using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class EditorStates : MonoBehaviour
{

	[ContextMenuItem ("Add State", "AddState")]
	[ContextMenuItem ("Activate State", "ActivaeState")]
	[SerializeField] string NameState;
	[ContextMenuItem ("Activate State", "ActivaeStateByIndex")]
	[SerializeField] int StateIndex;
	public List<State> States;
	[ContextMenu ("AddState")]
	void AddState ()
	{
#if UNITY_EDITOR
		var objects = UnityEditor.Selection.gameObjects.Select (go => new ObjectState () { gameObject = go, active = go.activeSelf }).ToList ();
		States.Add (new State () { ObjectStates = objects, name = NameState });
#endif
	}
	public void ActivaeStateByIndex ()
	{
		var _state = States[StateIndex];
		if (_state != null) _state.ActivateState ();
	}
	public void ActivaeState ()
	{
		var _state = States.Find (state => state.name == NameState);
		if (_state != null) _state.ActivateState ();
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
	[ContextMenuItem ("ActivateState", "ActivateState")]
	public string name;
	public List<ObjectState> ObjectStates;
	public void ActivateState ()
	{
		foreach (var os in ObjectStates)
			os.gameObject.SetActive (os.active);
	}
}
