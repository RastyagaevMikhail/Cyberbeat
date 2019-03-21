namespace GameCore
{
	using System.Collections;

	using UnityEngine.Monetization;
	using UnityEngine;

	public class UniyAdsObject : MonoBehaviour
	{
		public static void ShowAd (string placement, ShowResultUnityEventStringSelector selector)
		{
			new GameObject (placement, typeof (UniyAdsObject))
				.GetComponent<UniyAdsObject> ()
				.ShowAdWhenReady (placement, selector);
		}
		public void ShowAdWhenReady (string placement, ShowResultUnityEventStringSelector selector)
		{
			StartCoroutine (cr_ShowAdWhenReady (placement, selector));
		}
		private IEnumerator cr_ShowAdWhenReady (string placement, ShowResultUnityEventStringSelector selector)
		{
			WaitForSeconds wfs = new WaitForSeconds (0.25f);

			while (!Monetization.IsReady (placement))
				yield return wfs;

			ShowAdPlacementContent ad = null;
			ad = Monetization.GetPlacementContent (placement) as ShowAdPlacementContent;

			if (ad != null)
			{
				ad.Show (state =>
				{
					selector[state].Invoke (placement);
					Destroy (gameObject);
				});
			}
		}
	}
}
