using System;
using System.Collections.Generic;

namespace XIV.UpgradeSystem
{
    public class Player : IUpgradeContainer<PlayerUpgrade>
    {
        public List<IUpgrade<PlayerUpgrade>> upgradeList;
        IEnumerable<IUpgrade<PlayerUpgrade>> IUpgradeContainer<PlayerUpgrade>.upgrades => upgradeList;

        public Player()
        {
            this.upgradeList = new List<IUpgrade<PlayerUpgrade>>(8);
        }

        public bool Add(IUpgrade<PlayerUpgrade> item)
        {
            if (IndexOf(item) >= 0) return false;
            
            upgradeList.Add(item);
            return true;
        }

        public bool Remove(IUpgrade<PlayerUpgrade> item)
        {
            int index = IndexOf(item);
            if (index < 0) return false;
            
            upgradeList.RemoveAt(index);
            return true;
        }

        public bool Contains(IUpgrade<PlayerUpgrade> other)
        {
            return IndexOf(other) >= 0;
        }

        public int IndexOf(IUpgrade<PlayerUpgrade> item)
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

        public bool ContainsType(Type type, out IUpgrade<PlayerUpgrade> current)
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