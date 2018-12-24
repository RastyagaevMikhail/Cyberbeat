using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace CyberBeat
{
    public class SocialButton : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] Button button;
        [SerializeField] string Url;

        private void OnValidate ()
        {
            icon = GetComponent<Image> ();
            button = GetComponent<Button> ();
        }

        public void Init (SocialInfo info)
        {
            Url = info.URL;
            icon.sprite = info.Icon;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(OpenUrl);
        }

        private void OpenUrl()
        {
            Application.OpenURL(Url);
        }
    }
}
