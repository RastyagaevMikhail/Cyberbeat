using FluffyUnderware.Curvy;
using FluffyUnderware.Curvy.Generator;
using FluffyUnderware.Curvy.Generator.Modules;

using GameCore;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{

    public class TimePointsBuilder : MonoBehaviour
    {
        public TimeOfEventsData dataTime;
        public TimePointsData DataSave { get { return dataTime.PointsData; } }
        public List<TimePoints> points { get { return DataSave.points; } set { DataSave.points = value; } }
        public CurvySpline Spline;
        public Material material;
        public CurvyGenerator generatorPrefab;
#if UNITY_EDITOR
        public TimePointsGizmoDrawer drawer;
#endif
        public Transform parent;
        public string namePrefix = "Line";
        public string payloadFilter;
        public ATimePointsPostBuilder postBuilder;
        [SerializeField] List<GameObject> BuildedObjects = new List<GameObject> ();
        [ContextMenu ("Build")]
        void Build ()
        {
            var FilterdIndexesOfTimes = dataTime[payloadFilter].Select (t => dataTime.Times.IndexOf (t));
            var FiltredPoints = dataTime.PointsData[payloadFilter];
            foreach (var point in FiltredPoints)
            {
                var generator = Instantiate (generatorPrefab, parent);
                generator.name = namePrefix + "{0}".AsFormat (generator.GetInstanceID ());
                Debug.LogFormat ("{0}", generator.name);

                InputSplinePath inputSplinePath = generator.GetModule<InputSplinePath> ("Input Spline Path", true);
                inputSplinePath.Spline = Spline;

                var shapeEtr = generator.GetModule<BuildShapeExtrusion> ("Shape Extrusion");

                if (material)
                {
                    var volumes = generator.FindModules<BuildVolumeMesh> ();
                    foreach (var volume in volumes)
                    {
                        volume.SetMaterial (0, material);
                    }
                }

                float FromTF = point.Start.F;
                float ToTF = point.End.F;

                shapeEtr.From = FromTF;
                shapeEtr.To = ToTF;

                // shapeEtr.From = FromTF;
                // shapeEtr.To = ToTF;
                generator.Initialize ();
                generator.Refresh ();

                var GOBuild = postBuilder.PostBuild (this, generator.gameObject);
                BuildedObjects.Add (GOBuild);
            }
#if UNITY_EDITOR
            drawer = new TimePointsGizmoDrawer (points);
#endif
        }
#if UNITY_EDITOR

        private void OnDrawGizmos ()
        {
            if (drawer != null)
            {
                drawer.Draw ();
            }
        }

        [ContextMenu ("Clear")] public void Clear ()
        {
            foreach (var go in BuildedObjects)
            {
                Tools.Destroy (go);
            }
            BuildedObjects.Clear ();
        }
#endif
    }
}
