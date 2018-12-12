using System;

using Sirenix.OdinInspector;

using UnityEngine;

namespace CyberBeat
{
	[System.Serializable]
	public class LayerColor
	{
		[HideInInspector]
		private bool _enable;
		[InlineButton ("OpenEditor", "E")]
		[HorizontalGroup ("LayerColor"), HideLabel]
		[ShowInInspector]
		[PropertyOrder(-1)]
		public bool enable
		{
			get
			{
				return _enable;
			}
			set
			{
				if (value != _enable)
				{
					_enable = value;
#if UNITY_EDITOR
					if (UnityEditor.Selection.activeGameObject)
					{
						UnityEditor.Selection.activeGameObject = null;
					}
#endif
				}
			}
		}
		public System.Action<LayerColor, bool> OnEnabeleChnaged;

		[HorizontalGroup ("LayerColor"), HideLabel, DisplayAsString]
		public LayerType layer;
		[HorizontalGroup ("LayerColor"), HideLabel]
		public Color color;
		private Action OnOpenEditor;
		
		public void OpenEditor ()
		{
			if (OnOpenEditor != null) OnOpenEditor ();
		}

		public void OnDisable ()
		{
			// Debug.LogFormat ("OnDisable LayerColor = {0}\n LayerType = {1}\n DifficultyTrackData = {2}\n Track = {3}", this, layer, difficultyTrackData, difficultyTrackData.track);
		}

		public LayerColor (LayerType layer, Color color, Action onOpenEditor)
		{
			this.layer = layer;
			this.color = color;
			OnOpenEditor = onOpenEditor;

		}

		[HideInPlayMode]
		[ShowIf ("enable"), LabelText ("Размер точек")]
		public float SizePoints = 0.2f;
		[HideInEditorMode]
		[ShowIf ("enable")]
		public float OffsetSizePoint = 2.5f;
	}
}
