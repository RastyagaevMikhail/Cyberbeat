using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class DateTimeTImer : MonoBehaviour
{
    [SerializeField] UnityEvent OnMoreThen;
    [SerializeField] float seconds = 30f;
    void Start ()
    {
        string dateTimeStringValue = PlayerPrefs.GetString ("TimeExit", DateTime.Now.ToString ());

        DateTime timeExit = new DateTime ();
        DateTime.TryParse (dateTimeStringValue, out timeExit);

        TimeSpan deltaTime = timeExit.Subtract (DateTime.Now);

        if (deltaTime.Seconds >= seconds)
        {
            OnMoreThen.Invoke ();
        }

    }
    void OnApplicationQuit ()
    {
        PlayerPrefs.SetString ("TimeExit", DateTime.Now.ToString ());
    }
}
