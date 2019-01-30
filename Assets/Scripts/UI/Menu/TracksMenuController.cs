using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	public class TracksMenuController : RectTransformObject
	{
		private Animator _animator = null;
		public Animator animator { get { if (_animator == null) _animator = GetComponent<Animator> (); return _animator; } }

		[SerializeField] TrackScrollList trackScroll;
		public TracksCollection tracksColletion { get { return TracksCollection.instance; } }
		protected override void Awake ()
		{
			base.Awake();
			trackScroll.UpdateData (tracksColletion.Objects.Select (t => new TrackScrollData (t)).ToList ());
		}
		private void OnEnable ()
		{
			animator.Play ("Show");
		}
		public void Show ()
		{
			gameObject.SetActive (true);

		}
	}
}
