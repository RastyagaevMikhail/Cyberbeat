using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace CyberBeat
{
	public class IAPShopCard : MonoBehaviour
	{
		private Action IAPAction;

		public void Buy ()
		{
			if (IAPAction != null) IAPAction ();
		}

		public void Init (Action iapAction, bool bayed = false)
		{
			IAPAction = iapAction;
			gameObject.SetActive (!bayed);
		}
	}
}
