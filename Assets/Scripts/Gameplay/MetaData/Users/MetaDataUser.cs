using FluffyUnderware.Curvy;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	public abstract class MetaDataUser<TMetaDataComponent, TMetaData> : MonoBehaviour
	where TMetaDataComponent : Component, ICurvyMetadata
	where TMetaData : IMetaData
	{

		public void _OnPointReached (CurvySplineMoveEventArgs e)
		{
			var data = e.ControlPoint.GetMetadata<TMetaDataComponent> ();
			if (data == null) return;
			OnMetaReached (data);
		}

		public abstract void OnMetaReached (TMetaDataComponent metaData);
		public abstract void OnMetaData (TMetaData metaData);
	}
}
