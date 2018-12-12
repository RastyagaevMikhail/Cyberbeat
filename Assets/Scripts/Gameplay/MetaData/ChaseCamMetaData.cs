namespace CyberBeat
{
    using FluffyUnderware.Curvy;
    using Sirenix.OdinInspector;
	using Sirenix.Utilities;

	using UnityEngine;

	public class ChaseCamMetaData : MonoBehaviour, ICurvyMetadata
	{
		[HideLabel]
		public ChaseCamData data;

		public string NameOfMetaType
		{
			get
			{
				return this.GetType ().Name.Replace ("MetaData", "").SplitPascalCase ();
			}
		}
	}
}
