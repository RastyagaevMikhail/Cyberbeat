using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace CyberBeat
{
	using System;

	using UnityEngine.UI.Extensions;
	public class AuthorsScrolList : FancyScrollView<AuthorsData, AuthorsContext>
	{

		new void Awake ()
		{
			scrollPositionController.OnUpdatePosition.AddListener (UpdatePosition);

			scrollPositionController.OnItemSelected.AddListener (CellSelected);

			SetContext (new AuthorsContext ());
			base.Awake ();
		}
		private void CellSelected (int cellIndex)
		{
	// Update context.SelectedIndex and call UpdateContents for updating cell's content.
			context.SelectedIndex = cellIndex;
			UpdateContents ();
		}

		public void UpdateData (List<AuthorsData> data)
		{
			cellData = data;
			scrollPositionController.SetDataCount (cellData.Count);
			UpdateContents ();
			UpdatePosition (0);
		}
	}
}
