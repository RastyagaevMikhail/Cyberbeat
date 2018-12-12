using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class ActionUser : MetaDataUser<ActionMetaData>
	{
		public override void OnMetaReached (ActionMetaData metaData)
		{
			metaData.actionEvent.Invoke ();
		}
	}
}
