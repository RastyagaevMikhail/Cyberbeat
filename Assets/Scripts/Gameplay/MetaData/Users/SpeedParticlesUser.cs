using DG.Tweening;

using FluffyUnderware.Curvy;

using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	public class SpeedParticlesUser : MetaDataUser<SpeedParticlesMetaData, PartsData>
	{
		SpeedParticles speedParts { get { return SpeedParticles.instance; } }

		public override void OnMetaData (PartsData data)
		{
			speedParts.MoveOnBit = data.MoveOnBit;
			DOVirtual.Float (speedParts.StartLifetime, data.StartLifetime, data.timeDuaration, (val) => speedParts.StartLifetime = val);
			DOVirtual.Float (speedParts.StartSpeed, data.StartSpeed, data.timeDuaration, (val) => speedParts.StartSpeed = val);
			DOVirtual.Float (speedParts.StartSize, data.StartSize, data.timeDuaration, (val) => speedParts.StartSize = val);
			DOVirtual.Float (speedParts.LengthScale, data.LengthScale, data.timeDuaration, (val) => speedParts.LengthScale = val);
			DOVirtual.Float (speedParts.OverRate, data.OverRate, data.timeDuaration, (val) => speedParts.OverRate = val);
		}

		public override void OnMetaReached (SpeedParticlesMetaData meta)
		{
			OnMetaData (meta.data);
		}
	}
}
