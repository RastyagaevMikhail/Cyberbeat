using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
	using GameCore;

	using System.Linq;
	public class TracksCollection : DataCollections<TracksCollection, Track>
	{
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/Collections/Tracks")] public static void Select () { UnityEditor.Selection.activeObject = instance; }

		public override void InitOnCreate ()
		{
			Objects = Tools.GetAtPath<Track> ("Assets/Resources/Data/Tracks").ToList ();
		}
		public override void ResetDefault ()
		{

		}
#endif
	}
}
