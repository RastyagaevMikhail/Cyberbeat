using GameCore;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Text = TMPro.TextMeshProUGUI;
namespace CyberBeat
{
    public class NoBosters : MonoBehaviour
    {
        [SerializeField] GameObject Back;
        [SerializeField] ButtonActionByVideoAds videoButton;
        [SerializeField] Button notesButton;
        [SerializeField] Image Icon;
        [SerializeField] Text Title;
        [SerializeField] Text Price;
        [SerializeField] Text Description;
        [SerializeField] GameEvent NotesIsOver;
        [SerializeField] GameEvent Continue;
        [SerializeField] GameEvent Pause;

#if UNITY_EDITOR
        private void OnValidate ()
        {
            Back = transform.GetChild (0).gameObject;
            Transform Panel = Back.transform.GetChild (0);

            videoButton = Panel.Find ("VideoButton").GetComponent<ButtonActionByVideoAds> ();
            notesButton = Panel.Find ("NotesButton").GetComponent<Button> ();

            Icon = Panel.Find ("BoosterImage").GetComponent<Image> ();

            Title = Panel.Find ("Title").GetComponent<Text> ();
            Description = Panel.Find ("Description").GetComponent<Text> ();

            NotesIsOver = Tools.GetAssetAtPath<GameEvent> ("Assets/Data/Events/UI/Notes/NotesIsOver.asset");
            Continue = Tools.GetAssetAtPath<GameEvent> ("Assets/Data/Events/GameController/Continue.asset");
            Pause = Tools.GetAssetAtPath<GameEvent> ("Assets/Data/Events/GameController/Pause.asset");

            Price = notesButton.GetComponentInChildren<Text> ();
        }
#endif

        [ContextMenu ("Show")]
        public void Show ()
        {
            Back.SetActive (true);
            Pause.Raise ();
        }

        [ContextMenu ("Close")]
        public void Close ()
        {
            Continue.Raise ();
            Back.SetActive (false);
        }

        public void OnBoosterIsOver (BoosterData boosterData)
        {
            Icon.sprite = boosterData.Icon;

            Description.text = boosterData.description.localized ();
            Title.text = "nobooster_title".localized ().AsFormat (boosterData.name.ToLower ().localized ());

            notesButton.onClick.RemoveAllListeners ();
            notesButton.onClick.AddListener (() =>
            {
                if (!boosterData.TryBuy ())
                    NotesIsOver.Raise ();
            });

            videoButton.Init (() =>
            {
                boosterData.Increment ();
                Close ();
            });

            Price.text = boosterData.Price.ToString ();

            Show ();
        }

    }
}
