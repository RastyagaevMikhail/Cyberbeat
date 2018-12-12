using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Controllers;
using FluffyUnderware.Curvy.Generator;
using FluffyUnderware.Curvy.Generator.Modules;

using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	public class TimePointsGenerator : MonoBehaviour
	{

		private void OnValidate ()
		{
			if (editMode == null)
				validateEditMode ();
		}

		private void validateEditMode ()
		{
#if UNITY_EDITOR
			const string Path = "Assets/Data/EditMode.asset";
			editMode = Tools.GetAssetAtPath<BoolVariable> (Path);
			if (editMode == null)
			{
				editMode = ScriptableObject.CreateInstance<BoolVariable> ();
				editMode.CreateAsset (Path);
			}
#endif
		}

		[SerializeField, HideInInspector]
		BoolVariable editMode;
		[ShowInInspector, PropertyOrder (-200), GUIColor (0, 1, 0)]
		bool EditMode { get { return editMode.Value; } set { editMode.Value = value; } }

		[ShowIf ("EditMode")]
		[SerializeField] TimeOfEventsData dataTime;
		List<TimeOfEvent> Times { get { return dataTime.Times; } }
		public bool outOfIndexTime { get { return (Times != null ? Times.Count <= indexOfTime : false); } }
		TimePointsData DataSave { get { return dataTime.PointsData; } }
		List<TimePoints> points { get { return DataSave.points; } set { DataSave.points = value; } }
		TimeOfEvent currentTime;
		[ShowIf ("EditMode")]
		float time = 0;
		[ShowIf ("EditMode")]
		int indexOfTime = 0;
		TimePoints LastPoint { get { return points[points.Count - 1]; } }

		[SerializeField] TimeEventsController timeEventsCtrl;

		[ShowIf ("EditMode")]
		[SerializeField] SplineController controller;
		private void Start ()
		{

			if (EditMode)
			{
				timeEventsCtrl.OnChanged += OnChanged;
				points = new List<TimePoints> ();
			}
		}

		private void OnChanged (bool isTime, TimeOfEvent currentTime)
		{
			if (isTime)
			{
				points.Add (new TimePoints ());
				var F = currentTime.Start == 0f ? 0f : controller.Position / controller.Length;
				LastPoint.Start = new TimePointInfo (F, controller.transform.position, controller.transform.rotation, transform.up);
			}
			else
			{
				var F = controller.Position / controller.Length;
				LastPoint.End = new TimePointInfo (F, controller.transform.position, controller.transform.rotation, transform.up);
			}
		}

#if UNITY_EDITOR
		private void OnApplicationQuit ()
		{
			// Debug.Log ("OnApplicationQuit");
			if (EditMode)
			{
				DataSave.Save ();
				EditMode = false;
			}
		}

		[RuntimeInitializeOnLoadMethod]
		public void AutoExitEditMode ()
		{
			if (points != null && points.Count > 0)
			{
				EditMode = false;
			}
		}
#endif

		[HideIf ("EditMode")]
		[SerializeField]
		CurvySpline Spline;
		[HideIf ("EditMode")]
		[SerializeField]
		Material material;
		[HideIf ("EditMode")]
		[SerializeField]
		CurvyGenerator generatorPrefab;
#if UNITY_EDITOR
		[HideIf ("EditMode")]
		[SerializeField] TimePointsGizmoDrawer drawer;
#endif
		[HideIf ("EditMode")]
		[SerializeField] Transform parent;
		[SerializeField] string namePrefix = "Line";

		[Button]
		void Build ()
		{
			TimePoints CurrentLine = new TimePoints ();
			foreach (var point in points)
			{
				var generator = Instantiate (generatorPrefab, parent);
				generator.name = namePrefix + "{0}".AsFormat (generator.GetInstanceID ());
				Debug.LogFormat ("{0}", generator.name);

				InputSplinePath inputSplinePath = generator.GetModule<InputSplinePath> ("Input Spline Path", true);
				inputSplinePath.Spline = Spline;

				var shapeEtr = generator.GetModule<BuildShapeExtrusion> ("Shape Extrusion");

				if (material)
				{
					var volumes = generator.FindModules<BuildVolumeMesh> ();
					foreach (var volume in volumes)
						volume.SetMaterial (0, material);
				}

				float FromTF = point.Start.F;
				float ToTF = point.End.F;

				shapeEtr.From = FromTF;
				shapeEtr.To = ToTF;

				shapeEtr.From = FromTF;
				shapeEtr.To = ToTF;
				generator.Initialize ();
				generator.Refresh ();

			}
#if UNITY_EDITOR
			drawer = new TimePointsGizmoDrawer (points);
#endif
		}
#if UNITY_EDITOR

		private void OnDrawGizmos ()
		{
			if (drawer != null)
				drawer.Draw ();
		}

		[Button]
		void Clear ()
		{

			foreach (var generator in parent.GetComponentsInChildren<CurvyGenerator> ())
			{
				Tools.Destroy (generator.gameObject);
			}
		}
#endif

	}
}
