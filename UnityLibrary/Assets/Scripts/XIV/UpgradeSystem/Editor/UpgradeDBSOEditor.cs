using UnityEditor;
using UnityEngine;
using XIV.UpgradeSystem.Integration;

namespace XIV.UpgradeSystem.Editor
{
    [CustomEditor(typeof(UpgradeDBSO))]
    public class UpgradeDBSOEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Load Upgrades"))
            {
                var upgradeDBSO = target as UpgradeDBSO;
                upgradeDBSO.LoadUpgrades();
            }
        }
    }
}