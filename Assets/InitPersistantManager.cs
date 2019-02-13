using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class InitPersistantManager : MonoBehaviour
{

    public Slider LoadSlider;
    public Text ErrorText;

    [SerializeField] int currentVersion = 1;
    [SerializeField] GameObject repaetButton;

    public void Start ()
    {
         repaetButton.SetActive (false);
        RemoteResouceManager.Instance.OnProgressNormalizedChange += (val) =>
        {
            LoadSlider.normalizedValue = val;
        };

        RemoteResouceManager.LoadAll (() =>
        {
            Hide();
        },
            (error) =>
            {
                ErrorText.text = error;
                bool ChacheIsLoaded = RemoteResouceManager.LoadInChache (currentVersion);
                if (ChacheIsLoaded)
                {
                    Hide();
                }
                else
                {
                    repaetButton.SetActive (true);
                }
            });

        //PersistantManager.LoadVideoList(() =>
        //{
        //    PersistantManager.LoadAll();
        //    gameObject.SetActive(false);
        //});
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
