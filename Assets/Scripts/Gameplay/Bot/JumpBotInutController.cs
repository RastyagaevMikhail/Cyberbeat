using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace CyberBeat
{
    [CreateAssetMenu (fileName = "JumpBotInutController", menuName = "CyberBeat/Bot/InputController/Jump")]
    public class JumpBotInutController : BotInputController
    {
        public override float Speed { set => distanceRaycast = value * inputSettings.jumpUpTime; }
        private Ray forwardRay;
        [SerializeField] JumpInputSettings inputSettings;
        [SerializeField] UnityEvent onJump;
        private float distanceRaycast;
        Transform _target;
        private float SqrHeight;

        public override void Initialize (Transform target)
        {
            Speed = startSplineSpeedVariable.Value;
            SqrHeight = inputSettings.jumpHeight.Sqr ();
            _target = target;
        }

        public override void UpdatePosition ()
        {
            if (!matSwitch.Value) return;
            // nearBeats.ForEach (ForEachNear);
            Vector3 origin = _target.position;
            forwardRay = new Ray (origin, _target.forward);
            RaycastHit hit = new RaycastHit ();

            if (Physics.SphereCast (forwardRay, RadiusShpereCast, out hit, distanceRaycast))
                if (Checkhit (hit))
                    onJump.Invoke ();
            // inputControllerComponent.TapRight ();

        }

        protected bool Checkhit (RaycastHit hit)
        {
            if (bitAll) return true;
            GameObject hitGO = hit.transform.gameObject;
            bool isBit = hitGO.CompareTag ("Brick") || hitGO.CompareTag ("Switcher");
            if (!isBit) return false;
            MaterialSwitcher materialSwitcher = hitGO.GetComponent<MaterialSwitcher> ();
            bool isHit = !materialSwitcher.Constant && !(materialSwitcher.ChechColor (matSwitch.CurrentColor));
            return isHit;
        }

        public override void ForEachNear (ColorInterractor colorInterractor)
        {
            Vector3 dir = colorInterractor.position - _target.position;
            bool isDistance = dir.sqrMagnitude <= SqrHeight;
            if (!isDistance) return;

            bool isBrickNotMyColor = colorInterractor.gameObject.CompareTag ("Brick") && !matSwitch.ChechColor (colorInterractor.CurrentColor);
            if (isBrickNotMyColor)
                onJump.Invoke ();
            // inputControllerComponent.TapRight ();
        }
    }
}
