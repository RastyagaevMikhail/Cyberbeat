using SimpleJSON;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace GameCore
{

	[CreateAssetMenu (fileName = "List_int", menuName = "Variables/GameCore/List<int>", order = 0)]
	public class IntListVariable : SavableVariable<List<int>>, IEnumerable<int>
	{
		public override void ResetDefault ()
		{
			if (ResetByDefault)
			{
				base.Value = base.DefaultValue;
				SaveValue ();
			}
		}

		public string strValue
		{
			get
			{
				JSONNode node = new JSONArray ();
				foreach (var item in _value)
					node.Add (item);
				return node.AsArray.ToString ();
			}
		}

		public string strDeafultValue
		{
			get
			{
				JSONNode node = new JSONArray ();
				foreach (var item in DefaultValue)
					node.Add (item);
				return node.AsArray.ToString ();
			}
		}

		public override void SaveValue ()
		{
			PlayerPrefs.SetString (name, strValue);
		}
		public override void LoadValue ()
		{
			base.LoadValue ();
			string JSON_value = PlayerPrefs.GetString (name, strDeafultValue);
			_value = JSON.Parse (JSON_value).AsArray.Children.Select (n => n.AsInt).ToList ();
		}
		public int this [int index]
		{
			get { return Value[index]; }
			set { Value[index] = value; }
		}

		[ContextMenu ("Reset to Default")] public void ResetToDefault ()
		{
			_value = new List<int> (DefaultValue);
		}

		public IEnumerator<int> GetEnumerator ()
		{
			return ((IEnumerable<int>) _value).GetEnumerator ();
		}

		IEnumerator IEnumerable.GetEnumerator ()
		{
			return ((IEnumerable<int>) _value).GetEnumerator ();
		}
		public int Count { get { return _value.Count; } }
		public void Remove (int item)
		{
			if (Value == null)
			{
				Value = new List<int> ();
				return;
			}
			Value.Remove (item);
		}
		public void Add (int item)
		{
			if (Value == null)
				Value = new List<int> ();
			Value.Add (item);
		}
		public int random
		{
			get
			{
				if (Count == 0) ResetToDefault ();
				int rand = _value.GetRandom ();
				Remove (rand);
				return rand;
			}
		}
	}
}
