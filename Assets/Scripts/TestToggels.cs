using CyberBeat;

using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TestToggels : MonoBehaviour
{
    [SerializeField] BoolVariableRuntimeSet testSet;
    [SerializeField] ToggleBoolVariable ToggelPrefab;

    private void Start ()
    {
        testSet.ForEach (boolVar => Instantiate (ToggelPrefab, transform).Init (boolVar));
    }

}
