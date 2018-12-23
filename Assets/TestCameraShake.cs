using EZCameraShake;

using UnityEngine;
public class TestCameraShake : MonoBehaviour
{
    public CameraShaker shaker;
    public float magnitude;
    public float roughness;
    public float fadeInTime;
    public float fadeOutTime;
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            shaker.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
        }
    }
}
