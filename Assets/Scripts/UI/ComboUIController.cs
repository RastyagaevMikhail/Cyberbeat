using DG.Tweening;

using GameCore;
using Animator = GameCore.DoTween.DoTweenAnimatorController;

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
				_ResetCounter ();

			ComboCounter++;
			var obj = pool.Pop ("Text");
			if (!obj) return;
			var text = obj.Get<Text> ();
			if (!text) return;
			text.text = "x" + ComboCounter;
			obj.Get<Animator> ().Play ("ComboText", () => pool.Push (obj.gameObject));
		}
		public void _ResetCounter ()
		{
			ComboCounter = 0;
		}
	}

}
