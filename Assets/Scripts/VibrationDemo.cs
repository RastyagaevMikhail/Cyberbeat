using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class VibrationDemo : MonoBehaviour
{
	private void Start ()
	{
		Seconds.minValue = 0;
		Seconds.maxValue = 1;
		Seconds.value = Seconds.maxValue / 2f;

		Amlitude.minValue = 0;
		Amlitude.maxValue = 255;
		Amlitude.value = Amlitude.maxValue / 2f;
		Seconds.onValueChanged.AddListener (value => { GameCore.Settings.instance.VibrationTime = (long) value * 1000; });
	}

	private void Update ()
	{
		SecondsText.text = Seconds.value.ToString ();
		AmlitudeText.text = Amlitude.value.ToString ();
	}
	public void SimpleVibrate ()
	{
		Vibration.Vibrate ();
	}

	[SerializeField] Slider Seconds;
	[SerializeField] Text SecondsText;
	long milliseconds { get { return (long) (Seconds.value * 1000); } }
	int amplitude { get { return (int) Amlitude.value; } }

	public void VibrateWithseconds ()
	{
		Vibration.Vibrate (milliseconds);
	}

	[SerializeField] Slider Amlitude;
	[SerializeField] Text AmlitudeText;

	public void VibrateWithAmplitude ()
	{
		Vibration.VibrrateWithAmlitude (milliseconds, amplitude);
	}
	public void Cancel ()
	{
		Vibration.Cancel ();
	}

	public void ReturnToMenu ()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Menu");
	}
}
