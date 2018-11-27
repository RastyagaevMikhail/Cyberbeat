using Sirenix.OdinInspector;

using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	[CreateAssetMenu (fileName = "GameEventObject", menuName = "Events/GameCore/GameEventObject")]
	public class GameEventObject : GameEvent
	{
		/// <summary>
		/// The list of listeners that this event will notify if it is raised.
		/// </summary>
		[SerializeField]
		private readonly List<EventListener> eventListeners = new List<EventListener> ();
		public UnityObjectVariable arg;
		public void Raise (Object obj)
		{
			arg.SetValue (obj);
			for (int i = eventListeners.Count - 1; i >= 0; i--)
				eventListeners[i].OnEventRaised ();
		}

		public override void RegisterListener (EventListener listener)
		{
			if (!eventListeners.Contains (listener))
				eventListeners.Add (listener);
		}

		public override void UnregisterListener (EventListener listener)
		{
			if (eventListeners.Contains (listener))
				eventListeners.Remove (listener);
		}

		[Title ("Test")]
		[SerializeField] UnityEngine.Object testArg;
		[Button] public void TestRaise () { Raise (testArg); }
	}
}
