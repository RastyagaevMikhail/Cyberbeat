using System;

using UnityEngine;

namespace CyberBeat
{
	[System.Serializable]
	public class LayerColor
	{
		[SerializeField] private bool _enable;
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

		public LayerType layer;
		public Color color;
		private Action OnOpenEditor;
		[ContextMenu ("OpenEditor")]
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
		public float SizePoints = 0.2f;
		public float OffsetSizePoint = 2.5f;
	}
}
