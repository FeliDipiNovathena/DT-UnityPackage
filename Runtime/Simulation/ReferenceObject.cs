using DT.Scripts.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DT.Scripts.Simulation
{
    public class ReferenceObject : MonoBehaviour
    {
        [SerializeField] public DictionaryInspector<string, List<int>> IDs = new DictionaryInspector<string, List<int>>();

        public List<string> GetIDs()
        {
            return IDs.GetDictionary().Keys.ToList();
        }
    }
}