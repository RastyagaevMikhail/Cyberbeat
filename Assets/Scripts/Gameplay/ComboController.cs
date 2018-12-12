using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
	public class ComboController : SerializedMonoBehaviour
	{
		[SerializeField] TimePointsData data;
		[SerializeField] Transform PrefabGate;
		[SerializeField] Transform Parent;
		[Button]
		void SetGates ()
		{
			foreach (var point in data.points)
			{
				InstatiateGate (point.Start, "StartGate");
				InstatiateGate (point.End, "EndGate");
			}
		}

		[Button]
		void Clear ()
		{
			foreach (var so in GetComponentsInChildren<SpawnedObject> ())
			{
				Tools.Destroy (so.gameObject);
			}
		}
		private Transform InstatiateGate (TimePointInfo pointInfo, string prefixName)
		{
			Transform gate = Instantiate (PrefabGate, pointInfo.position, pointInfo.rotation, Parent);
			gate.localPosition = pointInfo.position;
			gate.name = prefixName + "{0}".AsFormat (gate.GetInstanceID ());
			return gate;
		}
		GameController gameCtrl { get { return GameController.instance; } }
		private TimeEventsController _timeEventsCtrl = null;
		public TimeEventsController timeEventsCtrl { get { return _timeEventsCtrl ?? (_timeEventsCtrl = GetComponent<TimeEventsController> ()); } }

		[SerializeField] GameEventObject OnDeathColorInterractor;
		[SerializeField] GameEventObject OnOnBitCombo;
		EventListener listener;
		private void Start ()
		{
			timeEventsCtrl.OnChanged += OnChanged;
			comboInGame = new Dictionary<TimeOfEvent, List<ComboBit>> (combo);
		}
		private void OnEnable ()
		{
			listener = new EventListener (OnDeathColorInterractor, () => CollectCombo (OnDeathColorInterractor.arg));
			listener.OnEnable ();
		}
		private void OnDisable ()
		{
			timeEventsCtrl.OnChanged -= OnChanged;
			listener.OnDisable ();
		}

		[SerializeField] BoolVariable reset;
		private void CollectCombo (UnityObjectVariable unityObject)
		{
			if (!isCombo) return;
			ColorInterractor inter = null;
			if (!unityObject.CheckAs<ColorInterractor> (out inter)) return;

			if (currentCombo.InRange (inter.bit))
			{
				// Debug.Log ("CurrentCombo InRange");
				List<ComboBit> comboList = comboInGame[currentCombo];
				var currentComboBit = comboList.Find (c => c.bit == inter.bit);
				reset.SetValue (comboList.Find (cb => cb.bit == currentComboBit.prev) != null);
				// Debug.LogFormat ("reset = {0}", reset.Value);
				OnOnBitCombo.Raise (reset);
				comboList.Remove (currentComboBit);
			}
		}

		bool isCombo;
		TimeOfEvent currentCombo;

		[SerializeField] Dictionary<TimeOfEvent, List<ComboBit>> combo = new Dictionary<TimeOfEvent, List<ComboBit>> ();
		[SerializeField] Dictionary<TimeOfEvent, List<ComboBit>> comboInGame = new Dictionary<TimeOfEvent, List<ComboBit>> ();

		Track track { get { return TracksCollection.instance.CurrentTrack; } }

		[SerializeField] TimeOfEventsData events;

		[Button]
		private void InitCombo ()
		{
			combo = new Dictionary<TimeOfEvent, List<ComboBit>> ();
			int i = 0;
			List<float> bits = track.BitsInfos.Select (bi => bi.time).ToList ();
			foreach (var bit in bits)
			{
				var e = events.Times.Find (t => t.InRange (bit));
				if (e != null)
				{

					if (!combo.ContainsKey (e))
					{
						combo.Add (e, new List<ComboBit> () { new ComboBit (bit, 0f) });
						continue;
					}
					combo[e].Add (new ComboBit (bit, bits[i - 1]));
				}
				i++;
			}
		}

		[SerializeField] GameEvent OnComboEnd;
		private void OnChanged (bool isCombo, TimeOfEvent timeEvent)
		{
			this.isCombo = isCombo;
			currentCombo = timeEvent;
			if (!isCombo) OnComboEnd.Raise ();
		}
	}

	public class ComboBit
	{
		public float bit;
		public float prev;

		public ComboBit (float bit, float prev)
		{
			this.bit = bit;
			this.prev = prev;
		}
	}
}
