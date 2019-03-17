using System;

using UnityEngine;
using UnityEngine.Purchasing;
using OnPurchaseCompletedEvent = UnityEngine.Purchasing.IAPButton.OnPurchaseCompletedEvent;
using OnPurchaseFailedEvent = UnityEngine.Purchasing.IAPButton.OnPurchaseFailedEvent;

namespace GameCore
{
    [CreateAssetMenu (fileName = "IAPProductData", menuName = "GameCore/IAPProductData", order = 0)]
    public class IAPProductData : ScriptableObject
    {
#if UNITY_EDITOR
        public void InitOnCreate (string id, ProductType type)
        {
            name = id;
            productID = id;
            productType = type;
            this.CreateAsset ("Assets/Data/IAPProducts/{0}.asset".AsFormat (name));
        }
        public void ResetDefault ()
        {
            if (NonConsumableVariable)
            {
                NonConsumableVariable.ResetDefault ();
            }
        }
#endif
        public string productID;
        public ProductType productType;
        [Header ("IAP Action Variables")]
        [SerializeField] BoolVariable NonConsumableVariable;
        [Header ("IAP Actions")]
        public OnPurchaseCompletedEvent OnPurchaseCompleted;

        public OnPurchaseFailedEvent OnPurchaseFailed;

        [Header ("IAP Action Variables")]
        [SerializeField]
        TimeSpan TimeOfSubscription;
        [SerializeField]
        DateTimeVariable LasTimeSubsciption;

        public bool buyed
        {
            get
            {
                switch (productType)
                {
                    case ProductType.Consumable:
                        return false;
                    case ProductType.NonConsumable:
                        return NonConsumableVariable && NonConsumableVariable.Value;
                    case ProductType.Subscription:
                        return LasTimeSubsciption &&
                            DateTime.Now.Subtract (LasTimeSubsciption.Value) < TimeOfSubscription;
                    default:
                        return false;
                }
            }
        }

        public void InitButton (IAPButton button, Action onPurchaseCompleted = null, Action onPurchaseFailed = null)
        {
            button.productId = productID;
            button.onPurchaseComplete.RemoveAllListeners ();
            button.onPurchaseFailed.RemoveAllListeners ();
            switch (productType)
            {
                case ProductType.Consumable:
                    button.onPurchaseComplete.AddListener (OnPurchaseCompleted.Invoke);
                    button.onPurchaseFailed.AddListener (OnPurchaseFailed.Invoke);
                    break;
                case ProductType.NonConsumable:
                    button.onPurchaseComplete.AddListener (ActivateNonConsumableVariable);
                    break;
                case ProductType.Subscription:
                    button.onPurchaseComplete.AddListener (Subscribe);
                    break;
                default:
                    break;
            }

            button.onPurchaseComplete.AddListener (p =>
            {
                if (onPurchaseCompleted != null) onPurchaseCompleted ();
            });
            button.onPurchaseComplete.AddListener (p =>
            {
                if (onPurchaseFailed != null) onPurchaseFailed ();
            });
        }
        public void InitButton (IAPButton button)
        {
            InitButton (button);
        }
        private void Subscribe (Product arg0)
        {
            if (LasTimeSubsciption)
                LasTimeSubsciption.Value = DateTime.Now;
        }

        private void ActivateNonConsumableVariable (Product product)
        {
            Debug.Log ("ActivateNonConsumableVariable.{0}".AsFormat (name));
            if (NonConsumableVariable)
                NonConsumableVariable.SetValue (true);
        }
    }
}
