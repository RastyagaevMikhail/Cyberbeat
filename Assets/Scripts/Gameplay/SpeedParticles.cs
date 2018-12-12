using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SonicBloom.Koreo;
using UnityEngine;
namespace CyberBeat
{
	public class SpeedParticles : MonoBehaviour
	{
		private static SpeedParticles _instance = null;
		public static SpeedParticles instance
		{
			get
			{
				if (_instance == null) _instance = GameObject.FindObjectOfType<SpeedParticles>();
				return _instance;
			}
		}
		private ParticleSystem _pSystem = null;
		public ParticleSystem pSystem
		{
			get
			{
				if (_pSystem == null) _pSystem = GetComponent<ParticleSystem>();
				return _pSystem;
			}
		}
		public float StartLifetime
		{
			get
			{
				return pSystem.main.startLifetimeMultiplier;
			}
			set
			{
				ParticleSystem.MainModule main = pSystem.main;
				main.startLifetimeMultiplier = value;
			}
		}
		public float StartSpeed
		{
			get
			{
				return pSystem.main.startSpeedMultiplier;
			}
			set
			{
				ParticleSystem.MainModule main = pSystem.main;
				main.startSpeedMultiplier = value;
			}
		}

		public float StartSize
		{
			get
			{
				return pSystem.main.startSizeMultiplier;
			}
			set
			{
				ParticleSystem.MainModule main = pSystem.main;
				main.startSizeMultiplier = value;
			}
		}
		public float OverRate
		{
			get
			{
				return pSystem.emission.rateOverTimeMultiplier;
			}
			set
			{
				ParticleSystem.EmissionModule emission = pSystem.emission;
				emission.rateOverTimeMultiplier = value;
			}
		}
		private ParticleSystemRenderer _pSystemRend = null;
		public ParticleSystemRenderer pSystemRend
		{
			get
			{
				if (_pSystemRend == null) _pSystemRend = GetComponent<ParticleSystemRenderer>();
				return _pSystemRend;
			}
		}
		public float LengthScale
		{
			get
			{
				return pSystemRend.lengthScale;
			}
			set
			{
				pSystemRend.lengthScale = value;
			}
		}
		public bool MoveOnBit;
		private int lastEmitFrame = -1;
		[SerializeField] int particlesPerBeat = 1000;

        private void Awake()
		{
			// Koreographer.Instance.RegisterForEvents(TracksCollection.instance.currentTrack.tracks.EventID, OnBit);
		}

		private void OnBit(KoreographyEvent koreoEvent)
		{
			if (MoveOnBit)
			{
				if (koreoEvent.HasIntPayload())
				{
					Debug.Log("OnBit Parts");
					OnParticleControlEvent();
				}
			}
		}

		void OnParticleControlEvent()
		{
			// // If two Koreography span events overlap, this can be called twice in the same frame.
			// //  This check ensures that we only ask the particle system to emit once for any frame.
			// if (Time.frameCount != lastEmitFrame)
			// {
				Debug.Log("OnBit Parts");
				// Spans get called over a specified amount of music time.  Use Koreographer's beat delta
				//  to calculate the number of particles to emit this frame based on the "particlesPerBeat"
				//  rate configured in the Inspector.
				int particleCount = (int) (particlesPerBeat * Koreographer.GetBeatTimeDelta());

				// Emit the calculated number of particles!
				pSystem.Emit(particleCount);

				lastEmitFrame = Time.frameCount;
			// }
		}
	}
}