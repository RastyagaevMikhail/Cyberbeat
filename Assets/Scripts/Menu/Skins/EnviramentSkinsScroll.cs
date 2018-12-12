using DG.Tweening;

using GameCore;

using Sirenix.OdinInspector;
using Sirenix.Serialization;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

namespace CyberBeat
{
	public class EnviramentSkinsScroll : SerializedMonoBehaviour
	{
		private ScrollPositionController _scrollPositionController = null;
		public ScrollPositionController scrollPositionController { get { if (_scrollPositionController == null) _scrollPositionController = GetComponent<ScrollPositionController> (); return _scrollPositionController; } }

		public SkinsDataCollection skinsData { get { return SkinsDataCollection.instance; } }

		private SkinType skinType { get { return skinsData.SkinType; } }

		SkinItem skin { get { return skinsData.skins[skinType][skinIdex]; } }

		TransformGroup transformGroup { get { return transfromGroupSelector[skinType]; } }
		TransformObject TargetScroll { get { return transformGroup; } }

		[SerializeField] SkinTypeActionSelector skinTypeActionSelector;
		[SerializeField] ScrollSettingsDataSelector transfromGroupSelector;
		[SerializeField] SkinSlelctorsSelector transfromObjectSelector;
		float lastPosition;
		public void _OnSkinTypeChnaged (UnityEngine.Object obj)
		{
			if (skinsData.isRoadType (skinType))
			{
				scrollPositionController.ScrollTo (0);
				foreach (var tg in transfromGroupSelector.Values)
					DOVirtual.Float (tg.z, 0, 1, value => tg.z = value);

				return;
			}
			else
			{
				scrollPositionController.ScrollTo (skinIdex);
			}
			// float tmp = lastPosition;
			// lastPosition = currentPosition;
			// currentPosition = tmp;
			// scrollPositionController.ScrollTo((int)currentPosition);

			SetDataCount (obj as SkinType);
			_OnItemSelected (skinIdex);

		}

		private void Start ()
		{
			DoRandomSelctedColor ();
		}

		public void DoRandomSelctedColor ()
		{
			if (!skinType.OnSeleted)
			{
				Invoke ("DoRandomSelctedColor", 1f);
				return;
			}
			skinType.OnSeleted.DOBlendableColor (Colors.instance.RandomColor, "_EmissionColor", 1f)
				.OnComplete (DoRandomSelctedColor);
		}

		private void SetDataCount (SkinType skinType)
		{
			if (!skinsData.skins.ContainsKey (skinType)) return;
			scrollPositionController.SetDataCount (skinsData.skins[skinType].Count);
		}

		int previndex = 0;
		int skinIdex
		{
			get
			{
				if (!skinsData.skinIndexs.ContainsKey (skinType)) return 0;
				return skinsData.skinIndexs[skinType].Value;
			}
		}
		PrefabSkinItem skinItem { get { return skinsData.skins[skinType][skinIdex] as PrefabSkinItem; } }
		PrefabSkinItem prevskinItem { get { return skinsData.skins[skinType][previndex] as PrefabSkinItem; } }
		public void _OnItemSelected (int index)
		{
			if (skinsData.isRoadType (skinType))
			{
				return;
			}
			if (!transformGroup) return;

			skinsData.SkinIndex = index;

			if (transformGroup.Count == 0)
			{
				skinTypeActionSelector.InvokeAll ();
			}

			prevskinItem.ApplyStateMaterial (transformGroup.GetAt<Renderer> (previndex));

			skinItem.ApplyStateMaterial (transformGroup.GetAt<Renderer> (index), true);

			var slector = transfromObjectSelector[skinType];

			slector.SetParent (transformGroup.GetAt (index));
			slector.localPosition = Vector3.zero;

			previndex = index;
		}

		public float currentPosition;
		public void _UpdatePosition (float position)
		{
			if (!transformGroup) return;
			float count = transformGroup.Count;

			float bounds = transformGroup.rawBounds.size.z;
			currentPosition = position / count * bounds;

			TargetScroll.z = -currentPosition;

		}

	}

}
