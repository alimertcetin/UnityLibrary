using System;
using System.Linq;

namespace XIV.UpgradeSystem
{
    public interface IUpgrade<T> : IEquatable<IUpgrade<T>>
        where T : Enum
    {
        public int upgradeLevel { get; }
        public T upgradeType { get; }

        public bool IsBetterThan(IUpgrade<T> other);
    }
}
