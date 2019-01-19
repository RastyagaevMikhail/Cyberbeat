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
        int countBeats = 0;
        private void Start ()
        {
            countBeats = 0;
        }
        public void _OnDeathCollorinterractot (ColorInterractor interractor)
        {
            countBeats++;
            bitstText.text = string.Format ("{0}/{1}", countBeats, 200);
            DOVirtual.Float (100, 120, 0.2f, value => bitstText.fontSize = value).SetLoops (1, LoopType.Yoyo);
        }
    }
}
