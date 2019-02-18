using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

public class AnimatorCallAnimationEvents : MonoBehaviour
{
	private Animator _animator = null;
	public Animator animator
	{
		get
		{
			if (_animator == null)
				_animator = GetComponent<Animator> ();
			return _animator;
		}
	}

	[SerializeField] List<AnimationEventInfo> Events;
	Dictionary<string, UnityEvent> dictEevnts = null;
	Dictionary<string, UnityEvent> DictEevnts { get { if (dictEevnts == null) dictEevnts = Events.ToDictionary (e => e.name, e => e.Event); return dictEevnts; } }

	public void CallEvent (string NameEvent)
	{
		DictEevnts[NameEvent].Invoke ();
		
	}

	[System.Serializable]
	class AnimationEventInfo
	{
		public string name;
		public UnityEvent Event;

	}
}
