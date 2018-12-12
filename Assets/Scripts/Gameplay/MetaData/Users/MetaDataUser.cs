using FluffyUnderware.Curvy;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	public abstract class MetaDataUser<TMetaData> : MonoBehaviour where TMetaData : Component, ICurvyMetadata
	{

		public void _OnPointReached (CurvySplineMoveEventArgs e)
		{
			var data = e.ControlPoint.GetMetadata<TMetaData> ();
			if (data == null) return;
			OnMetaReached (data);
		}

		public abstract void OnMetaReached (TMetaData metaData);
	}
}
