using DT.Scripts.Simulation;
using UnityEditor;
using UnityEngine;

namespace DT.Editor.CustomInspector
{
    [CustomEditor(typeof(SimulationObjectsManager))]
    public class SimulationObjectsManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SimulationObjectsManager manager = (SimulationObjectsManager)target;
            if (GUILayout.Button("Get Simulation Objects"))
            {
                manager.GetReferenceObjects();
            }
        }
    }
}

