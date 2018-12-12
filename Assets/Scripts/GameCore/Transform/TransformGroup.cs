using Sirenix.OdinInspector;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace GameCore
{
	public class TransformGroup : TransformObject
	{
		List<Transform> Objects = new List<Transform> ();
		public void Add (GameObject newObject)
		{
			Add (newObject.transform);
		}
		public void Add (Transform newObject)
		{
			if (!Contains (newObject))
				Objects.Add (newObject);
			newObject.SetParent (transform);
			Align ();

		}
		public bool Contains (GameObject go)
		{
			return Contains (go.transform);
		}
		public bool Contains (Transform trans)
		{
			return Objects.Contains (trans) || Objects.Find (o => o.name == trans.name);
		}
		public void Remove (GameObject oldObject)
		{
			Remove (oldObject.transform);
		}

		public void Remove (Transform oldObject)
		{
			if (Objects.Contains (oldObject))
				Objects.Remove (oldObject);
			Destroy (oldObject.gameObject);
			Align ();
		}

		public T AsAt<T> (int index) where T : Object
		{
			T t = Objects[index] as T;
			return t;
		}
		public T GetAt<T> (int index) where T : Component
		{
			T t = Objects[index].GetComponent<T> ();
			return t;
		}
		public Transform GetAt (int index) 
		{
			return  Objects[index];;
		}
		public T AddAt<T> (int index) where T : Component
		{
			T t = Objects[index].gameObject.AddComponent<T> ();
			return t;
		}
		public Vector3 Space;

		public void Align ()
		{
			int length = Objects.Count;
			for (int i = 0; i < length; i++)
			{
				var item = Objects[i];
				item.localPosition = Space * i;
			}
		}
		public int Count { get { return Objects.Count; } }

		[ShowInInspector] public Bounds rawBounds { get { return new Bounds (Vector3.zero, Count * Space.Abs ()); } }
	}
}
