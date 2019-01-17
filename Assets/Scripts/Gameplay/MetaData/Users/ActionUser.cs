using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	public class ActionUser : MetaDataUser<ActionMetaData, MetaAction>
	{
		public override void OnMetaData (MetaAction metaData)
		{
			metaData.Invoke ();
		}
		public override void OnMetaReached (ActionMetaData metaData)
		{
			OnMetaData (metaData.data);
		}
	}
}
