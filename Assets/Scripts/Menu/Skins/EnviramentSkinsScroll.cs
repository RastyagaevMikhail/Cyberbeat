using DG.Tweening;

using GameCore;

using UnityEngine;
using UnityEngine.UI.Extensions;

namespace CyberBeat
{
	public class EnviramentSkinsScroll : MonoBehaviour
	{
		[Header ("Conponents")]
		[SerializeField] ScrollPositionController scrollPositionController;

		[Header ("Data")]
		[SerializeField] SkinsDataCollection skinsData;
		[SerializeField] SkinIndexSelector skinIndexsSelector;
		[SerializeField] SkinsEnumDataSelector skinsSelector;
		[SerializeField] SkinTypeVariable skinTypeVariable;
		private SkinType skinType => skinTypeVariable.ValueFast;

		[SerializeField] ColorInfoRuntimeSet[] colorSets;
		ColorInfoRuntimeSet colors => colorSets.GetRandom ();

		TransformGroup transformGroup { get { return transfromGroupSelector[skinType]; } }
		TransformObject TargetScroll { get { return transformGroup; } }

		[Header ("Selectors")]
		[SerializeField] SkinTypeActionSelector skinTypeActionSelector;
		[SerializeField] ScrollSettingsDataSelector transfromGroupSelector;
		[SerializeField] SkinSlelctorsSelector transfromObjectSelector;
		float lastPosition;
		float currentPosition;
		public void _OnSkinTypeChnaged (SkinType skinType)
		{
			if (skinsData.isRoadType (skinType))
			{
				scrollPositionController.ScrollTo (0);
				foreach (var transformGroup in transfromGroupSelector.Values)
					DOVirtual.Float (transformGroup.z, 0, 1, value => transformGroup.z = value);

				return;
			}
			else
			{
				scrollPositionController.ScrollTo (skinIdex);
			}

			SetDataCount (skinType);
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
			skinType.OnSeleted.DOBlendableColor (colors.GetRandom ().color, "_Color", 1f)
				.OnComplete (DoRandomSelctedColor);
		}

		private void SetDataCount (SkinType skinType)
		{
			if (!skinsSelector.ContainsKey (skinType)) return;
			scrollPositionController.SetDataCount (skinsSelector[skinType].Count);
		}

		int previndex = 0;
		int skinIdex
		{
			get
			{
				if (!skinIndexsSelector.ContainsKey (skinType)) return 0;
				return skinIndexsSelector[skinType].Value;
			}
		}
		PrefabSkinItem skinItem { get { return skinsSelector[skinType][skinIdex] as PrefabSkinItem; } }
		PrefabSkinItem prevskinItem { get { return skinsSelector[skinType][previndex] as PrefabSkinItem; } }
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
			SkinComponent prevSkinComponent = transformGroup.GetAt<SkinComponent> (previndex);
			prevSkinComponent.StopAniamtion ();
			prevskinItem.ApplyStateMaterial (prevSkinComponent);

			SkinComponent currentSkinComponent = transformGroup.GetAt<SkinComponent> (index);
			currentSkinComponent.StartAniamtion ();
			skinItem.ApplyStateMaterial (currentSkinComponent, true);

			var slector = transfromObjectSelector[skinType];

			slector.SetParent (transformGroup.GetAt (index));
			slector.localPosition = Vector3.zero;

			previndex = index;
		}
		public void _UpdatePosition (float position)
		{
			if (!transformGroup) return;
			float count = transformGroup.Count;

			float bounds = transformGroup.rawBounds.size.z;
			currentPosition = (position / count) * bounds;

			TargetScroll.z = -currentPosition;

		}

	}

}
