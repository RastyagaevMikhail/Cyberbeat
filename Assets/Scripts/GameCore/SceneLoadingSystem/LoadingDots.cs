using System.Net.Mime;
namespace GameCore
{
	using System.Collections;
	using UnityEngine;
    using Text = TMPro.TextMeshProUGUI;
    [RequireComponent (typeof (Text))]
    public class LoadingDots : MonoBehaviour
    {
        [SerializeField] Text text;
        private void OnValidate ()
        {
            if (text == null) text = GetComponent<Text> ();
        }
        public IEnumerator Start ()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds (0.5f);
            while (true)
            {

                text.text = ".";
                yield return waitForSeconds;
                text.text = "..";
                yield return waitForSeconds;
                text.text = "...";
                yield return waitForSeconds;
            }
        }
    }
}
