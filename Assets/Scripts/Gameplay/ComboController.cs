using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
	[RequireComponent (typeof (GameEventListenerFloat))]
	public class ComboController : TimeEventsCatcher
	{
		[SerializeField] ComboList combo = null;
		[SerializeField] ComboList comboInGame = null;
		[SerializeField] GameEventBool OnBitCombo;
		[SerializeField] GameEventInt OnComboEnd;
		[SerializeField] GameEventInt OnActivateCurrentCombo;
		[SerializeField] IntVariable CurrentComboIndex;
		[SerializeField] IntVariable ScorePerBeat;
		[SerializeField] IntVariable CurrentComboMaxCount;
		[SerializeField] IntVariable CurrentComboBeatCount;
		int currentComboIndex { get { return CurrentComboIndex.Value; } set { CurrentComboIndex.Value = value; } }

		[SerializeField] ABayable Gates;
		[SerializeField] BoolVariable IsCombo;
		bool isCombo { get { return IsCombo.Value; } set { IsCombo.Value = value; } }
		TimeOfEvent currentCombo = null;
		private void Start ()
		{
			comboInGame = new ComboList (combo);
			currentComboIndex = 0;
			CurrentComboMaxCount.Value = comboInGame[currentComboIndex];
			isCombo = false;
			ScorePerBeat.Value = 1;
		}

		public void OnCollectBit (float bit)
		{
			if (!isCombo) return;

			if (currentCombo.InRange (bit))
			{
				var comboList = comboInGame[currentCombo];

				ComboBit currentComboBit = null;
				comboList.TryGetValue (bit, out currentComboBit);
				if (currentComboBit == null) return;
				bool isReset = false;
				if (currentComboBit.prev != 0)
				{
					ComboBit prevComboBit = null;
					comboList.TryGetValue (currentComboBit.prev, out prevComboBit);
					isReset = prevComboBit != null;
				}
				if (isReset) CurrentComboBeatCount.ResetDefault ();
				CurrentComboBeatCount.Increment ();

				OnBitCombo.Raise (isReset);
				comboList.Remove (currentComboBit.bit);
			}
		}

		public override void _OnChanged (TimeEvent timeEvent)
		{
			// Debug.Log ("_OnChanged State");

			currentCombo = timeEvent.timeOfEvent;
			CurrentComboMaxCount.Value = comboInGame[currentComboIndex];
			if (timeEvent.isTime)
			{
				isCombo = track.GetGateState (currentComboIndex);
				ScorePerBeat.Value = isCombo ? 2 : 1;
			}
			else
			{
				track.SetGateState (currentComboIndex, false);
				OnComboEnd.Raise (currentComboIndex);
				isCombo = false;
				currentComboIndex++;

			}
		}
		public void _DeActivateCurrentGate ()
		{
			if (!isCombo) return;
			track.SetGateState (currentComboIndex, false);

		}
		Track track { get { return TracksCollection.instance.CurrentTrack; } }

		public void _ActivetCurrentGate ()
		{
			Gates.TryUse (() =>
			{
				track.SetGateState (currentComboIndex);
				OnActivateCurrentCombo.Raise (currentComboIndex);
			});

		}

		[Header ("For Generation Combo Info")]
		[SerializeField] TimeOfEventsData events;
		[SerializeField] string payloadFilter = "Combo";
	/* 	[ContextMenu ("Init Combo")]
		public void InitCombo ()
		{
			combo = new ComboList ();
			int i = 0;
			List<float> bits = track.BitsInfos.Select (bi => bi.time).ToList ();
			foreach (var bit in bits)
			{
				TimeOfEvent e = events[payloadFilter].Find (t => t.InRange (bit));

				if (e != null)
				{

					if (!combo.ContainsKey (e))
					{
						combo.Add (e, new List<ComboBit> () { new ComboBit (bit, 0f) });
						i++;
						continue;
					}
					// Debug.LogFormat ("i = {0}", i);
					List<ComboBit> list = combo.items.Find (c => c.timeOfEvent == e).comboBits;
					list.Add (new ComboBit (bit, i == 0 ? 0 : bits[i - 1]));
				}
				i++;
			}
		} */
	}

	[Serializable]
	public class ComboBit
	{
		public float prev;
		public float bit;

		public ComboBit (float bit, float prev)
		{
			this.bit = bit;
			this.prev = prev;
		}
	}

	[Serializable]
	public class ComboListItem
	{
		public TimeOfEvent timeOfEvent;
		public List<ComboBit> comboBits;
	}

	[Serializable]
	public class ComboList
	{
		public List<ComboListItem> items;
		public ComboList ()
		{
			items = new List<ComboListItem> ();
		}
		public ComboList (ComboList combo)
		{
			items = new List<ComboListItem> (combo.items);
			InitDict ();
		}
		Dictionary<TimeOfEvent, Dictionary<float, ComboBit>> _dict = null;
		Dictionary<TimeOfEvent, Dictionary<float, ComboBit>> dict { get { if (_dict == null) InitDict (); return _dict; } }

		private void InitDict ()
		{
			_dict = items.ToDictionary (item => item.timeOfEvent,
				item => item.comboBits.ToDictionary (cli => cli.bit));
		}
		public Dictionary<float, ComboBit> this [TimeOfEvent timeOfEvent]
		{
			get
			{
				Dictionary<float, ComboBit> comboListItem = null;
				// Debug.LogFormat ("dict = {0}", dict);
				// Debug.Log (Tools.LogCollection (dict.Keys));
				dict.TryGetValue (timeOfEvent, out comboListItem);
				// Debug.LogFormat ("timeOfEvent = {0}", timeOfEvent);
				// Debug.LogFormat ("comboListItem = {0}", comboListItem);
				return comboListItem;
			}
		}
		///Get Count of Bint in currentCombo
		public int this [int index]
		{
			get
			{
				return items[index].comboBits.Count;
			}
		}

		public void Add (TimeOfEvent e, List<ComboBit> list)
		{
			if (items == null) items = new List<ComboListItem> ();
			items.Add (new ComboListItem () { timeOfEvent = e, comboBits = list });
		}

		public bool ContainsKey (TimeOfEvent e)
		{
			return items.Exists (i => i.timeOfEvent == e);
		}

	}

}
