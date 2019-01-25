using DG.Tweening;

using GameCore;

using UnityEngine;
using Text = TMPro.TextMeshProUGUI;
namespace CyberBeat
{
    public class GameUIControntoller : MonoBehaviour
    {
        [SerializeField] GameObject ActivateGateButton;
        [SerializeField] IntVariable currentCombo;
        [SerializeField] Text bitstText;

        public Track track { get { return TracksCollection.instance.CurrentTrack; } }

        public void _OnZoneGateActive (bool isZone)
        {
            bool activateGate = !track.GetGateState (currentCombo.Value) && isZone;

            ActivateGateButton.SetActive (activateGate);
        }
    }
}
