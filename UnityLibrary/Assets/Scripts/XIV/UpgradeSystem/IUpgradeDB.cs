using System;
using System.Collections.Generic;

namespace XIV.UpgradeSystem
{
    public interface IUpgradeDB<TUpgrade> 
        where TUpgrade : Enum
    {
        IEnumerable<IUpgrade<TUpgrade>> upgradeCollection { get; }

        public bool TryGetAvailables(IUpgradeContainer<TUpgrade> upgradeContainer, out IList<IUpgrade<TUpgrade>> availables);
        public bool TryGetNextLevelOf(IUpgrade<TUpgrade> current, out IUpgrade<TUpgrade> nextLevel);
        public bool TryGetFirstLevelOf(IUpgrade<PlayerUpgrade> current, out IUpgrade<PlayerUpgrade> first);
    }
}