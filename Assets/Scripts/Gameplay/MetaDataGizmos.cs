
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using GameCore;
using Tools = GameCore.Tools;
using UnityEngine;
namespace CyberBeat
{
	public class MetaDataGizmos : MonoBehaviour
	{
		public string MetaData;
#if UNITY_EDITOR
		private void OnDrawGizmos ()
		{
			if (MetaData.IsNullOrWhitespace ()) return;

			Vector3 p = transform.position;
			Vector3 up = transform.up;
			Handles.Label (p + up * HandleUtility.GetHandleSize (p), MetaData, Tools.BackdropHtmlLabel);
		}
#endif
	}
}
