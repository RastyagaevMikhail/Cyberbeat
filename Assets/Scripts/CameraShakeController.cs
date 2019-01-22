using EZCameraShake;

using UnityEngine;
namespace CyberBeat
{
    public class CameraShakeController : MonoBehaviour
    {
        public CameraShaker shaker;
        [SerializeField] ChaseCamDataPresetSelector Selector;
        public void OnCameraBit (IBitData bitData)
        {
            var data = Selector[bitData.StringValue].Data;
            Debug.LogFormat ("data = {0}", data);
            shaker.ShakeOnce (data.magnitude, data.roughness, data.fadeInTime, data.fadeOutTime);
        }
        public float magnitude;
        public float roughness;
        public float fadeInTime;
        public float fadeOutTime;

        private void Update ()
        {

            if (Input.GetMouseButtonDown (0))
            {
                shaker.ShakeOnce (magnitude, roughness, fadeInTime, fadeOutTime);
            }
        }
    }
}
