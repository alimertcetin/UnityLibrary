﻿using System;
using System.Collections.Generic;

namespace XIV.UpgradeSystem
{
    public interface IUpgradeContainer<TUpgrade> 
        where TUpgrade : Enum
    {
        public IEnumerable<IUpgrade<TUpgrade>> upgrades { get; }

        public bool Add(IUpgrade<TUpgrade> item);
        public bool Remove(IUpgrade<TUpgrade> item);
        public bool Contains(IUpgrade<TUpgrade> other);
        public bool ContainsType(Type type, out IUpgrade<TUpgrade> current);
    }
}