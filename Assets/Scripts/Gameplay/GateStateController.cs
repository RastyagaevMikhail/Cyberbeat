using GameCore;

using UnityEngine;

namespace CyberBeat
{
    public class GateStateController : MonoBehaviour
    {
        private Renderer _rend = null;

        public Renderer rend
        {
            get
            {
                if (_rend == null)
                {
                    _rend = StartGate.GetComponent<Renderer> ();
                }

                return _rend;
            }
        }

        [SerializeField] Material activeMaterial;
        [SerializeField] Material notActiveMaterial;
        [SerializeField] GameObject Trigger;
        [SerializeField] GameObject Portal;
        [SerializeField] GameObject Tunnel;
        [SerializeField] GameObject StartGate;
        [SerializeField] GameObject EndGate;
        [SerializeField] Track track;
        [SerializeField] int Index;
        [SerializeField] bool alwaysOpen;
        private void Start ()
        {
            if (!alwaysOpen)
                Activate (track.GetGateState (Index));

            DisableTunelAndGate ();
        }

        public void Init (int index, Track _track, GameObject tunel, GameObject startGate, GameObject endGate)
        {
            Index = index;
            track = _track;
            Tunnel = tunel;
            Tunnel.transform.SetParent (transform);
            StartGate = startGate;
            EndGate = endGate;
            Portal = StartGate.transform.Find ("Portal").gameObject;
            Portal.SetActive (false);
            Trigger.transform.SetParent (startGate.transform);
            Trigger.transform.localPosition = -Vector3.forward * 10f; //? Distance from Gates from hide Tunnel
            Trigger.transform.localRotation = Quaternion.identity;
        }

        public void Activate (bool active)
        {
            rend.sharedMaterial = active ? activeMaterial : notActiveMaterial;
            DisableTunelAndGate ();
            Trigger.SetActive (active);
        }

        public void _DeActivateIfMyIndex (int index)
        {
            if (index == Index)
            {
                Activate (false);
            }
        }

        public void _ActivateIfMyIndex (int index)
        {
            if (index == Index)
            {
                Activate (true);
            }
        }

        public void DisableTunelAndGate ()
        {
            Tunnel.SetActive (false);
            EndGate.SetActive (false);
        }

        public void _OpenGate ()
        {
            Portal.SetActive (true);
            Tools.DelayAction (this, 0.25f, () =>
            {
                Tunnel.SetActive (true);
                EndGate.SetActive (true);
            });
        }
    }
}
