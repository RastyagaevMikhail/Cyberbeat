using FluffyUnderware.Curvy.Controllers;

using GameCore;

using Sirenix.OdinInspector;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class TimePointsGenerator : TimeEventsCatcher
    {

        private void OnValidate ()
        {
            if (editMode == null)
            {
                validateEditMode ();
            }
        }

        private void validateEditMode ()
        {
#if UNITY_EDITOR
            const string Path = "Assets/Data/EditMode.asset";
            editMode = Tools.GetAssetAtPath<BoolVariable> (Path);
            if (editMode == null)
            {
                editMode = ScriptableObject.CreateInstance<BoolVariable> ();
                editMode.CreateAsset (Path);
            }
#endif
        }

        [SerializeField, HideInInspector]
        BoolVariable editMode;
        [ShowInInspector, PropertyOrder (-200), GUIColor (0, 1, 0)]
        bool EditMode
        {
            get
            {
                return editMode.Value;
            }
            set
            {
                editMode.Value = value;
                FindObjectsOfType<TimeEventsController> ().ToList ().ForEach (tec => tec.SetFilterEnabled (!value));
            }
        }

        [SerializeField] TimeOfEventsData dataTime;
        TimePointsData DataSave { get { return dataTime.PointsData; } }
        List<TimePoints> points { get { return DataSave.points; } set { DataSave.points = value; } }
        TimePoints LastPoint { get { return points[points.Count - 1]; } }

        [SerializeField] SplineController controller;
        private void Start ()
        {
            if (EditMode)
            {
                points = new List<TimePoints> ();
            }
        }

        public override void _OnChanged (TimeEvent timeEvent)
        {
            // Debug.Log("TimePointsGenerator._OnChanged {name}");
            // Debug.LogFormat("timeOfEvent = {0}", timeEvent.timeOfEvent);
            if (!EditMode)
            {
                return;
            }

            if (timeEvent.isTime)
            {
                points.Add (new TimePoints () { payload = timeEvent.timeOfEvent.payload });
                var F = timeEvent.timeOfEvent.Start == 0f ? 0f : controller.Position / controller.Length;
                LastPoint.Start = new TimePointInfo (F, controller.transform.position, controller.transform.rotation, transform.up);
            }
            else
            {
                var F = controller.Position / controller.Length;
                LastPoint.End = new TimePointInfo (F, controller.transform.position, controller.transform.rotation, transform.up);
            }
        }

#if UNITY_EDITOR
        private void OnApplicationQuit ()
        {
            // Debug.Log ("OnApplicationQuit");
            if (EditMode)
            {
                DataSave.Save ();
                EditMode = false;
            }
        }
#endif

    }
}
