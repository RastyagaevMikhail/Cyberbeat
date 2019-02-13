﻿using GameCore;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI.Extensions;
namespace CyberBeat
{

    public class SkinsScrollList : FancyScrollView<SkinsScrollData, SkinsScrollContext>
    {

        [SerializeField] SkinsDataCollection skinsData;
        [SerializeField] Material material;

        [SerializeField] UnityEventSkinItem OnSkinSelected;
        [SerializeField] UnityEventInt OnItemSelected;

        int index { get { return skinsData.RoadSkinTypeIndex; } set { skinsData.RoadSkinTypeIndex = value; } }
        new void Awake ()
        {
            scrollPositionController.OnUpdatePosition.AddListener (UpdatePosition);

            SetContext (new SkinsScrollContext () { OnPressedCell = onPressedCell });

            UpdateData ();
            base.Awake ();
            UpdateData (skinsData.RoadSkins.Select (si => new SkinsScrollData (si)).ToList ());
        }

        private void UpdateData ()
        {
            UpdatePosition (index);
            ScrollTo (index);
            UpdateContents ();
        }

        public void _OnSkinTypeChnaged (Object obj)
        {
            _OnSkinTypeChnaged (obj as SkinType);
        }
        public void _OnSkinTypeChnaged (SkinType skinType)
        {
            scrollPositionController.ScrollTo (index);
            CellSelected (index);
        }
        // An event triggered when a cell is selected.
        void CellSelected (int cellIndex)
        {
            // Update context.SelectedIndex and call UpdateContents for updating cell's content.
            context.SelectedIndex = cellIndex;
            UpdateContents ();
        }
        void onPressedCell (SkinsScrollViewCell cell)
        {
            scrollPositionController.ScrollTo (cell.DataIndex, 0.4f);
            context.SelectedIndex = cell.DataIndex;
            UpdateContents ();

            // Debug.Log ("onPressedCell");
            var skin = cellData[cell.DataIndex].skin;
            skin.Apply (material);
            if (skin.Bougth) index = cell.DataIndex;
            OnSkinSelected.Invoke (skin);
            OnItemSelected.Invoke (cell.DataIndex);
        }

        public void ReSelectionTo (int index)
        {
            var skin = cellData[index].skin;
            skin.Apply (material);
            UpdatePosition (index);
        }

        public void ScrollTo (int skinIndex)
        {
            scrollPositionController.ScrollTo (skinIndex);
        }

        public void UpdateData (List<SkinsScrollData> data)
        {
            cellData = data;
            scrollPositionController.SetDataCount (cellData.Count);
            UpdateContents ();
            UpdatePosition (index);
        }
    }

    [System.Serializable] public class UnityEventSkinItem : UnityEvent<SkinItem> { }
}
