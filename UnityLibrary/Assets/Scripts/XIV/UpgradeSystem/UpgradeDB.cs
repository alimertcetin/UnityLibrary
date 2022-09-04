using System.Collections.Generic;

namespace XIV.UpgradeSystem
{
    public class UpgradeDB : IUpgradeDB<PlayerUpgrade>
    {
        public Dictionary<string, List<IUpgrade<PlayerUpgrade>>> allUpgrades;

        IEnumerable<IUpgrade<PlayerUpgrade>> IUpgradeDB<PlayerUpgrade>.upgradeCollection => GetUpgrades();

        public bool TryGetAvailables(IUpgradeContainer<PlayerUpgrade> upgradeContainer,
            out IList<IUpgrade<PlayerUpgrade>> availables)
        {
            availables = new List<IUpgrade<PlayerUpgrade>>();
            foreach (string typeNameOfUpgrade in allUpgrades.Keys)
            {
                var upgradeType = allUpgrades[typeNameOfUpgrade][0].GetType();
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
            List<IUpgrade<PlayerUpgrade>> upgradeOfTypeList = allUpgrades[current.GetType().Name];
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
            List<IUpgrade<PlayerUpgrade>> upgradeOfTypeList = allUpgrades[current.GetType().Name];
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
            Dictionary<string, List<IUpgrade<PlayerUpgrade>>>.ValueCollection values = allUpgrades.Values;

            foreach (List<IUpgrade<PlayerUpgrade>> upgrades in values)
            {
                foreach (IUpgrade<PlayerUpgrade> upgrade in upgrades)
                {
                    yield return upgrade;
                }
            }
        }
    }
}