using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class EditorStates : MonoBehaviour
{

	[ContextMenuItem ("Add State", "AddState")]
	[ContextMenuItem ("Activate State", "ActivaeState")]
	[SerializeField] string NameState;
	[ContextMenuItem ("Activate State", "ActivaeStateByIndex")]
	[SerializeField] int StateIndex;
	public List<State> States;

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
	public UnityEvent ObjectState;
	
	[Button]
	public void ActivateState ()
	{
		ObjectState.Invoke ();
	}
}
