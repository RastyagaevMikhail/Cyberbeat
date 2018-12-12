﻿using FluffyUnderware.Curvy;

using GameCore;

using Sirenix.OdinInspector;
using Sirenix.Utilities;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
	public class TrackController : SerializedMonoBehaviour
	{
		private CurvySpline _Spline = null;
		public CurvySpline Spline { get { if (_Spline == null) _Spline = GetComponent<CurvySpline> (); return _Spline; } }

		public Track track;
		public float StartSpeed { get { return track.StartSpeed; } }

#if UNITY_EDITOR

		bool segmentSelected { get { return SelectedSegment != null; } }

		[ShowIf ("segmentSelected")]
		[SerializeField, ShowInInspector, ReadOnly, PropertyOrder (2)] CurvySplineSegment SelectedSegment { get { GameObject activeGameObject = UnityEditor.Selection.activeGameObject; return activeGameObject ? activeGameObject.GetComponent<CurvySplineSegment> () : null; } }
		private Type IMetaDataType { get { return typeof (IMetaData); } }
		private List<Type> listOfIMetaDataTypes = null;
		public List<Type> ListOfIMetaDataTypes
		{
			get
			{
				if (listOfIMetaDataTypes == null)
					listOfIMetaDataTypes = IMetaDataType.Assembly.GetTypes ().ToList ().FindAll (type => IMetaDataType.IsAssignableFrom (type) && !type.Equals (IMetaDataType));
				return listOfIMetaDataTypes;
			}
		}

		[SerializeField, PropertyOrder (3), FoldoutGroup ("Мета Данные")]
		public MetaList metaList;

		[ShowIf ("segmentSelected"), Button ("Add Meta", ButtonSizes.Medium), PropertyOrder (2)]
		void AddMeta ()
		{
			if (SelectedSegment == null) return;
			var menu = new UnityEditor.GenericMenu ();

			foreach (var type in ListOfIMetaDataTypes)
			{
				// if () continue;
				if (SelectedSegment.GetComponent (type) == null)
				{
					menu.AddItem (
						new GUIContent (Tools.AddSpace (type.Name.Replace ("MetaData", ""))),
						false,
						() =>
						{
							IMetaData meta = SelectedSegment.gameObject.AddComponent (type) as IMetaData;
							metaList.Add (meta);
						});
				}
			}
			UnityEditor.Undo.RegisterCompleteObjectUndo (this, name);
			menu.ShowAsContext ();
		}

		[HideInInspector]
		[SerializeField] public List<CurvyTrackBitViewer> viewers;

		[SerializeField, PropertyOrder (2), FoldoutGroup ("Слои"), Indent (2), ListDrawerSettings (HideAddButton = true, Expanded = true)]
		public List<LayerColor> layers;
		[Button]
		private void Validate ()
		{
			if (layers.IsNullOrEmpty ())
				Init_Layers ();

			if (viewers.IsNullOrEmpty ())
				Init_Viewers ();

			foreach (var segment in Spline.Segments)
			{
				metaList.OnValidate (segment.gameObject);
			}

		}

		[ButtonGroup ("Init")]
		private void Init_Layers ()
		{
			layers = Enums.LayerTypes
				.Select (
					layer => new LayerColor (
						layer,
						Colors.instance.DefaultLayersColors[layer],
						OpenEditor (layer)
					)
				)
				.ToList ();
		}

		[ButtonGroup ("Init")]
		private void Init_Viewers ()
		{
			viewers = layers
				.Select (
					layer => new CurvyTrackBitViewer (
						track,
						layer,
						Spline
					)
				)
				.ToList ();
		}

		private Action OpenEditor (LayerType layer)
		{
			return () => KoreoTools.OpenKoreographyEditor (track.koreography, track.GetTrack (layer));
		}

		[UnityEditor.DrawGizmo (UnityEditor.GizmoType.NotInSelectionHierarchy)]
		public static void DrawGizmosOfBits (TrackController controller, UnityEditor.GizmoType type)
		{
			if (!controller) return;
			if (controller.viewers == null) return;

			foreach (var viewer in controller.viewers)
				viewer.DrawPoints ();
		}
#endif

	}
}
