using System;
using System.Collections.Generic;
using XIV.UpgradeSystem.Examples;

namespace XIV.UpgradeSystem.Integration
{
    /// <summary>
    /// This implementation does not allow stacking of multiple upgrades of same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    public class UpgradeContainer<T> : IUpgradeContainer<T>
        where T : Enum
    {
        public List<UpgradeSO<T>> upgradeList = new List<UpgradeSO<T>>();
        IEnumerable<IUpgrade<T>> IUpgradeContainer<T>.upgrades => upgradeList;

        public bool TryAdd(IUpgrade<T> item)
        {
            if (TryGetIndexOfType(item.upgradeType, out int currentIndex))
            {
                if (upgradeList[currentIndex].IsBetterThan(item))
                {
                    return false;
                }

                upgradeList[currentIndex] = (UpgradeSO<T>)item;
                return true;
            }
            
            upgradeList.Add((UpgradeSO<T>)item);
            return true;
        }

        public bool TryRemove(IUpgrade<T> item)
        {
            int index = IndexOf(item);
            if (index < 0) return false;
            
            upgradeList.RemoveAt(index);
            return true;
        }

        public bool Contains(IUpgrade<T> other)
        {
            return IndexOf(other) >= 0;
        }

        public int IndexOf(IUpgrade<T> item)
        {
            for (int i = 0; i < upgradeList.Count; i++)
            {
                if (upgradeList[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool TryGetIndexOfType(T type, out int currentIndex)
        {
            currentIndex = -1;
            
            for (int i = 0; i < upgradeList.Count; i++)
            {
                if (upgradeList[i].upgradeType.Equals(type))
                {
                    currentIndex = i;
                    return true;
                }
            }

            return false;
        }

        public bool ContainsType(T type, out IUpgrade<T> current)
        {
            if (TryGetIndexOfType(type, out int currentIndex))
            {
                current = upgradeList[currentIndex];
                return true;
            }

            current = default;
            return false;
        }
    }
}