using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	[RequireComponent (typeof (Collider))]
	public class TriggerLayersActions : MonoBehaviour
	{
		private Collider _collider = null;
		public new Collider collider { get { if (_collider == null) _collider = GetComponent<Collider> (); return _collider; } }
		private void Awake () { collider.isTrigger = true; }

		[SerializeField] List<LayersAction> OnEnterTrigger;
		[SerializeField] List<LayersAction> OnExitTrigger;
		public void AddOnEnterAction (LayerMask layermask, UnityEventGameObject action)
		{
			OnEnterTrigger.Add (new LayersAction () { layermask = layermask, action = action });
		}
		public void AddOnExitAction (LayerMask layermask, UnityAction<GameObject> action)
		{
			var LayersAction = OnExitTrigger.Find (ta => ta.layermask == layermask);
			if (LayersAction == null) return;
			LayersAction.action.AddListener (action);
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
	public class LayersAction
	{
		public LayerMask layermask;
		public UnityEventGameObject action;
		public void Invoke (GameObject go)
		{
			if (layermask == (layermask | (1 << go.layer)))
			{
				action.Invoke (go);
			}
		}
	}
}
