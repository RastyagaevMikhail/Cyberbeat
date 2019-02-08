using GameCore;
using UnityEngine;

namespace CyberBeat
{
    public class AuthorsData : IDataItem
    {
        private readonly Track track;

        public AuthorsData(Track track)
        {
            this.track = track;
        }

        public void InitViewGameObject(GameObject go)
        {
            AuthorsViewCell cell  = go.GetComponent<AuthorsViewCell>();
            cell.UpdateContent(track) ;
        }
    }
}