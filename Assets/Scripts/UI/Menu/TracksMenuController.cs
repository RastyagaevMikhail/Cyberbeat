using GameCore;

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
