using System;
using System.Collections.Generic;

namespace XIV.UpgradeSystem.Implementation
{
    [System.Serializable]
    public class UpgradeContainer<T> : IUpgradeContainer<T>
        where T : Enum
    {
        public List<Upgrade<T>> upgradeList = new List<Upgrade<T>>();
        IEnumerable<IUpgrade<T>> IUpgradeContainer<T>.upgrades => upgradeList;

        public bool Add(IUpgrade<T> item)
        {
            if (IndexOf(item) >= 0) return false;
            
            upgradeList.Add((Upgrade<T>)item);
            return true;
        }

        public bool Remove(IUpgrade<T> item)
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

        public bool ContainsType(Type type, out IUpgrade<T> current)
        {
            current = default;
            if (type != typeof(IUpgrade<PlayerUpgrade>)) return false;
            
            for (int i = 0; i < upgradeList.Count; i++)
            {
                if (upgradeList[i].GetType() == type)
                {
                    current = upgradeList[i];
                    return true;
                }
            }

            return false;
        }
    }
}