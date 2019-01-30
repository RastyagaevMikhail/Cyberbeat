using CyberBeat;

using FluffyUnderware.Curvy;

using GameCore;

using IngameDebugConsole;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
public class ConsoleController : MonoBehaviour
{
    private static ConsoleController _instance = null;
    public static ConsoleController instance { get { if (_instance == null) _instance = GameObject.FindObjectOfType<ConsoleController> (); return _instance; } }

    [SerializeField] SplineControllerVariable playerController;
    [SerializeField] SplineControllerVariable generatorController;
    [ConsoleMethod ("ss", "Set Speed Player and Generator SplineController")]
    public static void SetSpeed (float speed)
    {
        instance.playerController.Speed = speed;
        instance.generatorController.Speed = speed;
    }
    
    [ConsoleMethod("es","Enable Selectors")]
    public static void EnableSelectprs ()
    {
        Selectors.instance.Init (true);
    }
}
