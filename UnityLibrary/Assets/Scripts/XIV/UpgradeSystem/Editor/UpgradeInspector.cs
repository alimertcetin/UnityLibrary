using System;
using UnityEditor;
using UnityEngine;
using XIV.UpgradeSystem.Implementation;

namespace XIV.UpgradeSystem.Editor
{
    [CustomEditor(typeof(Upgrade<Enum>))]
    public class UpgradeInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            var upgradeSO = target as Upgrade<Enum>;
            if (GUILayout.Button("Fix Name"))
            {
                upgradeSO.FixName();
            }
        }
    }
}