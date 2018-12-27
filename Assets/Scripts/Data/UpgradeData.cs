using GameCore;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.Purchasing;
namespace CyberBeat
{
    [CreateAssetMenu (fileName = "UpgradeData", menuName = "CyberBeat/UpgradeData", order = 0)]
    public class UpgradeData : ScriptableObject
    {
        GameData gameData { get { return GameData.instance; } }

        public int maxUpgradeLevel;
        public bool ReachedMaxLevel { get { return maxUpgradeLevel == CurrentUpgrade.Value; } }
        public bool CanUpgrade { get { return gameData.CanBuy (Price) && !ReachedMaxLevel; } }
        public int Price { get { return upgradeNotePrices[CurrentUpgrade.Value]; } }

        public string locTitleTag;
        public IntVariable CurrentUpgrade;
        public IntListVariable upgradeVideoAdsPrices;
        public IntListVariable upgradeNotePrices;

#if UNITY_EDITOR
        static int[] UpgradeVideoAdsPrices = new int[] { 1, 3, 5, 7, 9, 10, 12, 15, 17, 20 };
        static int[] UpgradeNotePrices = new int[] { 50, 100, 150, 200, 250, 300, 350, 400, 450, 500 };
        [ContextMenu ("Copy To Variable")]
        public void CopyToVariable ()
        {
            upgradeVideoAdsPrices.Value = new List<int> ();
            upgradeNotePrices.Value = new List<int> ();
            for (int i = 0; i < UpgradeNotePrices.Length; i++)
            {
                upgradeVideoAdsPrices.Value.Add (UpgradeVideoAdsPrices[i]);
                upgradeNotePrices.Value.Add (UpgradeNotePrices[i]);
            }
            upgradeVideoAdsPrices.Save ();
            upgradeNotePrices.Save ();
        }
#endif
        public void Inti ()
        {
            CurrentUpgrade.OnValueChanged += OnUpgradeChanged;
            onCheckVideoCount ();
        }
        private void OnDisable ()
        {
            CurrentUpgrade.OnValueChanged -= OnUpgradeChanged;
        }
        private void OnDestroy ()
        {
            CurrentUpgrade.OnValueChanged -= OnUpgradeChanged;
        }
        private void OnUpgradeChanged (int level)
        {
            countVideoAds.Value = upgradeVideoAdsPrices[level];
        }

        public void OnWatchVideo ()
        {
            countVideoAds.Decrement ();
            onCheckVideoCount (CurrentUpgrade.Increment);
        }
        void onCheckVideoCount (Action onCheckAction = null)
        {
            if (countVideoAds.Value == 0)
            {
                if (onCheckAction != null) onCheckAction ();
                countVideoAds.Value = upgradeVideoAdsPrices[CurrentUpgrade.Value];
            }
        }

        public IntVariable countVideoAds;
        public int CountVideoAds { get { return countVideoAds.Value; } }

        public void Upgrade ()
        {
            CurrentUpgrade.Increment ();
        }
        public void UpgradeByNotes ()
        {
            gameData.TryBuy (Price);
            if (CanUpgrade)
            {
                CurrentUpgrade.Increment ();
            }
        }

        public string IAPTag;
#if UNITY_EDITOR

        [ContextMenu ("Generate Products")]
        public void GenerateProducts ()
        {
            ProductCatalog productCatalog = ProductCatalog.LoadDefaultCatalog ();
            for (int i = 0; i <= maxUpgradeLevel; i++)
            {
                var item = new ProductCatalogItem ();
                item.id = "{0}_{1}".AsFormat (IAPTag, i);
                item.type = ProductType.Consumable;
                productCatalog.Add (item);
            }

            StreamWriter writer = new StreamWriter (ProductCatalog.kCatalogPath);
            writer.Write (ProductCatalog.Serialize (productCatalog));
            writer.Close ();

            UnityEditor.AssetDatabase.ImportAsset (ProductCatalog.kCatalogPath);
        }
        public void ResetDefault ()
        {
            CurrentUpgrade.Value = 0;
            CurrentUpgrade.Save ();
        }
#endif
    }
}
