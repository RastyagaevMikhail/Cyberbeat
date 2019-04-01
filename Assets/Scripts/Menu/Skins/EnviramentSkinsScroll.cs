using DG.Tweening;

using GameCore;

using UnityEngine;
using UnityEngine.UI.Extensions;

namespace CyberBeat
{
	public class EnviramentSkinsScroll : MonoBehaviour
	{

		[HideInInspector]
		[SerializeField] ScrollPositionController scrollPositionController;
		private void OnValidate ()
		{
			scrollPositionController = GetComponent<ScrollPositionController> ();
		}

		[Header ("Data")]
		[SerializeField] SkinIndexSelector skinIndexsSelector;
		[SerializeField] SkinsEnumDataSelector skinsSelector;
		[SerializeField] SkinType skinType;

		TransformGroup transformGroup { get { return transfromGroupSelector[skinType]; } }
		TransformObject TargetScroll { get { return transformGroup; } }

		[Header ("Selectors")]
		[SerializeField] SkinTypeActionSelector skinTypeActionSelector;
		[SerializeField] ScrollSettingsDataSelector transfromGroupSelector;
		[Header("Events")]
		[SerializeField] UnityEventInt skinSelected;
		float currentPosition;
		public void _OnSkinTypeChnaged (SkinType skinType)
		{
			if (!enabled) return;
			this.skinType = skinType;

			scrollPositionController.ScrollTo (skinIndex);

			SetDataCount (skinType);
			skinSelected.Invoke(skinIndex);
			// _OnItemSelected (skinIndex);

		}

		public void ScrollAllSkinsToStart ()
		{
			scrollPositionController.ScrollTo (0);
			foreach (var transformGroup in transfromGroupSelector.Values)
				DOVirtual.Float (transformGroup.z, 0, 1, value => transformGroup.z = value);
		}

		private void SetDataCount (SkinType skinType)
		{
			if (!skinsSelector.ContainsKey (skinType)) return;
			scrollPositionController.SetDataCount (skinsSelector[skinType].Count);
		}

		int previndex = 0;
		IntVariable currentSkinIndexVariable => skinIndexsSelector[skinType];
		int skinIndex
		{
			get { if (!currentSkinIndexVariable) return 0; return currentSkinIndexVariable.Value; }
			set { if (currentSkinIndexVariable) currentSkinIndexVariable.Value = value; }
		}
		PrefabSkinItem skinItem { get { return skinsSelector[skinType][skinIndex] as PrefabSkinItem; } }
		PrefabSkinItem prevskinItem { get { return skinsSelector[skinType][previndex] as PrefabSkinItem; } }
		public void _OnItemSelected (int index)
		{
			if (!enabled) return;

			if (!transformGroup) return;

			skinIndex = index;

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
