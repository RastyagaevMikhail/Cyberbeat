using GameCore;

using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
namespace CyberBeat
{
    public class BoostersData : SingletonData<BoostersData>
    {
#if UNITY_EDITOR
        [UnityEditor.MenuItem ("Game/Data/BoostersData")] public static void Select () { UnityEditor.Selection.activeObject = instance; }

        public override void ResetDefault () { Upgrades.ForEach (u => u.ResetDefault ()); }
        public override void InitOnCreate ()
        {
            boosters = Tools.GetAtPath<BoosterData> ("Assets/Data/Boosters").ToList ();
        }

        private const string DefaultUPgradeDataPath = "Assets/Data/UpgradeData/{0}/{1}.asset";
        [ContextMenu ("Generate Upgrade Data")]
        public void GenerateUpgradeData ()
        {
            foreach (var booster in boosters)
            {
                string booster_name = booster.name;

                var upgradeData = ScriptableObject.CreateInstance<UpgradeData> ();
                upgradeData.locTitleTag = booster_name;
                upgradeData.maxUpgradeLevel = 9;

                upgradeData.CurrentUpgrade = CreateVariable<IntVariable> (booster_name, "CurrentUpgrade");
                upgradeData.countVideoAds = CreateVariable<IntVariable> (booster_name, "CountVideoAds");
                upgradeData.upgradeNotePrices = CreateVariable<IntListVariable> (booster_name, "UpgradeNotePrices");
                upgradeData.upgradeVideoAdsPrices = CreateVariable<IntListVariable> (booster_name, "UpgradeVideoAdsPrices");
                upgradeData.IAPTag = "{0}_upgrade".AsFormat (booster_name.ToLower ());

                upgradeData.CurrentUpgrade.isSavable = true;
                upgradeData.countVideoAds.isSavable = true;

                upgradeData.CreateAsset (DefaultUPgradeDataPath.AsFormat (booster_name, "{0}UpgradeData".AsFormat (booster_name)));
            }
        }

        private static T CreateVariable<T> (string booster_name, string nameVariable) where T : ASavableVariable
        {
            T Variable = ScriptableObject.CreateInstance<T> ();
            Variable.CreateAsset (DefaultUPgradeDataPath.AsFormat (booster_name, "{0}{1}".AsFormat (booster_name, nameVariable)));
            return Variable;
        }

        [ContextMenu ("Validate Upgrades")]
        public void ValidateUpgrades ()
        {
            Upgrades = Tools.GetAtPath<UpgradeData> ("Assets/Data/UpgradeData").ToList ();
        }

        [ContextMenu ("Copy To Variable")]
        public void CopyToVariable ()
        {
            foreach (var item in Upgrades)
            {
                item.CopyToVariable ();
            }
        }
#endif
        [ContextMenu ("DeactivateAllBoosters")]
        public void DeactivateAllBoosters ()
        {
            ActiveBoosters.ForEach (b => b.DeActivate ());
        }
        [SerializeField] List<UpgradeData> Upgrades;
        public List<BoosterData> boosters;
        public List<BoosterData> ActiveBoosters = new List<BoosterData> ();
        public bool HasActiveBoosters { get { return ActiveBoosters.Count > 0; } }
        bool startActivated;
        Action OnActivationComplete;
        public void ActivateBoosters (ColorBrick brick)
        {
            startActivated = true;
            foreach (var boosterData in ActiveBoosters)
            {
                boosterData.Apply (brick);
            }
            startActivated = false;
            if (OnActivationComplete != null) OnActivationComplete ();
        }
        List<BoosterData> BoostersFromRemove = new List<BoosterData> ();
        public void DeActivate (BoosterData boosterData)
        {
            if (startActivated)
            {
                if (BoostersFromRemove == null) BoostersFromRemove = new List<BoosterData> ();
                BoostersFromRemove.Add (boosterData);
                OnActivationComplete += onActivationComplete;
            }
            else
                RemoveActiveBooster (boosterData);

        }

        private void RemoveActiveBooster (BoosterData boosterData)
        {
            ActiveBoosters.Remove (boosterData);
        }
        void onActivationComplete ()
        {
            OnActivationComplete -= onActivationComplete;
            foreach (var boosterData in BoostersFromRemove)
                RemoveActiveBooster (boosterData);
            BoostersFromRemove.Clear ();

        }
    }
}
