using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XIV.UpgradeSystem.DataStructures;
using XIV.UpgradeSystem.Examples;
using XIV.UpgradeSystem.Utils;

namespace XIV.UpgradeSystem.Integration
{
    [System.Serializable]
    public sealed class UpgradeDictionary : SerializedDictionaryBase<string, CustomSerializedList<UpgradeSO<PlayerUpgrade>>>
    {
            
    }

    [CreateAssetMenu(menuName = "Game/UpgradeDB", order = -99)]
    public class UpgradeDBSO : ScriptableObject, IUpgradeDB<PlayerUpgrade>
    {
        public UpgradeDictionary allUpgrades;

        IEnumerable<IUpgrade<PlayerUpgrade>> IUpgradeDB<PlayerUpgrade>.upgradeCollection => GetUpgrades();

        public bool TryGetAvailables(IUpgradeContainer<PlayerUpgrade> upgradeContainer,
            out IList<IUpgrade<PlayerUpgrade>> availables)
        {
            availables = new List<IUpgrade<PlayerUpgrade>>();
            foreach (string typeNameOfUpgrade in allUpgrades.Keys)
            {
                var upgradeType = allUpgrades[typeNameOfUpgrade][0].upgradeType;
                if (upgradeContainer.ContainsType(upgradeType, out var current))
                {
                    if (TryGetNextLevelOf(current, out var nextLevel))
                    {
                        availables.Add(nextLevel);
                    }
                }
                else
                {
                    availables.Add(allUpgrades[typeNameOfUpgrade][0]);
                }
            }

            return availables.Count > 0;
        }

        public bool TryGetNextLevelOf(IUpgrade<PlayerUpgrade> current, out IUpgrade<PlayerUpgrade> nextLevel)
        {
            nextLevel = default;
            List<UpgradeSO<PlayerUpgrade>> upgradeOfTypeList = allUpgrades[current.GetType().Name];
            var count = upgradeOfTypeList.Count;
            for (int i = 0; i < count; i++)
            {
                if (upgradeOfTypeList[i].IsBetterThan(current) == false) continue;
                
                nextLevel = upgradeOfTypeList[i];
                return true;
            }
            return false;
        }
        
        public bool TryGetFirstLevelOf(IUpgrade<PlayerUpgrade> current, out IUpgrade<PlayerUpgrade> first)
        {
            first = default;
            List<UpgradeSO<PlayerUpgrade>> upgradeOfTypeList = allUpgrades[current.GetType().Name];
            for (int i = 0; i < upgradeOfTypeList.Count; i++)
            {
                if (upgradeOfTypeList[i].upgradeLevel != 1) continue;
                
                first = upgradeOfTypeList[i];
                return true;
            }
            return false;
        }

        public IEnumerable<IUpgrade<PlayerUpgrade>> GetUpgrades()
        {
            var values = allUpgrades.Values;

            foreach (List<UpgradeSO<PlayerUpgrade>> upgrades in values)
            {
                foreach (UpgradeSO<PlayerUpgrade> upgrade in upgrades)
                {
                    yield return upgrade;
                }
            }
        }
        
#if UNITY_EDITOR
        [TextArea]
        public string UpgradeFolderPath = "Assets/Scripts/UpgradeSystem/Examples";
        
        public void LoadUpgrades()
        {
            Undo.RegisterCompleteObjectUndo(this, "Load Upgrades");
            allUpgrades.Clear();
            var upgrades = AssetUtils.LoadAssets<UpgradeSO<PlayerUpgrade>, CustomSerializedList<UpgradeSO<PlayerUpgrade>>>(UpgradeFolderPath);
            foreach (Type key in upgrades.Keys)
            {
                allUpgrades.Add(key.Name, upgrades[key]);
            }
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssetIfDirty(this);
        }
#endif
        
    }
}