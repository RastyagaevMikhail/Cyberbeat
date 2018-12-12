using GameCore;

using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace CyberBeat
{
	public class LineGenerator : MonoBehaviour
	{
		[ShowInInspector]
		List<TimeOfEvent> Lines { get { return GameData.instance.currentLines; } }

		[ShowInInspector] LineRenderer currentRend;
		[ShowInInspector] TimeOfEvent currentLine;
		[ShowInInspector] float time = 0;
		[ShowInInspector] int indexOfLine = 0;
		[ShowInInspector]
		public bool outOfIndexLines { get { return (Lines.Count <= indexOfLine); } }
		private Transform _transform = null;
		public new Transform transform { get { if (_transform == null) _transform = GetComponent<Transform> (); return _transform; } }
		public Vector3 newPosition { get { return transform.position + transform.up * 0.15f; } }

		[ShowInInspector] public bool timeInCurrentLine { get { return time.InRange (currentLine.Start, currentLine.End); } }

		private void Start ()
		{
			currentLine = Lines.First ();
			Debug.LogFormat ("currentLine = {0}", currentLine);
			AddLine ();
			// AddPosition ();
		}

		[SerializeField] LineRenderer original;
		private void AddLine ()
		{
			// Debug.Log ("AddLine");
			Quaternion identity = Quaternion.identity;
			currentRend = Instantiate (original, newPosition, identity, transform);
			currentRend.ReNameWithID ();

		}
		private void AddPosition ()
		{
			if (currentRend == null) AddLine ();
			var positions = new Vector3[currentRend.positionCount];
			currentRend.GetPositions (positions);
			List<Vector3> list = positions.ToList ();
			list.Add (newPosition);
			positions = list.ToArray ();
			currentRend.positionCount++;
			currentRend.SetPositions (positions);
		}
		void Update ()
		{

			if (!outOfIndexLines && timeInCurrentLine)
				AddPosition ();

			time += Time.deltaTime;

			if (!timeInCurrentLine && !outOfIndexLines && currentRend)
			{
				currentRend = null;
				// Debug.LogFormat ("indexOfLine = {0}", indexOfLine);
				indexOfLine++;
				// Debug.LogFormat ("indexOfLine = {0}", indexOfLine);
				currentLine = Lines[indexOfLine];
			}
		}
	}
}
