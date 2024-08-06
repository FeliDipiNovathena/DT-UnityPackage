using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DT.Scripts.Simulation
{
    public class SimulationObjectsManager : MonoBehaviour
    {
        public static SimulationObjectsManager Instance;

        [SerializeField] private ReferenceObject[] _objects;
        private readonly Dictionary<string, ReferenceObject> _objectsDic = new Dictionary<string, ReferenceObject>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            foreach (ReferenceObject obj in _objects)
            {
                foreach (string id in obj.GetIDs())
                {
                    if (!string.IsNullOrEmpty(id) && !_objectsDic.ContainsKey(id))
                        _objectsDic.Add(id, obj);
                }
            }
        }

        public ReferenceObject GetObject(string id)
        {
            if (!_objectsDic.ContainsKey(id)) return null;

            return _objectsDic[id];
        }

        public List<string> GetIDs() => _objectsDic.Keys.ToList();

        public List<ReferenceObject> GetObjects() => _objectsDic.Values.ToList();

        public void GetReferenceObjects()
        {
            _objects = FindObjectsOfType<ReferenceObject>();
        }
    }
}