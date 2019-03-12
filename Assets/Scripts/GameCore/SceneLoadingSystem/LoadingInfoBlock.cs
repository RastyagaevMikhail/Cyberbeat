using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class LoadingInfoBlock : MonoBehaviour
    {
        [SerializeField] BoolVariable TutorialComplited;
        [SerializeField] GameObject[] blocks;
        private void Awake ()
        {
            if (TutorialComplited.Value)
                blocks.GetRandom ().SetActive (true);
            else
                blocks[0].SetActive (true);
        }
        private void OnValidate ()
        {
            if (blocks == null || blocks.Length == 0)
                blocks = Enumerable.Range (0, transform.childCount).Select (i => transform.GetChild (i).gameObject).ToArray ();
        }
    }
}
