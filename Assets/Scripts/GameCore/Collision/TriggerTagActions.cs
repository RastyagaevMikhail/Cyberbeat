using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	[RequireComponent (typeof (Collider))]
	public class TriggerTagActions : MonoBehaviour
	{
		private Collider _collider = null;
		public new Collider collider { get { if (_collider == null) _collider = GetComponent<Collider> (); return _collider; } }
		private void Awake () { collider.isTrigger = true; }

		[SerializeField] List<TagAction> OnEnterTrigger;
		[SerializeField] List<TagAction> OnExitTrigger;
		public void AddOnEnterAction (string Tag, UnityEventGameObject action)
		{
			TagAction tagAction = new TagAction ();
			tagAction.Tag = Tag;
			tagAction.action = action;
			OnEnterTrigger.Add (tagAction);

		}
		public void AddOnExitAction (string Tag, UnityAction<GameObject> action)
		{
			var tagAction = OnExitTrigger.Find (ta => ta.Tag == Tag);
			if (tagAction == null) return;
			tagAction.action.AddListener (action);
		}

		private void OnTriggerEnter (Collider other)
		{
			foreach (var trigger in OnEnterTrigger)
				trigger.Invoke (other.gameObject);
		}

		private void OnTriggerExit (Collider other)
		{
			foreach (var trigger in OnExitTrigger)
				trigger.Invoke (other.gameObject);
		}
	}

	[System.Serializable]
	public class TagAction
	{
		public string Tag;
		public UnityEventGameObject action;
		public void Invoke (GameObject go)
		{
			if (go.CompareTag (Tag))
				action.Invoke (go);
		}
	}
}
