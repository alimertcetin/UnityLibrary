using System;
using UnityEditor;
using UnityEngine;
using XIV.UpgradeSystem.Integration;

namespace XIV.UpgradeSystem.Editor
{
    [CustomEditor(typeof(UpgradeSO<>), true)]
    public class UpgradeSOEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            //TODO : Draw Key-Value pair correctly
            //TODO : Ability to new Keys
            DrawDefaultInspector();
            if (GUILayout.Button("Fix Name"))
            {
                var upgradeSO = (dynamic)target;
                upgradeSO.FixName();
            }
        }
    }
}