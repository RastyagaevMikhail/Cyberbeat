using DG.Tweening;

using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Animations;

namespace CyberBeat
{
	public class SkinsCameraController : MonoBehaviour
	{
		[SerializeField] SkinType type;
		[SerializeField] TransformVariable targetVariable;

		[SerializeField] SkinTypeViewSettingsSelector PivotsBySkinType;
		[SerializeField] TransformReference cameraTransformReference;
		[SerializeField] Transform cameraTransform => cameraTransformReference.ValueFast;
		[SerializeField] TransformReference rotationHolderReference;
		Transform rotationHolder => rotationHolderReference.ValueFast;

		Transform positionTarget { get { return currentSettings.PositionTarget; } }
		Transform lookTarget { get { return currentSettings.LookTarget; } }
		Vector3 lookDirection { get { return lookTarget.position - positionTarget.position; } }
		float lookAngle { get { return Vector3.Angle (lookDirection, Vector3.forward); } }

		ViewSettings currentSettings;

		public void OnSkinTypeChnged (SkinType skinType)
		{
			if (!skinType) return;
			type = skinType;
			SetPivot ();
		}
		bool isStarted = false;
		private void Start ()
		{
			isStarted = true;
			currentSettings = PivotsBySkinType[type];
			StartRotation ();
		}

		private void StartRotation ()
		{
			float rotationAngle = currentSettings.RotationAngle;
			float Duration = CalculateDuration (rotationAngle);
			Ease ease = Ease.Linear;
			rotationHolder.DOLocalRotate (Vector3.up * rotationAngle, Duration)
				.SetEase (ease)
				.OnComplete (() =>
				{
					Duration = 30f;
					rotationHolder.DOLocalRotate (Vector3.up * -rotationAngle, Duration)
						.SetEase (ease)
						.OnComplete (StartRotation);
				});
		}

		private float CalculateDuration (float rotationAngle)
		{
			var speed = (rotationAngle * 2f) / 60f;
			// transform.forward.
			var dir = cameraTransform.forward.ProjectOnPlane (Vector3.forward);
			var difAngle = Vector3.forward.Angle (dir);
			var distance = (rotationAngle - difAngle).Abs ();
			float Duration = distance / speed;
			return Duration;
		}

		public void SetPivot ()
		{
			targetVariable.Value = PivotsBySkinType[type].LookTarget;
			if (isStarted)
			{
				var OldAngle = lookAngle;

				currentSettings = PivotsBySkinType[type];

				// rotationHolder.DOKill (true);
				rotationHolder.DOMove (lookTarget.position, currentSettings.DurationMove);
				cameraTransform.DOKill (true);
				cameraTransform.DOLocalMove (lookTarget.InverseTransformPoint (positionTarget.position), currentSettings.DurationMove) /* .OnComplete (StartRotation) */ ;

				DOVirtual.Float (RenderSettings.fogDensity, currentSettings.FogDensity, currentSettings.DurationMove, value => RenderSettings.fogDensity = value);

				DOVirtual.Float (OldAngle, lookAngle, currentSettings.DurationMove, value => cameraTransform.localRotation = Quaternion.AngleAxis (value, Vector3.right));
			}

		}

	}
}
