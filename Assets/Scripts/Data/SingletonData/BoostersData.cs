using GameCore;

using Sirenix.OdinInspector;
using Sirenix.Serialization;

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
            boosts  = boosters.ToDictionary( b =>b.name);
        }

        private const string DefaultUPgradeDataPath = "Assets/Data/UpgradeData/{0}/{1}.asset";
        [Button]
        public void GenerateUpgeadeData ()
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
        private static T CreateVariable<T> (string booster_name, string nameVariable) where T : ScriptableObject,
        ISavableVariable
        {
            T Variable = ScriptableObject.CreateInstance<T> ();
            Variable.CreateAsset (DefaultUPgradeDataPath.AsFormat (booster_name, "{0}{1}".AsFormat (booster_name, nameVariable)));
            return Variable;
        }

        [Button]
        public void CreateStylesByBoostes ()
        {
            foreach (var booster in boosters)
            {
                var style = ScriptableObject.CreateInstance<ColorsStyle> ();
                style.InitOnCreate ();
                style.CreateAsset ("Assets/Data/ColorsStyles/{0}Style.asset".AsFormat (booster.name));
            }
        }

        [Button] public void ValidateUpgrades ()
        {
            Upgrades = Tools.GetAtPath<UpgradeData> ("Assets/Data/UpgradeData").ToList ();
        }

        [Button] public void CopyToVariable ()
        {
            foreach (var item in Upgrades)
            {
                item.CopyToVariable ();
            }
        }

        [Button] public void GenerateShopItems ()
        {
            foreach (var booster in boosters)
            {
                var shopData = ScriptableObject.CreateInstance<ShopCardData> ();
                shopData.InitOnCreate (booster.name);
            }
        }
#endif
        [SerializeField] List<UpgradeData> Upgrades;
        public List<BoosterData> boosters;
        [SerializeField] Dictionary<string, BoosterData> boosts;

        public BoosterData this [string nameBooster]
        {
            get { return boosts[nameBooster]; }
        }

    }
}
