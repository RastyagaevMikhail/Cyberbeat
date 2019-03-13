using FluffyUnderware.Curvy.Controllers;

using GameCore;

using UnityEngine;
namespace CyberBeat
{
    public class SplineControllerVariableSetter : MonoBehaviour
    {
        [SerializeField] SplineControllerVariable variable;
        void OnEnable ()
        {
            variable.Value = GetComponent<SplineController> ();
        }
        void OnDisable ()
        {
            variable = null;
        }
#if UNITY_EDITOR

        [ContextMenu ("Create Variable Instance")]
        void CreateVariableInstance ()
        {
            variable = ScriptableObject.CreateInstance<SplineControllerVariable> ();
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
            variable.CreateAsset ($"Assets/CyberBeat/Data/VariableSetter/SplineController/{sceneName}/{name.ReplaceByRegex("[^a-zA-Z ]", string.Empty)}SplineController.asset");
        }
#endif
    }
}
