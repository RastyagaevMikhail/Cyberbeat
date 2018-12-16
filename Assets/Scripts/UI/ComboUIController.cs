using DG.Tweening;

using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Text = TMPro.TextMeshProUGUI;
namespace CyberBeat
{
	public class ComboUIController : MonoBehaviour
	{

		UIPool pool { get { return UIPool.instance; } }
		int ComboCounter = 0;
		// public void _OnBit (UnityObjectVariable arg)
		public void _OnBit (bool reset)
		{
			// Debug.Log ("OnBit UI");

			if (reset)
			{
				ComboCounter = 0;
			}
			ComboCounter++;
			var obj = pool.Pop ("Text");
			if (!obj) return;
			var text = obj.Get<Text> ();
			if (!text) return;
			text.text = "x" + ComboCounter;
			var rect = obj.Get<RectTransform> ();
			// rect.SetParent (transform);
			rect.anchoredPosition = new Vector2 (0, 100);
			var seq = DOTween.Sequence ();
			seq.Append (rect.DOAnchorPos (new Vector2 (0, 200), 1f));
			seq.Join (text.DOFade (0f, 1f));
			seq.Join (text.transform.DOScale (0.5f, 1f));
			seq.OnComplete (() =>
			{
				text.color = Color.white;
				text.transform.localScale = Vector3.one;
				pool.Push (text.gameObject);
			});
			seq.Play ();
		}
		public void _ResetCounter ()
		{
			ComboCounter = 0;
		}
	}

}
