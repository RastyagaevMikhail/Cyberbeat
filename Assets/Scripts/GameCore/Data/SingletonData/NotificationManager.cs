// using OneSignalPush.MiniJSON;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GameCore
{
	public class NotificationManager : ScriptableObject, IResetable
	{
		public void ResetDefault () { }
		private string oneSignalDebugMessage;
		INotificationManager notificationManager;
		public void InitWithConsent (bool consentValue)
		{
			notificationManager = new OneSiganlNotifictionManager ();
			notificationManager.Init ();
		}
		public void Send ()
		{
			notificationManager.Send ();
		}

	}
	public class OneSiganlNotifictionManager : INotificationManager
	{
		public void Init ()
		{
			// OneSignal.StartInit ("b2f7f966-d8cc-11e4-bed1-df8f05be55ba")
			// 	.HandleNotificationOpened (HandleNotificationOpened)
			// 	.EndInit ();

			// OneSignal.inFocusDisplayType = OneSignal.OSInFocusDisplayOption.Notification;
		}
		// private static void HandleNotificationOpened (OSNotificationOpenedResult result)
		// {

		// }
		public void Send ()
		{
			// // Just an example userId, use your own or get it the devices by using the GetPermissionSubscriptionState method
			// string userId = "b2f7f966-d8cc-11e4-bed1-df8f05be55ba";

			// var notification = new Dictionary<string, object> ();
			// notification["contents"] = new Dictionary<string, string> () { { "en", "Test Message" } };

			// notification["include_player_ids"] = new List<string> () { userId };
			// // Example of scheduling a notification in the future.
			// notification["send_after"] = System.DateTime.Now.ToUniversalTime ().AddSeconds (30).ToString ("U");

			// OneSignal.PostNotification (notification, (responseSuccess) =>
			// {
			// 	oneSignalDebugMessage = "Notification posted successful! Delayed by about 30 secounds to give you time to press the home button to see a notification vs an in-app alert.\n" + Json.Serialize (responseSuccess);
			// }, (responseFailure) =>
			// {
			// 	oneSignalDebugMessage = "Notification failed to post:\n" + Json.Serialize (responseFailure);
			// });

		}
	}

	public interface INotificationManager
	{
		void Init ();
		void Send ();
	}
}
