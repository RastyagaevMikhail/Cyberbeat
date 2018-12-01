using DG.Tweening;

using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace CyberBeat
{
	public class WindowSlideController : MonoBehaviour
	{
		[SerializeField] RectTransformObject Windows;
		[SerializeField] List<RectTransformObject> windows;
		[SerializeField] float timetransition = 0.5f;
		[SerializeField] int indexofWindow = 2;
		private bool startTransition;

		private void OnValidate ()
		{
			windows = Windows.transform.Cast<RectTransform> ().Select (t => t.GetComponent<RectTransformObject> ()).ToList ();
		}
		public void SetIndexWindow (int newIndexofWindow)
		{
			if (indexofWindow == newIndexofWindow || startTransition) return;
			startTransition = true;
			var newWindow = windows[newIndexofWindow];
			var currentWindow = windows[indexofWindow];
			int dir = (newIndexofWindow - indexofWindow).Sign ();
			if (newIndexofWindow == windows.Count && indexofWindow == 0) dir = -1;
			newWindow.x = dir * Screen.width;
			newWindow.gameObject.SetActive (true);
			newWindow.rectTransform.DOAnchorPos (Vector3.zero, timetransition);

			currentWindow.rectTransform
				.DOAnchorPos (Vector3.right * -dir * Screen.width, timetransition)
				.OnComplete (() =>
				{
					currentWindow.gameObject.SetActive (false);
					startTransition = false;
				});
			indexofWindow = newIndexofWindow;
		}
	}
}
