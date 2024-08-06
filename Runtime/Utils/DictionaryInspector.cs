using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DT.Scripts.Utils
{
    [Serializable]
    public class KeyValuePair<Tk, Tv>
    {
        public Tk Key;
        public Tv Value;
    }

    [Serializable]
    public class DictionaryInspector<Tk, Tv>
    {
        [SerializeField] private List<KeyValuePair<Tk, Tv>> _data = new List<KeyValuePair<Tk, Tv>>();

        public Dictionary<Tk, Tv> GetDictionary()
        {
            return _data.ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}