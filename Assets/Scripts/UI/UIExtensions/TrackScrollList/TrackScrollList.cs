using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI.Extensions;
namespace CyberBeat
{

	public class TrackScrollList : FancyScrollView<TrackScrollData, TrackScrollContext>
	{
		public TracksCollection collection { get { return TracksCollection.instance; } }
		new void Awake ()
		{
			scrollPositionController.OnUpdatePosition.AddListener (UpdatePosition);

			scrollPositionController.OnItemSelected.AddListener (CellSelected);

			SetContext (new TrackScrollContext ());
			base.Awake ();

		}
		// An event triggered when a cell is selected.
		void CellSelected (int cellIndex)
		{
			// Update context.SelectedIndex and call UpdateContents for updating cell's content.
			context.SelectedIndex = cellIndex;
			UpdateContents ();
		}
		public void UpdateData (List<TrackScrollData> data)
		{
			cellData = data;
			scrollPositionController.SetDataCount (cellData.Count);
			UpdateContents ();
			UpdatePosition (0);
		}
	}

}
