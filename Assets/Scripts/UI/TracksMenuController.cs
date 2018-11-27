using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
	public class TracksMenuController : MonoBehaviour
	{
		private Animator _animator = null;
		public Animator animator { get { if (_animator == null) _animator = GetComponent<Animator> (); return _animator; } }

		[SerializeField] TrackScrollList trackScroll;
		public TracksCollection tracksColletion { get { return TracksCollection.instance; } }
		private void Awake ()
		{
			trackScroll.UpdateData (tracksColletion.Objects.Select (t => new TrackScrollData (t)).ToList ());
		}

		public void Show()
		{
			gameObject.SetActive(true);
			animator.Play("Show");
		}
	}
}
