using GameCore;

using Sirenix.OdinInspector;
using Sirenix.Serialization;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
namespace CyberBeat
{
	[RequireComponent (typeof (GameEventListenerColorInterractor))]
	public class ComboController : TimeEventsCatcher
	{
		[SerializeField, HideLabel] ComboList combo = new ComboList ();
		[SerializeField, HideLabel] ComboList comboInGame = new ComboList ();
		[SerializeField] GameEventBool OnBitCombo;
		[SerializeField] GameEventInt OnComboEnd;
		[SerializeField] GameEventInt OnActivateCurrentCombo;
		[SerializeField] IntVariable _CurrentCombo;
		int CurrentCombo { get { return _CurrentCombo.Value; } set { _CurrentCombo.Value = value; } }

		[SerializeField] IntVariable Gates;
		[SerializeField] BoolVariable reset;

		bool isCombo;
		/* [ShowInInspector] */
		TimeOfEvent currentCombo = null;
		private void Start ()
		{
			comboInGame = new ComboList (combo);
			CurrentCombo = 0;
		}

		public void GEColorInterractor_CollectCombo (ColorInterractor inter)
		{
			if (!isCombo) return;

			if (currentCombo.InRange (inter.bit))
			{
				// Debug.Log ("CurrentCombo InRange");
				// Debug.LogFormat ("currentCombo = {0}", currentCombo);
				List<ComboBit> comboList = comboInGame[currentCombo];
				// Debug.LogFormat ("comboList = {0}", comboList);
				var currentComboBit = comboList.Find (c => c.bit == inter.bit);
				var prevComboBit = comboList.Find (cb => cb.bit == currentComboBit.prev);
				bool isReset = prevComboBit != null;
				// reset.SetValue (isReset);
				// Debug.LogFormat ("reset = {0}", reset.Value);
				OnBitCombo.Raise (isReset);
				comboList.Remove (currentComboBit);
			}
		}

		public override void _OnChanged (TimeEvent timeEvent)
		{
			// Debug.Log ("_OnChanged State");
			isCombo = timeEvent.isTime;
			currentCombo = timeEvent.timeOfEvent;
			if (!isCombo)
			{
				track.SetGateState (CurrentCombo, false);
				OnComboEnd.Raise (CurrentCombo);
				CurrentCombo++;
			}
		}
		public void _DeActivateCurrentGate ()
		{
			if (!isCombo) return;
			track.SetGateState (CurrentCombo, false);

		}
		Track track { get { return TracksCollection.instance.CurrentTrack; } }

		public void _ActivetCurrentGate ()
		{
			track.SetGateState (CurrentCombo);
			OnActivateCurrentCombo.Raise (CurrentCombo);
			Gates.Decrement ();
		}

		[Title ("For Generation Combo Info")]
		[SerializeField] TimeOfEventsData events;
		[SerializeField] string payloadFilter = "Combo";
		[Button (ButtonSizes.Medium)]
		public void InitCombo ()
		{
			combo = new ComboList ();
			int i = 0;
			List<float> bits = track.BitsInfos.Select (bi => bi.time).ToList ();
			foreach (var bit in bits)
			{
				TimeOfEvent e = events[payloadFilter].Find (t => t.InRange (bit));

				if (!object.ReferenceEquals (e, null))
				{

					if (!combo.ContainsKey (e))
					{
						combo.Add (e, new List<ComboBit> () { new ComboBit (bit, 0f) });
						continue;
					}
					// Debug.LogFormat ("i = {0}", i);
					List<ComboBit> list = combo[e];
					list.Add (new ComboBit (bit, i == 0 ? 0 : bits[i - 1]));
				}
				i++;
			}
		}

	}

	[Serializable]
	public class ComboBit
	{
		[HorizontalGroup, LabelText ("[-1]"), LabelWidth (30)]
		public float prev;
		[HorizontalGroup, LabelText ("[0]"), LabelWidth (25)]
		public float bit;

		public ComboBit (float bit, float prev)
		{
			this.bit = bit;
			this.prev = prev;
		}
		public static bool operator == (ComboBit left, ComboBit right)
		{
			return !object.ReferenceEquals (left, null) &&
				!object.ReferenceEquals (right, null) &&
				left.bit == right.bit &&
				left.prev == right.prev;
		}

		public static bool operator != (ComboBit left, ComboBit right)
		{
			return !(left == right);
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
			items = new List<ComboListItem> (combo.items);;
		}

		public List<ComboBit> this [TimeOfEvent timeOfEvent]
		{
			get
			{
				ComboListItem comboListItem = items.Find (i => i.timeOfEvent == timeOfEvent);
				if (comboListItem != null)
					return comboListItem.comboBits;
				return null;
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
