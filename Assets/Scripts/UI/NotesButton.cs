using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CyberBeat
{
    public class NotesButton : MonoBehaviour
    {
        [SerializeField] IntVariable Notes;
        [SerializeField] int Price;

        private Button _button = null;
        public Button button { get { if (_button == null) _button = GetComponent<Button> (); return _button; } }

        public void Init (UnityAction OnSucsessBuy,  int price)
        {
            Price = price;
            button.onClick.RemoveAllListeners ();
            button.onClick.AddListener (OnSucsessBuy);
        }
    }
}
