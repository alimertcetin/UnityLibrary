using System;
using UnityEditor;
using UnityEngine;

namespace XIV.UpgradeSystem.Implementation
{
    public abstract class Upgrade<T> : ScriptableObject, IUpgrade<T> where T : Enum
    {
        [SerializeField] int level;
        [SerializeField] float upgradePower;
        [SerializeField] T type;
        public int upgradeLevel => level;
        public T upgradeType => type;
        
        public abstract bool IsBetterThan(IUpgrade<T> other);
        public abstract bool Equals(IUpgrade<T> other);

#if UNITY_EDITOR
        /// <summary>
        /// Make sure overriden method is editor only
        /// </summary>
        protected abstract void GetName(out string name, out int instanceID);
        // protected override void GetName(out string name, out int instanceID)
        // {
        //     name = this.title + "_UpgradeDB";
        //     instanceID = this.GetInstanceID();
        // }

        public virtual void FixName()
        {
            GetName(out string name, out int instanceID);
            
            string assetPath = AssetDatabase.GetAssetPath(instanceID);
            var splitedPath = assetPath.Split('/');
            var nameOfAsset = splitedPath[splitedPath.Length - 1];
            if (name != nameOfAsset)
            {
                AssetDatabase.RenameAsset(assetPath, name);
                AssetDatabase.SaveAssets();
            }
        }
#endif
    }
}