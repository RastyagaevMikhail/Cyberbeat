using UnityEditor;

using UnityEngine;
namespace GameCore.Editor
{
    [CustomEditor (typeof (TestAction))]
    public class TestActionEditor : UnityEditor.Editor
    {
        TestAction Target { get { return target as TestAction; } }
        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI ();

            if (GUILayout.Button ("Invoke"))
            {
                Target.Invoke ();
            }

        }
    }
}
