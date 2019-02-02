using DG.Tweening;

using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class ChaseCamUser : MetaDataUser<ChaseCamMetaData, ChaseCamData>
	{
		ChaseCam chaseCam { get { return ChaseCam.instance; } }

		[SerializeField] Transform MoveTarget;
		[SerializeField] Transform LookTarget;
		public override void OnMetaData (ChaseCamData data)
		{
			MoveTarget.DOLocalMove (data.TargetMovePosition, data.DurationTimeOfMove);
			LookTarget.DOLocalMove (data.TargetLookPosition, data.DurationTimeOfLook);

			chaseCam.ChaseTime = data.TimeChase;
		}
		public override void OnMetaReached (ChaseCamMetaData meta)
		{
			OnMetaData (meta.data);
		}

	}
}
