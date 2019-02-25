using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class ParticleSystemColorSwitcher : MonoBehaviour
{
    [SerializeField] ParticleSystem pSystem;
    private void OnValidate ()
    {
        if (pSystem == null) pSystem = GetComponent<ParticleSystem> ();
    }
    public Color color
    {
        set
        {
			ParticleSystem.MainModule main = pSystem.main;
			main.startColor = value;
        }
    }
}
