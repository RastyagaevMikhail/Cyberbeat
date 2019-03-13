using GameCore;

using UnityEngine;
namespace CyberBeat
{
    public class PlayerVariableSetter : MonoBehaviour
    {
        [SerializeField] PlayerVariable variable;
        void OnEnable ()
        {
            variable.Value = GetComponent<Player> ();
        }
        void OnDisable ()
        {
            variable = null;
        }
#if UNITY_EDITOR

        [ContextMenu ("Create Variable Instance")]
        void CreateVariableInstance ()
        {
            variable = ScriptableObject.CreateInstance<PlayerVariable> ();
            var sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name;
            variable.CreateAsset ($"Assets/CyberBeat/Data/VariableSetter/Player/{sceneName}/{name.ReplaceByRegex("[^a-zA-Z ]", string.Empty)}Player.asset");
        }
#endif
    }
}
