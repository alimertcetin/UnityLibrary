using System;
using System.Collections.Generic;

namespace XIV.UpgradeSystem
{
    public interface IUpgradeContainer<TUpgrade> 
        where TUpgrade : Enum
    {
        public IEnumerable<IUpgrade<TUpgrade>> upgrades { get; }

        public bool TryAdd(IUpgrade<TUpgrade> item);
        public bool TryRemove(IUpgrade<TUpgrade> item);
        public bool Contains(IUpgrade<TUpgrade> other);
        public bool ContainsType(TUpgrade type, out IUpgrade<TUpgrade> current);
    }
}