using System.Collections;

using UnityEngine;

public static class Vibration
{

#if UNITY_ANDROID && !UNITY_EDITOR
	public static AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
	public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject> ("currentActivity");
	public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject> ("getSystemService", "vibrator");

#else
	public static AndroidJavaClass unityPlayer;
	public static AndroidJavaObject currentActivity;
	public static AndroidJavaObject vibrator;
#endif

	public static void Vibrate ()
	{
		if (isAndroid)
			vibrator.Call ("vibrate");
#if UNITY_ANDROID
		else
			Handheld.Vibrate ();
#endif
	}

	public static void Vibrate (long milliseconds)
	{
		if (isAndroid)
			vibrator.Call ("vibrate", milliseconds);
#if UNITY_ANDROID
		else
			Handheld.Vibrate ();
#endif
	}
	public static void Vibrate (float seconds)
	{
		if (isAndroid)
		{
			long milliseconds = (long) seconds * 1000;
			vibrator.Call ("vibrate", milliseconds);
		}
#if UNITY_ANDROID
		else
			Handheld.Vibrate ();
#endif
	}

	public static void Vibrate (long[] pattern, int repeat)
	{
		if (isAndroid)
			vibrator.Call ("vibrate", pattern, repeat);
#if UNITY_ANDROID
		else
			Handheld.Vibrate ();
#endif
	}
	public static void VibrrateWithAmlitude (long milliseconds, int amplitude)
	{
		AndroidJavaClass effectClass = new AndroidJavaClass ("com.unity3d.player.VibrationEffect");
		Debug.LogFormat ("effectClass = {0}", effectClass);
		AndroidJavaObject eEffect = effectClass.CallStatic<AndroidJavaObject> ("createOneShot", milliseconds, amplitude);
		vibrator.Call ("vibrate", eEffect);
	}
	public static bool HasVibrator ()
	{
		return isAndroid;
	}

	public static void Cancel ()
	{
		if (isAndroid)
			vibrator.Call ("cancel");
	}

	private static bool isAndroid =>
#if UNITY_ANDROID && !UNITY_EDITOR
		true;
#else
	false;
#endif
}
