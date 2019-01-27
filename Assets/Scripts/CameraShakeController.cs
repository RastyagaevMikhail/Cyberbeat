using EZCameraShake;

using UnityEngine;
namespace CyberBeat
{
    public class CameraShakeController : MonoBehaviour
    {
        public CameraShaker shaker;
        [SerializeField] ShakeDataPresetSelector Selector;
        public void OnShakeBit (IBitData bitData)
        {
            var data = Selector[bitData.StringValue].Data;
            float duration = bitData.Duration;
            // Debug.LogFormat ("bitData.Duration = {0}", duration);
            data.TimeDuaration = duration;
            // Debug.Log (data);
            data.ShakeOnce (shaker);
        }

        [SerializeField] bool test;
        [SerializeField] ShakeData data;
        private void Update ()
        {
            if (test && Input.GetMouseButtonDown (0))
            {
                data.ShakeOnce (shaker);
            }
        }

        [ContextMenuItem ("Copy to", "CopyToPreset")]
        [SerializeField] ShakeDataPreset PresetFromSave;
        [ContextMenu ("Copy To Preset")]
        void CopyToPreset ()
        {
            PresetFromSave.CopyFrom (data);
        }
    }
}
