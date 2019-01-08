using GameCore;

using UnityEngine;

namespace CyberBeat
{
    public class GateStateController : MonoBehaviour
    {
        private Renderer _rend = null;
        public Renderer rend { get { if (_rend == null) { _rend = StartGate.GetComponent<Renderer> (); } return _rend; } }

        [SerializeField] Material activeMaterial;
        [SerializeField] Material notActiveMaterial;
        [SerializeField] GameObject Trigger;
        [SerializeField] GameObject Tunnel;
        [SerializeField] GameObject StartGate;
        [SerializeField] GameObject StartPortal;
        [SerializeField] GameObject EndGate;
        [SerializeField] GameObject EndPortal;
        [SerializeField] Track track;
        [SerializeField] int Index;

        private void Start ()
        {
            bool active = track.GetGateState (Index);
            Activate (active);

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
            StartPortal = StartGate.transform.Find ("Portal").gameObject;
            StartPortal.SetActive (false);
            EndPortal = EndGate.transform.Find ("Portal").gameObject;
            EndPortal.SetActive (false);

            Trigger.transform.SetParent (startGate.transform);
            Trigger.transform.localPosition = Vector3.zero;
            Trigger.transform.localRotation = Quaternion.identity;
        }

        public void Activate (bool active)
        {
            rend.sharedMaterial = active ? activeMaterial : notActiveMaterial;
            DisableTunelAndGate ();
            StartPortal.SetActive (active);
            EndPortal.SetActive (active);
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

        public void _OpenTunnel ()
        {
            Tunnel.SetActive (true);
            EndGate.SetActive (true);

        }
    }
}
