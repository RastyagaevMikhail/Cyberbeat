using DG.Tweening;

using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class ChaseCamUser : MetaDataUser<ChaseCamMetaData>
	{
		ChaseCam chaseCam { get { return ChaseCam.instance; } }
		EZCameraShake.CameraShaker camShaker { get { return EZCameraShake.CameraShaker.Instance; } }

		[SerializeField] Transform MoveTarget;
		[SerializeField] Transform LookTarget;
		public override void OnMetaReached (ChaseCamMetaData meta)
		{
			var data = meta.data;
			MoveTarget.DOLocalMove (data.TargetMovePosition, data.DurationTimeOfMove);
			LookTarget.DOLocalMove (data.TargetLookPosition, data.DurationTimeOfLook);

			chaseCam.ChaseTime = data.TimeChase;
			camShaker.enabled = data.useShake;
			if (data.useShake)
			{

				camShaker.ShakeOnce (
					data.magnitude,
					data.roughness,
					data.fadeInTime,
					data.fadeOutTime,
					data.posInfluence,
					data.rotInfluence
				);
			}
		}
	}
}
