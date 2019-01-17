using FluffyUnderware.Curvy;

using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
	public class ActionMetaData : MonoBehaviour, ICurvyMetadata
	{
		public MetaAction data;
		public string NameOfMetaType
		{
			get
			{
				return this.GetType ().Name.Replace ("MetaData", "").SplitPascalCase ();
			}
		}
	}
}
