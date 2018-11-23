using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{

	public class Authors : MonoBehaviour
	{
		public TracksCollection tracksCollection { get { return TracksCollection.instance; } }

		[SerializeField] AuthorsScrolList list;
		private void Start ()
		{
			list.UpdateData (tracksCollection.Objects.Select (t => new AuthorsData () { track = t }).ToList ());
		}
	}
}
