using FluffyUnderware.Curvy;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
	public class ActionMetaData : MonoBehaviour, ICurvyMetadata, IMetaData
	{
		public UnityEvent actionEvent;
		public string NameOfMetaType
		{
			get
			{
				return this.GetType ().Name.Replace ("MetaData", "").SplitPascalCase ();
			}
		}
	}
}
