using DG.Tweening;

using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

namespace CyberBeat
{
	public class SkinsCameraController : TransformObject
	{
		[OnValueChanged ("SetPivot")]
		[ValueDropdown ("types")]
		[SerializeField] SkinType type;

		[SerializeField] SkinTypeViewSettingsDataSelector PivotsBySkinType;
		[SerializeField] GameCore.DebugTools.DebugSelfDirectionToTransfrom deb;
		[SerializeField] Transform RotationHolder;
		[SerializeField] UnityObjectVariable CurrentScinType;

		Transform PositionTarget { get { return currentSettings.PositionTarget; } }
		Transform LookTarget { get { return currentSettings.LookTarget; } }
		Vector3 LookDirection { get { return LookTarget.position - PositionTarget.position; } }
		float LookAngle { get { return Vector3.Angle (LookDirection, Vector3.forward); } }
		public SkinsDataCollection skinsData { get { return SkinsDataCollection.instance; } }
		List<SkinType> types { get { return skinsData.AllTypes; } }

		[SerializeField] ViewSettings currentSettings;
		private void OnEnable ()
		{
			CurrentScinType.OnValueChanged += OnSkinTypeChnged;
		}
		private void OnDisable ()
		{
			CurrentScinType.OnValueChanged -= OnSkinTypeChnged;
		}

		private void OnSkinTypeChnged (UnityEngine.Object obj)
		{
			if (!obj) return;
			var typeSkin = obj as SkinType;
			if (!typeSkin) return;
			type = typeSkin;
			SetPivot ();
		}

		private void Start ()
		{
			currentSettings = PivotsBySkinType[type];
			StartRotation ();
		}

		private void StartRotation ()
		{
			float rotationAngle = currentSettings.RotationAngle;
			float Duration = CalculateDuration (rotationAngle);
			Ease ease = Ease.Linear;
			RotationHolder.DOLocalRotate (Vector3.up * rotationAngle, Duration)
				.SetEase (ease)
				.OnComplete (() =>
				{
					Duration = 30f;
					RotationHolder.DOLocalRotate (Vector3.up * -rotationAngle, Duration)
						.SetEase (ease)
						.OnComplete (StartRotation);
				});
		}

		private float CalculateDuration (float rotationAngle)
		{
			var speed = (rotationAngle * 2f) / 60f;
			// transform.forward.
			var dir = transform.forward.ProjectOnPlane (Vector3.forward);
			var difAngle = Vector3.forward.Angle (dir);
			var distance = (rotationAngle - difAngle).Abs ();
			float Duration = distance / speed;
			return Duration;
		}

		public void SetPivot ()
		{
			deb.SetTarget (PivotsBySkinType[type].LookTarget);
			if (Application.isPlaying)
			{
				var OldAngle = LookAngle;

				currentSettings = PivotsBySkinType[type];

				// RotationHolder.DOKill ();
				RotationHolder.DOMove (LookTarget.position, currentSettings.DurationMove);
				transform.DOKill (true);
				transform.DOLocalMove (LookTarget.InverseTransformPoint (PositionTarget.position),currentSettings.DurationMove)/* .OnComplete (StartRotation) */;

				DOVirtual.Float (RenderSettings.fogDensity, currentSettings.FogDensity, currentSettings.DurationMove, value => RenderSettings.fogDensity = value);

				DOVirtual.Float (OldAngle, LookAngle, currentSettings.DurationMove, value => localRotation = Quaternion.AngleAxis (value, Vector3.right));
			}

		}

	}
}
