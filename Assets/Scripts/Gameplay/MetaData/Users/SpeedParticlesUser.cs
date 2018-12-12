using DG.Tweening;

using FluffyUnderware.Curvy;

using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class SpeedParticlesUser : MetaDataUser<SpeedParticlesMetaData>
	{
		SpeedParticles speedParts { get { return SpeedParticles.instance; } }
		public override void OnMetaReached (SpeedParticlesMetaData meta)
		{
			speedParts.MoveOnBit = meta.data.MoveOnBit;
			DOVirtual.Float (speedParts.StartLifetime, meta.data.StartLifetime, meta.data.Duration, (val) => speedParts.StartLifetime = val);
			DOVirtual.Float (speedParts.StartSpeed, meta.data.StartSpeed, meta.data.Duration, (val) => speedParts.StartSpeed = val);
			DOVirtual.Float (speedParts.StartSize, meta.data.StartSize, meta.data.Duration, (val) => speedParts.StartSize = val);
			DOVirtual.Float (speedParts.LengthScale, meta.data.LengthScale, meta.data.Duration, (val) => speedParts.LengthScale = val);
			DOVirtual.Float (speedParts.OverRate, meta.data.OverRate, meta.data.Duration, (val) => speedParts.OverRate = val);
		}
	}
}
