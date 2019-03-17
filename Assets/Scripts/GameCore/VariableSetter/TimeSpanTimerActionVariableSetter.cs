
using GameCore;
using UnityEngine;
namespace  GameCore
{
    public class TimeSpanTimerActionVariableSetter : MonoBehaviour
    {
        [SerializeField] TimeSpanTimerActionVariable variable;
        void OnEnable()
        {
            variable.Value = GetComponent<TimeSpanTimerAction>();
        }
        void OnDisable()
        {
            variable = null;
        }
        [ContextMenu ("Create Variable Instance")]
        void CreateVariableInstance ()
        {
            variable = ScriptableObject.CreateInstance<TimeSpanTimerActionVariable> ();
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
            variable.CreateAsset ($"Assets/GameCore/Data/VariableSetter/TimeSpanTimerAction/{sceneName}/{name.ReplaceByRegex("[^a-zA-Z ]", string.Empty)}TimeSpanTimerAction.asset");
        }
    }
}

