using Sirenix.OdinInspector;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Random = UnityEngine.Random;
namespace GameCore
{
    [Serializable]
    public class RandomStack<T>
    {

        [SerializeField] IEnumerable<T> RandCollection;
        [SerializeField] IEnumerable<T> StarCollection;
        [SerializeField] T LastRandom;

        public RandomStack () { }

        public RandomStack (IEnumerable<T> colection)
        {
            RandCollection = new List<T> (colection);
            StarCollection = colection;
        }
        public T Get ()
        {
            if (StarCollection.Count () == 1)
                return StarCollection.ElementAt (0);
            if (RandCollection.Count () == 0)
                RandCollection = new List<T> (StarCollection);
            T rand = default (T);
            do
            {
                rand = RandCollection.GetRandom();
            }
            while (rand.Equals (LastRandom));

            Remove (rand);
            LastRandom = rand;
            return rand;
        }

        private void Remove (T rand)
        {
            List<T> list = RandCollection.ToList ();
            list.Remove (rand);
            RandCollection = new List<T> (list);
            list.Clear ();
        }
    }
}
