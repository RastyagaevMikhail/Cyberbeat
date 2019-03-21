#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Examples;
#endif

using UnityEngine;

public class UnityRecorderHelper : MonoBehaviour
{
#if UNITY_EDITOR
	RecorderWindow window;

	RecorderWindow recorderWindow => window??(window = EditorWindow.GetWindow<RecorderWindow> ());

	public void StartRecord ()
	{
		recorderWindow.StartRecording ();
	}

	public void StopRecording ()
	{
		recorderWindow.StopRecording ();
	}
#endif
}
