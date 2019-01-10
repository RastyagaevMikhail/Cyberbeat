using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;
namespace CyberBeat
{
    public class ResultsTrackInfo : MonoBehaviour
    {
        [SerializeField] Text TrackName;
        [SerializeField] Text AuthorName;
        [SerializeField] Image AlbumImage;
        public void Init (MusicInfo info)
        {
            TrackName.text = info.TrackName;
            AuthorName.text = info.AuthorName;
            AlbumImage.sprite = info.AlbumImage;
        }
    }
}
