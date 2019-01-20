using UnityEditor;
namespace CyberBeat
{

    [CustomEditor (typeof (TimePointsGenerator))]
    public class TimePointsGeneratorEditor : Editor
    {
        public TimePointsGenerator Target { get { return target as TimePointsGenerator; } }
        
        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI ();
            Target.EditMode = EditorGUILayout.Toggle ("Edit Mode",Target.EditMode);

        }
    }
}
