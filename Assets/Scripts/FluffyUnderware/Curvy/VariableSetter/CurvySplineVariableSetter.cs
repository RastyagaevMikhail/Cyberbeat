
using GameCore;
using UnityEngine;
namespace  FluffyUnderware.Curvy
{
    public class CurvySplineVariableSetter : MonoBehaviour
    {
        [SerializeField] CurvySplineVariable variable;
        void OnEnable()
        {
            variable.Value = GetComponent<CurvySpline>();
        }
        void OnDisable()
        {
            variable = null;
        }
        [ContextMenu ("Create Variable Instance")]
        void CreateVariableInstance ()
        {
            variable = ScriptableObject.CreateInstance<CurvySplineVariable> ();
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
            variable.CreateAsset ($"Assets/FluffyUnderware.Curvy/Data/VariableSetter/CurvySpline/{sceneName}/{name.ReplaceByRegex("[^a-zA-Z ]", string.Empty)}CurvySpline.asset");
        }
    }
}

